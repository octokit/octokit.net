using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

public class RepositoryCommentsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.Get("fake", "repo", 42);

            connection.Received().Get<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42"),
                null);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new RepositoryCommentsClient(Substitute.For<IApiConnection>());

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", 1));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
            await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", 1));
        }

    }

    public class TheGetForRepositoryMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.GetForRepository("fake", "repo");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository(null, "name"));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("", "name"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("owner", ""));
        }
    }

    public class TheGetForCommitMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.GetForCommit("fake", "repo", "sha");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForCommit(null, "name", "sha"));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForCommit("", "name", "sha"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForCommit("owner", null, "sha"));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForCommit("owner", "", "sha"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForCommit("owner", "name", null));
            await AssertEx.Throws<ArgumentException>(async () => await client.GetForCommit("owner", "name", ""));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            NewCommitComment newComment = new NewCommitComment("body");
            
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.Create("fake", "repo", "sha", newComment);

            connection.Received().Post<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", "sha", new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", "sha", new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, "sha", new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", "sha", new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null, new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "name", "", new NewCommitComment("body")));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", "sha", null));
        }
    }

    public class TheUpdateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            const string issueCommentUpdate = "updated comment";
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.Update("fake", "repo", 42, issueCommentUpdate);

            connection.Received().Patch<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Update(null, "name", 42, "updated comment"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Update("", "name", 42, "updated comment"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", null, 42, "updated comment"));
            await AssertEx.Throws<ArgumentException>(async () => await client.Update("owner", "", 42, "updated comment"));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", "name", 42, null));
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public void DeletesCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.Delete("fake", "repo", 42);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNullOrEmpty()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "name", 42));
            await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "name", 42));
            await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", null, 42));
            await AssertEx.Throws<ArgumentException>(async () => await client.Delete("owner", "", 42));
        }
    }

    public class TheCtor
    {
        [Fact]
        public void EnsuresArgument()
        {
            Assert.Throws<ArgumentNullException>(() => new RepositoryCommentsClient(null));
        }
    }

    [Fact]
    public void CanDeserializeCommitComment()
    {
        const string commitCommentResponseJson = 
            "{\"html_url\": \"https://github.com/octocat/Hello-World/commit/6dcb09b5b57875f334f61aebed695e2e4193db5e#commitcomment-1\"," +
            "\"url\": \"https://api.github.com/repos/octocat/Hello-World/comments/1\"," +
            "\"id\": 1," +
            "\"body\": \"Me too\"," +
            "\"path\": \"file1.txt\"," +
            "\"position\": 4," +
            "\"line\": 14," +
            "\"commit_id\": \"6dcb09b5b57875f334f61aebed695e2e4193db5e\"," +
            "\"user\": {" +
            "\"login\": \"octocat\"," +
            "\"id\": 1," +
            "\"avatar_url\": \"https://github.com/images/error/octocat_happy.gif\"," +
            "\"gravatar_id\": \"somehexcode\"," +
            "\"url\": \"https://api.github.com/users/octocat\"" +
            "}," +
            "\"created_at\": \"2011-04-14T16:00:49Z\"," +
            "\"updated_at\": \"2011-04-14T16:00:49Z\"" +
            "}";

        var response = new ApiResponse<CommitComment>
        {
            Body = commitCommentResponseJson,
            ContentType = "application/json"
        };
        var jsonPipeline = new JsonHttpPipeline();

        jsonPipeline.DeserializeResponse(response);

        Assert.NotNull(response.BodyAsObject);
        Assert.Equal(commitCommentResponseJson, response.Body); 
        Assert.Equal(1, response.BodyAsObject.Id);
    }
}
