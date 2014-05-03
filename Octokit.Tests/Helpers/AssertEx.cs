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
        public static void Empty<T>(IEnumerable<T> actual, string message)
        {
            var empty = Enumerable.Empty<T>();
            WithMessage(() => Assert.Equal(empty, actual.ToArray()), message);
        }

        public static void WithMessage(Action assert, string message)
        {
            try
            {
                assert();
            }
            catch(AssertException ex)
            {
                throw new Exception(message, ex);
            }
        }

        public static void HasAttribute<TAttribute>(MemberInfo memberInfo, bool inherit = false) where TAttribute : Attribute
        {
            Assert.True(memberInfo.IsDefined(typeof(TAttribute), inherit), memberInfo.ToString() + Environment.NewLine);
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
            Assert.True(instance is IReadOnlyCollection<T> && (collection == null || collection.IsReadOnly));
        }
    }
}
