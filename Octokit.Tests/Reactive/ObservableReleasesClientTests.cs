using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableReleasesClientTests
    {
        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableReleasesClient(null));
            }
        }

        public class TheGetReleasesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAll("fake", "repo");

                gitHubClient.Connection.GetAsync<IReadOnlyList<Release>>(
                    new Uri("repos/fake/repo/releases", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll("owner", null));
            }
        }

        public class TheGetReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Get("fake", "repo", 1);

                gitHubClient.Connection.GetAsync<IReadOnlyList<Release>>(
                    new Uri("repos/fake/repo/releases/1", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.Get(null, "name", 1));
                AssertEx.Throws<ArgumentException>(async () => await releasesClient.Get("", "name", 1));
                AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.Get("owner", null, 1));
                AssertEx.Throws<ArgumentException>(async () => await releasesClient.Get("owner", "", 1));
            }
        }

        public class TheCreateReleaseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.CreateRelease("fake", "repo", data);

                gitHubClient.Connection.PostAsync<Release>(
                    new Uri("repos/fake/repo/releases", UriKind.Relative), data, null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var data = new ReleaseUpdate("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new ReleaseUpdate(null));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.CreateRelease(null, "name", data));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.CreateRelease("owner", null, data));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.CreateRelease("owner", "name", null));
            }
        }

        public class TheEditReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.EditRelease("fake", "repo", data);

                gitHubClient.Connection.PatchAsync<Release>(
                    new Uri("repos/fake/repo/releases", UriKind.Relative), data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.EditRelease(null, "name", new ReleaseUpdate("tag")));
                AssertEx.Throws<ArgumentException>(async () => await releasesClient.EditRelease("", "name", new ReleaseUpdate("tag")));
                AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.EditRelease("owner", null, new ReleaseUpdate("tag")));
                AssertEx.Throws<ArgumentException>(async () => await releasesClient.EditRelease("owner", "", new ReleaseUpdate("tag")));
                AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.EditRelease("owner", "name", null));
            }
        }

        public class TheDeleteReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.DeleteRelease("fake", "repo", 1);

                gitHubClient.Connection.DeleteAsync(
                    new Uri("repos/fake/repo/releases/1", UriKind.Relative));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteRelease(null, "name", 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteRelease("", "name", 1));
                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteRelease("owner", null, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteRelease("owner", "", 1));
            }
        }

        public class TheGetAssetsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAssets("fake", "repo", 1);

                gitHubClient.Connection.GetAsync<IReadOnlyList<ReleaseAsset>>(
                    new Uri("repos/fake/repo/releases/1/assets", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAssets(null, "name", 1));
                AssertEx.Throws<ArgumentException>(async () => await client.GetAssets("", "name", 1));
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAssets("owner", null, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.GetAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void UploadsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var release = new Release { UploadUrl = "https://uploads.test.dev/does/not/matter/releases/1/assets{?name}" };
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload { FileName = "example.zip", ContentType = "application/zip", RawData = rawData };

                releasesClient.UploadAsset(release, upload);

                gitHubClient.Connection.PostAsync<ReleaseAsset>(
                    new Uri("https://uploads.test.dev/does/not/matter/releases/1/assets?name=example.zip", UriKind.Absolute),
                    rawData,
                    "application/vnd.github.v3",
                    Arg.Is<string>(contentType => contentType == "application/zip"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                var release = new Release { UploadUrl = "https://uploads.github.com/anything" };
                var uploadData = new ReleaseAssetUpload { FileName = "good", ContentType = "good/good", RawData = Stream.Null };
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.UploadAsset(null, uploadData));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.UploadAsset(release, null));
            }
        }

        public class TheGetAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAsset("fake", "repo", 1, 1);

                gitHubClient.Connection.GetAsync<ReleaseAsset>(
                    new Uri("repos/fake/repo/releases/1/assets/1", UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAsset(null, "name", 1, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.GetAsset("", "name", 1, 1));
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAsset("owner", null, 1, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.GetAsset("owner", "", 1, 1));
            }
        }

        public class TheEditAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseAssetUpdate("asset");

                client.EditAsset("fake", "repo", 1, 1, data);

                gitHubClient.Connection.PatchAsync<ReleaseAsset>(
                    new Uri("repos/fake/repo/releases/1/assets/1", UriKind.Relative),
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.EditAsset(null, "name", 1, 1, new ReleaseAssetUpdate("name")));
                AssertEx.Throws<ArgumentException>(async () => await client.EditAsset("", "name", 1, 1, new ReleaseAssetUpdate("name")));
                AssertEx.Throws<ArgumentNullException>(async () => await client.EditAsset("owner", null, 1, 1, new ReleaseAssetUpdate("name")));
                AssertEx.Throws<ArgumentException>(async () => await client.EditAsset("owner", "", 1, 1, new ReleaseAssetUpdate("name")));
                AssertEx.Throws<ArgumentNullException>(async () => await client.EditAsset("owner", "name", 1, 1, null));
            }
        }

        public class TheDeleteAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.DeleteAsset("fake", "repo", 1);

                gitHubClient.Connection.DeleteAsync(
                    new Uri("repos/fake/repo/releases/assets/1", UriKind.Relative));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteAsset(null, "name", 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteAsset("", "name", 1));
                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteAsset("owner", null, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
