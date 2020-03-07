using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Octokit.CodeGen
{
    using TypeBuilderFunc = System.Func<PathMetadata, ApiCodeFileMetadata, ApiCodeFileMetadata>;

    public partial class Builders
    {
        static readonly Dictionary<string, string> translations = new Dictionary<string, string>
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

    }
}
