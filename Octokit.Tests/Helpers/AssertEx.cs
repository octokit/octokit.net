using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public static class AssertEx
    {
        public static void WithMessage(Action assert, string message)
        {
            // TODO: we should just :fire: this to the ground
            assert();
        }

        static readonly string[] whitespaceArguments = { " ", "\t", "\n", "\n\r", "  " };

        public static async Task ThrowsWhenGivenWhitespaceArgument(Func<string, Task> action)
        {
            foreach (var argument in whitespaceArguments)
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await action(argument));
            }
        }

        public static void IsReadOnlyCollection<T>(object instance)
        {
            var collection = instance as ICollection<T>;
            // The collection == null case is for .NET 4.0
            Assert.True(instance is IReadOnlyList<T> && (collection == null || collection.IsReadOnly));
        }
    }
}
