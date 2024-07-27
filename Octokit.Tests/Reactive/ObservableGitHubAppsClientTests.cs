using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableGitHubAppsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableGitHubAppsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Get(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.Get(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.Get("foobar");

                gitHubClient.GitHubApps.Received().Get("foobar");
            }
        }

        public class TheGetCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetCurrent();

                gitHubClient.GitHubApps.Received().GetCurrent();
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllInstallationsForCurrent(null));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetAllInstallationsForCurrent();

                connection.Received().Get<List<Installation>>(
                    Arg.Is<Uri>(u => u.ToString() == "app/installations"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1
                };

                client.GetAllInstallationsForCurrent(options);

                connection.Received().Get<List<Installation>>(
                    Arg.Is<Uri>(u => u.ToString() == "app/installations"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }
        }

        public class TheGetInstallationForCurrentMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetInstallationForCurrent(123);

                gitHubClient.GitHubApps.Received().GetInstallationForCurrent(123);
            }
        }

        public class TheGetAllInstallationsForCurrentUserMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetAllInstallationsForCurrentUser();

                connection.Received().Get<List<InstallationsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations"),
                    null);
            }

            [Fact]
            public void GetsFromCorrectUrlWithOptions()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1
                };

                client.GetAllInstallationsForCurrentUser(options);

                connection.Received().Get<List<InstallationsResponse>>(
                    Arg.Is<Uri>(u => u.ToString() == "user/installations"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["per_page"] == "1"));
            }
        }

        public class TheCreateInstallationTokenMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                int fakeInstallationId = 3141;

                client.CreateInstallationToken(fakeInstallationId);

                gitHubClient.GitHubApps.Received().CreateInstallationToken(fakeInstallationId);
            }
        }

        public class TheGetOrganizationInstallationForCurrentMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetOrganizationInstallationForCurrent(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.GetOrganizationInstallationForCurrent(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetOrganizationInstallationForCurrent("ducks");

                gitHubClient.GitHubApps.Received().GetOrganizationInstallationForCurrent("ducks");
            }
        }

        public class TheGetRepositoryInstallationForCurrentMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetRepositoryInstallationForCurrent(null, "ducks"));
                Assert.Throws<ArgumentNullException>(() => client.GetRepositoryInstallationForCurrent("mighty", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.GetRepositoryInstallationForCurrent("", "ducks"));
                Assert.Throws<ArgumentException>(() => client.GetRepositoryInstallationForCurrent("mighty", ""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetRepositoryInstallationForCurrent("mighty", "ducks");

                gitHubClient.GitHubApps.Received().GetRepositoryInstallationForCurrent("mighty", "ducks");
            }

            [Fact]
            public void GetsFromCorrectUrlByRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetRepositoryInstallationForCurrent(1234);

                gitHubClient.GitHubApps.Received().GetRepositoryInstallationForCurrent(1234);
            }
        }

        public class TheGetUserInstallationForCurrentMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetUserInstallationForCurrent(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.GetUserInstallationForCurrent(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetUserInstallationForCurrent("ducks");

                gitHubClient.GitHubApps.Received().GetUserInstallationForCurrent("ducks");
            }
        }

        public class TheCreateAppFromManifestMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.CreateAppFromManifest(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentException>(() => client.CreateAppFromManifest(""));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.CreateAppFromManifest("abc123");

                gitHubClient.GitHubApps.Received().CreateAppFromManifest("abc123");
            }
        }
    }
}
