using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests;
using Xunit;

public class RepositoryCommentsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Get("fake", "repo", 42);

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
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForRepository("fake", "repo");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"), Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAllForRepository("fake", "repo", options);

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"), options);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
        }
    }

    public class TheGetForCommitMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForCommit("fake", "repo", "sha");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"), ApiOptions.None);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAllForCommit("fake", "repo", "sha", options);

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"), options);
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, null, null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, null, null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, null, "sha1", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, "name", null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", null, null, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha1", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha1", Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha1", Args.ApiOptions));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "", "", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "", "", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "", "sha1", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "name", "", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "", "", null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", "sha1", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha1", Args.ApiOptions));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "name", "sha1", Args.ApiOptions));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            NewCommitComment newComment = new NewCommitComment("body");

            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Create("fake", "repo", "sha", newComment);

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
        public async Task PostsToCorrectUrl()
        {
            const string issueCommentUpdate = "updated comment";
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Update("fake", "repo", 42, issueCommentUpdate);

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
        public async Task DeletesCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Delete("fake", "repo", 42);

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
        public void EnsuresNonNullArguments()
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
