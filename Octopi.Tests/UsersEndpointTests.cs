using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octopi.Tests
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
            public async Task GetsAuthenticatedUser()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
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
        }

        public class TheUpdateAsyncMethod
        {
            [Fact]
            public async Task UpdatesUserWithSuppliedChanges()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                connection.PatchAsync<User>(endpoint, Args.UserUpdate).Returns(fakeUserResponse());
                var usersEndpoint = new UsersEndpoint(connection);

                var user = await usersEndpoint.Update(new UserUpdate { Name = "Tim" });

                user.Should().NotBeNull();
                connection.Received().PatchAsync<User>(endpoint, Args.UserUpdate);
            }
        }
    }
}
