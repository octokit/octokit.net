using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit
{
    public class DependencySubmissionClientTests
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
                var connection = Substitute.For<IApiConnection>();
                var client = new DependencySubmissionClient(connection);
                var expectedUrl = "repos/owner/name/dependency-graph/snapshots";

                client.Create("owner", "name", newDependencySnapshot);

                connection.Received(1).Post<DependencySnapshotSubmission>(Arg.Is<Uri>(u => u.ToString() == expectedUrl), Arg.Any<JsonObject>());
            }

            [Fact]
            public void PostsToTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new DependencySubmissionClient(connection);
                var expectedUrl = "repositories/1/dependency-graph/snapshots";

                client.Create(1, newDependencySnapshot);

                connection.Received(1).Post<DependencySnapshotSubmission>(Arg.Is<Uri>(u => u.ToString() == expectedUrl), Arg.Any<JsonObject>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new DependencySubmissionClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", newDependencySnapshot));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newDependencySnapshot));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new DependencySubmissionClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", newDependencySnapshot));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newDependencySnapshot));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new DependencySubmissionClient(null));
            }
        }
    }
}
