using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Octokit.Tests.Conventions
{
    public class ClientConstructorTests
    {
        [Theory]
        [MemberData(nameof(GetTestConstructorClasses))]
        public void CheckTestConstructorNames(Type type)
        {
            const string constructorTestClassName = "TheCtor";
            const string constructorTestMethodName = "EnsuresNonNullArguments";

            var classes = new HashSet<string>(type.GetTypeInfo().GetNestedTypes().Select(t => t.Name));

            if (!classes.Contains(constructorTestClassName))
            {
                throw new MissingClientConstructorTestClassException(type);
            }

            var ctors = type.GetTypeInfo().GetNestedTypes().Where(t => t.Name == constructorTestClassName)
                .SelectMany(t => t.GetMethods())
                .Where(info => info.ReturnType == typeof(void) && info.IsPublic)
                .Select(info => info.Name);

            var methods = new HashSet<string>(ctors);
            if (!methods.Contains(constructorTestMethodName))
            {
                throw new MissingClientConstructorTestMethodException(type);
            }
        }

        public static IEnumerable<object[]> GetTestConstructorClasses()
        {
            var tests = typeof(GitHubClientTests)
                .GetTypeInfo()
                .Assembly
                .ExportedTypes
                .Where(type => type.GetTypeInfo().IsClass && type.GetTypeInfo().IsPublic && type.Name.EndsWith("ClientTests"))
                .Select(type => new[] { type });
            return tests;
        }
    }
}