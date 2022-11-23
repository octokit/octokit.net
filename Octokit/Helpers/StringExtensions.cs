﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Text;
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
            Ensure.ArgumentNotNullOrEmptyString(pattern, nameof(pattern));
            var uriString = string.Format(CultureInfo.InvariantCulture, pattern, args).EncodeSharp();

            return new Uri(uriString, UriKind.Relative);
        }

        public static string UriEncode(this string input)
        {
            return WebUtility.UrlEncode(input);
        }

        public static string ToBase64String(this string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }

        public static string FromBase64String(this string encoded)
        {
            var decodedBytes = Convert.FromBase64String(encoded);
            return Encoding.UTF8.GetString(decodedBytes, 0, decodedBytes.Length);
        }

        static readonly Regex _optionalQueryStringRegex = new Regex("\\{\\?([^}]+)\\}");
        public static Uri ExpandUriTemplate(this string template, object values)
        {
            var optionalQueryStringMatch = _optionalQueryStringRegex.Match(template);
            if (optionalQueryStringMatch.Success)
            {
                var expansion = string.Empty;
                var parameters = optionalQueryStringMatch.Groups[1].Value.Split(',');

                foreach (var parameter in parameters)
                {
                    var parameterProperty = values.GetType().GetProperty(parameter);
                    if (parameterProperty != null)
                    {
                        expansion += string.IsNullOrWhiteSpace(expansion) ? "?" : "&";
                        expansion += parameter + "=" +
                            Uri.EscapeDataString("" + parameterProperty.GetValue(values, new object[0]));
                    }
                }
                template = _optionalQueryStringRegex.Replace(template, expansion);
            }
            return new Uri(template);
        }

        // :trollface:
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase",
            Justification = "Ruby don't care. Ruby don't play that.")]
        public static string ToRubyCase(this string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));

            // If the entire property is already all upper case, then do not split it across
            // word boundaries. For example, "UBUNTU" should not be changed to "u_b_u_n_t_u".
            if (string.Equals(propertyName, propertyName.ToUpperInvariant(), StringComparison.Ordinal))
            {
                return propertyName;
            }

            return string.Join("_", propertyName.SplitUpperCase()).ToLowerInvariant();
        }

        public static string FromRubyCase(this string propertyName)
        {
            Ensure.ArgumentNotNullOrEmptyString(propertyName, nameof(propertyName));
            return string.Join("", propertyName.Split('_')).ToCapitalizedInvariant();
        }

        public static string ToCapitalizedInvariant(this string value)
        {
            Ensure.ArgumentNotNullOrEmptyString(value, nameof(value));
            return string.Concat(value[0].ToString().ToUpperInvariant(), value.Substring(1));
        }

        internal static string EscapeDoubleQuotes(this string value)
        {
            if (value != null)
            {
                return value.Replace("\"", "\\\"");
            }

            return value;
        }

        internal static string EncodeSharp(this string value)
        {
            return !string.IsNullOrEmpty(value) ? value?.Replace("#", "%23") : string.Empty;
        }

        static IEnumerable<string> SplitUpperCase(this string source)
        {
            Ensure.ArgumentNotNullOrEmptyString(source, nameof(source));

            int wordStartIndex = 0;
            var letters = source.ToCharArray();
            var previousChar = char.MinValue;

            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current character.
                    yield return new string(letters, wordStartIndex, i - wordStartIndex);
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }

            //We need to have the last word.
            yield return new string(letters, wordStartIndex, letters.Length - wordStartIndex);
        }

        // the rule:
        // Username may only contain alphanumeric characters or single hyphens
        // and cannot begin or end with a hyphen
        static readonly Regex nameWithOwner = new Regex("[a-z0-9.-]{1,}/[a-z0-9.-_]{1,}",
#if HAS_REGEX_COMPILED_OPTIONS
            RegexOptions.Compiled |
#endif
            RegexOptions.IgnoreCase);

        internal static bool IsNameWithOwnerFormat(this string input)
        {
            return nameWithOwner.IsMatch(input);
        }
    }
}
