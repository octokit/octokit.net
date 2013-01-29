using System;
using FluentAssertions;
using Octokit.Authentication;
using Octokit.Http;
using Xunit;

namespace Octokit.Tests
{
    public class BasicAuthenticatorTests
    {
        public class TheConstructor
        {
        }

        public class TheAuthenticateMethod
        {
            [Fact]
            public void SetsRequestHeaderForToken()
            {
                var authenticator = new BasicAuthenticator();
                var request = new Request();

                authenticator.Authenticate(request, new Credentials("that-creepy-dude", "Fahrvergnügen"));

                request.Headers.Should().ContainKey("Authorization");
                request.Headers["Authorization"].Should().Be("Basic dGhhdC1jcmVlcHktZHVkZTpGYWhydmVyZ27DvGdlbg==");
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
