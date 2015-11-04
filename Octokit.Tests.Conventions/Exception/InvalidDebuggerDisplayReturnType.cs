using System;

namespace Octokit.Tests.Conventions
{
    public class InvalidDebuggerDisplayReturnType : Exception
    {
        public InvalidDebuggerDisplayReturnType(Type modelType, Type propertyType)
            : base(CreateMessage(modelType, propertyType))
        { }

        static string CreateMessage(Type modelType, Type propertyType)
        {
            return string.Format(
                "Model type '{0}' has invalid DebuggerDisplay return type '{1}'. Expected 'string'.",
                modelType.FullName,
                propertyType.Name);
        }
    }
}