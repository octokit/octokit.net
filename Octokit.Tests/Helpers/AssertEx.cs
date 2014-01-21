using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public static class AssertEx
    {
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

        public static async void ThrowsWhenGivenWhitespaceArgument(Func<string, Task> action)
        {
            foreach (var argument in whitespaceArguments)
            {
                await Throws<ArgumentException>(async () => await action(argument));
            }
        }
    }
}
