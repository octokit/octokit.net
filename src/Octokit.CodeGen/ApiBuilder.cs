using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiBuilderResult, ApiBuilderResult>;

    public class ApiBuilder
    {
        private List<TypeBuilderFunc> funcs = new List<TypeBuilderFunc>();

        public ApiBuilderResult Build(PathMetadata path)
        {
            var value = new ApiBuilderResult();

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

        public static readonly TypeBuilderFunc AddTypeNames = (metadata, data) =>
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
                var pascalCaseSegments = segments.Select(s => {
                    if (s.Length == 0) {
                        return s;
                    } else {
                        return Char.ToUpper(s[0]) + s.Substring(1);
                    }
                });
                className += string.Join("", pascalCaseSegments);
            }

            data.ClassName = className;
            data.InterfaceName = $"I{className}";
            return data;
        };
    }

    public interface IApiBuilderFunc
    {
        ApiBuilderResult Apply(PathMetadata pathResult, ApiBuilderResult result);
    }

    public class ApiBuilderResult
    {
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
    }
}
