using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Octokit.Internal;
using System.Net;

namespace Octokit.Tests.Clients
{
    public class MilestonesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>(), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Any<Dictionary<string, string>>(), options);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending });

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", new MilestoneRequest { SortDirection = SortDirection.Descending }, options);

                connection.Received().GetAll<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["state"] == "open"
                        && d["sort"] == "due_date"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new MilestonesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (MilestoneRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest()));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new MilestoneRequest(), null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new MilestoneRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new MilestoneRequest(), ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new MilestoneRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new MilestoneRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newMilestone = new NewMilestone("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Create("fake", "repo", newMilestone);

                connection.Received().Post<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones"),
                    newMilestone);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var milestoneUpdate = new MilestoneUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Update("fake", "repo", 42, milestoneUpdate);

                connection.Received().Patch<Milestone>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"),
                    milestoneUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewMilestone("title")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewMilestone("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

                client.Delete("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/milestones/42"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new MilestonesClient(connection);

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
                Assert.Throws<ArgumentNullException>(() => new MilestonesClient(null));
            }
        }

        [Fact]
        public void CanDeserializeMilestone()
        {
            const string milestoneResponseJson =
                "{\"url\": \"https://api.github.com/repos/octokit/octokit.net/milestones/6\", " +
                "\"html_url\": \"https://github.com/octokit/octokit.net/milestones/Pagination%20Support\", " +
                "\"labels_url\": \"https://api.github.com/repos/octokit/octokit.net/milestones/6/labels\", " +
                "\"id\": 1641780, " +
                "\"number\": 6," +
                "\"title\": \"Pagination Support\", " +
                "\"description\": \"This milestone tracks deploying pagination out across the entire API\", " +
                "\"creator\": { " +
                "\"login\": \"shiftkey\", " +
                "\"id\": 359239, " +
                "\"avatar_url\": \"https://avatars.githubusercontent.com/u/359239?v=3\", " +
                "\"gravatar_id\": \"\", " +
                "\"url\": \"https://api.github.com/users/shiftkey\", " +
                "\"html_url\": \"https://github.com/shiftkey\", " +
                "\"followers_url\": \"https://api.github.com/users/shiftkey/followers\", " +
                "\"following_url\": \"https://api.github.com/users/shiftkey/following{/other_user}\", " +
                "\"gists_url\": \"https://api.github.com/users/shiftkey/gists{/gist_id}\", " +
                "\"starred_url\": \"https://api.github.com/users/shiftkey/starred{/owner}{/repo}\", " +
                "\"subscriptions_url\": \"https://api.github.com/users/shiftkey/subscriptions\", " +
                "\"organizations_url\": \"https://api.github.com/users/shiftkey/orgs\", " +
                "\"repos_url\": \"https://api.github.com/users/shiftkey/repos\", " +
                "\"events_url\": \"https://api.github.com/users/shiftkey/events{/privacy}\", " +
                "\"received_events_url\": \"https://api.github.com/users/shiftkey/received_events\", " +
                "\"type\": \"User\", " +
                "\"site_admin\": true " +
                "}, " +
                "\"open_issues\": 23, " +
                "\"closed_issues\": 70, " +
                "\"state\": \"open\", " +
                "\"created_at\": \"2016-03-14T03:37:08Z\", " +
                "\"updated_at\": \"2016-06-14T00:15:05Z\", " +
                "\"due_on\": null, " +
                "\"closed_at\": null " +
                "} ";

            var httpResponse = new Response(
                HttpStatusCode.OK,
                milestoneResponseJson,
                new Dictionary<string, string>(),
                "application/json");
            var jsonPipeline = new JsonHttpPipeline();

            var response = jsonPipeline.DeserializeResponse<Milestone>(httpResponse);

            Assert.NotNull(response.Body);
            Assert.Equal(milestoneResponseJson, (string)response.HttpResponse.Body);
            Assert.Equal(new Uri("https://api.github.com/repos/octokit/octokit.net/milestones/6"), response.Body.Url);
            Assert.Equal(new Uri("https://github.com/octokit/octokit.net/milestones/Pagination%20Support"), response.Body.HtmlUrl);
            Assert.Equal(1641780, response.Body.Id);
            Assert.Equal(6, response.Body.Number);
            Assert.Equal("Pagination Support", response.Body.Title);
            Assert.Equal("This milestone tracks deploying pagination out across the entire API", response.Body.Description);
            // TODO: Test Creator
            Assert.Equal(23, response.Body.OpenIssues);
            Assert.Equal(70, response.Body.ClosedIssues);
            Assert.Equal(ItemState.Open, response.Body.State);
            Assert.Equal("3/14/2016 3:37:08 AM", response.Body.CreatedAt.UtcDateTime.ToString());
            Assert.Equal("6/14/2016 12:15:05 AM", response.Body.UpdatedAt.UtcDateTime.ToString());
            Assert.Null(response.Body.DueOn);
            Assert.Null(response.Body.ClosedAt);
        }
    }
}
