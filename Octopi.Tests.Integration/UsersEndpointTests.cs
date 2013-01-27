using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests.Integration
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

        public class TheUpdateMethod
        {
            [Fact]
            public async Task FailsWhenNotAuthenticated()
            {
                var github = new GitHubClient();
                var userUpdate = new UserUpdate
                { 
                    Name = "xapitestaccountx",
                    Bio = "UPDATED BIO"
                };

                var e = await AssertEx.Throws<AuthenticationException>(async 
                    () => await github.User.Update(userUpdate));
                e.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
            }

            [Fact]
            public async Task FailsWhenAuthenticatedWithBadCredentials()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "bad-password")
                };
                var userUpdate = new UserUpdate
                { 
                    Name = "xapitestaccountx",
                    Bio = "UPDATED BIO"
                };

                var e = await AssertEx.Throws<AuthenticationException>(async 
                    () => await github.User.Update(userUpdate));
                e.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
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
