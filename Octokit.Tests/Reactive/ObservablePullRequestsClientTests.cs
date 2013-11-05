using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit;
using Octokit.Internal;
using Octokit.Reactive.Clients;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestsClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void GetsFromClientRepositoryPullRequest()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Get("fake", "repo", 42);

                gitHubClient.Repository.PullRequest.Received().Get("fake", "repo", 42);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestsClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get(null, "", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", null, 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void ReturnsEveryPageOfPullRequests()
            {
                var firstPageUrl = new Uri("repos/fake/repo/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 1},
                        new PullRequest {Number = 2},
                        new PullRequest {Number = 3},
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 4},
                        new PullRequest {Number = 5},
                        new PullRequest {Number = 6},
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 7},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetAsync<List<PullRequest>>(firstPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => firstPageResponse));
                gitHubClient.Connection.GetAsync<List<PullRequest>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => secondPageResponse));
                gitHubClient.Connection.GetAsync<List<PullRequest>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = client.GetForRepository("fake", "repo").ToArray().Wait();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.BodyAsObject[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.BodyAsObject[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.BodyAsObject[0].Number, results[6].Number);
            }

            [Fact]
            public void SendsAppropriateParameters()
            {
                var firstPageUrl = new Uri("repos/fake/repo/pulls", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 1},
                        new PullRequest {Number = 2},
                        new PullRequest {Number = 3},
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 4},
                        new PullRequest {Number = 5},
                        new PullRequest {Number = 6},
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<PullRequest>>
                {
                    BodyAsObject = new List<PullRequest>
                    {
                        new PullRequest {Number = 7},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetAsync<List<PullRequest>>(Arg.Is(firstPageUrl),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["head"] == "user:ref-name"
                        && d["state"] == "open"
                        && d["base"] == "fake_base_branch"), Arg.Any<string>())
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => firstPageResponse));
                gitHubClient.Connection.GetAsync<List<PullRequest>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => secondPageResponse));
                gitHubClient.Connection.GetAsync<List<PullRequest>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequest>>>(() => lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = client.GetForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-name", Base = "fake_base_branch" }).ToArray().Wait();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.BodyAsObject[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.BodyAsObject[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.BodyAsObject[0].Number, results[6].Number);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesFromClientRepositoryPullRequest()
            {
                var newPullRequest = new NewPullRequest("some title");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                client.Create("fake", "repo", newPullRequest);

                gitHubClient.Repository.PullRequest.Received().Create("fake", "repo", newPullRequest);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create(null, "name", new NewPullRequest("title")));
                AssertEx.Throws<ArgumentException>(async () => await
                    client.Create("", "name", new NewPullRequest("x")));
                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create("owner", null, new NewPullRequest("x")));
                AssertEx.Throws<ArgumentException>(async () => await
                    client.Create("owner", "", new NewPullRequest("x")));
                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create("owner", "name", null));
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
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create(null, "name", new NewPullRequest("title")));
                AssertEx.Throws<ArgumentException>(async () => await
                    client.Create("", "name", new NewPullRequest("x")));
                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create("owner", null, new NewPullRequest("x")));
                AssertEx.Throws<ArgumentException>(async () => await
                    client.Create("owner", "", new NewPullRequest("x")));
                AssertEx.Throws<ArgumentNullException>(async () => await
                    client.Create("owner", "name", null));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new PullRequestsClient(null));
            }
        }

        static ApiInfo CreateApiInfo(IDictionary<string, Uri> links)
        {
            return new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
        }
    }
}
