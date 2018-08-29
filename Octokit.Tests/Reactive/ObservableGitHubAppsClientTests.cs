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
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllInstallationsForCurrent(null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetAllInstallationsForCurrent();

                connection.Received().Get<List<Installation>>(
                    Arg.Is<Uri>(u => u.ToString() == "app/installations"),
                    Args.EmptyDictionary,
                    "application/vnd.github.machine-man-preview+json");
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
                            && x["per_page"] == "1"),
                    "application/vnd.github.machine-man-preview+json");
            }
        }

        public class TheGetInstallationMethod
        {
            [Fact]
            public void GetsFromCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableGitHubAppsClient(gitHubClient);

                client.GetInstallation(123);

                gitHubClient.GitHubApps.Received().GetInstallation(123);
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
    }
}
