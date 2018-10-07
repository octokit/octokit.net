using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Octokit.Tests.Conventions
{
    public class PaginationTests
    {
        [Theory]
        [MemberData(nameof(GetClientInterfaces))]
        public void CheckPaginationApiOptionsOverloads(Type clientInterface)
        {
            var methodsOrdered = clientInterface.GetMethodsOrdered();

            var methodsWhichCanPaginate = methodsOrdered
                .Where(x => x.ReturnType.GetCustomTypeInfo().TypeCategory == TypeCategory.ReadOnlyList)
                .Where(x => x.Name.StartsWith("Get"))
                .Where(x => !x.HasAttribute<ExcludeFromPaginationApiOptionsConventionTestAttribute>())
                .Where(x => !x.HasAttribute<ObsoleteAttribute>());

            var invalidMethods = methodsWhichCanPaginate
                .Where(method => MethodHasAppropriateOverload(method, methodsOrdered) == null)
                .ToList();

            if (invalidMethods.Any())
            {
                throw new ApiOptionsMissingException(clientInterface, invalidMethods);
            }
        }

        [Theory]
        [MemberData(nameof(GetClientInterfaces))]
        public void CheckPaginationGetAllMethodNames(Type clientInterface)
        {
            var methodsOrdered = clientInterface.GetMethodsOrdered();

            var methodsThatCanPaginate = methodsOrdered
                .Where(x => x.ReturnType.GetCustomTypeInfo().TypeCategory == TypeCategory.ReadOnlyList)
                .Where(x => x.Name.StartsWith("Get"))
                .Where(x => !x.HasAttribute<ExcludeFromPaginationNamingConventionTestAttribute>())
                .Where(x => !x.HasAttribute<ObsoleteAttribute>());

            var invalidMethods = methodsThatCanPaginate
                .Where(x => !x.Name.StartsWith("GetAll"))
                .ToList();

            if (invalidMethods.Any())
            {
                throw new PaginationGetAllMethodNameMismatchException(clientInterface, invalidMethods);
            }
        }

        static MethodInfo MethodHasAppropriateOverload(MethodInfo method, MethodInfo[] methodsOrdered)
        {
            var parameters = method.GetParameters();
            var name = method.Name;
            return methodsOrdered
                .Where(x => x.Name == name)
                .FirstOrDefault(x => MethodHasOverloadForApiOptions(x, parameters));
        }

        static bool MethodHasOverloadForApiOptions(MethodInfo methodInfo, ParameterInfo[] expected)
        {
            var actual = methodInfo.GetParameters();

            if ((actual.Length != expected.Length) && (actual.Length != expected.Length + 1))
            {
                return false;
            }

            for (var i = 0; i < expected.Length; i++)
            {
                var a = actual.ElementAt(i);
                var e = expected.ElementAt(i);

                if (a.Name != e.Name)
                {
                    return false;
                }

                if (a.ParameterType != e.ParameterType)
                {
                    return false;
                }
            }

            var lastParameter = actual.LastOrDefault();

            return lastParameter != null
                   && lastParameter.Name == "options"
                   && lastParameter.ParameterType == typeof(ApiOptions);
        }

        public static IEnumerable<object[]> GetClientInterfaces()
        {
            return typeof(IGitHubClient)
                .GetTypeInfo()
                .Assembly
                .ExportedTypes
                .Where(TypeExtensions.IsClientInterface)
                .Where(type => type != typeof(IStatisticsClient))
                .Select(type => new[] { type });
        }
    }
}
