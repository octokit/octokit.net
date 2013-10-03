﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Clients;
using Octokit.Http;
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
                var client = Substitute.For<IApiConnection<Release>>();
                var releasesClient = new ReleasesClient(client);

                releasesClient.GetAll("fake", "repo");

                client.Received().GetAll(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/releases"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection<Release>>());

                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.GetAll("owner", null));
            }
        }

        public class TheCreateReleaseMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Release>>();
                var releasesClient = new ReleasesClient(client);
                var data = new ReleaseUpdate("fake-tag");

                releasesClient.CreateRelease("fake", "repo", data);

                client.Received().Create(Arg.Is<Uri>(u => u.ToString() == "/repos/fake/repo/releases"), data);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection<Release>>());
                var data = new ReleaseUpdate("fake-tag");

                Assert.Throws<ArgumentNullException>(() => new ReleaseUpdate(null));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.CreateRelease(null, "name", data));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.CreateRelease("owner", null, data));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.CreateRelease("owner", "name", null));
            }
        }

        public class TheUploadReleaseAssetMethod
        {
            [Fact]
            public void UploadsToCorrectUrl()
            {
                var client = Substitute.For<IApiConnection<Release>>();
                var releasesClient = new ReleasesClient(client);
                var release = new Release { UploadUrl = "https://uploads.test.dev/does/not/matter/releases/1/assets{?name}" };
                var rawData = Substitute.For<Stream>();
                var upload = new ReleaseAssetUpload { FileName = "example.zip", ContentType = "application/zip", RawData = rawData };

                releasesClient.UploadAsset(release, upload);

                client.Received().Upload<ReleaseAsset>(Arg.Is<Uri>(u => u.ToString() == "https://uploads.test.dev/does/not/matter/releases/1/assets?name=example.zip"),
                    rawData,
                    Arg.Is<string>(contentType => contentType == "application/zip"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var releasesClient = new ReleasesClient(Substitute.For<IApiConnection<Release>>());

                var release = new Release { UploadUrl = "https://uploads.github.com/anything" };
                var uploadData = new ReleaseAssetUpload { FileName = "good", ContentType = "good/good", RawData = Stream.Null };
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.UploadAsset(null, uploadData));
                await AssertEx.Throws<ArgumentNullException>(async () => await releasesClient.UploadAsset(release, null));
            }
        }
    }
}
