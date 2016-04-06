using System;
using System.Collections.Generic;
using System.Linq;
using Octokit.Tests.Clients;
using Xunit;

namespace Octokit.Tests.Conventions
{
    public class ClientConstructorTests
    {
        [Theory]
        [MemberData("GetTestConstructorsClasses")]
        public void CheckTestConstructorsNames(Type type)
        {
            const string constructorClassName = "TheCtor";
            var classes = new HashSet<string>(type.GetNestedTypes().Select(t => t.Name));
            
            if (!classes.Contains(constructorClassName))
            {
                throw new MissingClientConstructorTestClassException(type);
            }
        }

        public static IEnumerable<object[]> GetTestConstructorsClasses()
        {
            var tests = typeof(EventsClientTests)
                .Assembly
                .ExportedTypes
                .Where(type => type.IsClass && type.IsPublic && type.Name.EndsWith("ClientTests"))
                .Select(type => new[] { type });
            return tests;
        }
    }
}