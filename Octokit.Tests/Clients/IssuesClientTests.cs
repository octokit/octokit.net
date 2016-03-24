using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class IssuesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.Get("fake", "repo", 42);

                connection.Received().Get<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "issues"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForCurrent(new IssueRequest { SortDirection = SortDirection.Ascending });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["filter"] == "assigned"
                        && d["sort"] == "created"
                        && d["state"] == "open"
                        && d["direction"] == "asc"));
            }
        }

        public class TheGetAllForOwnedAndMemberRepositoriesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForOwnedAndMemberRepositories();

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "user/issues"),
                    Arg.Any<Dictionary<string, string>>());
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForRepository("fake", "repo", new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new RepositoryIssueRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", new RepositoryIssueRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new RepositoryIssueRequest()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", new RepositoryIssueRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var newIssue = new NewIssue("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.Create("fake", "repo", newIssue);

                connection.Received().Post<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    newIssue);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewIssue("title")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var issueUpdate = new IssueUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.Update("fake", "repo", 42, issueUpdate);

                connection.Received().Patch<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42"),
                    issueUpdate);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 1, null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new IssuesClient(null));
            }
        }

        [Fact]
        public void CanDeserializeIssue()
        {
            const string issueResponseJson = "{\"url\":\"https://api.github.com/repos/octokit-net-test/public-repo-" +
                "20131022050247078/issues/1\",\"labels_url\":\"https://api.github.com/repos/octokit-net-test/publ" +
                "ic-repo-20131022050247078/issues/1/labels{/name}\",\"comments_url\":\"https://api.github.com/rep" +
                "os/octokit-net-test/public-repo-20131022050247078/issues/1/comments\",\"events_url\":\"https://a" +
                "pi.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1/events\",\"html_url" +
                "\":\"https://github.com/octokit-net-test/public-repo-20131022050247078/issues/1\",\"id\":2139915" +
                "4,\"number\":1,\"title\":\"A test issue\",\"user\":{\"login\":\"octokit-net-test\",\"id\":558045" +
                "0,\"avatar_url\":\"https://2.gravatar.com/avatar/20724e5085dcbe92e660a61d282f665c?d=https%3A%2F%" +
                "2Fidenticons.github.com%2Fb21d03168ecd65836d6407e4cdd61e0c.png\",\"gravatar_id\":\"20724e5085dcb" +
                "e92e660a61d282f665c\",\"url\":\"https://api.github.com/users/octokit-net-test\",\"html_url\":\"h" +
                "ttps://github.com/octokit-net-test\",\"followers_url\":\"https://api.github.com/users/octokit-ne" +
                "t-test/followers\",\"following_url\":\"https://api.github.com/users/octokit-net-test/following{/" +
                "other_user}\",\"gists_url\":\"https://api.github.com/users/octokit-net-test/gists{/gist_id}\",\"" +
                "starred_url\":\"https://api.github.com/users/octokit-net-test/starred{/owner}{/repo}\",\"subscri" +
                "ptions_url\":\"https://api.github.com/users/octokit-net-test/subscriptions\",\"organizations_url" +
                "\":\"https://api.github.com/users/octokit-net-test/orgs\",\"repos_url\":\"https://api.github.com" +
                "/users/octokit-net-test/repos\",\"events_url\":\"https://api.github.com/users/octokit-net-test/e" +
                "vents{/privacy}\",\"received_events_url\":\"https://api.github.com/users/octokit-net-test/receiv" +
                "ed_events\",\"type\":\"User\",\"site_admin\":false},\"labels\":[],\"state\":\"open\",\"assignee" +
                "\":null,\"milestone\":null,\"comments\":0,\"created_at\":\"2013-10-22T17:02:48Z\",\"updated_at\"" +
                ":\"2013-10-22T17:02:48Z\",\"closed_at\":null,\"body\":\"A new unassigned issue\",\"closed_by\":null}";
            var httpResponse = new Response(
                HttpStatusCode.OK,
                issueResponseJson,
                new Dictionary<string, string>(),
                "application/json");

            var jsonPipeline = new JsonHttpPipeline();

            var response = jsonPipeline.DeserializeResponse<Issue>(httpResponse);

            Assert.NotNull(response.Body);
            Assert.Equal(issueResponseJson, response.HttpResponse.Body);

            Assert.Equal(1, response.Body.Number);

            Assert.Equal(new Uri("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1"), response.Body.Url);
            Assert.Equal(new Uri("https://github.com/octokit-net-test/public-repo-20131022050247078/issues/1"), response.Body.HtmlUrl);
            Assert.Equal(new Uri("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1/comments"), response.Body.CommentsUrl);
            Assert.Equal(new Uri("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1/events"), response.Body.EventsUrl);
        }
    }
}
