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
        [MemberData("GetClientInterfaces")]
        public void CheckObservableClients(Type clientInterface)
        {
            var methodsOrdered = clientInterface.GetMethodsOrdered();

            var methodsWhichCanPaginate = methodsOrdered
                .Where(x => x.Name.StartsWith("GetAll"));

            var invalidMethods = methodsWhichCanPaginate
                .Where(method => MethodHasAppropriateOverload(method, methodsOrdered) == null);

            if (invalidMethods.Any())
            {
                throw new ApiOptionsMissingException(clientInterface, invalidMethods);
            }
        }

        static MethodInfo MethodHasAppropriateOverload(MethodInfo method, MethodInfo[] methodsOrdered)
        {
            var parameters = method.GetParametersOrdered();
            var name = method.Name;
            return methodsOrdered
                .Where(x => x.Name == name)
                .FirstOrDefault(x => MethodHasOverloadForApiOptions(x, parameters));
        }

        static bool MethodHasOverloadForApiOptions(MethodInfo methodInfo, ParameterInfo[] expected)
        {
            var actual = methodInfo.GetParameters();

            if (actual.Count() != expected.Count() + 1)
            {
                return false;
            }

            for(var i = 0; i < expected.Count(); i++)
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

            var lastParameter = actual.Last();

            return lastParameter.Name == "options"
                   && lastParameter.ParameterType == typeof(ApiOptions);
        }

        public static IEnumerable<object[]> GetClientInterfaces()
        {
            return typeof(IEventsClient).Assembly.ExportedTypes.Where(TypeExtensions.IsClientInterface).Select(type => new[] { type });
        }
    }
}
