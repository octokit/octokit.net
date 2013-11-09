using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class SearchClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new SearchClient(null));
            }
        }

        public class TheSearchUsersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                client.SearchCode(new SearchTerm("something"));

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users?q=something"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchCode(null));
            }
        }

    }
}
