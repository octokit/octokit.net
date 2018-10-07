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
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await client.GetAll(42, 7);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/42/pulls/7/requested_reviewers"),
                    null,
                    "application/vnd.github.black-cat-preview+json");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAll("owner", "name", 7, options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/7/requested_reviewers"),
                    null,
                    "application/vnd.github.black-cat-preview+json",
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                await client.GetAll(42, 7, options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/42/pulls/7/requested_reviewers"),
                    null,
                    "application/vnd.github.black-cat-preview+json",
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));


                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(42, 1, null));
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
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                client.Create("fakeOwner", "fakeRepoName", 13, pullRequestReviewRequest);

                connection.Connection.Received().Post<PullRequest>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fakeOwner/fakeRepoName/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest,
                    "application/vnd.github.black-cat-preview+json",
                    null);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);
                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                client.Create(42, 13, pullRequestReviewRequest);

                connection.Connection.Received().Post<PullRequest>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/42/pulls/13/requested_reviewers"),
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
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await client.Delete("owner", "name", 13, pullRequestReviewRequest);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/pulls/13/requested_reviewers"),
                    pullRequestReviewRequest,
                    "application/vnd.github.black-cat-preview+json");
            }

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PullRequestReviewRequestsClient(connection);

                IReadOnlyList<string> fakeReviewers = new List<string> { "zxc", "asd" };
                var pullRequestReviewRequest = new PullRequestReviewRequest(fakeReviewers);

                await client.Delete(43, 13, pullRequestReviewRequest);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/43/pulls/13/requested_reviewers"),
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
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1, pullRequestReviewRequest));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1, pullRequestReviewRequest));
            }
        }
    }
}
