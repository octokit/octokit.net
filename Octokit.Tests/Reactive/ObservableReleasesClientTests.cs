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

                gitHubClient.Connection.Received(1).Get<List<Release>>(
                    new Uri("repos/fake/repo/releases", UriKind.Relative), null, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll("owner", null));
            }
        }

        public class TheGetReleaseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Get("fake", "repo", 1);

                gitHubClient.Release.Received(1).Get("fake", "repo", 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.Get(null, "name", 1));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Get("owner", null, 1));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("owner", "", 1));
            }
        }

        public class TheCreateReleaseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.CreateRelease("fake", "repo", data);

                gitHubClient.Release.Received(1).CreateRelease("fake", "repo", data);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var data = new ReleaseUpdate("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new ReleaseUpdate(null));
                Assert.Throws<ArgumentNullException>(() => releasesClient.CreateRelease(null, "name", data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.CreateRelease("owner", null, data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.CreateRelease("owner", "name", null));
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

                releasesClient.EditRelease("fake", "repo", 1, data);

                gitHubClient.Release.Received(1).EditRelease("fake", "repo", 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var update = new ReleaseUpdate("tag");

                Assert.Throws<ArgumentNullException>(() => releasesClient.EditRelease(null, "name", 1, update));
                Assert.Throws<ArgumentException>(() => releasesClient.EditRelease("", "name", 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.EditRelease("owner", null, 1, update));
                Assert.Throws<ArgumentException>(() => releasesClient.EditRelease("owner", "", 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.EditRelease("owner", "name", 1, null));
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

                gitHubClient.Release.Received(1).DeleteRelease("fake", "repo", 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteRelease(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteRelease("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteRelease("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.DeleteRelease("owner", "", 1));
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

                gitHubClient.Connection.Received(1).Get<List<ReleaseAsset>>(
                    new Uri("repos/fake/repo/releases/1/assets", UriKind.Relative), null, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAssets(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAssets("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAssets("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.GetAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var release = new Release { UploadUrl = "https://uploads.test.dev/does/not/matter/releases/1/assets{?name}" };
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload { FileName = "example.zip", ContentType = "application/zip", RawData = rawData };

                releasesClient.UploadAsset(release, upload);

                gitHubClient.Release.Received(1).UploadAsset(release, upload);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                var release = new Release { UploadUrl = "https://uploads.github.com/anything" };
                var uploadData = new ReleaseAssetUpload { FileName = "good", ContentType = "good/good", RawData = Stream.Null };

                Assert.Throws<ArgumentNullException>(() => releasesClient.UploadAsset(null, uploadData));
                Assert.Throws<ArgumentNullException>(() => releasesClient.UploadAsset(release, null));
            }
        }

        public class TheGetAssetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAsset("fake", "repo", 1, 1);

                gitHubClient.Release.Received(1).GetAsset("fake", "repo", 1, 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAsset(null, "name", 1, 1));
                Assert.Throws<ArgumentException>(() => client.GetAsset("", "name", 1, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAsset("owner", null, 1, 1));
                Assert.Throws<ArgumentException>(() => client.GetAsset("owner", "", 1, 1));
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

                gitHubClient.Release.Received(1).EditAsset("fake", "repo", 1, 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.EditAsset(null, "name", 1, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentException>(() => client.EditAsset("", "name", 1, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", null, 1, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentException>(() => client.EditAsset("owner", "", 1, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", "name", 1, 1, null));
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

                gitHubClient.Connection.Delete(
                    new Uri("repos/fake/repo/releases/assets/1", UriKind.Relative));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteAsset("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
