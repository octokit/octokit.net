using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableCheckSuitesClientTests
    {
        public class TheGetMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheGetMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task GetsCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Need to get a CheckSuiteId so we can test the Get method
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, "master");
                    var checkSuite = (await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha)).CheckSuites.First();

                    // Get Check Suite by Id
                    var result = await _github.Check.Suite.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, checkSuite.Id);

                    // Check result
                    Assert.Equal(checkSuite.Id, result.Id);
                    Assert.Equal(headCommit.Sha, result.HeadSha);
                }
            }

            [GitHubAppsTest]
            public async Task GetsCheckSuiteWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Need to get a CheckSuiteId so we can test the Get method
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var checkSuite = (await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryId, headCommit.Sha)).CheckSuites.First();

                    // Get Check Suite by Id
                    var result = await _github.Check.Suite.Get(repoContext.RepositoryId, checkSuite.Id);

                    // Check result
                    Assert.Equal(checkSuite.Id, result.Id);
                    Assert.Equal(headCommit.Sha, result.HeadSha);
                }
            }
        }

        public class TheGetAllForReferenceMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheGetAllForReferenceMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckSuites()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, "master");

                    var checkSuites = await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha);

                    Assert.NotEmpty(checkSuites.CheckSuites);
                    foreach (var checkSuite in checkSuites.CheckSuites)
                    {
                        Assert.Equal(headCommit.Sha, checkSuite.HeadSha);
                    }
                }
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckSuitesWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");

                    var checkSuites = await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryId, headCommit.Sha);

                    Assert.NotEmpty(checkSuites.CheckSuites);
                    foreach (var checkSuite in checkSuites.CheckSuites)
                    {
                        Assert.Equal(headCommit.Sha, checkSuite.HeadSha);
                    }
                }
            }
        }

        public class TheUpdatePreferencesMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheUpdatePreferencesMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task UpdatesPreferences()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var preference = new CheckSuitePreferences(new[]
                    {
                        new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false)
                    });
                    var result = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    Assert.Equal(repoContext.RepositoryId, result.Repository.Id);
                    Assert.Equal(Helper.GitHubAppId, result.Preferences.AutoTriggerChecks[0].AppId);
                    Assert.False(result.Preferences.AutoTriggerChecks[0].Setting);
                }
            }

            [GitHubAppsTest]
            public async Task UpdatesPreferencesWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    var preference = new CheckSuitePreferences(new[]
                    {
                        new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false)
                    });
                    var result = await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryId, preference);

                    Assert.Equal(repoContext.RepositoryId, result.Repository.Id);
                    Assert.Equal(Helper.GitHubAppId, result.Preferences.AutoTriggerChecks[0].AppId);
                    Assert.False(result.Preferences.AutoTriggerChecks[0].Setting);
                }
            }
        }

        public class TheCreateMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheCreateMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task CreatesCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Turn off auto creation of check suite for this repo
                    var preference = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false) });
                    await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryOwner, repoContext.RepositoryName, preference);

                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check suite for the feature branch
                    var newCheckSuite = new NewCheckSuite(featureBranch.Object.Sha);
                    var result = await _githubAppInstallation.Check.Suite.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckSuite);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }

            [GitHubAppsTest]
            public async Task CreatesCheckSuiteWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Turn off auto creation of check suite for this repo
                    var preference = new CheckSuitePreferences(new[] { new CheckSuitePreferenceAutoTrigger(Helper.GitHubAppId, false) });
                    await _githubAppInstallation.Check.Suite.UpdatePreferences(repoContext.RepositoryId, preference);

                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check suite for the feature branch
                    var newCheckSuite = new NewCheckSuite(featureBranch.Object.Sha);
                    var result = await _githubAppInstallation.Check.Suite.Create(repoContext.RepositoryId, newCheckSuite);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }
        }

        public class TheRerequestMethod
        {
            IObservableGitHubClient _github;
            IObservableGitHubClient _githubAppInstallation;

            public TheRerequestMethod()
            {
                _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = new ObservableGitHubClient(Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName));
            }

            [GitHubAppsTest]
            public async Task RerequestsCheckSuite()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Need to get a CheckSuiteId so we can test the Get method
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var checkSuite = (await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryId, headCommit.Sha)).CheckSuites.First();

                    var result = await _githubAppInstallation.Check.Suite.Rerequest(repoContext.RepositoryOwner, repoContext.RepositoryName, checkSuite.Id);

                    Assert.True(result);
                }
            }

            [GitHubAppsTest]
            public async Task RerequestsCheckSuiteWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Need to get a CheckSuiteId so we can test the Get method
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
                    var checkSuite = (await _githubAppInstallation.Check.Suite.GetAllForReference(repoContext.RepositoryId, headCommit.Sha)).CheckSuites.First();

                    var result = await _githubAppInstallation.Check.Suite.Rerequest(repoContext.RepositoryId, checkSuite.Id);

                    Assert.True(result);
                }
            }
        }
    }
}
