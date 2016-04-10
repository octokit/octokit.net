using System;

namespace Octokit.Tests.Conventions
{
    public class MissingClientConstructorTestClassException : Exception
    {
        public MissingClientConstructorTestClassException(Type modelType)
            : base(CreateMessage(modelType))
        { }

        static string CreateMessage(Type ctorTest)
        {
            return string.Format("Constructor test class is missing {0}.", ctorTest.FullName);
        }
    }
}