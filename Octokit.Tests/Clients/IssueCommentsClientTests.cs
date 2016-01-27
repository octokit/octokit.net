using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

public class IssueCommentsClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            client.Get("fake", "repo", 42);

            connection.Received().Get<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42"));
        }

        [Fact]
        public async Task EnsuresNonNullArguments()
        {
            var client = new IssueCommentsClient(Substitute.For<IApiConnection>());

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
            var client = new IssueCommentsClient(connection);

            client.GetAllForRepository("fake", "repo");

            connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
        }
    }

    public class TheGetForIssueMethod
    {
        [Fact]
        public void RequestsCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            client.GetAllForIssue("fake", "repo", 3);

            connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/3/comments"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1));
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            const string newComment = "some title";
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            client.Create("fake", "repo", 1, newComment);

            connection.Received().Post<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/1/comments"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", 1, "title"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", 1, "x"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, 1, "x"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", 1, "x"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", 1, null));
        }
    }

    public class TheUpdateMethod
    {
        [Fact]
        public void PostsToCorrectUrl()
        {
            const string issueCommentUpdate = "Worthwhile update";
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            client.Update("fake", "repo", 42, issueCommentUpdate);

            connection.Received().Patch<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42"), Arg.Any<object>());
        }

        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 42, "title"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 42, "x"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 42, "x"));
            await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 42, "x"));
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 42, null));
        }
    }

    public class TheDeleteMethod
    {
        [Fact]
        public void DeletesCorrectUrl()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

            client.Delete("fake", "repo", 42);

            connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42"));
        }

        [Fact]
        public async Task EnsuresArgumentsNotNullOrEmpty()
        {
            var connection = Substitute.For<IApiConnection>();
            var client = new IssueCommentsClient(connection);

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
            Assert.Throws<ArgumentNullException>(() => new IssueCommentsClient(null));
        }
    }

    [Fact]
    public void CanDeserializeIssueComment()
    {
        const string issueResponseJson =
            "{\"id\": 1," +
            "\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/comments/1\"," +
            "\"html_url\": \"https://github.com/octocat/Hello-World/issues/1347#issuecomment-1\"," +
            "\"body\": \"Me too\"," +
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
            issueResponseJson,
            new Dictionary<string, string>(),
            "application/json");

        var jsonPipeline = new JsonHttpPipeline();

        var response = jsonPipeline.DeserializeResponse<IssueComment>(httpResponse);

        Assert.NotNull(response.Body);
        Assert.Equal(issueResponseJson, response.HttpResponse.Body);
        Assert.Equal(1, response.Body.Id);
    }
}
