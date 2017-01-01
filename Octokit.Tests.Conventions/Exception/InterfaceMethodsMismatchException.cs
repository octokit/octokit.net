﻿using System;
using System.Linq;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class InterfaceMethodsMismatchException : Exception
    {
        public InterfaceMethodsMismatchException(Type observableType, Type clientInterface)
            : base(CreateMessage(observableType, clientInterface))
        { }

        public InterfaceMethodsMismatchException(Type type, Type clientInterface, Exception innerException)
            : base(CreateMessage(type, clientInterface), innerException)
        { }

        static string Format(ParameterInfo parameterInfo)
        {
            return string.Format("{0} {1}", parameterInfo.ParameterType.Name, parameterInfo.Name);
        }

        static string Format(MethodInfo methodInfo)
        {
            var parameters = methodInfo.GetParameters().Select(Format);

            return string.Format("{0} {1}({2})", methodInfo.ReturnType, methodInfo.Name, string.Join(", ", parameters));
        }

        static string CreateMessage(Type observableInterface, Type clientInterface)
        {
            var mainMethods = clientInterface.GetMethodsOrdered();
            var observableMethods = observableInterface.GetMethodsOrdered();

            var formattedMainMethods = string.Join("\r\n", mainMethods.Select(Format).Select(m => string.Format(" - {0}", m)));
            var formattedObservableMethods = string.Join("\r\n", observableMethods.Select(Format).Select(m => string.Format(" - {0}", m)));

            return
                "There are some overloads which are confusing the convention tests. Check everything is okay in these types:\r\n{0}\r\n{1}\r\n{2}\r\n{3}"
                    .FormatWithNewLine(
                        clientInterface.Name,
                        formattedMainMethods,
                        observableInterface.Name,
                        formattedObservableMethods);
        }
    }
}