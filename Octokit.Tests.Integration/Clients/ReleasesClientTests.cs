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
    public class TheCtor
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ReleasesClient(null));
        }
    }

    public class TheCreateReleasesMethod : IDisposable
    {
        private readonly IReleasesClient _releaseClient;
        private readonly RepositoryContext _context;

        public TheCreateReleasesMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _releaseClient = github.Repository.Release;

            _context = github.CreateRepositoryContext("public-repo").Result;
        }

        [Fact]
        public async Task SendsCreateToCorrectUrl()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };

            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            Assert.NotNull(release);
        }

        [Fact]
        public async Task SendsCreateToCorrectUrlWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };

            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            Assert.NotNull(release);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

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
        public async Task ReturnsAuthorWithRepositoryId()
        {
            var release = await _releaseClient.Get(252774, 2276624);

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
        public async Task ReturnsAssetsWithRepositoryId()
        {
            var release = await _releaseClient.Get(252774, 2276624);

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
            Assert.Contains(releases, release => release.TagName == "v0.18.0");
        }

        [IntegrationTest]
        public async Task ReturnsReleasesWithRepositoryId()
        {
            var releases = await _releaseClient.GetAll(252774);

            Assert.True(releases.Count > 5);
            Assert.Contains(releases, release => release.TagName == "v0.18.0");
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

        [IntegrationTest]
        public async Task ReturnsReleasesWithNullPublishDateWithRepositoryDate()
        {
            // create a release without a publish date
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var releases = await _releaseClient.GetAll(_context.Repository.Id);

            Assert.True(releases.Count == 1);
            Assert.False(releases.First().PublishedAt.HasValue);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheGetMethod
    {
        private readonly IReleasesClient _releaseClient;
        private readonly IGitHubClient _client;

        public TheGetMethod()
        {
            _client = Helper.GetAuthenticatedClient();
            _releaseClient = _client.Repository.Release;
        }

        [IntegrationTest]
        public async Task ReturnsReleaseByTag()
        {
            var releaseByTag = await _releaseClient.Get("octokit", "octokit.net", "v0.28.0");

            Assert.Equal(8396883, releaseByTag.Id);
            Assert.Equal("v0.28 - Get to the Chopper!!!", releaseByTag.Name);
            Assert.Equal("v0.28.0", releaseByTag.TagName);
        }

        [IntegrationTest]
        public async Task ReturnsReleaseWithRepositoryIdByTag()
        {
            var releaseByTag = await _releaseClient.Get(7528679, "v0.28.0");

            Assert.Equal(8396883, releaseByTag.Id);
            Assert.Equal("v0.28 - Get to the Chopper!!!", releaseByTag.Name);
            Assert.Equal("v0.28.0", releaseByTag.TagName);
        }

        [IntegrationTest]
        public async Task ThrowsWhenTagNotFound()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _releaseClient.Get("octokit", "octokit.net", "0.0"));
        }

        [IntegrationTest]
        public async Task ThrowsWhenTagNotFoundWithRepositoryId()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _releaseClient.Get(7528679, "0.0"));
        }
    }

    public class TheGetAllMethod
    {
        readonly IReleasesClient _releaseClient;
        const string owner = "octokit";
        const string name = "octokit.net";
        const long repositoryId = 252774;

        public TheGetAllMethod()
        {
            var github = Helper.GetAuthenticatedClient();
            _releaseClient = github.Repository.Release;
        }

        [IntegrationTest]
        public async Task ReturnsReleases()
        {
            var releases = await _releaseClient.GetAll(owner, name);

            Assert.NotEmpty(releases);
        }

        [IntegrationTest]
        public async Task ReturnsReleasesWithRepositoryId()
        {
            var releases = await _releaseClient.GetAll(repositoryId);

            Assert.NotEmpty(releases);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var releases = await _releaseClient.GetAll(owner, name, options);

            Assert.Equal(5, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithoutStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var releases = await _releaseClient.GetAll(repositoryId, options);

            Assert.Equal(5, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var releases = await _releaseClient.GetAll(owner, name, options);

            Assert.Equal(5, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var releases = await _releaseClient.GetAll(repositoryId, options);

            Assert.Equal(5, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPage = await _releaseClient.GetAll(owner, name, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _releaseClient.GetAll(owner, name, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
            Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
            Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPage = await _releaseClient.GetAll(repositoryId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _releaseClient.GetAll(repositoryId, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
            Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
            Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
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
        public async Task CanChangeBodyOfReleaseWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var editRelease = release.ToUpdate();
            editRelease.Body = "**This is an updated release";
            editRelease.Draft = false;

            var updatedRelease = await _releaseClient.Edit(_context.Repository.Id, release.Id, editRelease);

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

        [IntegrationTest]
        public async Task CanChangeCommitIshOfReleaseWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            Assert.Equal("master", release.TargetCommitish);

            var newHead = await _github.CreateTheWorld(_context.Repository);

            var editRelease = release.ToUpdate();
            editRelease.Draft = false;
            editRelease.TargetCommitish = newHead.Object.Sha;

            var updatedRelease = await _releaseClient.Edit(_context.Repository.Id, release.Id, editRelease);

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
        const string owner = "octokit";
        const string name = "octokit.net";
        const int releaseId = 2248679;
        const long repositoryId = 7528679;

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
        public async Task CanUploadAndRetrieveAnAssetWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var assets = await _releaseClient.GetAllAssets(_context.Repository.Id, release.Id);

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
        public async Task CanEditAnAssetLabelWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);
            var asset = await _releaseClient.GetAsset(_context.Repository.Id, result.Id);

            var updateAsset = asset.ToUpdate();
            updateAsset.Label = "some other thing";

            var updatedAsset = await _releaseClient.EditAsset(_context.Repository.Id, result.Id, updateAsset);

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
        public async Task CanDownloadAnAssetWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);

            Assert.True(result.Id > 0);

            var asset = await _releaseClient.GetAsset(_context.Repository.Id, result.Id);

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

            string textContent;

            using (var zipstream = new MemoryStream((byte[])response.Body))
            using (var archive = new ZipArchive(zipstream))
            {
                var entry = archive.Entries[0];
                var data = new byte[entry.Length];
                await entry.Open().ReadAsync(data, 0, data.Length);
                textContent = Encoding.ASCII.GetString(data);
            }

            Assert.Contains("This is a plain text file.", textContent);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1
            };

            var releases = await _releaseClient.GetAllAssets(owner, name, releaseId, options);

            Assert.Equal(2, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithoutStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 2,
                PageCount = 1
            };

            var releases = await _releaseClient.GetAllAssets(repositoryId, releaseId, options);

            Assert.Equal(2, releases.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var assets = await _releaseClient.GetAllAssets(owner, name, releaseId, options);

            Assert.Equal(1, assets.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReleasesWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var assets = await _releaseClient.GetAllAssets(repositoryId, releaseId, options);

            Assert.Equal(1, assets.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _releaseClient.GetAllAssets(owner, name, releaseId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _releaseClient.GetAllAssets(owner, name, releaseId, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _releaseClient.GetAllAssets(repositoryId, releaseId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _releaseClient.GetAllAssets(repositoryId, releaseId, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
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
        public async Task ReturnsLatestReleaseWithRepositoryId()
        {
            var lastReleaseFromGetAll = (await _releaseClient.GetAll(252774)).OrderBy(r => r.CreatedAt).Last();
            var lastRelease = await _releaseClient.GetLatest(252774);

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

        [IntegrationTest]
        public async Task NoReleaseOnRepoWithRepositoryId()
        {
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            var repository = await _client.Repository.Create(new NewRepository(repoName));

            await Assert.ThrowsAsync<NotFoundException>(() => _releaseClient.GetLatest(repository.Id));

            await _client.Repository.Delete(Helper.UserName, repoName);
        }
    }

    public class TheDeleteAssetsMethod
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;
        readonly IReleasesClient _releaseClient;

        public TheDeleteAssetsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _releaseClient = _github.Repository.Release;

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanDeleteAsset()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);
            var asset = await _releaseClient.GetAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.NotNull(asset);

            await _releaseClient.DeleteAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _releaseClient.GetAsset(_context.RepositoryOwner, _context.RepositoryName, result.Id));
        }

        [IntegrationTest]
        public async Task CanDeleteAssetWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var stream = Helper.LoadFixture("hello-world.txt");

            var newAsset = new ReleaseAssetUpload("hello-world.txt", "text/plain", stream, null);

            var result = await _releaseClient.UploadAsset(release, newAsset);
            var asset = await _releaseClient.GetAsset(_context.Repository.Id, result.Id);

            Assert.NotNull(asset);

            await _releaseClient.DeleteAsset(_context.Repository.Id, result.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _releaseClient.GetAsset(_context.Repository.Id, result.Id));
        }
    }

    public class TheDeleteMethod
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;
        readonly IReleasesClient _releaseClient;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _releaseClient = _github.Repository.Release;

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanDelete()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.RepositoryOwner, _context.RepositoryName, releaseWithNoUpdate);

            var createdRelease = await _releaseClient.Get(_context.RepositoryOwner, _context.RepositoryName, release.Id);

            Assert.NotNull(createdRelease);

            await _releaseClient.Delete(_context.RepositoryOwner, _context.RepositoryName, createdRelease.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _releaseClient.Get(_context.RepositoryOwner, _context.RepositoryName, createdRelease.Id));
        }

        [IntegrationTest]
        public async Task CanDeleteWithRepositoryId()
        {
            var releaseWithNoUpdate = new NewRelease("0.1") { Draft = true };
            var release = await _releaseClient.Create(_context.Repository.Id, releaseWithNoUpdate);

            var createdRelease = await _releaseClient.Get(_context.Repository.Id, release.Id);

            Assert.NotNull(createdRelease);

            await _releaseClient.Delete(_context.Repository.Id, createdRelease.Id);

            await Assert.ThrowsAsync<NotFoundException>(async () => await _releaseClient.Get(_context.Repository.Id, createdRelease.Id));
        }
    }
}
