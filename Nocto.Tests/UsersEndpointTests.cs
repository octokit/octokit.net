using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Nocto.Endpoints;
using Nocto.Http;
using Nocto.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

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
            [Theory]
            [InlineData(AuthenticationType.Basic)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task GetsAuthenticatedUserWhenAuthenticated(AuthenticationType authenticationType)
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                connection.GetAsync<User>(endpoint).Returns(fakeUserResponse());
                var usersEndpoint = new UsersEndpoint(connection);

                var user = await usersEndpoint.Current();

                user.Should().NotBeNull();
                connection.Received().GetAsync<User>(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                var userEndpoint = new UsersEndpoint(new Connection());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Update(null));
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                await AssertEx.Throws<AuthenticationException>(async () => await new GitHubClient().User.Current());
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Theory]
            [InlineData(AuthenticationType.Basic)]
            [InlineData(AuthenticationType.Oauth)]
            public async Task SucceedsWhenAuthenticatedForAllAuthenticationTypes(AuthenticationType authenticationType)
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.AuthenticationType.Returns(authenticationType);
                connection.PatchAsync<User>(endpoint, Args.UserUpdate).Returns(fakeUserResponse());
                var usersEndpoint = new UsersEndpoint(connection);

                var user = await usersEndpoint.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Args.UserUpdate);
            }

            [Fact]
            public async Task ThrowsIfNotAuthenticated()
            {
                var usersEndpoint = new UsersEndpoint(new Connection());
                await AssertEx.Throws<AuthenticationException>(async () => 
                    await usersEndpoint.Update(new UserUpdate()));
            }
        }
    }
}
