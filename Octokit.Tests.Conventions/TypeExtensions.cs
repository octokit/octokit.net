using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Octokit.Reactive;

namespace Octokit.Tests.Conventions
{
    public static class TypeExtensions
    {
        const string ClientSuffix = "Client";
        const string ObservablePrefix = "IObservable";
        const int RealNameIndex = 1;

        public static ParameterInfo[] GetParametersOrdered(this MethodInfo method)
        {
            return method.GetParameters().OrderBy(p => p.Name).ToArray();
        }

        public static MethodInfo[] GetMethodsOrdered(this Type type)
        {
            // BF: for the moment, i don't care about checking
            // for parameters which are accepting CancellationTokens
            // In Rx, disposing of the subscriber should have
            // the same effect as cancelling the token, so we should
            // think about how we want to ensure these conventions are
            // validated
            return type.GetMethods().OrderBy(m => m.Name)
                .Where(m => m.GetParameters().All(p => p.ParameterType != typeof(CancellationToken)))
                .ToArray();
        }

        public static CustomTypeInfo GetCustomTypeInfo(this Type type)
        {
            var customTypeInfo = new CustomTypeInfo { Type = type, TypeCategory = TypeCategory.Other };
            if (type.IsClientInterface())
            {
                customTypeInfo.TypeCategory = TypeCategory.ClientInterface;
            }
            else if (type.IsTask())
            {
                if (!type.GetTypeInfo().IsGenericType)
                {
                    customTypeInfo.TypeCategory = TypeCategory.Task;
                }
                else
                {
                    var taskResultType = type.GetGenericArgument();
                    if (taskResultType.IsList())
                    {
                        customTypeInfo.TypeCategory = TypeCategory.ReadOnlyList;
                        customTypeInfo.Type = taskResultType.GetGenericArgument();
                    }
                    else
                    {
                        customTypeInfo.TypeCategory = TypeCategory.GenericTask;
                        customTypeInfo.Type = taskResultType;
                    }
                }
            }
            return customTypeInfo;
        }

        public static bool IsModel(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return !typeInfo.IsGenericType && !typeInfo.IsInterface && !typeInfo.IsEnum && typeInfo.Assembly == typeof(AuthorizationUpdate).GetTypeInfo().Assembly;
        }

        public static bool IsClientInterface(this Type type)
        {
            return type.GetTypeInfo().IsInterface && type.Name.EndsWith(ClientSuffix) && type.Namespace == typeof(IGitHubClient).Namespace;
        }

        public static bool IsClientClass(this Type type)
        {
            return type.GetTypeInfo().IsClass && type.Name.EndsWith(ClientSuffix) && type.Namespace == typeof(IGitHubClient).Namespace;
        }

        public static Type GetObservableClientInterface(this Type type)
        {
            var observableClient = typeof(IObservableEventsClient);
            var observableClientName = observableClient.Namespace + "." + ObservablePrefix + type.Name.Substring(RealNameIndex);
            var observableInterface = observableClient.GetTypeInfo().Assembly.GetType(observableClientName);
            if (observableInterface == null)
            {
                throw new InterfaceNotFoundException(observableClientName);
            }
            return observableInterface;
        }

        public static bool IsTask(this Type type)
        {
            return typeof(Task).IsAssignableFrom(type);
        }

        public static bool IsList(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        }

        public static Type GetGenericArgument(this Type type)
        {
            return type.GetGenericArguments()[0];
        }

        public static bool IsReadOnlyCollection(this Type type)
        {
            var typeInfo = type.GetTypeInfo();

            var isReadOnlyList = typeInfo.HasGenericTypeDefinition(typeof(IReadOnlyList<>));
            var isReadOnlyDictionary = typeInfo.HasGenericTypeDefinition(typeof(IReadOnlyDictionary<,>));

            return isReadOnlyList || isReadOnlyDictionary;
        }

        private static bool HasGenericTypeDefinition(this TypeInfo type, Type genericTypeDefinition)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition;
        }
    }

    public enum TypeCategory { Other, Task, GenericTask, ReadOnlyList, ClientInterface }

    public struct CustomTypeInfo
    {
        public Type Type { get; set; }
        public TypeCategory TypeCategory { get; set; }
    }
}
