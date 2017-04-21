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

                await client.GetAll("owner", "name", 7);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/requested_reviewers"),
                    null,
                    "application/vnd.github.black-cat-preview+json");
            }

            [Fact]
            public async Task EnsuresNotNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd"};
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                client.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);

                connection.Connection.Received().Post<PullRequestReviewRequestCreate>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest,
                    "application/vnd.github.black-cat-preview+json",
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));

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
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await client.Delete("owner", "name", 13, pullRequestReviewRequest);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest,
                    "application/vnd.github.black-cat-preview+json");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1, pullRequestReviewRequest));
            }
        }
    }
}
