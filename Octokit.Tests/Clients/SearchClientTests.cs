using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;
using System.Collections.Generic;

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

                client.SearchUsers(new SearchTerm("something"));

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/users"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchUsers(null));
            }
        }

        public class TheSearchRepoMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                client.SearchRepo(new SearchTerm("something"));
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/repositories"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchRepo(null));
            }
        }

        public class TheSearchIssuesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                client.SearchIssues(new SearchTerm("something"));
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/issues"), Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new SearchClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.SearchIssues(null));
            }
        }

        public class TheSearchCodeMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SearchClient(connection);

                client.SearchCode(new SearchTerm("something"));
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "search/code"), Arg.Any<Dictionary<string, string>>());
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
