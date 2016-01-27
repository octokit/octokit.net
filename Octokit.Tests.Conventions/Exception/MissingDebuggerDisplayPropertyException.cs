using System;

namespace Octokit.Tests.Conventions
{
    public class MissingDebuggerDisplayPropertyException : Exception
    {
        public MissingDebuggerDisplayPropertyException(Type modelType)
            : base(string.Format("Model type '{0}' is missing the DebuggerDisplay property.", modelType.FullName))
        { }
    }
}