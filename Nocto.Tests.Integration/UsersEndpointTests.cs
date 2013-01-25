using System.Threading.Tasks;
using FluentAssertions;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Integration
{
    public class UsersEndpointTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                // Get a user by username
                var user = await github.User.Get("tclem");

                user.Company.Should().Be("GitHub");
            }
        }

        public class TheCurrentMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var user = await github.User.Current();

                user.Login.Should().Be("xapitestaccountx");
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task ReturnsAPageOfUsers()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var users = await github.User.GetAll();

                users.Should().HaveCount(c => c > 0);
            }
        }
    }
}
