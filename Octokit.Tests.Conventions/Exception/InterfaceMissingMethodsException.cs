using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class InterfaceMissingMethodsException : Exception
    {
        public InterfaceMissingMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient)
            : base(CreateMessage(type, methodsMissingOnReactiveClient)) { }

        public InterfaceMissingMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient, Exception innerException)
            : base(CreateMessage(type, methodsMissingOnReactiveClient), innerException) { }

        protected InterfaceMissingMethodsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateMessage(Type type, IEnumerable<string> methods)
        {
            var methodsFormatted = String.Join("\r\n", methods.Select(m => String.Format(" - {0}", m)));
            return String.Format("Methods not found on interface {0} which are required:\r\n{1}", type.Name, methodsFormatted);
        }
    }
}