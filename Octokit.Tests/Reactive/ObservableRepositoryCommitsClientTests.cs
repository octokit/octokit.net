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
