using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class BlobClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                client.Get("fake", "repo", "123456ABCD");

                connection.Received().Get<Blob>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/blobs/123456ABCD"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new BlobsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "123456ABCD"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "123456ABCD"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "123456ABCD"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "123456ABCD"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newBlob = new NewBlob();
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                client.Create("fake", "repo", newBlob);

                connection.Received().Post<BlobReference>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/blobs"), newBlob);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewBlob()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewBlob()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewBlob()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewBlob()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
            }
        }

        [Fact]
        public void CanDeserializeBlob()
        {
            const string blobResponseJson =
                "{\"content\": \"Content of the blob\", " +
                "\"encoding\": \"utf-8\"," +
                "\"sha\": \"3a0f86fb8db8eea7ccbb9a95f325ddbedfb25e15\"," +
                "\"size\": 100" +
                "}";
            var response = new ApiResponse<Blob>
            {
                Body = blobResponseJson,
                ContentType = "application/json"
            };
            var jsonPipeline = new JsonHttpPipeline();

            jsonPipeline.DeserializeResponse(response);

            Assert.NotNull(response.BodyAsObject);
            Assert.Equal(blobResponseJson, response.Body);
            Assert.Equal(100, response.BodyAsObject.Size);
            Assert.Equal(EncodingType.Utf8, response.BodyAsObject.Encoding);
        }
    }
}
