using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class ReturnValueMismatchException : Exception
    {
        public ReturnValueMismatchException(MethodInfo method, Type expected, Type actual)
            : base(CreateMessage(method, expected, actual)) { }

        public ReturnValueMismatchException(MethodInfo method, Type expected, Type actual, Exception innerException)
            : base(CreateMessage(method, expected, actual), innerException) { }

        protected ReturnValueMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateMessage(MethodInfo method, Type expected, Type actual)
        {
            return String.Format("Return value for {0}.{1} must be {2} but is {3}", method.DeclaringType.Name,  method.Name, expected, actual);
        }
    }
}