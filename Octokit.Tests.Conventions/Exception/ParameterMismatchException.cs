using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class ParameterMismatchException : Exception
    {
        public ParameterMismatchException(MethodInfo method, int position, ParameterInfo expected, ParameterInfo actual)
            : base(CreateMessage(method, position, expected, actual)) { }

        public ParameterMismatchException(MethodInfo method, int position, ParameterInfo expected, ParameterInfo actual, Exception innerException)
            : base(CreateMessage(method, position, expected, actual), innerException) { }

        protected ParameterMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateParameterSignature(ParameterInfo parameter)
        {
            return String.Format("{0} {1}", parameter.ParameterType.Name, parameter.Name);
        }

        static string CreateMessage(MethodInfo method, int position, ParameterInfo expected, ParameterInfo actual)
        {
            var expectedMethodSignature = CreateParameterSignature(expected);
            var actualMethodSignature = CreateParameterSignature(actual);

            return String.Format("Parameter {0} for method {1}.{2} must be \"{3}\" but is \"{4}\"", position, method.DeclaringType.Name, method.Name, expectedMethodSignature, actualMethodSignature);
        }
    }
}