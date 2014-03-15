using System;

namespace Octokit.Tests.Conventions
{
    internal static class StringExtensions
    {
        public static string FormatWithNewLine(this string s, params object[] args)
        {
            var template = Environment.NewLine == "\r\n"
                ? s
                : s.Replace("\r\n", Environment.NewLine);

            return String.Format(template, args);
        }
    }
}
