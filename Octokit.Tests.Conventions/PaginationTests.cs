using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Sdk;

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

            foreach (var method in methodsWhichCanPaginate)
            {
                var parameters = method.GetParametersOrdered();
                var name = method.Name;
                var parameterWithOverload = methodsOrdered
                    .Where(x => x.Name == name)
                    .FirstOrDefault(x => MethodHasSameParametersWithApiOptionOverload(x, parameters));

                if (parameterWithOverload == null)
                {

                }
                else
                {

                }

            }
        }

        static bool MethodHasSameParametersWithApiOptionOverload(MethodInfo methodInfo, ParameterInfo[] expected)
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
