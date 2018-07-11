﻿using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class CheckRunsClientTests
    {
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
                    // Create a new feature branch
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
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
                    var headCommit = await _github.Repository.Commit.Get(repoContext.RepositoryId, "master");
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
    }
}