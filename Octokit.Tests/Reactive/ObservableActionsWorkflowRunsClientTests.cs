using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableActionsWorkflowRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableActionsWorkflowRunsClient(null));
            }
        }

        public class TheApproveMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.Approve("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.Approve("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Approve(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Approve("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Approve("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Approve("fake", "", 123));
            }
        }

        public class TheCancelMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.Cancel("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.Cancel("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Cancel(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Cancel("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Cancel("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Cancel("fake", "", 123));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.Delete("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.Delete("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Delete("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Delete("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Delete("fake", "", 123));
            }
        }

        public class TheDeleteLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.DeleteLogs("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.DeleteLogs("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.DeleteLogs(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.DeleteLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.DeleteLogs("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.DeleteLogs("fake", "", 123));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.List("fake", "repo");

                connection.Received().Actions.Workflows.Runs.List("fake", "repo");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest();

                client.List("fake", "repo", request);

                connection.Received().Actions.Workflows.Runs.List("fake", "repo", request);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithOptions()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                client.List("fake", "repo", request, options);

                connection.Received().Actions.Workflows.Runs.List("fake", "repo", request, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null));

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", request));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, request));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", request, options));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, request, options));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", null, options));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", request, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.List("", "repo"));
                Assert.Throws<ArgumentException>(() => client.List("fake", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.Get("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.Get("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", 123));
            }
        }

        public class TheGetLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.GetLogs("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.GetLogs("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetLogs(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.GetLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetLogs("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.GetLogs("fake", "", 123));
            }
        }

        public class TheGetAttemptMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.GetAttempt("fake", "repo", 123, 456);

                connection.Received().Actions.Workflows.Runs.GetAttempt("fake", "repo", 123, 456);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetAttempt(null, "repo", 123, 456));
                Assert.Throws<ArgumentNullException>(() => client.GetAttempt("fake", null, 123, 456));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetAttempt("", "repo", 123, 456));
                Assert.Throws<ArgumentException>(() => client.GetAttempt("fake", "", 123, 456));
            }
        }

        public class TheGetAttemptLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.GetAttemptLogs("fake", "repo", 123, 456);

                connection.Received().Actions.Workflows.Runs.GetAttemptLogs("fake", "repo", 123, 456);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetAttemptLogs(null, "repo", 123, 456));
                Assert.Throws<ArgumentNullException>(() => client.GetAttemptLogs("fake", null, 123, 456));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetAttemptLogs("", "repo", 123, 456));
                Assert.Throws<ArgumentException>(() => client.GetAttemptLogs("fake", "", 123, 456));
            }
        }

        public class TheGetUsageMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.GetUsage("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.GetUsage("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetUsage(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.GetUsage("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetUsage("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.GetUsage("fake", "", 123));
            }
        }

        public class TheRerunMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.Rerun("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.Rerun("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Rerun(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Rerun("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Rerun("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Rerun("fake", "", 123));
            }
        }

        public class TheRerunFailedJobsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                client.RerunFailedJobs("fake", "repo", 123);

                connection.Received().Actions.Workflows.Runs.RerunFailedJobs("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.RerunFailedJobs(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.RerunFailedJobs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowRunsClient(connection);

                Assert.Throws<ArgumentException>(() => client.RerunFailedJobs("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.RerunFailedJobs("fake", "", 123));
            }
        }
    }
}
