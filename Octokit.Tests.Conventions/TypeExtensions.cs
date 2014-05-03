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

        public static TypeInfo GetTypeInfo(this Type type)
        {
            var typeInfo = new TypeInfo { Type = type, TypeCategory = TypeCategory.Other };
            if(type.IsClientInterface())
            {
                typeInfo.TypeCategory = TypeCategory.ClientInterface;
            }
            else if(type.IsTask())
            {
                if(!type.IsGenericType)
                {
                    typeInfo.TypeCategory = TypeCategory.Task;
                }
                else
                {
                    var taskResultType = type.GetGenericArgument();
                    if(taskResultType.IsList())
                    {
                        typeInfo.TypeCategory = TypeCategory.ReadOnlyList;
                        typeInfo.Type = taskResultType.GetGenericArgument();
                    }
                    else
                    {
                        typeInfo.TypeCategory = TypeCategory.GenericTask;
                        typeInfo.Type = taskResultType;
                    }
                }
            }
            return typeInfo;
        }

        public static bool IsModel(this Type type)
        {
            return !type.IsInterface && type.Assembly == typeof(AuthorizationUpdate).Assembly;
        }

        public static bool IsClientInterface(this Type type)
        {
            return type.IsInterface && type.Name.EndsWith(ClientSuffix) && type.Namespace == typeof(IEventsClient).Namespace;
        }

        public static Type GetObservableClientInterface(this Type type)
        {
            var observableClient = typeof(IObservableEventsClient);
            var observableClientName = observableClient.Namespace + "." + ObservablePrefix + type.Name.Substring(RealNameIndex);
            var observableInterface = observableClient.Assembly.GetType(observableClientName);
            if(observableInterface == null)
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
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>);
        }

        public static Type GetGenericArgument(this Type type)
        {
            return type.GetGenericArguments()[0];
        }
    }

    public enum TypeCategory { Other, Task, GenericTask, ReadOnlyList, ClientInterface }

    public struct TypeInfo
    {
        public Type Type { get; set; }
        public TypeCategory TypeCategory { get; set; }
    }
}