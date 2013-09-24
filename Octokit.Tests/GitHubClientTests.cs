using System;
using NSubstitute;
using Octokit.Http;
using Xunit;

namespace Octokit.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesAnonymousClientByDefault()
            {
                var client = new GitHubClient();

                Assert.Equal(AuthenticationType.Anonymous, client.Credentials.AuthenticationType);
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("tclem", "pwd") };

                Assert.Equal(AuthenticationType.Basic, client.Credentials.AuthenticationType);
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("token") };

                Assert.Equal(AuthenticationType.Oauth, client.Credentials.AuthenticationType);
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

                Assert.Equal(new Uri("https://api.github.com"), client.BaseAddress);
            }
        }

        public class TheCredentialsProperty
        {
            [Fact]
            public void DefaultsToAnonymous()
            {
                var client = new GitHubClient();
                Assert.Same(Credentials.Anonymous, client.Credentials);
            }

            [Fact]
            public void WhenSetCreatesInMemoryStoreThatReturnsSpecifiedCredentials()
            {
                var credentials = new Credentials("Peter", "Griffin");
                var client = new GitHubClient(Substitute.For<ICredentialStore>())
                {
                    Credentials = credentials
                };

                Assert.IsType<InMemoryCredentialStore>(client.Connection.CredentialStore);
                Assert.Same(credentials, client.Credentials);
            }

            [Fact]
            public void IsRetrievedFromCredentialStore()
            {
                var credentialStore = Substitute.For<ICredentialStore>();
                credentialStore.GetCredentials().Returns(new Credentials("foo", "bar"));
                var client = new GitHubClient(credentialStore);

                Assert.Equal("foo", client.Credentials.Login);
                Assert.Equal("bar", client.Credentials.Password);
            }
        }
    }
}
