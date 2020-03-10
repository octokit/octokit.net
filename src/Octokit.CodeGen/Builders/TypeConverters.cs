using System;
using System.Linq;

namespace Octokit.CodeGen
{

    public partial class Builders
    {
        private static bool IsPlaceHolder(string str)
        {
            return str.StartsWith("{") && str.EndsWith("}");
        }

        private static string Singularize(string str)
        {
            if (str.Length < 3)
            {
                return str;
            }

            if (str.EndsWith("ies"))
            {
                return str;
            }

            if (str[^1] == 's' && str[^2] != 's')
            {
                return str.Substring(0, str.Length - 1);
            }

            return str;
        }

        private static string ConvertToPascalCase(string s, bool ensureSingular = false)
        {
            if (translations.ContainsKey(s))
            {
                s = translations[s];
            }

            if (s.Length < 2)
            {
                return ensureSingular ? Singularize(s) : s;
            }
            else
            {
                var newString = Char.ToUpper(s[0]) + s.Substring(1);
                return ensureSingular ? Singularize(newString) : newString;
            }
        }

        private static string GetPropertyName(string name, bool ensureSingular = false)
        {
            var segments = name.Replace("_", " ").Replace("-", " ").Split(" ");
            var pascalCaseSegments = segments.Select(s => ConvertToPascalCase(s, ensureSingular));
            return string.Join("", pascalCaseSegments);
        }


        private static string GetClassName(PathMetadata metadata)
        {
            var className = "";
            var tokens = metadata.Path.Split("/");
            foreach (var token in tokens)
            {
                if (token.Length == 0)
                {
                    continue;
                }

                if (IsPlaceHolder(token))
                {
                    continue;
                }

                var segments = token.Replace("_", " ").Replace("-", " ").Split(" ");
                var pascalCaseSegments = segments.Select(s => ConvertToPascalCase(s, true));
                className += string.Join("", pascalCaseSegments);
            }

            return className;
        }

    }

}
