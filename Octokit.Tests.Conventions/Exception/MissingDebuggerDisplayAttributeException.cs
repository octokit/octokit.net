using System;

namespace Octokit.Tests.Conventions
{
    public class MissingDebuggerDisplayAttributeException : Exception
    {
        public MissingDebuggerDisplayAttributeException(Type modelType)
            : base(string.Format("Model type '{0}' is missing the DebuggerDisplayAttribute.", modelType.FullName))
        { }
    }
}