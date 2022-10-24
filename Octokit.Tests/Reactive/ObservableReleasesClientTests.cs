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
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
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
                    new Uri("repos/fake/repo/releases", UriKind.Relative), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAll(1);

                gitHubClient.Connection.Received(1).Get<List<Release>>(
                    new Uri("repositories/1/releases", UriKind.Relative), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("fake", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<Release>>(
                    new Uri("repos/fake/repo/releases", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(1, options);

                gitHubClient.Connection.Received(1).Get<List<Release>>(
                    new Uri("repositories/1/releases", UriKind.Relative), Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll(null, "name"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => releasesClient.GetAll(1, null));

                Assert.Throws<ArgumentException>(() => releasesClient.GetAll("", "name"));
                Assert.Throws<ArgumentException>(() => releasesClient.GetAll("owner", ""));
                Assert.Throws<ArgumentException>(() => releasesClient.GetAll("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => releasesClient.GetAll("owner", "", ApiOptions.None));
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

                gitHubClient.Repository.Release.Received(1).Get("fake", "repo", 1);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Get(1, 1);

                gitHubClient.Repository.Release.Received(1).Get(1, 1);
            }

            [Fact]
            public void RequestsTheCorrectUrlByTag()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Get("fake", "repo", "tag");

                gitHubClient.Repository.Release.Received(1).Get("fake", "repo", "tag");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdByTag()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Get(1, "tag");

                gitHubClient.Repository.Release.Received(1).Get(1, "tag");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => releasesClient.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("owner", "", 1));

                Assert.Throws<ArgumentNullException>(() => releasesClient.Get(null, "name", "tag"));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("", "name", "tag"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Get("owner", null, "tag"));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("owner", "", "tag"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Get("owner", "name", null));
                Assert.Throws<ArgumentException>(() => releasesClient.Get("owner", "name", ""));
            }
        }

        public class TheGetLatestReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetLatest("fake", "repo");

                gitHubClient.Repository.Release.Received(1).GetLatest("fake", "repo");
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetLatest(1);

                gitHubClient.Repository.Release.Received(1).GetLatest(1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => releasesClient.GetLatest(null, "name"));
                Assert.Throws<ArgumentNullException>(() => releasesClient.GetLatest("owner", null));

                Assert.Throws<ArgumentException>(() => releasesClient.GetLatest("", "name"));
                Assert.Throws<ArgumentException>(() => releasesClient.GetLatest("owner", ""));
            }
        }

        public class TheCreateReleaseMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new NewRelease("fake-tag");

                releasesClient.Create("fake", "repo", data);

                gitHubClient.Repository.Release.Received(1).Create("fake", "repo", data);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new NewRelease("fake-tag");

                releasesClient.Create(1, data);

                gitHubClient.Repository.Release.Received(1).Create(1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());
                var data = new NewRelease("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new NewRelease(null));

                Assert.Throws<ArgumentNullException>(() => releasesClient.Create(null, "name", data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Create("owner", null, data));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => releasesClient.Create(1, null));

                Assert.Throws<ArgumentException>(() => releasesClient.Create("", "name", data));
                Assert.Throws<ArgumentException>(() => releasesClient.Create("owner", "", data));
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
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var releasesClient = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseUpdate { TagName = "fake-tag" };

                releasesClient.Edit(1, 1, data);

                gitHubClient.Repository.Release.Received(1).Edit(1, 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var releasesClient = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                var update = new ReleaseUpdate { TagName = "tag" };

                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit(null, "name", 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", null, 1, update));
                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => releasesClient.Edit(1, 1, null));

                Assert.Throws<ArgumentException>(() => releasesClient.Edit("", "name", 1, update));
                Assert.Throws<ArgumentException>(() => releasesClient.Edit("owner", "", 1, update));
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
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.Delete(1, 1);

                gitHubClient.Repository.Release.Received(1).Delete(1, 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1));
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
                    new Uri("repos/fake/repo/releases/1/assets", UriKind.Relative), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAllAssets(1, 1);

                gitHubClient.Connection.Received(1).Get<List<ReleaseAsset>>(
                    new Uri("repositories/1/releases/1/assets", UriKind.Relative), Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var expectedUrl = string.Format("repos/{0}/{1}/releases/1/assets", "fake", "repo");

                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllAssets("fake", "repo", 1, options);

                gitHubClient.Connection.Received(1).Get<List<ReleaseAsset>>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                    Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var expectedUrl = "repositories/1/releases/1/assets";

                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAllAssets(1, 1, options);

                gitHubClient.Connection.Received(1).Get<List<ReleaseAsset>>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                    Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllAssets(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllAssets("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllAssets("owner", "", 1));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
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
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAsset("fake", "repo", 1);

                gitHubClient.Repository.Release.Received(1).GetAsset("fake", "repo", 1);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.GetAsset(1, 1);

                gitHubClient.Repository.Release.Received(1).GetAsset(1, 1);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAsset(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAsset("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.GetAsset("", "name", 1));
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
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);
                var data = new ReleaseAssetUpdate("asset");

                client.EditAsset(1, 1, data);

                gitHubClient.Repository.Release.Received(1).EditAsset(1, 1, data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.EditAsset(null, "name", 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", null, 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentNullException>(() => client.EditAsset("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.EditAsset(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.EditAsset("", "name", 1, new ReleaseAssetUpdate("name")));
                Assert.Throws<ArgumentException>(() => client.EditAsset("owner", "", 1, new ReleaseAssetUpdate("name")));
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
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReleasesClient(gitHubClient);

                client.DeleteAsset(1, 1);

                gitHubClient.Connection.Delete(
                    new Uri("repositories/1/releases/assets/1", UriKind.Relative));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableReleasesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteAsset("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.DeleteAsset("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteAsset("owner", "", 1));
            }
        }
    }
}
