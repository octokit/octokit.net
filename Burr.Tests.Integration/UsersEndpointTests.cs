using System;
using System.Threading.Tasks;
using Xunit;

namespace Burr.Tests.Integration
{
    public class UsersEndpointTests
    {
        public class TheGetUserAsyncMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

                // Get a user by username
                var user = await github.Users.GetUserAsync("tclem");

                Assert.Equal("GitHub", user.Company);
            }
        }

        public class TheGetAuthenticatedUserAsyncMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient { Login = "xapitestaccountx", Password = "octocat11" };

                // Get a user by username
                var user = await github.Users.GetAuthenticatedUserAsync();

                Assert.Equal("xapitestaccountx", user.Login);
            }
        }

        public class TheGetUsersAsyncMethod
        {
            [Fact]
            public async Task ReturnsAllUsers()
            {
                var github = new GitHubClient();

                // Get a user by username
                var users = await github.Users.GetUsersAsync();

                Console.WriteLine(users);
            }
        }
    }
}
