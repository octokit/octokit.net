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

        public List<ApiCodeFileMetadata> Build(List<PathMetadata> paths)
        {
            var results = new List<ApiCodeFileMetadata>();

            foreach (var path in paths)
            {
                var result = new ApiCodeFileMetadata();

                foreach (var func in funcs)
                {
                    result = func(path, result);
                }

                results.Add(result);
            }

            return results;
        }

        public void Register(TypeBuilderFunc func)
        {
            funcs.Add(func);
        }

        private static readonly Dictionary<string, string> translations = new Dictionary<string, string>
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

            data.Client.ClassName = baseClassName;
            data.Client.InterfaceName = $"I{baseClassName}";
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
            Func<VerbResult, List<ApiParameterMetadata>> convertToParameters = (verb) =>
            {
                var list = new List<ApiParameterMetadata>();

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

                    list.Add(new ApiParameterMetadata
                    {
                        Name = parameterName,
                        Type = parameter.Type
                    });
                }

                return list;
            };

            Func<VerbResult, IResponseType> convertToReturnType = (verb) =>
            {
                if (verb.Responses.Any(r => r.StatusCode == "204") && verb.Responses.Any(r => r.StatusCode == "204"))
                {
                    return new TaskOfType("boolean");
                }

                var singleJsonContent = verb.Responses.SingleOrDefault(r => r.StatusCode == "200" && r.ContentType == "application/json");

                if (singleJsonContent != null)
                {
                    var objectContent = singleJsonContent.Content as ObjectResponseContent;
                    if (objectContent != null)
                    {
                        return new TaskOfType("SomeObject");
                    }

                    var arrayContent = singleJsonContent.Content as ArrayResponseContent;
                    if (arrayContent != null)
                    {
                        return new TaskOfListType("SomeList");
                    }
                }

                return null;
            };

            foreach (var verb in metadata.Verbs)
            {
                data.Client.Methods.Add(new ApiMethodMetadata
                {
                    Name = convertVerbToMethodName(verb),
                    Parameters = convertToParameters(verb),
                    ReturnType = convertToReturnType(verb),
                    SourceMetadata = new SourceMetadata
                    {
                        Path = metadata.Path,
                        Verb = verb.Method.Method
                    }
                });
            }

            return data;
        };
    }

    public class ApiCodeFileMetadata
    {
        public ApiCodeFileMetadata()
        {
            Client = new ClientInformation();
        }

        public string FileName { get; set; }
        public ClientInformation Client { get; set; }
    }

    public class ClientInformation
    {
        public ClientInformation()
        {
            Methods = new List<ApiMethodMetadata>();
        }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
        public List<ApiMethodMetadata> Methods { get; set; }
    }

    public class ApiMethodMetadata
    {
        public ApiMethodMetadata()
        {
            Parameters = new List<ApiParameterMetadata>();
        }
        public string Name { get; set; }
        public List<ApiParameterMetadata> Parameters { get; set; }
        public IResponseType ReturnType { get; set; }
        public SourceMetadata SourceMetadata { get; set; }
    }

    public class ApiParameterMetadata
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public interface IResponseType
    {

    }

    public class TaskOfType : IResponseType
    {
        public TaskOfType(string type)
        {
            Type = type;
        }
        public string Type { get; private set; }
    }

    public class TaskOfListType : IResponseType
    {
        public TaskOfListType(string listType)
        {
            ListType = listType;
        }
        public string ListType { get; private set; }
    }

    public class SourceMetadata
    {
        public string Verb { get; set; }
        public string Path { get; set; }
    }
}
