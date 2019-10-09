using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssueCommentsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.Get("fake", "repo", 42);

                connection.Received().Get<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments/42"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.Get(1, 42);

                connection.Received().Get<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/42"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssueCommentsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
            }
        }

        public class TheGetForIssueMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.GetAllForIssue("fake", "repo", 3);

                connection.Received().GetAll<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await client.GetAllForIssue(1, 3);

                connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithIssueCommentRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var request = new IssueCommentRequest()
                {
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                };

                await client.GetAllForIssue("fake", "repo", 3, request);

                connection.Received().GetAll<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithIssueCommentRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var request = new IssueCommentRequest()
                {
                    Since = new DateTimeOffset(2016, 11, 23, 11, 11, 11, 00, new TimeSpan()),
                };

                await client.GetAllForIssue(1, 3, request);

                connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                await client.GetAllForIssue("fake", "repo", 3, options);

                connection.Received().GetAll<IssueComment>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageSize = 1,
                    PageCount = 1
                };

                await client.GetAllForIssue(1, 3, options);

                connection.Received().GetAll<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/3/comments"),
                    Arg.Any<Dictionary<string, string>>(),
                    "application/vnd.github.squirrel-girl-preview",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, request: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue("owner", "name", 1, null, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(1, 1, options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(1, 1, request: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForIssue(1, 1, null, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("", "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForIssue("owner", "", 1, ApiOptions.None));
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
            public void PostsToCorrectUrlWithRepositoryId()
            {
                const string newComment = "some title";
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                client.Create(1, 1, newComment);

                connection.Received().Post<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/1/comments"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", 1, "x"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, 1, "x"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", 1, "x"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", 1, "x"));
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
            public void PostsToCorrectUrlWithRepositoryId()
            {
                const string issueCommentUpdate = "Worthwhile update";
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                client.Update(1, 42, issueCommentUpdate);

                connection.Received().Patch<IssueComment>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/42"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 42, "x"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 42, "x"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, 42, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 42, "x"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 42, "x"));
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
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

                client.Delete(1, 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/comments/42"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNullOrEmpty()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssueCommentsClient(connection);

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

        [Fact]
        public void CanDeserializeIssueCommentWithReactions()
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
                "\"updated_at\": \"2011-04-14T16:00:49Z\"," +
                "\"reactions\": {" +
                "\"total_count\": 5," +
                "\"+1\": 3," +
                "\"-1\": 1," +
                "\"laugh\": 0," +
                "\"confused\": 0," +
                "\"heart\": 1," +
                "\"hooray\": 0," +
                "\"url\": \"https://api.github.com/repos/octocat/Hello-World/issues/comments/1/reactions\"" +
                "}" +
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
            Assert.NotNull(response.Body.Reactions);
            Assert.Equal(5, response.Body.Reactions.TotalCount);
            Assert.Equal(3, response.Body.Reactions.Plus1);
        }
    }
}
