using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

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
            _repository = github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
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
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            var releases = await _releaseClient.GetAll(_repositoryOwner, _repositoryName);

            Assert.True(releases.Count == 1);
            Assert.False(releases.First().PublishedAt.HasValue);
        }

        public void Dispose()
        {
            Helper.DeleteRepo(_repository);
        }
    }

    public class TheEditMethod : IDisposable
    {
        readonly IReleasesClient _releaseClient;
        readonly Repository _repository;
        readonly string _repositoryOwner;
        readonly string _repositoryName;
        readonly GitHubClient github;

        public TheEditMethod()
        {
            github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            _releaseClient = github.Release;

            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = github.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
            _repositoryOwner = _repository.Owner.Login;
            _repositoryName = _repository.Name;
        }

        [IntegrationTest]
        public async Task CanChangeBodyOfRelease()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            var editRelease = release.ToUpdate();
            editRelease.Body = "**This is an updated release";
            editRelease.Draft = false;

            var updatedRelease = await _releaseClient.Edit(_repositoryOwner, _repositoryName, release.Id, editRelease);

            Assert.Equal(release.Id, updatedRelease.Id);
            Assert.False(updatedRelease.Draft);
            Assert.Equal("**This is an updated release", updatedRelease.Body);
        }


        [IntegrationTest]
        public async Task CanChangeCommitIshOfRelease()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            Assert.Equal("master", release.TargetCommitish);

            var newHead = await github.CreateTheWorld(_repository);

            var editRelease = release.ToUpdate();
            editRelease.Draft = false;
            editRelease.TargetCommitish = newHead.Object.Sha;

            var updatedRelease = await _releaseClient.Edit(_repositoryOwner, _repositoryName, release.Id, editRelease);

            Assert.Equal(release.Id, updatedRelease.Id);
            Assert.False(updatedRelease.Draft);
            Assert.Equal(newHead.Object.Sha, updatedRelease.TargetCommitish);
        }

        public void Dispose()
        {
            Helper.DeleteRepo(_repository);
        }
    }

    public class TheUploadAssetMethod : IDisposable
    {
        readonly IReleasesClient _releaseClient;
        readonly Repository _repository;
        readonly string _repositoryOwner;
        readonly string _repositoryName;
        readonly GitHubClient _github;

        public TheUploadAssetMethod()
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
        public async Task CanUploadAndRetrieveAnAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload
            {
                ContentType = "text/plain", FileName = "hello-world.txt", RawData = stream
            };

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var assets = await _releaseClient.GetAssets(_repositoryOwner, _repositoryName, release.Id);

            Assert.Equal(1, assets.Count);
            var asset = assets[0];
            Assert.Equal(result.Id, asset.Id);
            Assert.NotNull(asset.Url);
        }

        [IntegrationTest]
        public async Task CanEditAnAssetLabel()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload
            {
                ContentType = "text/plain",
                FileName = "hello-world.txt",
                RawData = stream
            };

            var result = await _releaseClient.UploadAsset(release, newAsset);
            var asset = await _releaseClient.GetAsset(_repositoryOwner, _repositoryName, result.Id);

            var updateAsset = asset.ToUpdate();
            updateAsset.Label = "some other thing";

            var updatedAsset = await _releaseClient.EditAsset(_repositoryOwner, _repositoryName, result.Id, updateAsset);

            Assert.Equal("some other thing", updatedAsset.Label);
        }

        [IntegrationTest]
        public async Task CanDownloadAnAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload
            {
                ContentType = "text/plain",
                FileName = "hello-world.txt",
                RawData = stream
            };

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var asset = await _releaseClient.GetAsset(_repositoryOwner, _repositoryName, result.Id);

            Assert.Equal(result.Id, asset.Id);

            var response = await _github.Connection.Get<object>(new Uri(asset.Url), new Dictionary<string, string>(), "application/octet-stream");

            Assert.Equal("This is a plain text file.", response.Body);
        }

        public void Dispose()
        {
            Helper.DeleteRepo(_repository);
        }
    }
}
