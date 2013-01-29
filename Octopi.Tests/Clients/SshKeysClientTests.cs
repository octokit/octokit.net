﻿using System;
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
    public class SshKeysClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new SshKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("/user/keys/42", UriKind.Relative);
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

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
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

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
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

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
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

                sshKeysClient.Update(42, data);

                client.Received().Update(endpoint, data);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new SshKeysClient(Substitute.For<IApiConnection<SshKey>>());
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
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

                sshKeysClient.Create(data);

                client.Received().Create(endpoint, data);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var userEndpoint = new SshKeysClient(Substitute.For<IApiConnection<SshKey>>());
                await AssertEx.Throws<ArgumentNullException>(() => userEndpoint.Create(null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var endpoint = new Uri("/user/keys/42", UriKind.Relative);
                var client = Substitute.For<IApiConnection<SshKey>>();
                var sshKeysClient = new SshKeysClient(client);

                sshKeysClient.Delete(42);

                client.Received().Delete(endpoint);
            }
        }
    }
}
