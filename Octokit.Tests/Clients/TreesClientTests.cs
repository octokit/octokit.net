using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests
{
    public class TreesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await client.Get("fake", "repo", "123456ABCD");

                connection.Received().Get<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/trees/123456ABCD"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await client.Get(1, "123456ABCD");

                connection.Received().Get<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/trees/123456ABCD"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new TreesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, ""));
            }
        }

        public class TheGetRecursiveMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await client.GetRecursive("fake", "repo", "123456ABCD");

                connection.Received().Get<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/trees/123456ABCD?recursive=1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await client.GetRecursive(1, "123456ABCD");

                connection.Received().Get<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/trees/123456ABCD?recursive=1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new TreesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive(null, "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive("owner", null, "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRecursive(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("", "name", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("owner", "", "123456ABCD"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRecursive(1, ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newTree = new NewTree();
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                client.Create("fake", "repo", newTree);

                connection.Received().Post<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/trees"), newTree);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var newTree = new NewTree();
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                client.Create(1, newTree);

                connection.Received().Post<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/trees"), newTree);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewTree()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewTree()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewTree()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewTree()));
            }

            [Fact]
            public async Task EnsureExceptionIsThrownWhenModeIsNotProvided()
            {
                var newTree = new NewTree();
                newTree.Tree.Add(new NewTreeItem { Path = "README.md", Type = TreeType.Blob, Sha = "2e1a73d60f004fd842d4bad28aa42392d4f35d28" });

                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(
                    () => client.Create("fake", "repo", newTree));
            }

            [Fact]
            public async Task EnsureExceptionIsThrownWhenModeIsNotProvidedWithRepositoryId()
            {
                var newTree = new NewTree();
                newTree.Tree.Add(new NewTreeItem { Path = "README.md", Type = TreeType.Blob, Sha = "2e1a73d60f004fd842d4bad28aa42392d4f35d28" });

                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(
                    () => client.Create(1, newTree));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TreesClient(null));
            }
        }

        [Fact]
        public void CanDeserializeIssueComment()
        {
            const string issueResponseJson =
                "{" +
                "\"sha\": \"9fb037999f264ba9a7fc6274d15fa3ae2ab98312\"," +
                "\"url\": \"https://api.github.com/repos/octocat/Hello-World/trees/9fb037999f264ba9a7fc6274d15fa3ae2ab98312\"," +
                "\"tree\": [" +
                "{" +
                "\"path\": \"file.rb\"," +
                "\"mode\": \"100644\"," +
                "\"type\": \"blob\"," +
                "\"size\": 30," +
                "\"sha\": \"44b4fc6d56897b048c772eb4087f854f46256132\"," +
                "\"url\": \"https://api.github.com/repos/octocat/Hello-World/git/blobs/44b4fc6d56897b048c772eb4087f854f46256132\"" +
                "}," +
                "{" +
                "\"path\": \"subdir\"," +
                "\"mode\": \"040000\"," +
                "\"type\": \"tree\"," +
                "\"sha\": \"f484d249c660418515fb01c2b9662073663c242e\"," +
                "\"url\": \"https://api.github.com/repos/octocat/Hello-World/git/blobs/f484d249c660418515fb01c2b9662073663c242e\"" +
                "}," +
                "{" +
                "\"path\": \"exec_file\"," +
                "\"mode\": \"100755\"," +
                "\"type\": \"blob\"," +
                "\"size\": 75," +
                "\"sha\": \"45b983be36b73c0788dc9cbcb76cbb80fc7bb057\"," +
                "\"url\": \"https://api.github.com/repos/octocat/Hello-World/git/blobs/45b983be36b73c0788dc9cbcb76cbb80fc7bb057\"" +
                "}" +
                "]" +
                "}";
            var httpResponse = CreateResponse(
                HttpStatusCode.OK,
                issueResponseJson);

            var jsonPipeline = new JsonHttpPipeline();

            var response = jsonPipeline.DeserializeResponse<TreeResponse>(httpResponse);

            Assert.NotNull(response.Body);
            Assert.Equal(issueResponseJson, response.HttpResponse.Body);
            Assert.Equal("9fb037999f264ba9a7fc6274d15fa3ae2ab98312", response.Body.Sha);
        }
    }
}
