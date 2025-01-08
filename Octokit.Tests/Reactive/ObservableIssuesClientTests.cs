using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

public class ObservableIssuesClientTests
{
    public class TheGetMethod
    {
        [Fact]
        public void GetsFromClientIssueIssue()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Get("fake", "repo", 42);

            gitHubClient.Issue.Received().Get("fake", "repo", 42);
        }

        [Fact]
        public void GetsFromClientIssueIssueWithRepositoryId()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Get(1, 42);

            gitHubClient.Issue.Received().Get(1, 42);
        }

        [Fact]
        public void EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
            Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

            Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
            Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            var options = new ApiOptions();
            var request = new RepositoryIssueRequest();

            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (RepositoryIssueRequest)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null));

            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (RepositoryIssueRequest)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, request, null));

            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", options));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", options));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", request));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", request));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", request, options));
            Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", request, options));
        }

        [Fact]
        public void RequestsCorrectUrl()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.GetAllForRepository("fake", "repo");

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                Arg.Any<IDictionary<string, string>>());
        }

        [Fact]
        public void RequestsCorrectUrlWithRepositoryId()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.GetAllForRepository(1);

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                Arg.Any<IDictionary<string, string>>());
        }

        [Fact]
        public void RequestsCorrectUrlWithApiOptions()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            client.GetAllForRepository("fake", "repo", options);

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 6
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "desc"
                && d["page"] == "1"
                && d["per_page"] == "1"));
        }

        [Fact]
        public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            client.GetAllForRepository(1, options);

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 6
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "desc"
                && d["page"] == "1"
                && d["per_page"] == "1"));
        }

        [Fact]
        public void SendsAppropriateParameters()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.GetAllForRepository("fake", "repo", new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending
            });

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 4
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "asc"));
        }

        [Fact]
        public void SendsAppropriateParametersWithRepositoryId()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.GetAllForRepository(1, new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending
            });

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 4
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "asc"));
        }

        [Fact]
        public void SendsAppropriateParametersWithApiOptions()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            client.GetAllForRepository("fake", "repo", new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending
            }, options);

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 6
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "asc"
                && d["page"] == "1"
                && d["per_page"] == "1"));
        }

        [Fact]
        public void SendsAppropriateParametersWithRepositoryIdWithApiOptions()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            var options = new ApiOptions
            {
                PageCount = 1,
                StartPage = 1,
                PageSize = 1
            };

            client.GetAllForRepository(1, new RepositoryIssueRequest
            {
                SortDirection = SortDirection.Ascending
            }, options);

            gitHubClient.Connection.Received().Get<List<Issue>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/issues"),
                Arg.Is<IDictionary<string, string>>(d => d.Count == 6
                && d["filter"] == "assigned"
                && d["state"] == "open"
                && d["sort"] == "created"
                && d["direction"] == "asc"
                && d["page"] == "1"
                && d["per_page"] == "1"));
        }

        [Fact]
        public async Task ReturnsEveryPageOfIssues()
        {
            var firstPageUrl = new Uri("repos/fake/repo/issues", UriKind.Relative);
            var secondPageUrl = new Uri("https://example.com/page/2");
            var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
            var firstPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(firstPageLinks),
                new List<Issue>
                {
                    CreateIssue(1),
                    CreateIssue(2),
                    CreateIssue(3)
                }
            );
            var thirdPageUrl = new Uri("https://example.com/page/3");
            var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
            var secondPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(secondPageLinks),
                new List<Issue>
                {
                    CreateIssue(4),
                    CreateIssue(5),
                    CreateIssue(6)
                }
            );
            var lastPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponse(HttpStatusCode.OK),
                new List<Issue>
                {
                    CreateIssue(7)
                }
            );
            var gitHubClient = Substitute.For<IGitHubClient>();
            gitHubClient.Connection.Get<List<Issue>>(Arg.Is(firstPageUrl),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                    && d["direction"] == "desc"
                    && d["state"] == "open"
                    && d["sort"] == "created"
                    && d["filter"] == "assigned"))
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(lastPageResponse));
            var client = new ObservableIssuesClient(gitHubClient);

            var results = await client.GetAllForRepository("fake", "repo").ToArray();

            Assert.Equal(7, results.Length);
            Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
            Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
            Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
        }
    }

    public class TheGetAllForOwnedAndMemberRepositoriesMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            Assert.Throws<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories((ApiOptions)null));
            Assert.Throws<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories((IssueRequest)null));
            Assert.Throws<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories(null, new ApiOptions()));
            Assert.Throws<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories(new IssueRequest(), null));
        }

        [Fact]
        public async Task ReturnsEveryPageOfIssues()
        {
            var firstPageUrl = new Uri("user/issues", UriKind.Relative);
            var secondPageUrl = new Uri("https://example.com/page/2");
            var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
            var firstPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(firstPageLinks),
                new List<Issue>
                {
                    CreateIssue(1),
                    CreateIssue(2),
                    CreateIssue(3)
                }
            );
            var thirdPageUrl = new Uri("https://example.com/page/3");
            var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
            var secondPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(secondPageLinks),
                new List<Issue>
                {
                    CreateIssue(4),
                    CreateIssue(5),
                    CreateIssue(6)
                }
            );
            var lastPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponse(HttpStatusCode.OK),
                new List<Issue>
                {
                    CreateIssue(7)
                }
            );
            var gitHubClient = Substitute.For<IGitHubClient>();
            gitHubClient.Connection.Get<List<Issue>>(Arg.Is(firstPageUrl),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                    && d["direction"] == "desc"
                    && d["state"] == "open"
                    && d["sort"] == "created"
                    && d["filter"] == "assigned"))
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(lastPageResponse));
            var client = new ObservableIssuesClient(gitHubClient);

            var results = await client.GetAllForOwnedAndMemberRepositories().ToArray();

            Assert.Equal(7, results.Length);
            Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
            Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
            Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
        }
    }

    public class TheGetAllForOrganizationMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            var options = new ApiOptions();
            var request = new RepositoryIssueRequest();

            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization(null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization(null, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization(null, request));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization(null, request, options));

            Assert.Throws<ArgumentException>(() => client.GetAllForOrganization(""));
            Assert.Throws<ArgumentException>(() => client.GetAllForOrganization("", options));
            Assert.Throws<ArgumentException>(() => client.GetAllForOrganization("", request));
            Assert.Throws<ArgumentException>(() => client.GetAllForOrganization("", request, options));

            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization("org", (ApiOptions)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization("org", (IssueRequest)null));

            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization("org", null, options));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForOrganization("org", request, null));
        }

        [Fact]
        public async Task ReturnsEveryPageOfIssues()
        {
            var firstPageUrl = new Uri("orgs/test/issues", UriKind.Relative);
            var secondPageUrl = new Uri("https://example.com/page/2");
            var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
            var firstPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(firstPageLinks),
                new List<Issue>
                {
                    CreateIssue(1),
                    CreateIssue(2),
                    CreateIssue(3)
                }
            );
            var thirdPageUrl = new Uri("https://example.com/page/3");
            var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
            var secondPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(secondPageLinks),
                new List<Issue>
                {
                    CreateIssue(4),
                    CreateIssue(5),
                    CreateIssue(6)
                }
            );
            var lastPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponse(HttpStatusCode.OK),
                new List<Issue>
                {
                    CreateIssue(7)
                }
            );
            var gitHubClient = Substitute.For<IGitHubClient>();
            gitHubClient.Connection.Get<List<Issue>>(Arg.Is(firstPageUrl),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                    && d["direction"] == "desc"
                    && d["state"] == "open"
                    && d["sort"] == "created"
                    && d["filter"] == "assigned"))
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(lastPageResponse));

            var client = new ObservableIssuesClient(gitHubClient);

            var results = await client.GetAllForOrganization("test").ToArray();

            Assert.Equal(7, results.Length);
            Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
            Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
            Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [Fact]
        public void EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((IssueRequest)null));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(null, new ApiOptions()));
            Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(new IssueRequest(), null));
        }

        [Fact]
        public async Task ReturnsEveryPageOfIssues()
        {
            var firstPageUrl = new Uri("issues", UriKind.Relative);
            var secondPageUrl = new Uri("https://example.com/page/2");
            var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
            var firstPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(firstPageLinks),
                new List<Issue>
                {
                    CreateIssue(1),
                    CreateIssue(2),
                    CreateIssue(3)
                }
            );
            var thirdPageUrl = new Uri("https://example.com/page/3");
            var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
            var secondPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponseWithApiInfo(secondPageLinks),
                new List<Issue>
                {
                    CreateIssue(4),
                    CreateIssue(5),
                    CreateIssue(6)
                }
            );
            var lastPageResponse = new ApiResponse<List<Issue>>
            (
                CreateResponse(HttpStatusCode.OK),
                new List<Issue>
                {
                    CreateIssue(7)
                }
            );
            var gitHubClient = Substitute.For<IGitHubClient>();
            gitHubClient.Connection.Get<List<Issue>>(
                Arg.Is(firstPageUrl),
                Arg.Is<Dictionary<string, string>>(d => d.Count == 4
                    && d["direction"] == "desc"
                    && d["state"] == "open"
                    && d["sort"] == "created"
                    && d["filter"] == "assigned"))
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>())
                .Returns(Task.FromResult<IApiResponse<List<Issue>>>(lastPageResponse));
            var client = new ObservableIssuesClient(gitHubClient);

            var results = await client.GetAllForCurrent().ToArray();

            Assert.Equal(7, results.Length);
            Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
            Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
            Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
        }
    }

    public class TheCreateMethod
    {
        [Fact]
        public void CreatesFromClientIssueIssue()
        {
            var newIssue = new NewIssue("some title");
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Create("fake", "repo", newIssue);

            gitHubClient.Issue.Received().Create("fake", "repo", newIssue);
        }

        [Fact]
        public void CreatesFromClientIssueIssueWithRepositoryId()
        {
            var newIssue = new NewIssue("some title");
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Create(1, newIssue);

            gitHubClient.Issue.Received().Create(1, newIssue);
        }

        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewIssue("x")));
            Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewIssue("x")));
            Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

            Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

            Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewIssue("x")));
            Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewIssue("x")));
        }
    }

    public class TheUpdateMethod
    {
        [Fact]
        public void UpdatesClientIssueIssue()
        {
            var issueUpdate = new IssueUpdate();
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Update("fake", "repo", 42, issueUpdate);

            gitHubClient.Issue.Received().Update("fake", "repo", 42, issueUpdate);
        }

        [Fact]
        public void UpdatesClientIssueIssueWithRepositoryId()
        {
            var issueUpdate = new IssueUpdate();
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Update(1, 42, issueUpdate);

            gitHubClient.Issue.Received().Update(1, 42, issueUpdate);
        }

        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, new IssueUpdate()));
            Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, new IssueUpdate()));
            Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

            Assert.Throws<ArgumentNullException>(() => client.Update(1, 42, null));

            Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, new IssueUpdate()));
            Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, new IssueUpdate()));
        }
    }

    public class TheLockMethod
    {
        [Fact]
        public void LocksIssue()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.LockUnlock.Lock("fake", "repo", 42);

            gitHubClient.Issue.Received().LockUnlock.Lock("fake", "repo", 42);
        }

        [Fact]
        public void LocksIssueWithRepositoryId()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.LockUnlock.Lock(1, 42);

            gitHubClient.Issue.Received().LockUnlock.Lock(1, 42);
        }

        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.LockUnlock.Lock(null, "name", 42));
            Assert.Throws<ArgumentNullException>(() => client.LockUnlock.Lock("owner", null, 42));

            Assert.Throws<ArgumentException>(() => client.LockUnlock.Lock("", "name", 42));
            Assert.Throws<ArgumentException>(() => client.LockUnlock.Lock("owner", "", 42));
        }
    }

    public class TheUnlockMethod
    {
        [Fact]
        public void UnlocksIssue()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.LockUnlock.Unlock("fake", "repo", 42);

            gitHubClient.Issue.Received().LockUnlock.Unlock("fake", "repo", 42);
        }

        [Fact]
        public void UnlocksIssueWithRepositoryId()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.LockUnlock.Unlock(1, 42);

            gitHubClient.Issue.Received().LockUnlock.Unlock(1, 42);
        }

        [Fact]
        public void EnsuresNonNullArguments()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.LockUnlock.Unlock(null, "name", 42));
            Assert.Throws<ArgumentNullException>(() => client.LockUnlock.Unlock("owner", null, 42));

            Assert.Throws<ArgumentException>(() => client.LockUnlock.Unlock("", "name", 42));
            Assert.Throws<ArgumentException>(() => client.LockUnlock.Unlock("owner", "", 42));
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

    static Issue CreateIssue(long issueNumber)
    {
        var serializer = new SimpleJsonSerializer();
        return serializer.Deserialize<Issue>(@"{""number"": """ + issueNumber + @"""}");
    }

    static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
    {
        var apiInfo = new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
        var response = Substitute.For<IResponse>();
        response.ApiInfo.Returns(apiInfo);
        return response;
    }
}
