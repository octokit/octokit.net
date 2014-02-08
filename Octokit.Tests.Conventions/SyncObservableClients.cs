using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Conventions
{
    public class SyncObservableClients
    {
        [Fact]
        private void CheckObservableClientExample()
        {
            CheckObservableClients(typeof(IAssigneesClient));
        }

        [Theory]
        [ClassData(typeof(ClientInterfaces))]
        private void CheckObservableClients(Type clientInterface)
        {
            var observableClient = clientInterface.GetObservableClientInterface();
            var mainMethods = clientInterface.GetMethodsOrdered();
            var observableMethods = observableClient.GetMethodsOrdered();
            Assert.Equal(mainMethods.Length, observableMethods.Length);
            int index = 0;
            foreach(var mainMethod in mainMethods)
            {
                var observableMethod = observableMethods[index];
                CheckMethod(mainMethod, observableMethod);
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
            Assert.Equal(expectedType, observableReturnType);
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
            Assert.Equal(mainParameters.Length, observableParameters.Length);
            int index = 0;
            foreach(var mainParameter in mainParameters)
            {
                var observableParameter = observableParameters[index];
                Assert.Equal(mainParameter.Name, observableParameter.Name);
                var mainType = mainParameter.ParameterType; 
                var typeInfo = mainType.GetTypeInfo();
                var expectedType = GetObservableExpectedType(mainType);
                Assert.Equal(expectedType, observableParameter.ParameterType);
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