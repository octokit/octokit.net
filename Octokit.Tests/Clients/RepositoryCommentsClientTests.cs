using System;
using System.Collections.Generic;
using System.Net;
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

            connection.Received().Get<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new RepositoryCommentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
        }
    }

    public class TheGetForRepositoryMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.GetAllForRepository("fake", "repo");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
        }
    }

    public class TheGetForCommitMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            client.GetAllForCommit("fake", "repo", "sha");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "name", "sha"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", ""));
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

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null, new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "name", "", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", "sha", null));
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

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 42, null));
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

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 42));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 42));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 42));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 42));
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
        var httpResponse = new Response(
            HttpStatusCode.OK,
            commitCommentResponseJson,
            new Dictionary<string, string>(),
            "application/json");

        var jsonPipeline = new JsonHttpPipeline();

        var response = jsonPipeline.DeserializeResponse<CommitComment>(httpResponse);

        Assert.NotNull(response.Body);
        Assert.Equal(commitCommentResponseJson, response.HttpResponse.Body);
        Assert.Equal(1, response.Body.Id);
    }
}
