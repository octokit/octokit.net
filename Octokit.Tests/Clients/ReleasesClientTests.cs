using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReleasesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() =>
                    new ReleasesClient(null));
            }
        }

        public class TheGenerateReleaseNotesMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new GenerateReleaseNotesRequest("fake-tag");

                await releasesClient.GenerateReleaseNotes("fake", "repo", data);

                client.Received().Post<GeneratedReleaseNotes>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/generate-notes"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new GenerateReleaseNotesRequest("fake-tag");

                await releasesClient.GenerateReleaseNotes(1, data);

                client.Received().Post<GeneratedReleaseNotes>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/generate-notes"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => new GenerateReleaseNotesRequest(null));

                var data = new GenerateReleaseNotesRequest("fake-tag");

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GenerateReleaseNotes(null, "name", data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GenerateReleaseNotes("owner", null, data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GenerateReleaseNotes("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GenerateReleaseNotes(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GenerateReleaseNotes("", "name", data));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GenerateReleaseNotes("owner", "", data));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);

                await releasesClient.GetAll("fake", "repo");

                client.Received().GetAll<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    null,
                    "application/vnd.github.v3",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);

                await releasesClient.GetAll(1);

                client.Received().GetAll<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases"),
                    null,
                    "application/vnd.github.v3",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await releasesClient.GetAll("fake", "repo", options);

                client.Received().GetAll<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    null,
                    "application/vnd.github.v3",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                await releasesClient.GetAll(1, options);

                client.Received().GetAll<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases"),
                    null,
                    "application/vnd.github.v3",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheGetReleaseMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlByTag()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Get("fake", "repo", "tag");

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/tags/tag"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Get(1, 1);

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/1"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdByTag()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Get(1, "tag");

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/tags/tag"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("owner", "", 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get("owner", "name", null));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("owner", "name", ""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get(null, "name", "tag"));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("", "name", "tag"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get("owner", null, "tag"));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get("owner", "", "tag"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Get(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Get(1, ""));
            }
        }

        public class TheGetLatestReleaseMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetLatest("fake", "repo");

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/latest"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetLatest(1);

                connection.Received().Get<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/latest"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetLatest(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.GetLatest("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetLatest("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.GetLatest("owner", ""));
            }
        }
        public class TheCreateReleaseMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new NewRelease("fake-tag");

                await releasesClient.Create("fake", "repo", data);

                client.Received().Post<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var data = new NewRelease("fake-tag");

                await releasesClient.Create(1, data);

                client.Received().Post<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases"),
                    data,
                    "application/vnd.github.v3");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                Assert.Throws<ArgumentNullException>(() => new NewRelease(null));

                var data = new NewRelease("fake-tag");

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create(null, "name", data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create("owner", null, data));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Create("", "name", data));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Create("owner", "", data));
            }
        }

        public class TheEditReleaseMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(connection);
                var data = new ReleaseUpdate { TagName = "fake-tag" };

                await releasesClient.Edit("fake", "repo", 1, data);

                connection.Received().Patch<Release>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"), data);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(connection);

                var data = new ReleaseUpdate { TagName = "fake-tag" };

                await releasesClient.Edit(1, 1, data);

                connection.Received().Patch<Release>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/1"), data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection>());
                var releaseUpdate = new ReleaseUpdate { TagName = "tag" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit(null, "name", 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit("owner", null, 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => releasesClient.Edit(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Edit("", "name", 1, releaseUpdate));
                await Assert.ThrowsAsync<ArgumentException>(() => releasesClient.Edit("owner", "", 1, releaseUpdate));
            }
        }

        public class TheDeleteReleaseMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Delete("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.Delete(1, 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }

        public class TheGetAssetsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetAllAssets("fake", "repo", 1);

                connection.Received().GetAll<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets"),
                    null,
                    "application/vnd.github.v3",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetAllAssets(1, 1);

                connection.Received().GetAll<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/1/assets"),
                    null,
                    "application/vnd.github.v3",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllAssets("fake", "repo", 1, options);

                connection.Received().GetAll<ReleaseAsset>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/1/assets"),
                    null,
                    "application/vnd.github.v3", options);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAllAssets(1, 1, options);

                connection.Received().GetAll<ReleaseAsset>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/1/assets"),
                    null,
                    "application/vnd.github.v3", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAssets(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAssets("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public async Task UploadsToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var releasesClient = new ReleasesClient(client);
                var release = new Release("https://uploads.test.dev/does/not/matter/releases/1/assets{?name}");
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload("example.zip", "application/zip", rawData, null);

                await releasesClient.UploadAsset(release, upload);

                client.Received().Post<ReleaseAsset>(
                    Arg.Is<Uri>(u => u.ToString() == "https://uploads.test.dev/does/not/matter/releases/1/assets?name=example.zip"),
                    rawData,
                    "application/vnd.github.v3",
                    Arg.Is<string>(contentType => contentType == "application/zip"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
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

            [Fact]
            public async Task CanBeCancelled()
            {
                var httpClient = new CancellationTestHttpClient();
                var connection = new Connection(new ProductHeaderValue("TEST"), httpClient);
                var apiConnection = new ApiConnection(connection);

                var fixture = new ReleasesClient(apiConnection);

                var release = new Release("https://uploads.github.com/anything");
                var uploadData = new ReleaseAssetUpload("good", "good/good", Stream.Null, null);

                using (var cts = new CancellationTokenSource())
                {
                    var uploadTask = fixture.UploadAsset(release, uploadData, cts.Token);

                    cts.Cancel();

                    await Assert.ThrowsAsync<TaskCanceledException>(() => uploadTask);
                }
            }

            private class CancellationTestHttpClient : IHttpClient
            {
                public async Task<IResponse> Send(IRequest request, CancellationToken cancellationToken)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

                    throw new Exception("HTTP operation was not cancelled");
                }

                public void Dispose() { }

                public void SetRequestTimeout(TimeSpan timeout) { }
            }
        }

        public class TheGetAssetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetAsset("fake", "repo", 1);

                connection.Received().Get<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.GetAsset(1, 1);

                connection.Received().Get<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/assets/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAsset(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAsset("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAsset("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAsset("owner", "", 1));
            }
        }

        public class TheEditAssetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);
                var data = new ReleaseAssetUpdate("asset");

                await client.EditAsset("fake", "repo", 1, data);

                connection.Received().Patch<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"),
                    data);
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);
                var data = new ReleaseAssetUpdate("asset");

                await client.EditAsset(1, 1, data);

                connection.Received().Patch<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/assets/1"),
                    data);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset(null, "name", 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset("owner", null, 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditAsset(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.EditAsset("", "name", 1, new ReleaseAssetUpdate("name")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.EditAsset("owner", "", 1, new ReleaseAssetUpdate("name")));
            }
        }

        public class TheDeleteAssetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.DeleteAsset("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/releases/assets/1"));
            }

            [Fact]
            public async Task RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReleasesClient(connection);

                await client.DeleteAsset(1, 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/releases/assets/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReleasesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAsset(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAsset("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteAsset("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
