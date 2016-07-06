using System;

namespace Octokit.Tests.Conventions
{
    public class MissingPublicParameterlessCtorException : Exception
    {
        public MissingPublicParameterlessCtorException(Type modelType)
            : base(string.Format("Model type '{0}' is missing a Public Parameterless Constructor required by SimpleJson Deserializer.", modelType.FullName))
        { }
    }
}