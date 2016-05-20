using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryPagesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryPagesClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(githubClient);
                var options = new ApiOptions();

                client.GetAll("fake", "repo", options);
                githubClient.Received().Repository.Page.GetAll("fake", "repo", options);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "repo", new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "repo", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryPagesClient(githubClient);

                Assert.Throws<ArgumentException>(() => client.GetAll("", "repo", new ApiOptions()));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", new ApiOptions()));
            }
        }
    }
}
