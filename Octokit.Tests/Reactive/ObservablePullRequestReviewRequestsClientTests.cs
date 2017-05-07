using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Reactive.Internal;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestReviewRequestsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservablePullRequestReviewRequestsClient(null));
            }
        }

        public class TheGetAlltMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                client.GetAll("owner", "name", 7);

                gitHubClient.Received().PullRequest.ReviewRequest.GetAll("owner", "name", 7);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                client.GetAll(42, 7);

                gitHubClient.Received().PullRequest.ReviewRequest.GetAll(42, 7);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAll("owner", "name", 7, options);

                gitHubClient.Received().PullRequest.ReviewRequest.GetAll("owner", "name", 7, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                client.GetAll(42, 7, options);

                gitHubClient.Received().PullRequest.ReviewRequest.GetAll(42, 7, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", 1));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", 1, ApiOptions.None));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(42, 1, null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                client.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                client.Create(42, 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Create(42, 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));
                Assert.Throws<ArgumentNullException>(() => client.Create(42, 1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "fakeRepoName", 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentException>(() => client.Create("fakeOwner", "", 1, pullRequestReviewRequest));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await client.Delete("owner", "name", 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Delete("owner", "name", 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await client.Delete(42, 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Delete(42, 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", "name", 1, null));
                Assert.Throws<ArgumentNullException>(() => client.Delete(42, 1, null));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1, pullRequestReviewRequest));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 1, pullRequestReviewRequest));
            }
        }
    }
}