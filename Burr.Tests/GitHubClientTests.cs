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
                client.Login.Should().BeNull();
                client.Password.Should().BeNull();
                client.Token.Should().BeNull();
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Login = "tclem", Password = "pwd" };

                client.AuthenticationType.Should().Be(AuthenticationType.Basic);
                client.Login.Should().Be("tclem");
                client.Password.Should().Be("pwd");
                client.Token.Should().BeNull();
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Token = "abiawethoasdnoi" };

                client.AuthenticationType.Should().Be(AuthenticationType.Oauth);
                client.Token.Should().Be("abiawethoasdnoi");
                client.Login.Should().BeNull();
                client.Password.Should().BeNull();
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
            public void InvalidLoginFallsBackToAnon(string t)
            {
                var client = new GitHubClient { Login = t };

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

        public class TheBaseAddressProperty
        {
            [Fact]
            public void IsSetToGitHubApiV3()
            {
                var client = new GitHubClient();

                client.BaseAddress.Should().Be("https://api.github.com");
            }

            [Fact]
            public void CanSetToCustomAddress()
            {
                var client = new GitHubClient() { BaseAddress = new Uri("https://github.mydomain.com") };

                client.BaseAddress.Should().Be("https://github.mydomain.com");
            }
        }

        public class TheMiddlewareProperty
        {
            [Fact]
            public async Task SetsUpsDefaultMiddlewareStack()
            {
                var client = new GitHubClient();
                client.Connection.MiddlewareStack.Should().NotBeNull();
                var builder = new Builder();

                var app = client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(1);
                app.Should().BeOfType<SimpleJsonParser>();
            }
        }

        public class TheUserMethod
        {
            Func<Task<IResponse<User>>> fakeUserResponse =
                new Func<Task<IResponse<User>>>(() => Task.FromResult<IResponse<User>>(new Response<User> { BodyAsObject = new User() }));

            [Fact]
            public async Task GetsAuthenticatedUserWithBasic()
            {
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<User>(endpoint)).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
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
                try
                {
                    var user = await new GitHubClient().GetUserAsync();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException ex)
                {
                }
            }
        }
    }
}
