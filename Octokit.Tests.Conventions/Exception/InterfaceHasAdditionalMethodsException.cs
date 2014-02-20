using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Octokit.Tests.Conventions
{
    public class InterfaceHasAdditionalMethodsException : Exception
    {
        public InterfaceHasAdditionalMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient)
            : base(CreateMessage(type, methodsMissingOnReactiveClient)) { }

        public InterfaceHasAdditionalMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient, Exception innerException)
            : base(CreateMessage(type, methodsMissingOnReactiveClient), innerException) { }

        protected InterfaceHasAdditionalMethodsException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        static string CreateMessage(Type type, IEnumerable<string> methods)
        {
            var methodsFormatted = String.Join("\r\n", methods.Select(m => String.Format(" - {0}", m)));
            return String.Format("Methods found on type {0} which should be removed:\r\n{1}", type.Name, methodsFormatted);
        }
    }
}