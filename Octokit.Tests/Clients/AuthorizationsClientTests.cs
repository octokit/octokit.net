using System;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class AuthorizationsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new AuthorizationsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void GetsAListOfAuthorizations()
            {
                var client = Substitute.For<IApiConnection<Authorization>>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.GetAll();

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/authorizations"), null);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void GetsAnAuthorization()
            {
                var client = Substitute.For<IApiConnection<Authorization>>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Get(1);

                client.Received().Get(Arg.Is<Uri>(u => u.ToString() == "/authorizations/1"), null);
            }
        }

        public class TheUpdateAsyncMethod
        {
            [Fact]
            public void SendsUpdateToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Authorization>>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Update(1, new AuthorizationUpdate());

                client.Received().Update(Arg.Is<Uri>(u => u.ToString() == "/authorizations/1"),
                    Args.AuthorizationUpdate);
            }
        }

        public class TheCreateAsyncMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Authorization>>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Create(new AuthorizationUpdate());

                client.Received().Create(Arg.Is<Uri>(u => u.ToString() == "/authorizations")
                    , Args.AuthorizationUpdate);
            }
        }

        public class TheDeleteAsyncMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Authorization>>();
                var authEndpoint = new AuthorizationsClient(client);

                authEndpoint.Delete(1);

                client.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/authorizations/1"));
            }
        }
    }
}
