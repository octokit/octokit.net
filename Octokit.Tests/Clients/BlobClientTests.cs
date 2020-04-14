using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Clients
{
    public class BlobClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new BlobsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                await client.Get("fake", "repo", "123456ABCD");

                connection.Received().Get<Blob>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/blobs/123456ABCD"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                await client.Get(1, "123456ABCD");

                connection.Received().Get<Blob>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/blobs/123456ABCD"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new BlobsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                var newBlob = new NewBlob();

                client.Create("fake", "repo", newBlob);

                connection.Received().Post<BlobReference>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/blobs"), newBlob);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                var newBlob = new NewBlob();

                client.Create(1, newBlob);

                connection.Received().Post<BlobReference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/blobs"), newBlob);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new BlobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewBlob()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewBlob()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewBlob()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewBlob()));
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
            var httpResponse = CreateResponse(
                HttpStatusCode.OK,
                blobResponseJson);

            var jsonPipeline = new JsonHttpPipeline();

            var response = jsonPipeline.DeserializeResponse<Blob>(httpResponse);

            Assert.NotNull(response.Body);
            Assert.Equal(blobResponseJson, (string)response.HttpResponse.Body);
            Assert.Equal(100, response.Body.Size);
            Assert.Equal(EncodingType.Utf8, response.Body.Encoding);
        }
    }
}
