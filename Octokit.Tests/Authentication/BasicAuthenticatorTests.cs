using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests
{
    public class BasicAuthenticatorTests
    {
        public class TheAuthenticateMethod
        {
            [Fact]
            public void SetsRequestHeaderForToken()
            {
                var authenticator = new BasicAuthenticator();
                var request = new Request();

                authenticator.Authenticate(request, new Credentials("that-creepy-dude", "Fahrvergnügen"));

                Assert.Contains("Authorization", request.Headers.Keys);
                Assert.Equal("Basic dGhhdC1jcmVlcHktZHVkZTpGYWhydmVyZ27DvGdlbg==", request.Headers["Authorization"]);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var authenticator = new BasicAuthenticator();

                Assert.Throws<ArgumentNullException>(() => authenticator.Authenticate(new Request(), null));
                Assert.Throws<ArgumentNullException>(() => authenticator.Authenticate(null, new Credentials("x", "y")));
                Assert.Throws<ArgumentNullException>(() => authenticator.Authenticate(null, new Credentials("token")));
            }
        }
    }
}
