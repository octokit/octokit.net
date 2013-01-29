using System;
using System.Threading.Tasks;
using NSubstitute;
using Octopi.Clients;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class UsersClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new UsersClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/users/username", UriKind.Relative);
                var client = Substitute.For<IApiConnection<User>>();
                var usersClient = new UsersClient(client);

                usersClient.Get("username");

                client.Received().Get(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                var userEndpoint = new UsersClient(Substitute.For<IApiConnection<User>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Get(null));
            }
        }

        public class TheCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var client = Substitute.For<IApiConnection<User>>();
                var usersClient = new UsersClient(client);

                usersClient.Current();

                client.Received().Get(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                var userEndpoint = new UsersClient(Substitute.For<IApiConnection<User>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Get(null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void SendsUpdateToCorrectUrl()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var client = Substitute.For<IApiConnection<User>>();
                var usersClient = new UsersClient(client);

                usersClient.Update(new UserUpdate());

                client.Received().Update(endpoint, Args.UserUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new UsersClient(Substitute.For<IApiConnection<User>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Update(null));
            }
        }
    }
}
