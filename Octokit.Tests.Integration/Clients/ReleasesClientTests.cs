﻿using System;
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
            readonly GitHubClient _github;

            public TheGetReleasesMethod()
            {
                _github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };
                _releaseClient = _github.Release;

                var repoName = Helper.MakeNameWithTimestamp("public-repo");
                _repository = _github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
                _repositoryOwner = _repository.Owner.Login;
                _repositoryName = _repository.Name;
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
                // create a release without a publish date
                var releaseWithNoUpdate = new ReleaseUpdate("0.1") { Draft = true };
                var release = _releaseClient.CreateRelease(_repositoryOwner, _repositoryName, releaseWithNoUpdate).Result;

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
