using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Octokit.Caching;

namespace Octokit.Tests
{
    public class GitHubClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((IConnection)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient((ProductHeaderValue)null));

                var productInformation = new ProductHeaderValue("UnitTest");
                var baseAddress = new Uri("http://github.com");
                var credentialStore = Substitute.For<ICredentialStore>();

                Assert.Throws<ArgumentNullException>(() => new GitHubClient(productInformation, (ICredentialStore)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, credentialStore));

                Assert.Throws<ArgumentNullException>(() => new GitHubClient(productInformation, (Uri)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, baseAddress));

                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, (ICredentialStore)null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, (Uri)null));

                Assert.Throws<ArgumentNullException>(() => new GitHubClient(productInformation, null, null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, credentialStore, null));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, null, baseAddress));

                Assert.Throws<ArgumentNullException>(() => new GitHubClient(null, credentialStore, baseAddress));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(productInformation, null, baseAddress));
                Assert.Throws<ArgumentNullException>(() => new GitHubClient(productInformation, credentialStore, null));
            }

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
                credentialStore.GetCredentials().Returns(Task.FromResult(new Credentials("foo", "bar")));
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"), credentialStore);

                Assert.Equal("foo", client.Credentials.Login);
                Assert.Equal("bar", client.Credentials.Password);
            }
        }

        public class TheResponseCacheProperty
        {
            [Fact]
            public void WhenSetWrapsExistingHttpClientWithCachingHttpClient()
            {
                var responseCache = Substitute.For<IResponseCache>();
                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"));
                Assert.IsType<Connection>(client.Connection);
                var existingConnection = (Connection) client.Connection;
                var existingHttpClient = existingConnection._httpClient;

                client.ResponseCache = responseCache;

                Assert.Equal(existingConnection, client.Connection);
                Assert.IsType<CachingHttpClient>(existingConnection._httpClient);
                var cachingHttpClient = (CachingHttpClient) existingConnection._httpClient;
                Assert.Equal(existingHttpClient, cachingHttpClient._httpClient);
                Assert.Equal(responseCache, cachingHttpClient._responseCache);
            }

            [Fact]
            public void WhenResetWrapsUnderlyingHttpClientWithCachingHttpClient()
            {
                var responseCache = Substitute.For<IResponseCache>();

                var client = new GitHubClient(new ProductHeaderValue("OctokitTests"));
                Assert.IsType<Connection>(client.Connection);
                var existingConnection = (Connection) client.Connection;
                var existingHttpClient = existingConnection._httpClient;
                client.ResponseCache = Substitute.For<IResponseCache>();

                client.ResponseCache = responseCache;

                Assert.Equal(existingConnection, client.Connection);
                Assert.IsType<CachingHttpClient>(existingConnection._httpClient);
                var cachingHttpClient = (CachingHttpClient) existingConnection._httpClient;
                Assert.Equal(existingHttpClient, cachingHttpClient._httpClient);
                Assert.Equal(responseCache, cachingHttpClient._responseCache);
            }
        }

        public class TheLastApiInfoProperty
        {
            [Fact]
            public async Task ReturnsNullIfNew()
            {
                var connection = Substitute.For<IConnection>();
                connection.GetLastApiInfo().Returns((ApiInfo)null);
                var client = new GitHubClient(connection);

                var result = client.GetLastApiInfo();

                Assert.Null(result);

                var temp = connection.Received(1).GetLastApiInfo();
            }

            [Fact]
            public async Task ReturnsObjectIfNotNew()
            {
                var apiInfo = new ApiInfo(
                                new Dictionary<string, Uri>
                                {
                                    {
                                        "next",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=4&per_page=5")
                                    },
                                    {
                                        "last",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=131&per_page=5")
                                    },
                                    {
                                        "first",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=1&per_page=5")
                                    },
                                    {
                                        "prev",
                                        new Uri("https://api.github.com/repos/rails/rails/issues?page=2&per_page=5")
                                    }
                                },
                                new List<string>
                                {
                                    "user"
                                },
                                new List<string>
                                {
                                    "user",
                                    "public_repo",
                                    "repo",
                                    "gist"
                                },
                                "5634b0b187fd2e91e3126a75006cc4fa",
                                new RateLimit(100, 75, 1372700873)
                            );
                var connection = Substitute.For<IConnection>();
                connection.GetLastApiInfo().Returns(apiInfo);
                var client = new GitHubClient(connection);

                var result = client.GetLastApiInfo();

                Assert.NotNull(result);

                var temp = connection.Received(1).GetLastApiInfo();
            }
        }

        public class TheSetRequestTimeoutMethod
        {
            [Fact]
            public void SetsTheTimeoutOnTheUnderlyingHttpClient()
            {
                var httpClient = Substitute.For<IHttpClient>();
                var client = new GitHubClient(new Connection(new ProductHeaderValue("OctokitTests"), httpClient));

                client.SetRequestTimeout(TimeSpan.FromSeconds(15));


                httpClient.Received(1).SetRequestTimeout(TimeSpan.FromSeconds(15));
            }
        }

        public class TheNestedClients
        {
            private static void VisitAllClientTypes(Type rootType, HashSet<Type> result)
            {
                const BindingFlags ifPropBinding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
                if (!result.Add(rootType))
                    return;

                foreach (var pi in rootType.GetProperties(ifPropBinding).Where(pi => pi.CanRead && pi.PropertyType.Name.EndsWith("Client", StringComparison.Ordinal)))
                {
                    VisitAllClientTypes(pi.PropertyType, result);
                }
            }

            internal static IEnumerable<Type> GetGitHubClientNestedInterfaces()
            {
                var visitedTypes = new HashSet<Type>();
                var rootType = typeof(GitHubClient);
                VisitAllClientTypes(rootType, visitedTypes);
                visitedTypes.Remove(rootType);
                return visitedTypes;
            }

            public static IEnumerable<object[]> GetGitHubClientNestedInterfacesMemberData() =>
                GetGitHubClientNestedInterfaces().Select(t => new[] { t });

            [Theory]
            [MemberData(nameof(GetGitHubClientNestedInterfacesMemberData))]
            public void HasImplementationClassWithIApiConnectionCtor(Type clientInterface)
            {
                var octokitAssembly = typeof(GitHubClient).Assembly;

                var implTypes = octokitAssembly.GetTypes()
                    .Where(t => t.IsClass && t.IsPublic)
                    .Where(t => t.GetInterfaces().Contains(clientInterface))
                    .ToList();

                Assert.Single(implTypes, t =>
                {
                    const BindingFlags ctorBinding = BindingFlags.Instance | BindingFlags.Public;
                    var ctor = t.GetConstructor(ctorBinding, Type.DefaultBinder,
                        new[] { typeof(IApiConnection) }, null)
                        ?? t.GetConstructor(ctorBinding, Type.DefaultBinder,
                        new[] { typeof(IConnection) }, null);
                    return !(ctor is null);
                });
            }
        }
    }
}
