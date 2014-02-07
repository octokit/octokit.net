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
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

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
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.CreateRelease("fake", "repo", data);

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
                var connection = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(connection);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.EditRelease("fake", "repo", data);

                connection.Received().Patch<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"), data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

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
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.DeleteRelease("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

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
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.GetAssets("fake", "repo", 1);

                connection.Received().GetAll<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets"),
                    null,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

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

                client.GetAsset("fake", "repo", 1, 1);

                connection.Received().Get<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets/1"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

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
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);
                var data = new ReleaseAssetUpdate("asset");

                client.EditAsset("fake", "repo", 1, 1, data);

                connection.Received().Patch<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets/1"), 
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

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
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                client.DeleteAsset("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteAsset(null, "name", 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteAsset("", "name", 1));
                AssertEx.Throws<ArgumentNullException>(async () => await client.DeleteAsset("owner", null, 1));
                AssertEx.Throws<ArgumentException>(async () => await client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
