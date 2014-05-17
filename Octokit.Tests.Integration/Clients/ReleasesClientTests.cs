using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

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
            var releaseWithNoUpdate = new ReleaseUpdate("0.1") { Draft = true };
            var release = await _releaseClient.CreateRelease(_repositoryOwner, _repositoryName, releaseWithNoUpdate);

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

        public void Dispose()
        {
            Helper.DeleteRepo(_repository);
        }
    }
}
