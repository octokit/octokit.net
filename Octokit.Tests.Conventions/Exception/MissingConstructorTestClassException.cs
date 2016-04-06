using System;

namespace Octokit.Tests.Conventions
{
    public class MissingConstructorTestClassException : Exception
    {
        public MissingConstructorTestClassException(Type modelType)
            : base(CreateMessage(modelType))
        { }

        static string CreateMessage(Type ctorTest)
        {
            return string.Format("Constructor test method is missing {0}.", ctorTest.FullName);
        }
    }
}