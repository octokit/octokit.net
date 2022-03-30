using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableActionsWorkflowJobsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableActionsWorkflowJobsClient(null));
            }
        }

        public class TheRerunMethod
        {
            [Fact]
            public async Task CallRerunOnClient()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                client.Rerun("fake", "repo", 123);

                connection.Received().Actions.Workflows.Jobs.Rerun("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Rerun(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Rerun("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Rerun("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Rerun("fake", "", 123));
            }
        }
    }
}
