using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

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
        public async Task EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "", 1).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", null, 1).ToTask());
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        [Fact]
        public async Task EnsuresArgumentsNotNull()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            var options = new ApiOptions();
            var request = new RepositoryIssueRequest();

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name").ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", options).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name").ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", options).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, options).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "").ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", options).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (RepositoryIssueRequest)null).ToTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, options).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null).ToTask());
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
                new Response(),
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
                    && d["filter"] == "assigned"), Arg.Any<string>())
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string,string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => lastPageResponse));
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
        public async Task EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            await Assert.ThrowsAsync<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories((ApiOptions)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories((IssueRequest)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories(null, new ApiOptions()).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => client.GetAllForOwnedAndMemberRepositories(new IssueRequest(), null).ToTask());
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
                new Response(),
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
                    && d["filter"] == "assigned"),
                Arg.Any<string>())
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string,string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => lastPageResponse));
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
        public async Task EnsuresArgumentsNotNull()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            var options = new ApiOptions();
            var request = new RepositoryIssueRequest();

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, options).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, request).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("").ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", options).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", request).ToTask());
            await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", request, options).ToTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ApiOptions)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (IssueRequest)null).ToTask());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", null, options).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", request, null).ToTask());
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
                new Response(),
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
                    && d["filter"] == "assigned"), Arg.Any<string>())
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string,string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => lastPageResponse));
            
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
        public async Task EnsuresNonNullArguments()
        {
            var client = new ObservableIssuesClient(Substitute.For<IGitHubClient>());

            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((IssueRequest)null).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null, new ApiOptions()).ToTask());
            await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(new IssueRequest(), null).ToTask());
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
                new Response(),
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
                    && d["filter"] == "assigned"), Arg.Any<string>())
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => firstPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(secondPageUrl, Arg.Any<Dictionary<string, string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => secondPageResponse));
            gitHubClient.Connection.Get<List<Issue>>(thirdPageUrl, Arg.Any<Dictionary<string, string>>(), null)
                .Returns(Task.Factory.StartNew<IApiResponse<List<Issue>>>(() => lastPageResponse));
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
        public void EnsuresArgumentsNotNull()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewIssue("title")));
            Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewIssue("x")));
            Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewIssue("x")));
            Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewIssue("x")));
            Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
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
        public void EnsuresArgumentsNotNull()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, new IssueUpdate()));
            Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, new IssueUpdate()));
            Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, new IssueUpdate()));
            Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, new IssueUpdate()));
            Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));
        }
    }

    public class TheLockMethod
    {
        [Fact]
        public void LocksIssue()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Lock("fake", "repo", 42);
            gitHubClient.Issue.Received().Lock("fake", "repo", 42);
        }

        [Fact]
        public void EnsuresArgumentsNotNull()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Lock(null, "name", 42));
            Assert.Throws<ArgumentException>(() => client.Lock("", "name", 42));
            Assert.Throws<ArgumentNullException>(() => client.Lock("owner", null, 42));
            Assert.Throws<ArgumentException>(() => client.Lock("owner", "", 42));
        }
    }

    public class TheUnlockMethod
    {
        [Fact]
        public void UnlocksIssue()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            client.Unlock("fake", "repo", 42);
            gitHubClient.Issue.Received().Unlock("fake", "repo", 42);
        }

        [Fact]
        public void EnsuresArgumentsNotNull()
        {
            var gitHubClient = Substitute.For<IGitHubClient>();
            var client = new ObservableIssuesClient(gitHubClient);

            Assert.Throws<ArgumentNullException>(() => client.Unlock(null, "name", 42));
            Assert.Throws<ArgumentException>(() => client.Unlock("", "name", 42));
            Assert.Throws<ArgumentNullException>(() => client.Unlock("owner", null, 42));
            Assert.Throws<ArgumentException>(() => client.Unlock("owner", "", 42));
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

    static Issue CreateIssue(int issueNumber)
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
