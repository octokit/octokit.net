using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;
using System.Reactive.Linq;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryCommitsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryCommitsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();
                var request = new CommitRequest();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", request, options).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();
                var request = new CommitRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", request, null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();
                var request = new CommitRequest();

                client.GetAll("fake", "repo", request, options);
                githubClient.Received().Repository.Commit.GetAll("fake", "repo", request, options);
            }
        }

        public class TheGetSha1Method
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("", "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "name", "").ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1(null, "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", null, "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSha1("owner", "name", null).ToTask());
            }

            [Fact]
            public async Task GetsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                await client.GetSha1("owner", "name", "reference");

                gitHubClient
                    .Received(1)
                    .Repository
                    .Commit
                    //.GetSha1("owner1", "name", "reference");
                    .GetAll(1);
            }
        }

        public class ThePullRequestsMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("", "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("owner", "", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.PullRequests("owner", "name", "").ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests(null, "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests("owner", null, "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.PullRequests("owner", "name", null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();

                client.PullRequests("fake", "repo", "reference", options);
                githubClient.Received().Repository.Commit.PullRequests("fake", "repo", "reference", options);
            }
        }

        public class TheBranchesWhereHeadMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.BranchesWhereHead("", "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.BranchesWhereHead("owner", "", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.BranchesWhereHead("owner", "name", "").ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.BranchesWhereHead(null, "name", "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.BranchesWhereHead("owner", null, "reference").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.BranchesWhereHead("owner", "name", null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();

                client.BranchesWhereHead("fake", "repo", "reference", options);
                githubClient.Received().Repository.Commit.BranchesWhereHead("fake", "repo", "reference", options);
            }
        }
    }
}
