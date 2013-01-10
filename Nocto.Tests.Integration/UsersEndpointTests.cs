using System;
using System.Threading.Tasks;
using FluentAssertions;
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
                var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

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
                var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

                var user = await github.User.Current();

                user.Login.Should().Be("xapitestaccountx");
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task ReturnsAllUsers()
            {
                var github = new GitHubClient();

                var users = await github.User.GetAll();

                users.Should().HaveCount(c => c > 0);
            }
        }
    }
}
