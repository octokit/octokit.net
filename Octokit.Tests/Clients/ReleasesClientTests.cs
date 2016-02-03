using System;
using System.IO;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReleasesClientTests
    {
        public class TheGetReleasesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);

                releasesClient.GetAll("fake", "repo");

                client.Received().GetAll<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    null,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll("owner", null));
            }
        }

        public class TheGetReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.Get("fake", "repo", 1);

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("owner", "", 1));
            }
        }

        public class TheGetLatestReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetLatest("fake", "repo");

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetLatest(null, "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetLatest("", "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetLatest("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetLatest("owner", ""));
            }
        }
        public class TheCreateReleaseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new NewRelease("fake-tag");

                releasesClient.Create("fake", "repo", data);

                client.Received().Post<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                var data = new NewRelease("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new NewRelease(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create(null, "name", data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create("owner", null, data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create("owner", "name", null));
            }
        }

        public class TheEditReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(connection);
                var data = new ReleaseUpdate { TagName = "fake-tag" };

                releasesClient.Edit("fake", "repo", 1, data);

                connection.Received().Patch<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"), data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                var releaseUpdate = new ReleaseUpdate { TagName = "tag" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit(null, "name", 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Edit("", "name", 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit("owner", null, 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Edit("owner", "", 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit("owner", "name", 1, null));
            }
        }

        public class TheDeleteReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.Delete("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }

        public class TheGetAssetsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetAllAssets("fake", "repo", 1);

                connection.Received().GetAll<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets"),
                    null,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAssets("", "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void UploadsToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var release = new Release("https://uploads.test.dev/does/not/matter/releases/1/assets{?name}");
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload("example.zip", "application/zip", rawData, null);

                releasesClient.UploadAsset(release, upload);

                client.Received().Post<ReleaseAsset>(
                    Arg.Is<Uri>(u => u.ToString() == "https://uploads.test.dev/does/not/matter/releases/1/assets?name=example.zip"),
                    rawData,
                    "application/vnd.github.v3",
                    Arg.Is<string>(contentType => contentType == "application/zip"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                var release = new Release("https://uploads.github.com/anything");
                var uploadData = new ReleaseAssetUpload("good", "good/good", Stream.Null, null);
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.UploadAsset(null, uploadData));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.UploadAsset(release, null));
            }

            [Fact]
            public async Task OverrideDefaultTimeout()
            {
                var newTimeout = TimeSpan.FromSeconds(100);

                var apiConnection = Substitute.For<IApiConnection>();

                var fixture = new ReleasesClient(apiConnection);

                var release = new Release("https://uploads.github.com/anything");
                var uploadData = new ReleaseAssetUpload("good", "good/good", Stream.Null, newTimeout);

                await fixture.UploadAsset(release, uploadData);

                apiConnection.Received().Post<ReleaseAsset>(Arg.Any<Uri>(), uploadData.RawData, Arg.Any<string>(), uploadData.ContentType, newTimeout);
            }
        }

        public class TheGetAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetAsset("fake", "repo", 1);

                connection.Received().Get<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAsset(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAsset("", "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAsset("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAsset("owner", "", 1));
            }
        }

        public class TheEditAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);
                var data = new ReleaseAssetUpdate("asset");

                client.EditAsset("fake", "repo", 1, data);

                connection.Received().Patch<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"),
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset(null, "name", 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.EditAsset("", "name", 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset("owner", null, 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.EditAsset("owner", "", 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset("owner", "name", 1, null));
            }
        }

        public class TheDeleteAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.DeleteAsset("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAsset(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteAsset("", "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAsset("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
