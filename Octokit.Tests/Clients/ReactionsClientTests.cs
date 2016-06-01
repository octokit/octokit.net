using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ReactionsClient(null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await client.Delete(42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "reactions/42"));
            }
        }
    }
}
