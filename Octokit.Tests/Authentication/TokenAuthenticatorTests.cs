using System;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests
{
    public class TokenAuthenticatorTests
    {
        public class TheAuthenticateMethod
        {
            [Fact]
            public void SetsRequestHeaderForToken()
            {
                var authenticator = new TokenAuthenticator();
                var request = new Request();

                authenticator.Authenticate(request, new Credentials("abcda1234a"));

                Assert.Contains("Authorization", request.Headers.Keys);
                Assert.Equal("Token abcda1234a", request.Headers["Authorization"]);
            }

            [Fact]
            public void EnsuresCredentialsAreOfTheRightType()
            {
                var authenticator = new TokenAuthenticator();
                var request = new Request();

                Assert.Throws<InvalidOperationException>(() =>
                    authenticator.Authenticate(request, new Credentials("login", "password")));
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var authenticator = new TokenAuthenticator();
                Assert.Throws<ArgumentNullException>(() => authenticator.Authenticate(null, Credentials.Anonymous));
                Assert.Throws<ArgumentNullException>(() => 
                    authenticator.Authenticate(Substitute.For<IRequest>(), null));
            }
        }
    }
}
