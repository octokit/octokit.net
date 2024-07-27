using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class GitHubAppsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new GitHubAppsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.Get("foobar");

                connection.Received().Get<GitHubApp>(Arg.Is<Uri>(u => u.ToString() == "apps/foobar"), null);
            }
        }

        public class TheGetCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetCurrent();

                connection.Received().Get<GitHubApp>(Arg.Is<Uri>(u => u.ToString() == "app"), null);
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllInstallationsForCurrent(null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetAllInstallationsForCurrent();

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAllInstallationsForCurrent(options);

                connection.Received().GetAll<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations"), null, options);
            }
        }

        public class TheGetInstallationForCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetInstallationForCurrent(123);

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "app/installations/123"), null);
            }
        }

        public class TheGetAllInstallationsForCurrentUserMethod
        {
            [Fact]
            public async Task GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await client.GetAllInstallationsForCurrentUser();

                await connection.Received().GetAll<InstallationsResponse>(Arg.Is<Uri>(u => u.ToString() == "user/installations"));
            }

            [Fact]
            public async Task GetsFromCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await client.GetAllInstallationsForCurrentUser(options);

                await connection.Received().GetAll<InstallationsResponse>(Arg.Is<Uri>(u => u.ToString() == "user/installations"), null, options);
            }
        }

        public class TheCreateInstallationTokenMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                int fakeInstallationId = 3141;

                client.CreateInstallationToken(fakeInstallationId);

                connection.Received().Post<AccessToken>(Arg.Is<Uri>(u => u.ToString() == "app/installations/3141/access_tokens"), string.Empty);
            }
        }

        public class TheGetOrganizationInstallationForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetOrganizationInstallationForCurrent(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetOrganizationInstallationForCurrent(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetOrganizationInstallationForCurrent("ducks");

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "orgs/ducks/installation"), null);
            }
        }

        public class TheGetRepositoryInstallationForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRepositoryInstallationForCurrent(null, "ducks"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRepositoryInstallationForCurrent("mighty", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRepositoryInstallationForCurrent("", "ducks"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRepositoryInstallationForCurrent("mighty", ""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetRepositoryInstallationForCurrent("mighty", "ducks");

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "repos/mighty/ducks/installation"), null);
            }

            [Fact]
            public void GetsFromCorrectUrlByRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetRepositoryInstallationForCurrent(1234);

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "repositories/1234/installation"), null);
            }
        }

        public class TheGetUserInstallationForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUserInstallationForCurrent(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUserInstallationForCurrent(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.GetUserInstallationForCurrent("ducks");

                connection.Received().Get<Installation>(Arg.Is<Uri>(u => u.ToString() == "users/ducks/installation"), null);
            }
        }

        public class TheCreateAppFromManifestMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateAppFromManifest(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateAppFromManifest(""));
            }

            [Fact]
            public void PostsFromCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new GitHubAppsClient(connection);

                client.CreateAppFromManifest("abc123");

                connection.Received().Post<GitHubAppFromManifest>(Arg.Is<Uri>(u => u.ToString() == "app-manifests/abc123/conversions"));
            }
        }
    }
}
