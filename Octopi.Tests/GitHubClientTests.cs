using System;
using FluentAssertions;
using NSubstitute;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesAnonymousClientByDefault()
            {
                var client = new GitHubClient();

                client.Credentials.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("tclem", "pwd") };

                client.Credentials.AuthenticationType.Should().Be(AuthenticationType.Basic);
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("token") };

                client.Credentials.AuthenticationType.Should().Be(AuthenticationType.Oauth);
            }

            [Fact]
            public void EnsuresArgumentNotNull()
            {
                var uri = new Uri("http://example.com", UriKind.Absolute);
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((Uri)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((ICredentialStore)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, uri));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(Substitute.For<ICredentialStore>(), null));
            }
        }

        public class TheBaseAddressProperty
        {
            [Fact]
            public void IsSetToGitHubApiV3()
            {
                var client = new GitHubClient();

                client.BaseAddress.Should().Be("https://api.github.com");
            }
        }

        public class TheCredentialsProperty
        {
            [Fact]
            public void DefaultsToAnonymous()
            {
                var client = new GitHubClient();
                client.Credentials.Should().BeSameAs(Credentials.Anonymous);
            }

            [Fact]
            public void WhenSetCreatesInMemoryStoreThatReturnsSpecifiedCredentials()
            {
                var credentials = new Credentials("Peter", "Griffin");
                var client = new GitHubClient(Substitute.For<ICredentialStore>())
                {
                    Credentials = credentials
                };

                client.Connection.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
                client.Credentials.Should().BeSameAs(credentials);
            }

            [Fact]
            public void IsRetrievedFromCredentialStore()
            {
                var credentialStore = Substitute.For<ICredentialStore>();
                credentialStore.GetCredentials().Returns(new Credentials("foo", "bar"));
                var client = new GitHubClient(credentialStore);

                client.Credentials.Login.Should().Be("foo");
                client.Credentials.Password.Should().Be("bar");
            }
        }
    }
}
