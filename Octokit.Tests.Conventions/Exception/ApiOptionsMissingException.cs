using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ApiOptionsMissingException : Exception
    {
        public ApiOptionsMissingException(Type type, IEnumerable<MethodInfo> methods)
            : base(CreateMessage(type, methods)) { }

        static string CreateMessage(Type type, IEnumerable<MethodInfo> methods)
        {
            var methodsFormatted = String.Join("\r\n", methods.Select(m => String.Format(" - {0}", m)));
            return "Methods found on type {0} require an overload which accepts an parameter of type ApiOptions:\r\n{1}"
                      .FormatWithNewLine(
                          type.Name,
                          methodsFormatted);
        }
    }
}
