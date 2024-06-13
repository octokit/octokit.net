using NSubstitute;
using Octokit.Reactive;
using System;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableDependencySubmissionClientTests
    {
        public class TheCreateMethod
        {
            private NewDependencySnapshot newDependencySnapshot = new NewDependencySnapshot(
                    1,
                    "sha",
                    "ref",
                    "scanned",
                    new NewDependencySnapshotJob("runId", "jobCorrelator"),
                    new NewDependencySnapshotDetector("detectorName", "detectorVersion", "detectorUrl"));

            [Fact]
            public void PostsToTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableDependencySubmissionClient(gitHubClient);

                client.Create("fake", "repo", newDependencySnapshot);

                gitHubClient.DependencyGraph.DependencySubmission.Received().Create("fake", "repo", newDependencySnapshot);
            }

            [Fact]
            public void PostsToTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableDependencySubmissionClient(gitHubClient);

                client.Create(1, newDependencySnapshot);

                gitHubClient.DependencyGraph.DependencySubmission.Received().Create(1, newDependencySnapshot);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableDependencySubmissionClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", newDependencySnapshot));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, newDependencySnapshot));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new ObservableDependencySubmissionClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentException>(() => client.Create("", "name", newDependencySnapshot));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", newDependencySnapshot));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableDependencySubmissionClient(null));
            }
        }
    }
}
