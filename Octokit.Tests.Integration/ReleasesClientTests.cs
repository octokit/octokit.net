using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ReleasesClientTests
    {
        public class TheGetReleasesMethod : IDisposable
        {
            readonly IReleasesClient _releaseClient;
            readonly Repository _repository;
            readonly string _repositoryOwner;
            readonly string _repositoryName;

            public TheGetReleasesMethod()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };
                _releaseClient = github.Release;

                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                _repository = github.Repository.Create(new NewRepository { Name = repoName }).Result;
                _repositoryOwner = _repository.Owner.Login;
                _repositoryName = _repository.Name;

                // TODO: create test blob
                // TODO: create test tree
                // TODO: create test commit
                // TODO: update master reference to latest commit

                var releaseWithNoUpdate = new ReleaseUpdate("0.1");
                var release = github.Release.CreateRelease(_repositoryOwner, _repositoryName, releaseWithNoUpdate).Result;
            }

            [IntegrationTest]
            public async Task ReturnsReleases()
            {
                var releases = await _releaseClient.GetAll("git-tfs", "git-tfs");

                Assert.True(releases.Count > 5);
                Assert.True(releases.Any(release => release.TagName == "v0.18.0"));
            }

            [IntegrationTest]
            public async Task ReturnsReleasesWithNullPublishDate()
            {
                var releases = await _releaseClient.GetAll(_repositoryOwner, _repositoryName);

                Assert.True(releases.Count == 1);
                Assert.False(releases.First().PublishedAt.HasValue);
            }

            public void Dispose()
            {
                Helper.DeleteRepo(_repository);
            }
        }
    }
}
