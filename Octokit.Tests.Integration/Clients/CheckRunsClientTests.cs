using Octokit.Tests.Integration.Helpers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class CheckRunsClientTests
    {
        public class TheGetMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheGetMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsCheckRun()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a Check Run
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var newCheckRun = new NewCheckRun("checks", "master", headCommit.Sha);
                    var checkRun = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Get Check Run
                    var result = await _github.Check.Run.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, checkRun.Id);

                    // Check result
                    Assert.Equal(checkRun.Id, result.Id);
                    Assert.Equal(newCheckRun.HeadSha, result.HeadSha);
                }
            }
        }

        public class TheCreateMethod
        {
            IGitHubClient _github;
            IGitHubClient _githubAppInstallation;

            public TheCreateMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task CreatesCheckRun()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");

                    var newCheckRun = new NewCheckRun("checks", "master", headCommit.Sha);

                    var result = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);
                    
                    Assert.NotNull(result);
                    Assert.Equal(headCommit.Sha, result.HeadSha);
                }
            }
        }
    }
}
