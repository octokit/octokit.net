using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Clients
{
    public class IssuesClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.Get("fake", "repo", 42);

                connection.Received().Get<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.Get(1, 42);

                connection.Received().Get<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((IssueRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(new IssueRequest(), null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForCurrent();

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForCurrent(new IssueRequest { SortDirection = SortDirection.Ascending });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["filter"] == "assigned"
                        && d["sort"] == "created"
                        && d["state"] == "open"
                        && d["direction"] == "asc"),
                    Args.ApiOptions);
            }
        }

        public class TheGetAllForOwnedAndMemberRepositoriesMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOwnedAndMemberRepositories((ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOwnedAndMemberRepositories((IssueRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOwnedAndMemberRepositories(null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOwnedAndMemberRepositories(new IssueRequest(), null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForOwnedAndMemberRepositories();

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "user/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                var options = new ApiOptions();
                var request = new RepositoryIssueRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, request, options));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", request, options));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (IssueRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", request, null));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForOrganization("fake");

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.GetAllForOrganization("fake", new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"),
                    Args.ApiOptions);
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new IssuesClient(Substitute.For<IApiConnection>());

                var options = new ApiOptions();
                var request = new RepositoryIssueRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (RepositoryIssueRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (RepositoryIssueRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, options));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, request, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request, options));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request, options));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                    Arg.Any<Dictionary<string, string>>(),
                    options);
            }

            [Fact]
            public async Task SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForRepository("fake", "repo", new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await client.GetAllForRepository(1, new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                });

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                }, options);

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"),
                    options);
            }

            [Fact]
            public async Task SendsAppropriateParametersWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, new RepositoryIssueRequest
                {
                    SortDirection = SortDirection.Ascending
                }, options);

                connection.Received().GetAll<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                        && d["state"] == "open"
                        && d["direction"] == "asc"
                        && d["sort"] == "created"
                        && d["filter"] == "assigned"),
                    options);
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

                connection.Received().Post<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"), newIssue);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var newIssue = new NewIssue("some title");
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.Create(1, newIssue);

                connection.Received().Post<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                    newIssue);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewIssue("x")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewIssue("x")));
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

                connection.Received().Patch<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42"), issueUpdate);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var issueUpdate = new IssueUpdate();
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.Update(1, 42, issueUpdate);

                connection.Received().Patch<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42"),
                    issueUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 1, new IssueUpdate()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 1, new IssueUpdate()));
            }
        }

        public class TheLockMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.LockUnlock.Lock("fake", "repo", 42);

                connection.Received().Put<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/lock"), Arg.Any<object>());
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.LockUnlock.Lock(1, 42);

                connection.Received().Put<Issue>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/lock"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LockUnlock.Lock(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LockUnlock.Lock("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.LockUnlock.Lock("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.LockUnlock.Lock("owner", "", 1));
            }
        }

        public class TheUnlockMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.LockUnlock.Unlock("fake", "repo", 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/42/lock"));
            }

            [Fact]
            public void PostsToCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                client.LockUnlock.Unlock(1, 42);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues/42/lock"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new IssuesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LockUnlock.Unlock(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LockUnlock.Unlock("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.LockUnlock.Unlock("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.LockUnlock.Unlock("owner", "", 1));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
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
            var httpResponse = CreateResponse(
                HttpStatusCode.OK,
                issueResponseJson);

            var jsonPipeline = new JsonHttpPipeline();

            var response = jsonPipeline.DeserializeResponse<Issue>(httpResponse);

            Assert.NotNull(response.Body);
            Assert.Equal(issueResponseJson, response.HttpResponse.Body);

            Assert.Equal(1, response.Body.Number);

            Assert.Equal("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1", response.Body.Url);
            Assert.Equal("https://github.com/octokit-net-test/public-repo-20131022050247078/issues/1", response.Body.HtmlUrl);
            Assert.Equal("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1/comments", response.Body.CommentsUrl);
            Assert.Equal("https://api.github.com/repos/octokit-net-test/public-repo-20131022050247078/issues/1/events", response.Body.EventsUrl);
        }
    }
}
