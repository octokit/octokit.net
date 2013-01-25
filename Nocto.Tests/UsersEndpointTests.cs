using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Endpoints;
using Nocto.Http;
using Nocto.Tests.Helpers;
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
                    Credentials = new Credentials("tclem", "pwd"),
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
                    Credentials = new Credentials("xyz"),
                    Connection = connection
                };

                var user = await client.User.Current();

                user.Should().NotBeNull();
                connection.Received().GetAsync<User>(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                var user = (new GitHubClient { Credentials = new Credentials("token") }).User;
                await AssertEx.Throws<ArgumentNullException>(() => user.Update(null));
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                await AssertEx.Throws<AuthenticationException>(async () => await new GitHubClient().User.Current());
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Fact]
            public async Task UpdatesAuthenticatedUserWithBasic()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<User>(endpoint, Args.UserUpdate).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Credentials = new Credentials("tclem", "pwd"),
                    Connection = connection
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Args.UserUpdate);
            }

            [Fact]
            public async Task UpdatesAuthenticatedUserWithToken()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<User>(endpoint, Args.UserUpdate).Returns(fakeUserResponse());
                var client = new GitHubClient
                {
                    Credentials = new Credentials("token"),
                    Connection = connection
                };

                var user = await client.User.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Args.UserUpdate);
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                var user = (new GitHubClient()).User;
                await AssertEx.Throws<AuthenticationException>(async () => await user.Update(new UserUpdate()));
            }
        }
    }
}
