using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Reactive
{
    [Trait("Category", "CheckClients")]
    public class SyncObservableClients
    {
        public static IEnumerable<object[]> ClientInterfaces
        {
            get
            {
                return typeof(IEventsClient).Assembly.ExportedTypes.Where(TypeExtensions.IsClientInterface).Select(type => new[]{type});
            }
        }

        [Fact]
        private void CheckClient()
        {
            CheckClientInterfaces(typeof(IAssigneesClient));
        }

        [Theory]
        [PropertyData("ClientInterfaces")]
        private void CheckClientInterfaces(Type clientInterface)
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
            if(mainReturnType.IsTask())
            {
                CheckTaskReturnType(mainReturnType, observableReturnType);
                return;
            }
            CheckClientInterface(mainReturnType, observableReturnType);
        }

        private static void CheckClientInterface(Type mainType, Type observableType)
        {
            // client interface - IClient => IObservableClient
            var expectedType = mainType.IsClientInterface() ? mainType.GetObservableClientInterface() : mainType;
            Assert.Equal(expectedType, observableType);
        }

        private static void CheckTaskReturnType(Type mainReturnType, Type observableReturnType)
        {
            // void - Task => IObservable<Unit>
            if(!mainReturnType.IsGenericType)
            {
                Assert.Equal(typeof(IObservable<Unit>), observableReturnType);
                return;
            }
            var taskResultType = mainReturnType.GetGenericArgument();
            // single item - Task<TResult> => IObservable<TResult>
            // list - Task<IReadOnlyList<TResult>> => IObservable<TResult>
            var expectedInnerType = taskResultType.IsList() ? taskResultType.GetGenericArgument() : taskResultType;
            var expectedType = typeof(IObservable<>).MakeGenericType(expectedInnerType);
            Assert.Equal(expectedType, observableReturnType);
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
                CheckClientInterface(mainParameter.ParameterType, observableParameter.ParameterType);
                index++;
            }
        }
    }

    public static class TypeExtensions
    {
        const string ClientSufix = "Client";
        const string ObservablePrefix = "IObservable";
        const int RealNameIndex = 1;

        public static ParameterInfo[] GetParametersOrdered(this MethodInfo method)
        {
            return method.GetParameters().OrderBy(p=>p.Name).ToArray();
        }

        public static MethodInfo[] GetMethodsOrdered(this Type type)
        {
            return type.GetMethods().OrderBy(m=>m.Name).ToArray();
        }

        public static bool IsClientInterface(this Type type)
        {
            return type.IsInterface && type.Name.EndsWith(ClientSufix) && type.Namespace == typeof(IEventsClient).Namespace;
        }
  
        public static Type GetObservableClientInterface(this Type type)
        {
            var observableClient = typeof(IObservableEventsClient);
            var observableClientName = observableClient.Namespace + "." + ObservablePrefix + type.Name.Substring(RealNameIndex);
            return observableClient.Assembly.GetType(observableClientName, throwOnError: true);
        }

        public static bool IsTask(this Type type)
        {
            return typeof(Task).IsAssignableFrom(type);
        }

        public static bool IsList(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        }

        public static Type GetGenericArgument(this Type type)
        {
            return type.GetGenericArguments()[0];
        }
    }
}