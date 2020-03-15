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

                client.Get("owner", "name", 7);

                gitHubClient.Received().PullRequest.ReviewRequest.Get("owner", "name", 7);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                client.Get(42, 7);

                gitHubClient.Received().PullRequest.ReviewRequest.Get(42, 7);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));

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
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                client.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                client.Create(42, 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Create(42, 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

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
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await client.Delete("owner", "name", 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Delete("owner", "name", 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await client.Delete(42, 13, pullRequestReviewRequest);

                gitHubClient.Received().PullRequest.ReviewRequest.Delete(42, 13, pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewRequestsClient(gitHubClient);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

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
