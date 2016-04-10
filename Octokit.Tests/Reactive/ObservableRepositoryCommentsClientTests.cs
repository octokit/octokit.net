using NSubstitute;
using Octokit.Reactive;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryCommentsClientTests
    {
        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);
                var options = new ApiOptions();

                client.GetAllForRepository("fake", "repo", options);
                githubClient.Received().Repository.Comment.GetAllForRepository("fake", "repo", options);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));
            }
        }

        public class TheGetForCommitMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);
                var options = new ApiOptions();

                client.GetAllForCommit("fake", "repo", "sha", options);
                githubClient.Received().Repository.Comment.GetAllForCommit("fake", "repo", "sha", options);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCommentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit(null, "name", "sha", new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("", "name", "sha", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", null, "sha", new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "", "sha", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", null, new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAllForCommit("owner", "name", "", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCommit("owner", "name", "sha", null));
            }
        }
    }
}
