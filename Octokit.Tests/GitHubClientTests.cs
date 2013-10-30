using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests
{
    public class GitHubClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesAnonymousClientByDefault()
            {
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests", "1.0"));

                Assert.Equal(AuthenticationType.Anonymous, client.Credentials.AuthenticationType);
            }

            [Fact]
            public void CanCreateBasicAuthClient()
            {
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests", "1.0"))
                {
                    Credentials = new Credentials("tclem", "pwd")
                };

                Assert.Equal(AuthenticationType.Basic, client.Credentials.AuthenticationType);
            }

            [Fact]
            public void CanCreateOauthClient()
            {
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = new Credentials("token")
                };

                Assert.Equal(AuthenticationType.Oauth, client.Credentials.AuthenticationType);
            }


            [Theory]
            [InlineData("http://github.com", "https://api.github.com/")]
            [InlineData("http://github.com/", "https://api.github.com/")]
            [InlineData("http://example.com/", "http://example.com/api/v3/")]
            [InlineData("http://example.com/anything-really", "http://example.com/api/v3/")]
            [InlineData("http://example.com/anything/really/ok", "http://example.com/api/v3/")]
            [InlineData("http://example.com/api/v3", "http://example.com/api/v3/")]
            [InlineData("https://api.example.com/api/v3", "https://api.example.com/api/v3/")]
            public void FixesUpNonGitHubApiAddress(string baseAddress, string expected)
            {
                var client = new GitHubClient(new ProductHeaderValue("UnitTest"), new Uri(baseAddress));

                Assert.Equal(new Uri(expected), client.BaseAddress);
            }
        }

        public class TheBaseAddressProperty
        {
            [Fact]
            public void IsSetToGitHubApiV3()
            {
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests", "1.0"));

                Assert.Equal(new Uri("https://api.github.com"), client.BaseAddress);
            }
        }

        public class TheCredentialsProperty
        {
            [Fact]
            public void DefaultsToAnonymous()
            {
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests", "1.0"));
                Assert.Same(Credentials.Anonymous, client.Credentials);
            }

            [Fact]
            public void WhenSetCreatesInMemoryStoreThatReturnsSpecifiedCredentials()
            {
                var credentials = new Credentials("Peter", "Griffin");
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"),
                    Substitute.For<ICredentialStore>())
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
                credentialStore.GetCredentials().Returns(Task.Factory.StartNew(() => new Credentials("foo", "bar")));
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"), credentialStore);

                Assert.Equal("foo", client.Credentials.Login);
                Assert.Equal("bar", client.Credentials.Password);
            }
        }
    }
}
