using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Helpers
{
    public static class AssertEx
    {
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
    }
}
