using System;
using System.Threading.Tasks;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Octopi.Tests.Helpers;
using Xunit;

namespace Octopi.Tests
{
    /// <summary>
    /// Endpoint tests mostly just need to make sure they call the IApiClient with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiClientTests.cs.
    /// </summary>
    public class UsersEndpointTests
    {
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
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var client = Substitute.For<IApiClient<User>>();
                var usersClient = new UsersEndpoint(client);

                usersClient.Current();

                client.Received().Get(endpoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullUser()
            {
                var userEndpoint = new UsersEndpoint(Substitute.For<IApiClient<User>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Get(null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void SendsUpdateToCorrectUrl()
            {
                var endpoint = new Uri("/user", UriKind.Relative);
                var client = Substitute.For<IApiClient<User>>();
                var usersClient = new UsersEndpoint(client);

                usersClient.Update(new UserUpdate());

                client.Received().Update(endpoint, Args.UserUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new UsersEndpoint(Substitute.For<IApiClient<User>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Update(null));
            }
        }
    }
}
