using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Repository.PullRequest.Received().Get("fake", "repo", 42);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Get(1, 42);

                gitHubClient.Repository.PullRequest.Received().Get(1, 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Received().PullRequest.GetAllForRepository("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Received().PullRequest.GetAllForRepository(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", options);

                gitHubClient.Received().PullRequest.GetAllForRepository("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository(1, options);

                gitHubClient.Received().PullRequest.GetAllForRepository(1, options);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var pullRequestRequest = new PullRequestRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository("fake", "repo", pullRequestRequest);

                gitHubClient.Received().PullRequest.GetAllForRepository("fake", "repo", pullRequestRequest, Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var pullRequestRequest = new PullRequestRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository(1, pullRequestRequest);

                gitHubClient.Received().PullRequest.GetAllForRepository(1, pullRequestRequest, Args.ApiOptions);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var pullRequestRequest = new PullRequestRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository("fake", "repo", pullRequestRequest, options);

                gitHubClient.Received().PullRequest.GetAllForRepository("fake", "repo", pullRequestRequest, options);
            }

            [Fact]
            public void SendsAppropriateParametersWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var pullRequestRequest = new PullRequestRequest { SortDirection = SortDirection.Descending };
                client.GetAllForRepository(1, pullRequestRequest, options);

                gitHubClient.Received().PullRequest.GetAllForRepository(1, pullRequestRequest, options);
            }

            [Fact]
            public async Task ReturnsEveryPageOfPullRequests()
            {
                var firstPageUrl = new Uri("repos/fake/repo/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(1),
                        new PullRequest(2),
                        new PullRequest(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(4),
                        new PullRequest(5),
                        new PullRequest(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequest>
                    {
                        new PullRequest(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequest>>(firstPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = await client.GetAllForRepository("fake", "repo").ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public async Task ReturnsEveryPageOfPullRequestsWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(1),
                        new PullRequest(2),
                        new PullRequest(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(4),
                        new PullRequest(5),
                        new PullRequest(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequest>
                    {
                        new PullRequest(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequest>>(firstPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = await client.GetAllForRepository(1).ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public async Task SendsAppropriateParametersMulti()
            {
                var firstPageUrl = new Uri("repos/fake/repo/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(1),
                        new PullRequest(2),
                        new PullRequest(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(4),
                        new PullRequest(5),
                        new PullRequest(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequest>
                    {
                        new PullRequest(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequest>>(Arg.Is(firstPageUrl),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = await client.GetAllForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-name", Base = "fake_base_branch" }).ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public async Task SendsAppropriateParametersMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(1),
                        new PullRequest(2),
                        new PullRequest(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequest>
                    {
                        new PullRequest(4),
                        new PullRequest(5),
                        new PullRequest(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequest>
                    {
                        new PullRequest(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequest>>(Arg.Is(firstPageUrl),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 5
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"
                        && d["sort"] == "created"
                        && d["direction"] == "desc"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequest>>>(lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = await client.GetAllForRepository(1, new PullRequestRequest { Head = "user:ref-name", Base = "fake_base_branch" }).ToArray();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new PullRequestRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new PullRequestRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (PullRequestRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new PullRequestRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new PullRequestRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", new PullRequestRequest(), null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (PullRequestRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, new PullRequestRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new PullRequestRequest()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new PullRequestRequest()));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new PullRequestRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new PullRequestRequest(), ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesFromClientRepositoryPullRequest()
            {
                var newPullRequest = new NewPullRequest("some title", "branch:name", "branch:name");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Create("fake", "repo", newPullRequest);

                gitHubClient.Repository.PullRequest.Received().Create("fake", "repo", newPullRequest);
            }

            [Fact]
            public void CreatesFromClientRepositoryPullRequestWithRepositoryId()
            {
                var newPullRequest = new NewPullRequest("some title", "branch:name", "branch:name");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Create(1, newPullRequest);

                gitHubClient.Repository.PullRequest.Received().Create(1, newPullRequest);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", new NewPullRequest("title", "ref", "ref2")));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void UpdatesClientRepositoryPullRequest()
            {
                var pullRequestUpdate = new PullRequestUpdate();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Update("fake", "repo", 42, pullRequestUpdate);

                gitHubClient.Repository.PullRequest.Received().Update("fake", "repo", 42, pullRequestUpdate);
            }

            [Fact]
            public void UpdatesClientRepositoryPullRequestWithRepositoryId()
            {
                var pullRequestUpdate = new PullRequestUpdate();
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Update(1, 42, pullRequestUpdate);

                gitHubClient.Repository.PullRequest.Received().Update(1, 42, pullRequestUpdate);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 42, new PullRequestUpdate()));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 42, new PullRequestUpdate()));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.Update(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", 42, new PullRequestUpdate()));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", 42, new PullRequestUpdate()));
            }
        }

        public class TheMergeMethod
        {
            [Fact]
            public void MergesPullRequest()
            {
                var mergePullRequest = new MergePullRequest { CommitMessage = "fake commit message" };
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Merge("fake", "repo", 42, mergePullRequest);

                gitHubClient.Repository.PullRequest.Received().Merge("fake", "repo", 42, mergePullRequest);
            }

            [Fact]
            public void MergesPullRequestWithRepositoryId()
            {
                var mergePullRequest = new MergePullRequest { CommitMessage = "fake commit message" };
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Merge(1, 42, mergePullRequest);

                gitHubClient.Repository.PullRequest.Received().Merge(1, 42, mergePullRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Merge(null, "name", 42, new MergePullRequest { CommitMessage = "message" }));
                Assert.Throws<ArgumentNullException>(() => client.Merge("owner", null, 42, new MergePullRequest { CommitMessage = "message" }));
                Assert.Throws<ArgumentNullException>(() => client.Merge("owner", "name", 42, null));

                Assert.Throws<ArgumentNullException>(() => client.Merge(1, 42, null));

                Assert.Throws<ArgumentException>(() => client.Merge("", "name", 42, new MergePullRequest { CommitMessage = "message" }));
                Assert.Throws<ArgumentException>(() => client.Merge("owner", "", 42, new MergePullRequest { CommitMessage = "message" }));
            }
        }

        public class TheMergedMethod
        {
            [Fact]
            public void PullRequestMerged()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Merged("fake", "repo", 42);

                gitHubClient.Repository.PullRequest.Received().Merged("fake", "repo", 42);
            }

            [Fact]
            public void PullRequestMergedWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Merged(1, 42);

                gitHubClient.Repository.PullRequest.Received().Merged(1, 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Merged(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Merged("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Merged("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Merged("owner", "", 1));
            }
        }

        public class TheCommitsMethod
        {
            [Fact]
            public async Task FetchesAllCommitsForPullRequest()
            {
                var commit = new PullRequestCommit("123ABC", null, null, null, null, null, Enumerable.Empty<GitReference>(), null, null);
                var expectedUrl = "repos/fake/repo/pulls/42/commits";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestCommit>> response = new ApiResponse<List<PullRequestCommit>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestCommit> { commit }
                );
                connection.Get<List<PullRequestCommit>>(Args.Uri, null)
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var commits = await client.Commits("fake", "repo", 42).ToList();

                Assert.Single(commits);
                Assert.Same(commit, commits[0]);
                connection.Received().Get<List<PullRequestCommit>>(new Uri(expectedUrl, UriKind.Relative), null);
            }

            [Fact]
            public async Task FetchesAllCommitsForPullRequestWithRepositoryId()
            {
                var commit = new PullRequestCommit("123ABC", null, null, null, null, null, Enumerable.Empty<GitReference>(), null, null);
                var expectedUrl = "repositories/1/pulls/42/commits";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestCommit>> response = new ApiResponse<List<PullRequestCommit>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestCommit> { commit }
                );
                connection.Get<List<PullRequestCommit>>(Args.Uri, null)
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var commits = await client.Commits(1, 42).ToList();

                Assert.Single(commits);
                Assert.Same(commit, commits[0]);
                connection.Received().Get<List<PullRequestCommit>>(new Uri(expectedUrl, UriKind.Relative), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Commits(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Commits("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Commits("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Commits("owner", "", 1));
            }
        }

        public class TheFilesMethod
        {
            [Fact]
            public async Task FetchesAllFilesForPullRequest()
            {
                var file = new PullRequestFile(null, null, null, 0, 0, 0, null, null, null, null, null);
                var expectedUrl = "repos/fake/repo/pulls/42/files";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestFile>> response = new ApiResponse<List<PullRequestFile>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestFile> { file }
                );
                connection.Get<List<PullRequestFile>>(Args.Uri, Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var files = await client.Files("fake", "repo", 42).ToList();

                Assert.Single(files);
                Assert.Same(file, files[0]);
                connection.Received().Get<List<PullRequestFile>>(new Uri(expectedUrl, UriKind.Relative), Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public async Task FetchesAllFilesForPullRequestWithRepositoryId()
            {
                var file = new PullRequestFile(null, null, null, 0, 0, 0, null, null, null, null, null);
                var expectedUrl = "repositories/1/pulls/42/files";
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestFile>> response = new ApiResponse<List<PullRequestFile>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestFile> { file }
                );
                connection.Get<List<PullRequestFile>>(Args.Uri, Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var files = await client.Files(1, 42).ToList();

                Assert.Single(files);
                Assert.Same(file, files[0]);
                connection.Received().Get<List<PullRequestFile>>(new Uri(expectedUrl, UriKind.Relative), Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Files(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Files("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Files("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Files("owner", "", 1));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PullRequestsClient(null));
            }
        }

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }
    }
}
