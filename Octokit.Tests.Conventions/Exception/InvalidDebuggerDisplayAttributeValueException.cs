using System;

namespace Octokit.Tests.Conventions
{
    public class InvalidDebuggerDisplayAttributeValueException : Exception
    {
        public InvalidDebuggerDisplayAttributeValueException(Type modelType, string value)
            : base(CreateMessage(modelType, value))
        { }

        static string CreateMessage(Type modelType, string value)
        {
            return string.Format(
                "Model type '{0}' has invalid DebuggerDisplayAttribute value '{1}'. Expected '{{DebuggerDisplay, nq}}'",
                modelType.FullName,
                value);
        }
    }
}