using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Octokit
{
    internal static class StringExtensions
    {
        public static bool IsBlank(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNotBlank(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public static Uri FormatUri(this string pattern, params object[] args)
        {
            Ensure.ArgumentNotNullOrEmptyString(pattern, "pattern");

            return new Uri(string.Format(CultureInfo.InvariantCulture, pattern, args), UriKind.Relative);
        }

        public static string UriEncode(this string input)
        {
            return System.Net.WebUtility.UrlEncode(input);
        }

        static readonly Regex _optionalQueryStringRegex = new Regex("\\{\\?([^}]+)\\}");
        public static Uri ExpandUriTemplate(this string template, object values)
        {
            var optionalQueryStringMatch = _optionalQueryStringRegex.Match(template);
            if(optionalQueryStringMatch.Success)
            {
                var expansion = "";
                var parameterName = optionalQueryStringMatch.Groups[1].Value;
                var parameterProperty = values.GetType().GetProperty(parameterName);
                if(parameterProperty != null)
                {
                    expansion = "?" + parameterName + "=" + Uri.EscapeDataString("" + parameterProperty.GetValue(values, new object[0]));
                }
                template = _optionalQueryStringRegex.Replace(template, expansion);
            }
            return new Uri(template);
        }

#if NETFX_CORE
        public static PropertyInfo GetProperty(this Type t, string propertyName)
        {
            return t.GetTypeInfo().GetDeclaredProperty(propertyName);
        }
#endif

        // :trollface:
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "Ruby don't care. Ruby don't play that.")]
        public static string ToRubyCase(this string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(propertyName, "s");
            return string.Join("_", propertyName.SplitUpperCase()).ToLowerInvariant();
        }

        static IEnumerable<string> SplitUpperCase(this string source)
        {
            Ensure.ArgumentNotNullOrEmptyString(source, "source");

            int wordStartIndex = 0;
            var letters = source.ToCharArray();
            var previousChar = char.MinValue;

            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current character.
                    yield return new String(letters, wordStartIndex, i - wordStartIndex);
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }

            //We need to have the last word.
            yield return new String(letters, wordStartIndex, letters.Length - wordStartIndex);
        }
    }
}
