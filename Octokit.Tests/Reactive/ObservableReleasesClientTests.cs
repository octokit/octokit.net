using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using Octokit.Reactive;
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

                gitHubClient.Repository.Release.Received(1).Get("fake", "repo", 1);
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
        public class TheGetLatestReleaseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetLatest("fake", "repo");

                gitHubClient.Repository.Release.Received(1).GetLatest("fake", "repo");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.GetLatest(null, "name"));
                Assert.Throws<ArgumentException>(() => releasesClient.GetLatest("", "name"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetLatest("owner", null));
                Assert.Throws<ArgumentException>(() => releasesClient.GetLatest("owner", ""));
            }
        }
        public class TheCreateReleaseMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new NewRelease("fake-tag");

                releasesClient.Create("fake", "repo", data);

                gitHubClient.Repository.Release.Received(1).Create("fake", "repo", data);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var data = new NewRelease("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new NewRelease(null));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Create(null, "name", data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Create("owner", null, data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Create("owner", "name", null));
            }
        }

        public class TheEditReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseUpdate { TagName = "fake-tag" };

                releasesClient.Edit("fake", "repo", 1, data);

                gitHubClient.Repository.Release.Received(1).Edit("fake", "repo", 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var update = new ReleaseUpdate { TagName = "tag" };

                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit(null, "name", 1, update));
                Assert.Throws<ArgumentException>(() => releasesClient.Edit("", "name", 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", null, 1, update));
                Assert.Throws<ArgumentException>(() => releasesClient.Edit("owner", "", 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", "name", 1, null));
            }
        }

        public class TheDeleteReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Delete("fake", "repo", 1);

                gitHubClient.Repository.Release.Received(1).Delete("fake", "repo", 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

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
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAllAssets("fake", "repo", 1);

                gitHubClient.Connection.Received(1).Get<List<ReleaseAsset>>(
                    new Uri("repos/fake/repo/releases/1/assets", UriKind.Relative), null, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllAssets("", "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1));
                Assert.Throws<ArgumentException>(() => client.GetAllAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var release = new Release("https://uploads.test.dev/does/not/matter/releases/1/assets{?name}");
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload("example.zip", "application/zip", rawData, null);

                releasesClient.UploadAsset(release, upload);

                gitHubClient.Repository.Release.Received(1).UploadAsset(release, upload);
            }

            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                var release = new Release("https://uploads.github.com/anything");
                var uploadData = new ReleaseAssetUpload("good", "good/good", Stream.Null, null);

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

                client.GetAsset("fake", "repo", 1);

                gitHubClient.Repository.Release.Received(1).GetAsset("fake", "repo", 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

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
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseAssetUpdate("asset");

                client.EditAsset("fake", "repo", 1, data);

                gitHubClient.Repository.Release.Received(1).EditAsset("fake", "repo", 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

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
