using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests;
using Xunit;

using static Octokit.Internal.TestSetup;

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

            connection.Received().Get<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/42"), Arg.Any<Dictionary<string, string>>());
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Get(1, 42);

            connection.Received().Get<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42"), Arg.Any<Dictionary<string, string>>());
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new RepositoryCommentsClient(Substitute.For<IApiConnection>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForRepository("fake", "repo");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"), Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForRepository(1);

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments"), Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
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

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments"), Arg.Any<Dictionary<string, string>>(), options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAllForRepository(1, options);

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments"), Arg.Any<Dictionary<string, string>>(), options);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
        }
    }

    public class TheGetAllForCommitMethod
    {
        [Fact]
        public async Task RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForCommit("fake", "repo", "sha");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"),
                Arg.Any<Dictionary<string, string>>(),
                Args.ApiOptions);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.GetAllForCommit(1, "sha");

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/comments"),
                Arg.Any<Dictionary<string, string>>(),
                Args.ApiOptions);
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

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"), Arg.Any<Dictionary<string, string>>(), options);
        }

        [Fact]
        public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            var options = new ApiOptions
            {
                StartPage = 1,
                PageCount = 1,
                PageSize = 1
            };

            await client.GetAllForCommit(1, "sha", options);

            connection.Received().GetAll<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/comments"),
                Arg.Any<Dictionary<string, string>>(), options);
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha", null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(1, null, ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCommit(1, "sha", null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "name", "sha"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", ""));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("", "name", "sha", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha", ApiOptions.None));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", ApiOptions.None));

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForCommit(1, "", ApiOptions.None));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public async Task PostsToCorrectUrl()
        {
            var newComment = new NewCommitComment("body");

            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Create("fake", "repo", "sha", newComment);

            connection.Received().Post<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/commits/sha/comments"), Arg.Any<object>());
        }

        [Fact]
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            var newComment = new NewCommitComment("body");

            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Create(1, "sha", newComment);

            connection.Received().Post<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/commits/sha/comments"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null, new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", "sha", null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null, new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, "sha", null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", "sha", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "name", "", new NewCommitComment("body")));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "", new NewCommitComment("body")));
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
        public async Task PostsToCorrectUrlWithRepositoryId()
        {
            const string issueCommentUpdate = "updated comment";
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Update(1, 42, issueCommentUpdate);

            connection.Received().Patch<CommitComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, 42, null));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 42, "updated comment"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 42, "updated comment"));
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
        public async Task DeletesCorrectUrlWithRepositoryId()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await client.Delete(1, 42);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/comments/42"));
        }

        [Fact]
        public async Task EnsuresNonNullArgumentsOrEmpty()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new RepositoryCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 42));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 42));

            await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 42));
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
        var httpResponse = CreateResponse(
            HttpStatusCode.OK,
            commitCommentResponseJson);

        var jsonPipeline = new JsonHttpPipeline();

        var response = jsonPipeline.DeserializeResponse<CommitComment>(httpResponse);

        Assert.NotNull(response.Body);
        Assert.Equal(commitCommentResponseJson, response.HttpResponse.Body);
        Assert.Equal(1, response.Body.Id);
    }
}
