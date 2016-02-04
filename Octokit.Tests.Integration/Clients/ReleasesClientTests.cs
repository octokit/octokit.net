using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ReleasesClientTests
{
    public class TheGetReleasesMethod : IDisposable
    {
        private readonly IReleasesClient _releaseClient;
        private readonly RepositoryContext _context;

        public TheGetReleasesMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _releaseClient = github.Repository.Release;

            _context = github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task ReturnsAuthor()
        {
            var release = await _releaseClient.Get("git-tfs", "git-tfs", 2276624);

            Assert.NotNull(release);
            Assert.NotNull(release.Author);
            Assert.Equal("spraints", release.Author.Login);
        }

        [IntegrationTest]
        public async Task ReturnsAssets()
        {
            var release = await _releaseClient.Get("git-tfs", "git-tfs", 2276624);

            Assert.NotNull(release);
            Assert.Equal(1, release.Assets.Count);
            Assert.Equal("GitTfs-0.24.1.zip", release.Assets.First().Name);
            Assert.Equal("https://api.github.com/repos/git-tfs/git-tfs/tarball/v0.24.1", release.TarballUrl);
            Assert.Equal("https://api.github.com/repos/git-tfs/git-tfs/zipball/v0.24.1", release.ZipballUrl);
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
            await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var releases = await _releaseClient.GetAll(_context.RepositoryOwner, _context.RepositoryName);

            Assert.True(releases.Count == 1);
            Assert.False(releases.First().PublishedAt.HasValue);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheEditMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly RepositoryContext _context;
        private readonly IReleasesClient _releaseClient;

        public TheEditMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _releaseClient = _github.Repository.Release;

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanChangeBodyOfRelease()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var editRelease = release.ToUpdate();
            editRelease.Body = "**This is an updated release";
            editRelease.Draft = false;

            var updatedRelease = await _releaseClient.Edit(_context.RepositoryOwner, _context.RepositoryName, release.Id, editRelease);

            Assert.Equal(release.Id, updatedRelease.Id);
            Assert.False(updatedRelease.Draft);
            Assert.Equal("**This is an updated release", updatedRelease.Body);
        }


        [IntegrationTest]
        public async Task CanChangeCommitIshOfRelease()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            Assert.Equal("master", release.TargetCommitish);

            var newHead = await _github.CreateTheWorld(_context.Repository);

            var editRelease = release.ToUpdate();
            editRelease.Draft = false;
            editRelease.TargetCommitish = newHead.Object.Sha;

            var updatedRelease = await _releaseClient.Edit(_context.RepositoryOwner, _context.RepositoryName, release.Id, editRelease);

            Assert.Equal(release.Id, updatedRelease.Id);
            Assert.False(updatedRelease.Draft);
            Assert.Equal(newHead.Object.Sha, updatedRelease.TargetCommitish);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheUploadAssetMethod : IDisposable
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;
        readonly IReleasesClient _releaseClient;

        public TheUploadAssetMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _releaseClient = _github.Repository.Release;

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanUploadAndRetrieveAnAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var assets = await _releaseClient.GetAllAssets(_context.RepositoryOwner, _context.RepositoryName, release.Id);

            Assert.Equal(1, assets.Count);
            var asset = assets[0];
            Assert.Equal(result.Id, asset.Id);
            Assert.NotNull(asset.Url);
            Assert.NotNull(asset.BrowserDownloadUrl);
        }

        [IntegrationTest]
        public async Task CanEditAnAssetLabel()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);
            var asset = await _releaseClient.GetAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            var updateAsset = asset.ToUpdate();
            updateAsset.Label = "some other thing";

            var updatedAsset = await _releaseClient.EditAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id, updateAsset);

            Assert.Equal("some other thing", updatedAsset.Label);
        }

        [IntegrationTest]
        public async Task CanDownloadAnAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var asset = await _releaseClient.GetAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.Equal(result.Id, asset.Id);

            var response = await _github.Connection.Get<object>(new Uri(asset.Url), new Dictionary<string, string>(), "application/octet-stream");

            Assert.Contains("This is a plain text file.", Encoding.ASCII.GetString((byte[])response.Body));
        }
        [IntegrationTest]
        public async Task CanDownloadBinaryAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.zip");

            var newAsset = new ReleaseAssetUpload("hello-world.zip"
                , "application/octet-stream"
                , stream
                , null);

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var asset = await _releaseClient.GetAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.Equal(result.Id, asset.Id);

            var response = await _github.Connection.Get<object>(new Uri(asset.Url), new Dictionary<string, string>(), "application/octet-stream");

            var textContent = string.Empty;

            using (var zipstream = new MemoryStream((byte[])response.Body))
            using (var archive = new ZipArchive(zipstream))
            {
                var enttry = archive.Entries[0];
                var data = new byte[enttry.Length];
                await enttry.Open().ReadAsync(data, 0, data.Length);
                textContent = Encoding.ASCII.GetString(data);
            }

            Assert.Contains("This is a plain text file.", textContent);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheGetLatestReleaseMethod
    {
        private readonly IReleasesClient _releaseClient;
        private readonly IGitHubClient _client;

        public TheGetLatestReleaseMethod()
        {
            _client = Helper.GetAuthenticatedClient();
            _releaseClient = _client.Repository.Release;
        }

        [IntegrationTest]
        public async Task ReturnsLatestRelease()
        {
            var lastReleaseFromGetAll = (await _releaseClient.GetAll("octokit", "octokit.net")).OrderBy(r => r.CreatedAt).Last();
            var lastRelease = await _releaseClient.GetLatest("octokit", "octokit.net");

            Assert.Equal(lastReleaseFromGetAll.Id, lastRelease.Id);
        }

        [IntegrationTest]
        public async Task NoReleaseOnRepo()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            await _client.Repository.Create(new NewRepository(repoName));

            await Assert.ThrowsAsync<NotFoundException>(() => _releaseClient.GetLatest(Helper.UserName, repoName));

            await _client.Repository.Delete(Helper.UserName, repoName);
        }
    }
}
