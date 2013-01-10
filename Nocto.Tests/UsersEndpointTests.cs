using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Nocto.Endpoints;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
{
    public class UsersEndpointTests
    {
        static readonly Func<Task<IResponse<User>>> fakeUserResponse =
            () => Task.FromResult<IResponse<User>>(new Response<User> { BodyAsObject = new User() });

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
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<User>(endpoint)).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var user = await client.User.Current();

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

                var user = await client.User.Current();

                user.Should().NotBeNull();
                c.Verify(x => x.GetAsync<User>(endpoint));
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                try
                {
                    var user = await (new GitHubClient { Token = "axy" }).User.Update(null);

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
                    var user = await new GitHubClient().User.Current();

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
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.PatchAsync<User>(endpoint, It.IsAny<UserUpdate>())).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                c.Verify(x => x.PatchAsync<User>(endpoint, It.IsAny<UserUpdate>()));
            }

            [Fact]
            public async Task UpdatesAuthenticatedUserWithToken()
            {
                var endpoint = "/user";
                var c = new Mock<IConnection>();
                c.Setup(x => x.PatchAsync<User>(endpoint, It.IsAny<UserUpdate>())).Returns(fakeUserResponse);
                var client = new GitHubClient
                {
                    Token = "xyz",
                    Connection = c.Object
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                c.Verify(x => x.PatchAsync<User>(endpoint, It.IsAny<UserUpdate>()));
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                try
                {
                    var user = await new GitHubClient().User.Update(new UserUpdate());

                    Assert.True(false, "AuthenticationException was not thrown");
                }
                catch (AuthenticationException)
                {
                }
            }
        }
    }
}
