using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Xunit.Extensions;
using Moq;
using Burr.Http;

namespace Burr.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CanCreateAnonymousClient()
            {
                var client = new GitHubClient();

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Username = "tclem", Password = "pwd" };

                client.AuthenticationType.Should().Be(AuthenticationType.Basic);
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Token = "abiawethoasdnoi" };

                client.AuthenticationType.Should().Be(AuthenticationType.Oauth);
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidTokenFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Token = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidUserNameFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Username = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [InlineData("")]
            [InlineData(" ")]
            [InlineData(null)]
            [Theory]
            public void InvalidPasswordFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Password = t };

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }
        }

        public class TheUserMethod
        {
            Func<Task<IResponse<User>>> fakeUserResponse =
                new Func<Task<IResponse<User>>>(() => new Task<IResponse<User>>(() => new Response<User> { BodyAsObject = new User() }));

            [Fact]
            public async Task GetsAuthenticatedUserWithBasic()
            {
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<User>(endpoint)).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Username = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var user = await client.GetUserAsync();

                user.Should().NotBeNull();
                c.Verify(x => x.GetAsync<User>(endpoint));
            }

            [Fact]
            public async Task GetsAuthenticatedUserWithToken()
            {
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<User>(endpoint)).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Token = "xyz",
                    Connection = c.Object
                };

                var user = await client.GetUserAsync();

                user.Should().NotBeNull();
                c.Verify(x => x.GetAsync<User>(endpoint));
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                Assert.Throws<AuthenticationException>(async () =>
                {
                    var user = await new GitHubClient().GetUserAsync();
                });
            }
        }
    }
}
