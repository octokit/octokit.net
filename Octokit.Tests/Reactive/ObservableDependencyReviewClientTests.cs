using NSubstitute;
using Octokit.Reactive;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableDependencyReviewClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableDependencyReviewClient(gitHubClient);

                client.GetAll("fake", "repo", "base", "head");

                gitHubClient.Received().DependencyGraph.DependencyReview.GetAll("fake", "repo", "base", "head");
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableDependencyReviewClient(gitHubClient);

                client.GetAll(1, "base", "head");

                gitHubClient.Received().DependencyGraph.DependencyReview.GetAll(1, "base", "head");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableDependencyReviewClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", "base", "head"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, "base", "head"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", null, "head"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", "base", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, null, "head"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, "base", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new ObservableDependencyReviewClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", "base", "head"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", "base", "head"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "name", "", "head"));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "name", "base", ""));

                Assert.Throws<ArgumentException>(() => client.GetAll(1, "", "head"));
                Assert.Throws<ArgumentException>(() => client.GetAll(1, "base", ""));
            }
        }
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableDependencyReviewClient(null));
            }
        }
    }
}
