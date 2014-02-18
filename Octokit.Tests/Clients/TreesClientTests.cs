using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests
{
    public class TreesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                client.Get("fake", "repo", "123456ABCD");

                connection.Received().Get<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/trees/123456ABCD"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new TreesClient(Substitute.For<IApiConnection>());

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
                var newTree = new NewTree();
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                client.Create("fake", "repo", newTree);

                connection.Received().Post<TreeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/git/trees"), newTree);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewTree()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewTree()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewTree()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewTree()));
            }

            [Fact]
            public async Task EnsureExceptionIsThrownWhenModeIsNotProvided()
            {
                var newTree = new NewTree();
                newTree.Tree.Add(new NewTreeItem { Path = "README.md", Type = TreeType.Blob, Sha = "2e1a73d60f004fd842d4bad28aa42392d4f35d28" });

                var connection = Substitute.For<IApiConnection>();
                var client = new TreesClient(connection);

                await AssertEx.Throws<ArgumentException>(
                    async () => await client.Create("fake", "repo", newTree));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
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
            var response = new ApiResponse<TreeResponse>
            {
                Body = issueResponseJson,
                ContentType = "application/json"
            };
            var jsonPipeline = new JsonHttpPipeline();

            jsonPipeline.DeserializeResponse(response);

            Assert.NotNull(response.BodyAsObject);
            Assert.Equal(issueResponseJson, response.Body);
            Assert.Equal("9fb037999f264ba9a7fc6274d15fa3ae2ab98312", response.BodyAsObject.Sha);
        }
    }
}
