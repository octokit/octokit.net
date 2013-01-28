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
    public class SshKeysEndpointTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new SshKeysEndpoint(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/user/keys/42", UriKind.Relative);
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.Get(42);

                client.Received().Get(endpoint);
            }
        }
        
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/users/username/keys", UriKind.Relative);
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.GetAll("username");

                client.Received().GetAll(endpoint);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/user/keys", UriKind.Relative);
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.GetAllForCurrent();

                client.Received().GetAll(endpoint);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void SendsUpdateToCorrectUrl()
            {
                var endpoint = new Uri("/user/keys/42", UriKind.Relative);
                var data = new SshKeyUpdate();
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.Update(42, data);

                client.Received().Update(endpoint, data);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new SshKeysEndpoint(Substitute.For<IApiClient<SshKey>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Update(1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var endpoint = new Uri("/user/keys", UriKind.Relative);
                var data = new SshKeyUpdate();
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.Create(data);

                client.Received().Create(endpoint, data);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new SshKeysEndpoint(Substitute.For<IApiClient<SshKey>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Create(null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var endpoint = new Uri("/user/keys/42", UriKind.Relative);
                var client = Substitute.For<IApiClient<SshKey>>();
                var sshKeysClient = new SshKeysEndpoint(client);

                sshKeysClient.Delete(42);

                client.Received().Delete(endpoint);
            }
        }
    }
}
