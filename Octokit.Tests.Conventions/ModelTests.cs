using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using System.Reflection;

namespace Octokit.Tests.Conventions
{
    public class ModelTests
    {
        [Theory]
        [MemberData("ModelTypes")]
        public void AllModelsHaveDebuggerDisplayAttribute(Type modelType)
        {
            var attribute = modelType.GetCustomAttribute<DebuggerDisplayAttribute>(inherit: false);
            if (attribute == null)
            {
                throw new MissingDebuggerDisplayAttributeException(modelType);
            }

            if (attribute.Value != "{DebuggerDisplay,nq}")
            {
                throw new InvalidDebuggerDisplayAttributeValueException(modelType, attribute.Value);
            }

            var property = modelType.GetProperty("DebuggerDisplay", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property == null)
            {
                throw new MissingDebuggerDisplayPropertyException(modelType);
            }

            if (property.PropertyType != typeof(string))
            {
                throw new InvalidDebuggerDisplayReturnType(modelType, property.PropertyType);
            }
        }

        [Theory]
        [MemberData("ResponseModelTypes")]
        public void ResponseModelsHaveGetterOnlyProperties(Type modelType)
        {
            var mutableProperties = new List<PropertyInfo>();

            foreach (var property in modelType.GetProperties())
            {
                var setter = property.GetSetMethod(nonPublic: true);

                if (setter == null || !setter.IsPublic)
                {
                    continue;
                }

                mutableProperties.Add(property);
            }

            if (mutableProperties.Any())
            {
                throw new MutableModelPropertiesException(modelType, mutableProperties);
            }
        }

        [Theory]
        [MemberData("ResponseModelTypes")]
        public void ResponseModelsHaveReadOnlyCollections(Type modelType)
        {
            var mutableCollectionProperties = new List<PropertyInfo>();

            foreach (var property in modelType.GetProperties())
            {
                var propertyType = property.PropertyType;

                if (typeof(IEnumerable).IsAssignableFrom(propertyType))
                {
                    // Let's skip arrays as well for now.
                    // There seems to be some special array handling in the Gist model.
                    if (propertyType == typeof(string) || propertyType.IsArray)
                    {
                        continue;
                    }

                    if (propertyType.IsReadOnlyCollection())
                    {
                        continue;
                    }

                    mutableCollectionProperties.Add(property);
                }
            }

            if (mutableCollectionProperties.Any())
            {
                throw new MutableModelPropertiesException(modelType, mutableCollectionProperties);
            }
        }

        //TODO: This should (probably) be moved to the PaginationTests class that is being introduced in PR #760
        [Theory]
        [MemberData("GetClientInterfaces")]
        public void CheckPaginationGetAllMethodNames(Type clientInterface)
        {
            var methodsOrdered = clientInterface.GetMethodsOrdered();

            var methodsThatCanPaginate = methodsOrdered
                .Where(x => x.ReturnType.GetTypeInfo().TypeCategory == TypeCategory.ReadOnlyList)
                .Where(x => x.Name.StartsWith("Get"));

            var invalidMethods = methodsThatCanPaginate
                .Where(x => !x.Name.StartsWith("GetAll"))
                .ToList();

            if (invalidMethods.Any())
            {
                throw new PaginationGetAllMethodNameMismatchException(clientInterface, invalidMethods);
            }
        }

        public static IEnumerable<object[]> GetClientInterfaces()
        {
            return typeof(IEventsClient)
                .Assembly
                .ExportedTypes
                .Where(TypeExtensions.IsClientInterface)
                .Where(t => t != typeof(IStatisticsClient)) // This convention doesn't apply to this one type.
                .Select(type => new[] { type });
        }

        public static IEnumerable<object[]> ModelTypes
        {
            get { return GetModelTypes(includeRequestModels: true).Select(type => new[] { type }); }
        }

        public static IEnumerable<object[]> ResponseModelTypes
        {
            get { return GetModelTypes(includeRequestModels: false).Select(type => new[] { type }); }
        }

        private static IEnumerable<Type> GetModelTypes(bool includeRequestModels)
        {
            var allModelTypes = new HashSet<Type>();

            var clientInterfaces = typeof(IEventsClient).Assembly.ExportedTypes
                .Where(type => type.IsClientInterface());

            foreach (var exportedType in clientInterfaces)
            {
                var methods = exportedType.GetMethods();

                var modelTypes = methods.SelectMany(method => UnwrapGenericArguments(method.ReturnType));

                if (includeRequestModels)
                {
                    var requestModels = methods.SelectMany(method => method.GetParameters(),
                        (method, parameter) => parameter.ParameterType);

                    modelTypes = modelTypes.Union(requestModels);
                }

                foreach (var modelType in modelTypes.Where(type => type.IsModel()))
                {
                    allModelTypes.Add(modelType);
                }
            }

            return GetAllModelTypes(allModelTypes);
        }

        static IEnumerable<Type> GetAllModelTypes(ISet<Type> allModelTypes)
        {
            foreach (var modelType in allModelTypes.ToList())
            {
                GetPropertyModelTypes(modelType, allModelTypes);
            }

            return allModelTypes;
        }

        static void GetPropertyModelTypes(Type modelType, ISet<Type> allModelTypes)
        {
            var properties = modelType.GetProperties();

            foreach (var propertyType in properties.SelectMany(x => UnwrapGenericArguments(x.PropertyType)))
            {
                if (allModelTypes.Contains(propertyType))
                {
                    continue;
                }

                if (!propertyType.IsModel())
                {
                    continue;
                }

                allModelTypes.Add(propertyType);

                GetPropertyModelTypes(propertyType, allModelTypes);
            }
        }

        private static IEnumerable<Type> UnwrapGenericArguments(Type returnType)
        {
            if (returnType.IsGenericType)
            {
                var arguments = returnType.GetGenericArguments();

                foreach (var argument in arguments)
                {
                    if (argument.IsModel())
                    {
                        yield return argument;
                    }
                    else
                    {
                        foreach (var unwrappedTypes in UnwrapGenericArguments(argument))
                        {
                            yield return unwrappedTypes;
                        }
                    }
                }
            }
            else
            {
                yield return returnType;
            }
        }
    }
}
