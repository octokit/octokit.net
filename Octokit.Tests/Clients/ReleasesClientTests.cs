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

                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll("owner", null));
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

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"),
                    null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.Get(null, "name", 1));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Get("owner", null, 1));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("owner", "", 1));
            }
        }

        public class TheCreateReleaseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.Create("fake", "repo", data);

                client.Received().Post<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                var data = new ReleaseUpdate("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new ReleaseUpdate(null));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.Create(null, "name", data));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.Create("owner", null, data));
                await AssertEx.Throws<ArgumentNullException>(async () =>
                    await releasesClient.Create("owner", "name", null));
            }
        }

        public class TheEditReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(connection);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.Edit("fake", "repo", 1, data);

                connection.Received().Patch<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"), data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit(null, "name", 1, new ReleaseUpdate("tag")));
                Assert.Throws<ArgumentException>(() => releasesClient.Edit("", "name", 1, new ReleaseUpdate("tag")));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", null, 1, new ReleaseUpdate("tag")));
                Assert.Throws<ArgumentException>(() => releasesClient.Edit("owner", "", 1, new ReleaseUpdate("tag")));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", "name", 1, null));
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
            public void EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }

        public class TheGetAssetsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetAssets("fake", "repo", 1);

                connection.Received().GetAll<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets"),
                    null,
                    "application/vnd.github.v3");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAssets(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAssets("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAssets("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.GetAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void UploadsToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var release = new Release { UploadUrl = "https://uploads.test.dev/does/not/matter/releases/1/assets{?name}" };
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload { FileName = "example.zip", ContentType = "application/zip", RawData = rawData };

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
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetAsset("fake", "repo", 1);

                connection.Received().Get<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"), null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAsset(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAsset("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAsset("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.GetAsset("owner", "", 1));
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
            public void EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.EditAsset(null, "name", 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentException>(() => client.EditAsset("", "name", 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", null, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentException>(() => client.EditAsset("owner", "", 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", "name", 1, null));
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
            public void EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteAsset("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
