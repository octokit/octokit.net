using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ObservableGitHubAppInstallationsClientTests
    {
        public class TheGetAllRepositoriesForCurrentMethod
        {
            IObservableGitHubClient _github;

            public TheGetAllRepositoriesForCurrentMethod()
            {
                // Authenticate as a GitHubApp Installation
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task GetsAllRepositories()
            {
                var result = await _github.GitHubApps.Installation.GetAllRepositoriesForCurrent();

                Assert.NotNull(result);
                Assert.True(result.TotalCount > 0);
            }
        }

        public class TheGetAllRepositoriesForCurrentUserMethod
        {
            IObservableGitHubClient _github;

            public TheGetAllRepositoriesForCurrentUserMethod()
            {
                // Need to Authenticate as User to Server but not possible without receiving redirect from github.com
                //_github = new ObservableGitHubClient(Helper.GetAuthenticatedUserToServer());
                _github = null;
            }

            [GitHubAppsTest(Skip = "Not possible to authenticate with User to Server auth")]
            public async Task GetsAllRepositories()
            {
                var installationId = Helper.GetGitHubAppInstallationForOwner(Helper.UserName).Id;

                var result = await _github.GitHubApps.Installation.GetAllRepositoriesForCurrentUser(installationId);

                Assert.NotNull(result);
                Assert.True(result.TotalCount > 0);
            }
        }
    }
}