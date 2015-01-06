using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace Octokit.Tests.Helpers
{
    public static class AssertEx
    {
        public static void WithMessage(Action assert, string message)
        {
            // TODO: we should just :fire: this to the ground
            assert();
        }

        public static TAttribute HasAttribute<TAttribute>(MemberInfo memberInfo, bool inherit = false) where TAttribute : Attribute
        {
            var attribute = memberInfo.GetCustomAttribute<TAttribute>(inherit);

            Assert.NotNull(attribute);

            return attribute;
        }

        public async static Task<T> Throws<T>(Func<Task> testCode) where T : Exception
        {
            try
            {
                await testCode();
                Assert.Throws<T>(() => { }); // Use xUnit's default behavior.
            }
            catch (T exception)
            {
                return exception;
            }
            // We should never reach this line. It's here because the compiler doesn't know that 
            // Assert.Throws above will always throw.
            return null;
        }

        static readonly string[] whitespaceArguments = { " ", "\t", "\n", "\n\r", "  " };

        public static async Task ThrowsWhenGivenWhitespaceArgument(Func<string, Task> action)
        {
            foreach (var argument in whitespaceArguments)
            {
                await Throws<ArgumentException>(async () => await action(argument));
            }
        }

        public static void IsReadOnlyCollection<T>(object instance) where T : class
        {
            var collection = instance as ICollection<T>;
            // The collection == null case is for .NET 4.0
            Assert.True(instance is IReadOnlyList<T> && (collection == null || collection.IsReadOnly));
        }
    }
}
