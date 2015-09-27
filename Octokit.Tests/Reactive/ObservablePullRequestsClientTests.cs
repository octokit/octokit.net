using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", null, 1).ToTask());
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
                    new Response(),
                    new List<PullRequest>
                    {
                        new PullRequest(7)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequest>>(firstPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = client.GetAllForRepository("fake", "repo").ToArray().Wait();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
            }

            [Fact]
            public void SendsAppropriateParameters()
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
                    new Response(),
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
                        && d["direction"] == "desc"), Arg.Any<string>())
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequest>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<PullRequest>>>(() => lastPageResponse));
                var client = new ObservablePullRequestsClient(gitHubClient);

                var results = client.GetAllForRepository("fake", "repo", new PullRequestRequest { Head = "user:ref-name", Base = "fake_base_branch" }).ToArray().Wait();

                Assert.Equal(7, results.Length);
                Assert.Equal(firstPageResponse.Body[0].Number, results[0].Number);
                Assert.Equal(secondPageResponse.Body[1].Number, results[4].Number);
                Assert.Equal(lastPageResponse.Body[0].Number, results[6].Number);
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
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("", "name", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", "name", null).ToTask());
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

                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create(null, "name", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("", "name", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", null, new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() =>
                    client.Create("owner", "", new NewPullRequest("title", "ref", "ref2")).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Create("owner", "name", null).ToTask());
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
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Merge(null, "name", 42, new MergePullRequest { CommitMessage = "message" }));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Merge("owner", null, 42, new MergePullRequest { CommitMessage = "message" }));
                await Assert.ThrowsAsync<ArgumentNullException>(() =>
                    client.Merge("owner", "name", 42, null));
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
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merged(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merged("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Merged(null, "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Merged("", null, 1));
            }
        }

        public class TheCommitsMethod
        {
            [Fact]
            public async Task FetchesAllCommitsForPullRequest()
            {
                var commit = new PullRequestCommit(null, null, null, null, null, Enumerable.Empty<GitReference>(), null, null);
                var expectedUrl = string.Format("repos/fake/repo/pulls/42/commits");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestCommit>> response = new ApiResponse<List<PullRequestCommit>>
                (
                    new Response(),
                    new List<PullRequestCommit> { commit }
                );
                connection.Get<List<PullRequestCommit>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var commits = await client.Commits("fake", "repo", 42).ToList();

                Assert.Equal(1, commits.Count);
                Assert.Same(commit, commits[0]);
                connection.Received().Get<List<PullRequestCommit>>(new Uri(expectedUrl, UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Commits(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Commits("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Commits(null, "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Commits("", null, 1));
            }
        }

        public class TheFilesMethod
        {
            [Fact]
            public async Task FetchesAllFilesForPullRequest()
            {
                var file = new PullRequestFile(null, null, null, 0, 0, 0, null, null, null, null);
                var expectedUrl = string.Format("repos/fake/repo/pulls/42/files");
                var gitHubClient = Substitute.For<IGitHubClient>();
                var connection = Substitute.For<IConnection>();
                IApiResponse<List<PullRequestFile>> response = new ApiResponse<List<PullRequestFile>>
                (
                    new Response(),
                    new List<PullRequestFile> { file }
                );
                connection.Get<List<PullRequestFile>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));
                gitHubClient.Connection.Returns(connection);
                var client = new ObservablePullRequestsClient(gitHubClient);

                var files = await client.Files("fake", "repo", 42).ToList();

                Assert.Equal(1, files.Count);
                Assert.Same(file, files[0]);
                connection.Received().Get<List<PullRequestFile>>(new Uri(expectedUrl, UriKind.Relative), null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Files(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Files("owner", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Files("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Files("owner", "", 1));
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

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }
    }
}
