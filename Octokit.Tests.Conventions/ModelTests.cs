using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using System.Reflection;
using Octokit.Internal;

namespace Octokit.Tests.Conventions
{
    public class ModelTests
    {
        private static readonly Assembly Octokit = typeof(AuthorizationUpdate).GetTypeInfo().Assembly;

        [Theory]
        [MemberData(nameof(ModelTypes))]
        public void AllModelsHaveDebuggerDisplayAttribute(Type modelType)
        {
            var attribute = modelType.GetTypeInfo().GetCustomAttribute<DebuggerDisplayAttribute>(inherit: false);
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
        [MemberData(nameof(ResponseModelTypes))]
        public void AllResponseModelsHavePublicParameterlessCtors(Type modelType)
        {
            var ctor = modelType.GetConstructor(Type.EmptyTypes);

            if (ctor == null || !ctor.IsPublic)
            {
                throw new MissingPublicParameterlessCtorException(modelType);
            }
        }

        [Theory]
        [MemberData(nameof(ResponseModelTypes))]
        public void AllResponseModelsHavePublicCtorWithAllProperties(Type modelType)
        {
            var excludedProperties = modelType.GetCustomAttribute<ExcludeFromCtorWithAllPropertiesConventionTestAttribute>()?
                                         .Properties ??
                                     new string[] { };

            var constructors = modelType.GetConstructors();
            var properties = modelType.GetProperties()
                .Where(prop => prop.CanWrite &&
                               !excludedProperties.Contains(prop.Name))
                .ToList();

            var missingProperties = properties.ToList();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();

                var constructorMissingProperties = properties.Where(property =>
                        !parameters.Any(param =>
                            string.Equals(param.Name, property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    .ToList();

                if (constructorMissingProperties.Count < missingProperties.Count)
                {
                    missingProperties = constructorMissingProperties;
                }
            }

            if (missingProperties.Any())
            {
                throw new MissingPublicConstructorWithAllPropertiesException(modelType, missingProperties);
            }
        }

        [Theory]
        [MemberData(nameof(ResponseModelTypes))]
        public void ResponseModelsHaveNoPublicSettableProperties(Type modelType)
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
        [MemberData(nameof(ResponseModelTypes))]
        public void ResponseModelsHaveNoSetterlessAutoPropertiesForReflection(Type modelType)
        {
            var setterlessAutoProperties = new List<PropertyInfo>();

            foreach (var property in modelType.GetProperties())
            {
                var propertyHasNoSetter = property.GetSetMethod(true) is null;
                if (IsAutoProperty(property) && propertyHasNoSetter)
                {
                    setterlessAutoProperties.Add(property);
                }
            }

            if (setterlessAutoProperties.Any())
            {
                throw new ResponseModelSetterlessAutoPropertyException(modelType, setterlessAutoProperties);
            }
        }

        private bool IsAutoProperty(PropertyInfo prop)
        {
            return prop.DeclaringType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Any(f => f.Name.Contains("<" + prop.Name + ">"));
        }

        [Theory]
        [MemberData(nameof(ResponseModelTypes))]
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

        [Theory]
        [MemberData(nameof(ResponseModelTypes))]
        public void ResponseModelsUseStringEnumWrapper(Type modelType)
        {
            var enumProperties = modelType.GetProperties()
                .Where(x => x.PropertyType.GetTypeInfo().IsEnum);

            if (enumProperties.Any())
            {
                throw new ModelNotUsingStringEnumException(modelType, enumProperties);
            }
        }

        [Theory]
        [MemberData(nameof(ModelTypesWithUrlProperties))]
        public void ModelsHaveUrlPropertiesOfTypeString(Type modelType)
        {
            var propertiesWithInvalidType = modelType
                .GetProperties()
                .Where(IsUrlProperty)
                .Where(x => x.PropertyType != typeof(string))
                .ToList();

            if (propertiesWithInvalidType.Count > 0)
            {
                throw new InvalidUrlPropertyTypeException(modelType, propertiesWithInvalidType);
            }
        }

        [Theory]
        [MemberData(nameof(EnumTypes))]
        public void EnumMembersHaveParameterAttribute(Type enumType)
        {
            if (enumType == typeof(Language))
            {
                return; // TODO: Annotate all Language entries with a ParameterAttribute.
            }

            var membersWithoutProperty = enumType.GetRuntimeFields()
                .Where(x => x.Name != "value__")
                .Where(x => x.GetCustomAttribute(typeof(ParameterAttribute), false) == null);

            if (membersWithoutProperty.Any())
            {
                throw new EnumMissingParameterAttributeException(enumType, membersWithoutProperty);
            }
        }

        public static IEnumerable<object[]> ModelTypes
        {
            get { return GetModelTypes(includeRequestModels: true).Select(type => new[] { type }); }
        }

        public static IEnumerable<object[]> ModelTypesWithUrlProperties
        {
            get
            {
                return GetModelTypes(includeRequestModels: true)
                    .Where(type => type.GetProperties().Any(IsUrlProperty))
                    .Select(type => new[] { type });
            }
        }

        public static IEnumerable<object[]> ResponseModelTypes
        {
            get { return GetModelTypes(includeRequestModels: false).Select(type => new[] { type }); }
        }

        public static IEnumerable<object[]> EnumTypes
        {
            get
            {
                return GetModelTypes(includeRequestModels: true)
                    .SelectMany(type => type.GetProperties())
                    .SelectMany(property => UnwrapGenericArguments(property.PropertyType))
                    .Where(type => type.GetTypeInfo().Assembly.Equals(Octokit) && type.GetTypeInfo().IsEnum)
                    .Distinct()
                    .Select(type => new[] { type });
            }
        }

        private static IEnumerable<Type> GetModelTypes(bool includeRequestModels)
        {
            var allModelTypes = new HashSet<Type>();

            var clientInterfaces = typeof(IGitHubClient).GetTypeInfo().Assembly.ExportedTypes
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
            if (returnType.GetTypeInfo().IsGenericType)
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

        private static bool IsUrlProperty(PropertyInfo property)
        {
            return property.Name.EndsWith("Url");
        }
    }
}
