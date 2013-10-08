using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Authentication
{
    public class CredentialsTests
    {
        public class TheAuthenticationTypeProperty
        {
            [Fact]
            public void ReturnsAnonymousForEmptyCtor()
            {
                var credentials = Credentials.Anonymous;
                Assert.Equal(AuthenticationType.Anonymous, credentials.AuthenticationType);
            }

            [Fact]
            public void ReturnsBasicWhenProvidedLoginAndPassword()
            {
                var credentials = new Credentials("login", "password");
                Assert.Equal(AuthenticationType.Basic, credentials.AuthenticationType);
            }

            [Fact]
            public void ReturnsOuthWhenProvidedToken()
            {
                var credentials = new Credentials("token");
                Assert.Equal(AuthenticationType.Oauth, credentials.AuthenticationType);
            }
        }

        public class TheLoginProperty
        {
            [Fact]
            public void IsSetFromCtor()
            {
                var credentials = new Credentials("login", "password");
                Assert.Equal("login", credentials.Login);
            }
        }

        public class ThePasswordProperty
        {
            [Fact]
            public void IsSetFromCtor()
            {
                var credentials = new Credentials("login", "password");
                Assert.Equal("password", credentials.Password);
            }
        }
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgumentsNotNullNorEmpty()
            {
                Assert.Throws<ArgumentNullException>(() => new Credentials(null));
                Assert.Throws<ArgumentException>(() => new Credentials(" "));
                Assert.Throws<ArgumentNullException>(() => new Credentials(null, "password"));
                Assert.Throws<ArgumentNullException>(() => new Credentials("login", null));
                Assert.Throws<ArgumentException>(() => new Credentials(" ", "Password"));
                Assert.Throws<ArgumentException>(() => new Credentials("login", " "));
            }
        }
    }
}
