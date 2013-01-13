using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Endpoints;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
{
    public class UsersEndpointTests
    {
        static readonly Func<Task<IResponse<User>>> fakeUserResponse =
            () => Task.FromResult<IResponse<User>>(new GitHubResponse<User> { BodyAsObject = new User() });

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new UsersEndpoint(null));
            }
        }

        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task GetsAuthenticatedUserWithBasic()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<User>(endpoint).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var user = await client.User.Current();

                user.Should().NotBeNull();
                connection.Received().GetAsync<User>(endpoint);
            }

            [Fact]
            public async Task GetsAuthenticatedUserWithToken()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<User>(endpoint).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Token = "xyz",
                    Connection = connection
                };

                var user = await client.User.Current();

                user.Should().NotBeNull();
                connection.Received().GetAsync<User>(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                try
                {
                    await (new GitHubClient { Token = "axy" }).User.Update(null);

                    Assert.True(false, "ArgumentNullException was not thrown");
                }
                catch (ArgumentNullException)
                {
                }
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                try
                {
                    await new GitHubClient().User.Current();

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Fact]
            public async Task UpdatesAuthenticatedUserWithBasic()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<User>(endpoint, Arg.Any<UserUpdate>()).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = connection
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Arg.Any<UserUpdate>());
            }

            [Fact]
            public async Task UpdatesAuthenticatedUserWithToken()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<User>(endpoint, Arg.Any<UserUpdate>()).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Token = "xyz",
                    Connection = connection
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Arg.Any<UserUpdate>());
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                try
                {
                    await new GitHubClient().User.Update(new UserUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }
        }
    }
}
