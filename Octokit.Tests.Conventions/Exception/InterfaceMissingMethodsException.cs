﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.Tests.Conventions
{
    public class InterfaceMissingMethodsException : Exception
    {
        public InterfaceMissingMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient)
            : base(CreateMessage(type, methodsMissingOnReactiveClient))
        { }

        public InterfaceMissingMethodsException(Type type, IEnumerable<string> methodsMissingOnReactiveClient, Exception innerException)
            : base(CreateMessage(type, methodsMissingOnReactiveClient), innerException)
        { }

        static string CreateMessage(Type type, IEnumerable<string> methods)
        {
            var methodsFormatted = string.Join("\r\n", methods.Select(m => string.Format(" - {0}", m)));
            return "Methods not found on interface {0} which are required:\r\n{1}"
                       .FormatWithNewLine(type.Name, methodsFormatted);
        }
    }
}