using System;
using FluentAssertions;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests.Authentication
{
    public class CredentialsTests
    {
        public class TheAuthenticationTypeProperty
        {
            [Fact]
            public void ReturnsAnonymousForEmptyCtor()
            {
                var credentials = Credentials.Anonymous;
                credentials.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [Fact]
            public void ReturnsBasicWhenProvidedLoginAndPassword()
            {
                var credentials = new Credentials("login", "password");
                credentials.AuthenticationType.Should().Be(AuthenticationType.Basic);
            }

            [Fact]
            public void ReturnsOuthWhenProvidedToken()
            {
                var credentials = new Credentials("token");
                credentials.AuthenticationType.Should().Be(AuthenticationType.Oauth);
            }
        }

        public class TheLoginProperty
        {
            [Fact]
            public void IsSetFromCtor()
            {
                var credentials = new Credentials("login", "password");
                credentials.Login.Should().Be("login");
            }
        }

        public class ThePasswordProperty
        {
            [Fact]
            public void IsSetFromCtor()
            {
                var credentials = new Credentials("login", "password");
                credentials.Password.Should().Be("password");
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
