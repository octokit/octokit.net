using System;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ReturnValueMismatchException : Exception
    {
        public ReturnValueMismatchException(MethodInfo method, Type expected, Type actual)
            : base(CreateMessage(method, expected, actual))
        { }

        public ReturnValueMismatchException(MethodInfo method, Type expected, Type actual, Exception innerException)
            : base(CreateMessage(method, expected, actual), innerException)
        { }

        static string CreateMessage(MethodInfo method, Type expected, Type actual)
        {
            return string.Format("Return value for {0}.{1} must be \"{2}\" but is \"{3}\"", method.DeclaringType.Name, method.Name, expected, actual);
        }
    }
}