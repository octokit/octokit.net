using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PullRequestReviewRequestsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PullRequestReviewRequestsClient(null));
            }
        }

        public class TheGetAlltMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await client.Get("owner", "name", 7);

                connection.Received().Get<RequestedReviews>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/requested_reviewers"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await client.Get(42, 7);

                connection.Received().Get<RequestedReviews>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/42/pulls/7/requested_reviewers"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                client.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);

                connection.Received().Post<PullRequest>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);
                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                client.Create(42, 13, pullRequestReviewRequest);

                connection.Received().Post<PullRequest>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/42/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "fakeRepoName", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fakeOwner", "", 1, pullRequestReviewRequest));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await client.Delete("owner", "name", 13, pullRequestReviewRequest);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest);
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await client.Delete(43, 13, pullRequestReviewRequest);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/43/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = PullRequestReviewRequest.ForReviewers(fakeReviewers);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "name", 1, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1, pullRequestReviewRequest));
            }
        }
    }
}
