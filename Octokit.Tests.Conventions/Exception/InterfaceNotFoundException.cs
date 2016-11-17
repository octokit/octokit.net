using System;

namespace Octokit.Tests.Conventions
{
    public class InterfaceNotFoundException : Exception
    {
        public InterfaceNotFoundException() { }

        public InterfaceNotFoundException(string type)
            : base(CreateMessage(type))
        { }

        public InterfaceNotFoundException(string type, Exception innerException)
            : base(CreateMessage(type), innerException)
        { }

        static string CreateMessage(string type)
        {
            return string.Format("Could not find the interface {0}. Add this to the Octokit.Reactive project", type);
        }
    }
}
