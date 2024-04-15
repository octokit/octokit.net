using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class CheckRunsClientTests
    {
        public class TheCreateMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

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
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var result = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }

            [GitHubAppsTest]
            public async Task CreatesCheckRunWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var result = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                }
            }
        }

        public class TheUpdateMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

            public TheUpdateMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task UpdatesCheckRun()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var checkRun = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Update the check run
                    var update = new CheckRunUpdate
                    {
                        Name = "new-name",
                        Status = CheckStatus.InProgress
                    };
                    var result = await _githubAppInstallation.Check.Run.Update(repoContext.RepositoryOwner, repoContext.RepositoryName, checkRun.Id, update);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                    Assert.Equal("new-name", result.Name);
                    Assert.Equal(CheckStatus.InProgress, result.Status);
                }
            }

            [GitHubAppsTest]
            public async Task UpdatesCheckRunWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.Queued
                    };
                    var checkRun = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Update the check run
                    var update = new CheckRunUpdate
                    {
                        Name = "new-name",
                        Status = CheckStatus.InProgress
                    };
                    var result = await _githubAppInstallation.Check.Run.Update(repoContext.RepositoryId, checkRun.Id, update);

                    // Check result
                    Assert.NotNull(result);
                    Assert.Equal(featureBranch.Object.Sha, result.HeadSha);
                    Assert.Equal("new-name", result.Name);
                    Assert.Equal(CheckStatus.InProgress, result.Status);
                }
            }
        }

        public class TheGetAllForReferenceMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

            public TheGetAllForReferenceMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckRuns()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Get the check
                    var request = new CheckRunRequest
                    {
                        CheckName = "name",
                        Status = CheckStatusFilter.InProgress
                    };
                    var checkRuns = await _githubAppInstallation.Check.Run.GetAllForReference(repoContext.RepositoryOwner, repoContext.RepositoryName, featureBranch.Object.Sha, request);

                    // Check result
                    Assert.NotEmpty(checkRuns.CheckRuns);
                    foreach (var checkRun in checkRuns.CheckRuns)
                    {
                        Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                        Assert.Equal("name", checkRun.Name);
                        Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    }
                }
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckRunsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Get the check
                    var request = new CheckRunRequest
                    {
                        CheckName = "name",
                        Status = CheckStatusFilter.InProgress
                    };
                    var checkRuns = await _githubAppInstallation.Check.Run.GetAllForReference(repoContext.RepositoryId, featureBranch.Object.Sha, request);

                    // Check result
                    Assert.NotEmpty(checkRuns.CheckRuns);
                    foreach (var checkRun in checkRuns.CheckRuns)
                    {
                        Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                        Assert.Equal("name", checkRun.Name);
                        Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    }
                }
            }
        }

        public class TheGetAllForCheckSuiteMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

            public TheGetAllForCheckSuiteMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckRuns()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Get the check
                    var request = new CheckRunRequest
                    {
                        CheckName = "name",
                        Status = CheckStatusFilter.InProgress
                    };

                    var checkRuns = await _githubAppInstallation.Check.Run.GetAllForCheckSuite(repoContext.RepositoryOwner, repoContext.RepositoryName, created.CheckSuite.Id, request);

                    // Check result
                    Assert.NotEmpty(checkRuns.CheckRuns);
                    foreach (var checkRun in checkRuns.CheckRuns)
                    {
                        Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                        Assert.Equal("name", checkRun.Name);
                        Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    }
                }
            }

            [GitHubAppsTest]
            public async Task GetsAllCheckRunsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Get the check
                    var request = new CheckRunRequest
                    {
                        CheckName = "name",
                        Status = CheckStatusFilter.InProgress
                    };
                    var checkRuns = await _githubAppInstallation.Check.Run.GetAllForCheckSuite(repoContext.RepositoryId, created.CheckSuite.Id, request);

                    // Check result
                    Assert.NotEmpty(checkRuns.CheckRuns);
                    foreach (var checkRun in checkRuns.CheckRuns)
                    {
                        Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                        Assert.Equal("name", checkRun.Name);
                        Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    }
                }
            }
        }

        public class TheGetMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

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
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Get the check
                    var checkRun = await _githubAppInstallation.Check.Run.Get(repoContext.RepositoryOwner, repoContext.RepositoryName, created.Id);

                    // Check result
                    Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                    Assert.Equal("name", checkRun.Name);
                    Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    Assert.Equal(created.DetailsUrl, checkRun.DetailsUrl);
                }
            }

            [GitHubAppsTest]
            public async Task GetsCheckRunWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Get the check
                    var checkRun = await _githubAppInstallation.Check.Run.Get(repoContext.RepositoryId, created.Id);

                    // Check result
                    Assert.Equal(featureBranch.Object.Sha, checkRun.HeadSha);
                    Assert.Equal("name", checkRun.Name);
                    Assert.Equal(CheckStatus.InProgress, checkRun.Status);
                    Assert.Equal(created.DetailsUrl, checkRun.DetailsUrl);
                }
            }
        }

        public class TheGetAllAnnotationsMethod
        {
            readonly IGitHubClient _github;
            readonly IGitHubClient _githubAppInstallation;

            public TheGetAllAnnotationsMethod()
            {
                _github = Helper.GetAuthenticatedClient();

                // Authenticate as a GitHubApp Installation
                _githubAppInstallation = Helper.GetAuthenticatedGitHubAppInstallationForOwner(Helper.UserName);
            }

            [GitHubAppsTest]
            public async Task GetsAllAnnotations()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress,
                        Output = new NewCheckRunOutput("title", "summary")
                        {
                            Annotations = new[]
                            {
                                new NewCheckRunAnnotation("file.txt", 1, 1, CheckAnnotationLevel.Warning, "this is a warning")
                            }
                        }
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryOwner, repoContext.RepositoryName, newCheckRun);

                    // Get the annotations
                    var annotations = await _githubAppInstallation.Check.Run.GetAllAnnotations(repoContext.RepositoryOwner, repoContext.RepositoryName, created.Id);

                    // Check result
                    Assert.Single(annotations);
                    Assert.Equal("this is a warning", annotations.First().Message);
                    Assert.Equal(CheckAnnotationLevel.Warning, annotations.First().AnnotationLevel);
                }
            }

            [GitHubAppsTest]
            public async Task GetsAllAnnotationsWithRepositoryId()
            {
                using (var repoContext = await _github.CreateRepositoryContext(new NewRepository(Helper.MakeNameWithTimestamp("public-repo")) { AutoInit = true }))
                {
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, repoContext.RepositoryDefaultBranch);
                    var featureBranch = await Helper.CreateFeatureBranch(repoContext.RepositoryOwner, repoContext.RepositoryName, headCommit.Sha, "my-feature");

                    // Create a check run for the feature branch
                    var newCheckRun = new NewCheckRun("name", featureBranch.Object.Sha)
                    {
                        Status = CheckStatus.InProgress,
                        Output = new NewCheckRunOutput("title", "summary")
                        {
                            Annotations = new[]
                            {
                                new NewCheckRunAnnotation("file.txt", 1, 1, CheckAnnotationLevel.Warning, "this is a warning")
                            }
                        }
                    };
                    var created = await _githubAppInstallation.Check.Run.Create(repoContext.RepositoryId, newCheckRun);

                    // Get the annotations
                    var annotations = await _githubAppInstallation.Check.Run.GetAllAnnotations(repoContext.RepositoryId, created.Id);

                    // Check result
                    Assert.Single(annotations);
                    Assert.Equal("this is a warning", annotations.First().Message);
                    Assert.Equal(CheckAnnotationLevel.Warning, annotations.First().AnnotationLevel);
                }
            }
        }
    }
}