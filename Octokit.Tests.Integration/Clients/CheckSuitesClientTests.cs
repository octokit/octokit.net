using Octokit.Tests.Integration.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class CheckSuitesClientTests
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
            public async Task GetsCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Turn off auto creation of check suite for this repo
                    var preference = new AutoTriggerChecksObject(new[] { new CheckSuitePreference(Helper.GitHubAppId, false) });
                    var response = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check suite for the feature branch
                    var newCheckSuite = new NewCheckSuite(featureBranch.Object.Sha)
                    {
                        HeadBranch = "my-feature"
                    };
                    var checkSuite = await _githubAppInstallation.Check.Suite.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckSuite);

                    // Get Check Suite
                    var result = await _github.Check.Suite.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, checkSuite.Id);

                    // Check result
                    Assert.Equal(checkSuite.Id, result.Id);
                    Assert.Equal(newCheckSuite.HeadSha, result.HeadSha);
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
            public async Task CreatesCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Turn off auto creation of check suite for this repo
                    var preference = new AutoTriggerChecksObject(new[] { new CheckSuitePreference(Helper.GitHubAppId, false) });
                    var response = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check suite for the feature branch
                    var newCheckSuite = new NewCheckSuite(featureBranch.Object.Sha)
                    {
                        HeadBranch = "my-feature"
                    };
                    var result = await _githubAppInstallation.Check.Suite.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckSuite);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }
        }
    }
}
