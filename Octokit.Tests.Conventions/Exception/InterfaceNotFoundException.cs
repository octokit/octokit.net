using System;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class InterfaceNotFoundException : Exception
    {
        public InterfaceNotFoundException() { }

        public InterfaceNotFoundException(string type)
            : base(CreateMessage(type)) { }

        public InterfaceNotFoundException(string type, Exception innerException)
            : base(CreateMessage(type), innerException) { }

        protected InterfaceNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateMessage(string type)
        {
            return String.Format("Could not find the interface {0}. Add this to the Octokit.Reactive project", type);
        }

    }
}
