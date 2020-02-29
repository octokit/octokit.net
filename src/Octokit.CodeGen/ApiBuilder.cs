using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiCodeFileMetadata, ApiCodeFileMetadata>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public ApiCodeFileMetadata Build(PathMetadata path)
        {
            var value = new ApiCodeFileMetadata();

            foreach (var func in funcs)
            {
                value = func(path, value);
            }

            return value;
        }

        public void Register(TypeBuilderFunc func)
        {
            funcs.Add(func);
        }

        private static readonly Dictionary<string,string> translations = new Dictionary<string, string>
        {
          { "repos", "repositories" },
        };

        public static readonly TypeBuilderFunc AddTypeNamesAndFileName = (metadata, data) =>
        {
            var className = "";

            var tokens = metadata.Path.Split("/");

            Func<string, bool> isPlaceHolder = (str) =>
            {
                return str.StartsWith("{") && str.EndsWith("}"); ;
            };

            foreach (var token in tokens)
            {
                if (token.Length == 0)
                {
                    continue;
                }

                if (isPlaceHolder(token))
                {
                    continue;
                }

                var segments = token.Replace("_", " ").Replace("-", " ").Split(" ");
                var pascalCaseSegments = segments.Select(s =>
                {
                    if (translations.ContainsKey(s))
                    {
                      s = translations[s];
                    }

                    if (s.Length <= 2)
                    {
                        return s;
                    }
                    else
                    {
                        return Char.ToUpper(s[0]) + s.Substring(1);
                    }
                });
                className += string.Join("", pascalCaseSegments);
            }

            var baseClassName = $"{className}Client";

            data.ClassName = baseClassName;
            data.InterfaceName = $"I{baseClassName}";
            data.FileName = Path.Join("Octokit", "Clients", $"{baseClassName}.cs");
            return data;
        };

        static readonly Func<VerbResult, string> convertVerbToMethodName = (verb) =>
        {
            if (verb.Method == HttpMethod.Get)
            {
                // what about Get with 200 response being a list?
                // this should be GetAll instead to align with our conventions?
                return "Get";
            }

            if (verb.Method == HttpMethod.Delete)
            {
                return "Delete";
            }

            if (verb.Method == HttpMethod.Put)
            {
                return "GetOrCreate";
            }

            return "???";
        };


        public static readonly TypeBuilderFunc AddMethodForEachVerb = (metadata, data) =>
        {
            Func<VerbResult, List<ApiParameterResult>> convertToParameters = (verb) =>
            {
                var list = new List<ApiParameterResult>();

                foreach (var parameter in verb.Parameters.Where(p => p.In == "path" && p.Required))
                {
                    var segments = parameter.Name.Replace("_", " ").Replace("-", " ").Split(" ");
                    var pascalCaseSegments = segments.Select(s =>
                    {
                        if (s.Length < 2)
                        {
                            return s;
                        }
                        else
                        {
                            return Char.ToUpper(s[0]) + s.Substring(1);
                        }
                    });
                    var parameterName = string.Join("", pascalCaseSegments);
                    parameterName = Char.ToLower(parameterName[0]) + parameterName.Substring(1);

                    list.Add(new ApiParameterResult
                    {
                        Name = parameterName,
                        Type = parameter.Type
                    });
                }

                return list;
            };

            foreach (var verb in metadata.Verbs)
            {
                data.Methods.Add(new ApiMethodResult
                {
                    Name = convertVerbToMethodName(verb),
                    Parameters = convertToParameters(verb)
                });
            }

            return data;
        };
    }

    public class ApiCodeFileMetadata
    {
        public ApiCodeFileMetadata()
        {
            Methods = new List<ApiMethodResult>();
        }

        public string FileName { get; set; }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }

        public List<ApiMethodResult> Methods { get; set; }
    }

    public class ApiMethodResult
    {
        public ApiMethodResult()
        {
            Parameters = new List<ApiParameterResult>();
        }
        public string Name { get; set; }
        public List<ApiParameterResult> Parameters { get; set; }
    }

    public class ApiParameterResult
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
