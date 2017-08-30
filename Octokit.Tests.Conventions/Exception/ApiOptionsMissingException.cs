using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ApiOptionsMissingException : Exception
    {
        public ApiOptionsMissingException(Type type, IEnumerable<MethodInfo> methods)
            : base(CreateMessage(type, methods))
        { }

        static string CreateMessage(Type type, IEnumerable<MethodInfo> methods)
        {
            var methodsFormatted = String.Join("\r\n", methods.Select(FormatMethod));
            return "Methods found on type {0} require an overload which accepts a parameter of type ApiOptions:\r\n{1}"
                      .FormatWithNewLine(
                          type.Name,
                          methodsFormatted);
        }

        static string FormatMethod(MethodInfo m)
        {
            var formattedParameters = m.GetParameters()
                .Select(p => string.Format("{0} {1}", p.ParameterType.Name, p.Name));

            var parameterList = string.Join(", ", formattedParameters);

            return string.Format(" - {0}({1})", m.Name, parameterList);
        }
    }
}
