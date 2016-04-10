using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryCommitsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryCommitsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();
                var request = new CommitRequest();

                Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", request, options).ToTask());
                Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", request, options).ToTask());
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(githubClient);
                var options = new ApiOptions();
                var request = new CommitRequest();

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", request, options).ToTask());
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, request, options).ToTask());
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null, options).ToTask());
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", request, null).ToTask());

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
            public void EnsuresNonEmptyArguments()
            {
                var client = new ObservableRepositoryCommitsClient(Substitute.For<IGitHubClient>());

                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("", "name", "reference").ToTask());
                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "", "reference").ToTask());
                Assert.ThrowsAsync<ArgumentException>(() => client.GetSha1("owner", "name", "").ToTask());
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
            public void GetsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommitsClient(gitHubClient);

                client.GetSha1("owner", "name", "reference");

                gitHubClient
                    .Received()
                    .Repository
                    .Commit
                    .GetSha1("owner", "name", "reference");
            }
        }
    }
}
