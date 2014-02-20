using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class ParameterCountMismatchException : Exception
    {
        public ParameterCountMismatchException(MethodInfo method, IEnumerable<ParameterInfo> expected, IEnumerable<ParameterInfo> actual)
            : base(CreateMessage(method, expected, actual)) { }

        public ParameterCountMismatchException(MethodInfo method, IEnumerable<ParameterInfo> expected, IEnumerable<ParameterInfo> actual, Exception innerException)
            : base(CreateMessage(method, expected, actual), innerException) { }

        protected ParameterCountMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateMethodSignature(IEnumerable<ParameterInfo> parameters)
        {
            return String.Join(",", parameters.Select(p => String.Format("{0} {1}", p.ParameterType.Name, p.Name)));
        }

        static string CreateMessage(MethodInfo method, IEnumerable<ParameterInfo> expected, IEnumerable<ParameterInfo> actual)
        {
            var expectedMethodSignature = CreateMethodSignature(expected);
            var actualMethodSignature = CreateMethodSignature(actual);

            return String.Format("Method signature for {0}.{1} must be ({2}) but is ({3})", method.DeclaringType.Name, method.Name, expectedMethodSignature, actualMethodSignature);
        }
    }
}