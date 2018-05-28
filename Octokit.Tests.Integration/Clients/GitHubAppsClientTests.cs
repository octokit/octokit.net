using Octokit.Tests.Integration.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class GitHubAppsClientTests
    {
        public class TheGetMethod
        {
            IGitHubClient _github;

            public TheGetMethod()
            {
                // Regular authentication
                _github = Helper.GetAuthenticatedClient();
            }

            [GitHubAppsTest]
            public async Task GetsApp()
            {
                var result = await _github.GitHubApps.Get(Helper.GitHubAppSlug);

                Assert.Equal(Helper.GitHubAppId, result.Id);
                Assert.False(string.IsNullOrEmpty(result.Name));
                Assert.NotNull(result.Owner);
            }
        }

        public class TheGetCurrentMethod
        {
            IGitHubClient _github;

            public TheGetCurrentMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task GetsCurrentApp()
            {
                var result = await _github.GitHubApps.GetCurrent();

                Assert.Equal(Helper.GitHubAppId, result.Id);
                Assert.False(string.IsNullOrEmpty(result.Name));
                Assert.NotNull(result.Owner);
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            IGitHubClient _github;

            public TheGetAllInstallationsForCurrentMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task GetsAllInstallations()
            {
                var result = await _github.GitHubApps.GetAllInstallationsForCurrent();

                foreach (var installation in result)
                {
                    Assert.Equal(Helper.GitHubAppId, installation.AppId);
                    Assert.NotNull(installation.Account);
                    Assert.NotNull(installation.Permissions);
                    Assert.Equal(InstallationPermissionLevel.Read, installation.Permissions.Metadata);
                    Assert.False(string.IsNullOrEmpty(installation.HtmlUrl));
                    Assert.NotEqual(0, installation.TargetId);
                }
            }
        }

        public class TheGetInstallationMethod
        {
            IGitHubClient _github;

            public TheGetInstallationMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task GetsInstallation()
            {
                // Get the installation Id
                var installationId = Helper.GetGitHubAppInstallationForOwner(Helper.UserName).Id;

                // Get the installation by Id
                var result = await _github.GitHubApps.GetInstallation(installationId);

                Assert.True(result.AppId == Helper.GitHubAppId);
                Assert.Equal(Helper.GitHubAppId, result.AppId);
                Assert.NotNull(result.Account);
                Assert.NotNull(result.Permissions);
                Assert.Equal(InstallationPermissionLevel.Read, result.Permissions.Metadata);
                Assert.False(string.IsNullOrEmpty(result.HtmlUrl));
                Assert.NotEqual(0, result.TargetId);
            }
        }

        public class TheCreateInstallationTokenMethod
        {
            IGitHubClient _github;

            public TheCreateInstallationTokenMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task CreatesInstallationToken()
            {
                // Get the installation Id
                var installationId = Helper.GetGitHubAppInstallationForOwner(Helper.UserName).Id;

                // Create installation token
                var result = await _github.GitHubApps.CreateInstallationToken(installationId);

                Assert.NotNull(result.Token);
                Assert.True(DateTimeOffset.Now < result.ExpiresAt);
            }
        }
    }
}
