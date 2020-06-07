using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Octokit.Tests.Conventions
{
    public class ClientRouteTests
    {
        [Theory]
        [MemberData(nameof(GetClientClasses))]
        public void ClassMethodsAreMarkedAsGeneratedManualOrDotNetSpecific(Type clientClass)
        {
            var methodFailures = new List<MethodInfo>();

            foreach (var method in clientClass.GetMethodsOrdered().Where(IsNotBoilerplateMethod))
            {
                var success = MethodIsMarkedGeneratedManualOrDotNetSpecific(method);
                if (!success)
                {
                    methodFailures.Add(method);
                }
            }

            if (methodFailures.Count > 0)
            {
                var methodNames = string.Join(", ", methodFailures.Select(m => m.Name));
                throw new Xunit.Sdk.XunitException($"These methods on {clientClass.Name} are not marked with one of ManualRouteAttribute, GeneratedRouteAttribute or DotNetSpecificRouteAttribute: '{methodNames}'");
            }
        }

        static bool MethodIsMarkedGeneratedManualOrDotNetSpecific(MethodInfo method)
        {
            var manualRoute = method.GetCustomAttribute<ManualRouteAttribute>();
            if (manualRoute != null)
            {
                return true;
            }

            var generatedRoute = method.GetCustomAttribute<GeneratedRouteAttribute>();
            if (generatedRoute != null)
            {
                return true;
            }

            var dotnetSpecificRoute = method.GetCustomAttribute<DotNetSpecificRouteAttribute>();
            if (dotnetSpecificRoute != null)
            {
                return true;
            }

            var obsolete = method.GetCustomAttribute<ObsoleteAttribute>();
            if (obsolete != null)
            {
                return true;
            }

            return false;
        }

        static bool IsNotBoilerplateMethod(MethodInfo method)
        {
            if (method.IsSpecialName)
            {
                return false;
            }

            if (method.Name == "GetType" || method.Name == "ToString" || method.Name == "GetHashCode" || method.Name == "Equals")
            {
                return false;
            }

            return true;
        }

        public static IEnumerable<object[]> GetClientClasses()
        {
            return typeof(IGitHubClient)
                .GetTypeInfo()
                .Assembly
                .ExportedTypes
                .Where(TypeExtensions.IsClientClass)
                .Where(t => t != typeof(GitHubClient))
                .Select(type => new[] { type });
        }
    }
}
