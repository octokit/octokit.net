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
            readonly IGitHubClient _github;

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
                Assert.Equal(Helper.GitHubAppSlug, result.Slug);
                Assert.False(string.IsNullOrEmpty(result.Name));
                Assert.NotNull(result.Owner);
            }
        }

        public class TheGetCurrentMethod
        {
            readonly IGitHubClient _github;

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
                Assert.Equal(Helper.GitHubAppSlug, result.Slug);
                Assert.False(string.IsNullOrEmpty(result.Name));
                Assert.NotNull(result.Owner);
            }
        }

        public class TheGetAllInstallationsForCurrentMethod
        {
            readonly IGitHubClient _github;

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
                    Assert.Equal(InstallationReadWritePermissionLevel.Read, installation.Permissions.Metadata);
                    Assert.False(string.IsNullOrEmpty(installation.HtmlUrl));
                    Assert.NotEqual(0, installation.TargetId);
                    Assert.Null(installation.SuspendedBy);
                    Assert.Null(installation.SuspendedAt);
                }
            }
        }

        public class TheGetInstallationForCurrentMethod
        {
            readonly IGitHubClient _github;

            public TheGetInstallationForCurrentMethod()
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
                var result = await _github.GitHubApps.GetInstallationForCurrent(installationId);

                Assert.True(result.AppId == Helper.GitHubAppId);
                Assert.Equal(Helper.GitHubAppId, result.AppId);
                Assert.NotNull(result.Account);
                Assert.NotNull(result.Permissions);
                Assert.Equal(InstallationReadWritePermissionLevel.Read, result.Permissions.Metadata);
                Assert.False(string.IsNullOrEmpty(result.HtmlUrl));
                Assert.NotEqual(0, result.TargetId);
            }
        }

        public class TheGetAllInstallationsForCurrentUserMethod
        {
            readonly IGitHubClient _github;

            public TheGetAllInstallationsForCurrentUserMethod()
            {
                // Need to Authenticate as User to Server but not possible without receiving redirect from github.com
                //_github = Helper.GetAuthenticatedUserToServer();
                _github = null;
            }

            [GitHubAppsTest(Skip = "Not possible to authenticate with User to Server auth")]
            public async Task GetsAllInstallationsForCurrentUser()
            {
                var result = await _github.GitHubApps.GetAllInstallationsForCurrentUser();

                Assert.NotNull(result);
                Assert.True(result.TotalCount > 0);
            }
        }

        public class TheCreateInstallationTokenMethod
        {
            readonly IGitHubClient _github;

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

        public class TheGetOrganizationInstallationForCurrentMethod
        {
            readonly IGitHubClient _github;

            public TheGetOrganizationInstallationForCurrentMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task GetsOrganizationInstallations()
            {
                var result = await _github.GitHubApps.GetOrganizationInstallationForCurrent(Helper.Organization);

                Assert.NotNull(result);
            }
        }

        public class TheGetRepositoryInstallationForCurrentMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

            public TheGetRepositoryInstallationForCurrentMethod()
            {
                // Autheticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsRepositoryInstallations()
            {
                // Find a repo under the installation
                var repos = await _githubAppInstallation.GitHubApps.Installation.GetAllRepositoriesForCurrent();
                var repo = repos.Repositories.First();

                // Now, using the GitHub App auth, find this repository installation
                var result = await _github.GitHubApps.GetRepositoryInstallationForCurrent(repo.Owner.Login, repo.Name);

                Assert.NotNull(result);
            }

            [GitHubAppsTest]
            public async Task GetsRepositoryInstallationsWithRepositoryId()
            {
                // Find a repo under the installation
                var repos = await _githubAppInstallation.GitHubApps.Installation.GetAllRepositoriesForCurrent();
                var repo = repos.Repositories.First();

                // Now, using the GitHub App auth, find this repository installation
                var result = await _github.GitHubApps.GetRepositoryInstallationForCurrent(repo.Id);

                Assert.NotNull(result);
            }
        }

        public class TheGetUserInstallationForCurrentMethod
        {
            readonly IGitHubClient _github;

            public TheGetUserInstallationForCurrentMethod()
            {
                // Authenticate as a GitHubApp
                _github = Helper.GetAuthenticatedGitHubAppsClient();
            }

            [GitHubAppsTest]
            public async Task GetsUserInstallations()
            {
                var result = await _github.GitHubApps.GetUserInstallationForCurrent(Helper.UserName);

                Assert.NotNull(result);
            }
        }
    }
}
