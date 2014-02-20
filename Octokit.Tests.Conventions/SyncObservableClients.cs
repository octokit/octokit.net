using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Conventions
{
    public class SyncObservableClients
    {
        [Theory]
        [ClassData(typeof(ClientInterfaces))]
        public void CheckObservableClients(Type clientInterface)
        {
            var observableClient = clientInterface.GetObservableClientInterface();
            var mainMethods = clientInterface.GetMethodsOrdered();
            var observableMethods = observableClient.GetMethodsOrdered();
            var mainNames = Array.ConvertAll(mainMethods, m => m.Name);
            var observableNames = Array.ConvertAll(observableMethods, m => m.Name);

            var methodsMissingOnReactiveClient = mainNames.Except(observableNames);
            if (methodsMissingOnReactiveClient.Any())
            {
                throw new InterfaceMissingMethodsException(observableClient, methodsMissingOnReactiveClient);
            }

            var additionalMethodsOnReactiveClient = observableNames.Except(mainNames);
            if (additionalMethodsOnReactiveClient.Any())
            {
                throw new InterfaceHasAdditionalMethodsException(observableClient, additionalMethodsOnReactiveClient);
            }

            int index = 0;
            foreach(var mainMethod in mainMethods)
            {
                var observableMethod = observableMethods[index];
                AssertEx.WithMessage(() => CheckMethod(mainMethod, observableMethod), "Invalid signature for " + observableMethod);
                index++;
            }
        }

        private static void CheckMethod(MethodInfo mainMethod, MethodInfo observableMethod)
        {
            Assert.Equal(mainMethod.MemberType, observableMethod.MemberType);
            Assert.Equal(mainMethod.Name, observableMethod.Name);
            CheckParameters(mainMethod, observableMethod);
            CheckReturnValue(mainMethod, observableMethod);
        }

        private static void CheckReturnValue(MethodInfo mainMethod, MethodInfo observableMethod)
        {
            var mainReturnType = mainMethod.ReturnType;
            var observableReturnType = observableMethod.ReturnType;
            var expectedType = GetObservableExpectedType(mainReturnType);

            if (expectedType != observableReturnType)
            {
                throw new ReturnValueMismatchException(observableMethod, expectedType, observableReturnType);
            }
        }

        private static Type GetObservableExpectedType(Type mainType)
        {
            var typeInfo = mainType.GetTypeInfo();
            switch(typeInfo.TypeCategory)
            {
                case TypeCategory.ClientInterface:
                    // client interface - IClient => IObservableClient
                    return mainType.GetObservableClientInterface();
                case TypeCategory.Task:
                    // void - Task => IObservable<Unit>
                    return typeof(IObservable<Unit>);
                case TypeCategory.GenericTask:
                    // single item - Task<TResult> => IObservable<TResult>
                case TypeCategory.ReadOnlyList:
                    // list - Task<IReadOnlyList<TResult>> => IObservable<TResult>
                    return typeof(IObservable<>).MakeGenericType(typeInfo.Type);
                case TypeCategory.Other:
                    return mainType;
                default:
                    throw new Exception("Unknown type category " + typeInfo.TypeCategory);
            }
        }

        private static void CheckParameters(MethodInfo mainMethod, MethodInfo observableMethod)
        {
            var mainParameters = mainMethod.GetParametersOrdered();
            var observableParameters = observableMethod.GetParametersOrdered();

            if (mainParameters.Length != observableParameters.Length)
            {
                throw new ParameterCountMismatchException(observableMethod, mainParameters, observableParameters);
            }

            int index = 0;
            foreach(var mainParameter in mainParameters)
            {
                var observableParameter = observableParameters[index];
                if (mainParameter.Name != observableParameter.Name)
                {
                    throw new ParameterMismatchException(observableMethod, index, mainParameter, observableParameter);
                }

                var mainType = mainParameter.ParameterType;
                var expectedType = GetObservableExpectedType(mainType);
                Assert.Equal(expectedType, observableParameter.ParameterType);

                if (expectedType != observableParameter.ParameterType)
                {
                    throw new ParameterMismatchException(observableMethod, index, mainParameter, observableParameter);
                }
                index++;
            }
        }
    }

    public class ClientInterfaces : IEnumerable<object[]>
    {
        private readonly IEnumerable<object[]> data = GetClientInterfaces();

        public static IEnumerable<object[]> GetClientInterfaces()
        {
            return typeof(IEventsClient).Assembly.ExportedTypes.Where(TypeExtensions.IsClientInterface).Select(type => new[] { type });
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}