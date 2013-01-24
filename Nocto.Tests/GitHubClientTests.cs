using System;
using FluentAssertions;
using NSubstitute;
using Nocto.Authentication;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesAnonymousClientByDefault()
            {
                var client = new GitHubClient();

                client.AuthenticationType.Should().Be(AuthenticationType.Anonymous);
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<Authenticator>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[2](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("tclem", "pwd") };

                client.AuthenticationType.Should().Be(AuthenticationType.Basic);
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<Authenticator>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[2](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient { Credentials = new Credentials("token") };

                client.AuthenticationType.Should().Be(AuthenticationType.Oauth);
                var builder = new Builder();
                client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                builder.Handlers[0](Substitute.For<IApplication>()).Should().BeOfType<Authenticator>();
                builder.Handlers[1](Substitute.For<IApplication>()).Should().BeOfType<ApiInfoParser>();
                builder.Handlers[2](Substitute.For<IApplication>()).Should().BeOfType<SimpleJsonParser>();
            }

            [Fact]
            public void EnsuresArgumentNotNull()
            {
                var uri = new Uri("http://example.com", UriKind.Absolute);
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((Uri)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((Credentials)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((ICredentialStore)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(Credentials.Anonymous, null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((Credentials)null, uri));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((ICredentialStore)null, uri));
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

        public class TheCredentialStoreProperty
        {
            [Fact]
            public void SwapsOutCredentialStore()
            {
                var credentials = new Credentials("Peter", "Griffin");
                var client = new GitHubClient(credentials);
                client.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
                var newCredentialStore = Substitute.For<ICredentialStore>();

                client.CredentialStore = newCredentialStore;

                client.CredentialStore.Should().BeSameAs(newCredentialStore);
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

                client.CredentialStore.Should().BeOfType<InMemoryCredentialStore>();
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

        public class TheMiddlewareProperty
        {
            [Fact]
            public void SetsUpsDefaultMiddlewareStack()
            {
                var client = new GitHubClient();
                client.Connection.MiddlewareStack.Should().NotBeNull();
                var builder = new Builder();

                var app = client.Connection.MiddlewareStack(builder);
                builder.Handlers.Count.Should().Be(3);
                app.Should().BeOfType<Authenticator>();
            }
        }
    }
}
