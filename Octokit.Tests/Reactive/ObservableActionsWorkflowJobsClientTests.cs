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

        public class TheGetMethod
        {
            [Fact]
            public async Task CallGetOnClient()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                client.Get("fake", "repo", 123);

                connection.Received().Actions.Workflows.Jobs.Get("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", 123));
            }
        }

        public class TheGetLogsMethod
        {
            [Fact]
            public async Task CallGetLogsOnClient()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                client.GetLogs("fake", "repo", 123);

                connection.Received().Actions.Workflows.Jobs.GetLogs("fake", "repo", 123);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetLogs(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.GetLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetLogs("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.GetLogs("fake", "", 123));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task CallListOnClient()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                client.List("fake", "repo", 123);

                connection.Received().Actions.Workflows.Jobs.List("fake", "repo", 123);
            }

            [Fact]
            public async Task CallListOnClientWithRequest()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);
                var workflowRunJobsRequest = new WorkflowRunJobsRequest();

                client.List("fake", "repo", 123, workflowRunJobsRequest);

                connection.Received().Actions.Workflows.Jobs.List("fake", "repo", 123, workflowRunJobsRequest);
            }

            [Fact]
            public async Task CallListOnClientWithRequestAndOptions()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);
                var workflowRunJobsRequest = new WorkflowRunJobsRequest();
                var options = new ApiOptions();

                client.List("fake", "repo", 123, workflowRunJobsRequest, options);

                connection.Received().Actions.Workflows.Jobs.List("fake", "repo", 123, workflowRunJobsRequest, options);
            }

            [Fact]
            public async Task CallListOnClientForAttempt()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);

                client.List("fake", "repo", 123, 456);

                connection.Received().Actions.Workflows.Jobs.List("fake", "repo", 123, 456);
            }

            [Fact]
            public async Task CallListOnClientForAttemptWithOptions()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);
                var options = new ApiOptions();

                client.List("fake", "repo", 123, 456, options);

                connection.Received().Actions.Workflows.Jobs.List("fake", "repo", 123, 456, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);
                var workflowRunJobsRequest = new WorkflowRunJobsRequest();
                var options = new ApiOptions();

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, 123));

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", 123, workflowRunJobsRequest));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, 123, workflowRunJobsRequest));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", 123, null));

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", 123, workflowRunJobsRequest, options));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, 123, workflowRunJobsRequest, options));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", 123, null, options));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowJobsClient(connection);
                var workflowRunJobsRequest = new WorkflowRunJobsRequest();
                var options = new ApiOptions();

                Assert.Throws<ArgumentException>(() => client.List("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.List("fake", "", 123));

                Assert.Throws<ArgumentException>(() => client.List("", "repo", 123, workflowRunJobsRequest));
                Assert.Throws<ArgumentException>(() => client.List("fake", "", 123, workflowRunJobsRequest));

                Assert.Throws<ArgumentException>(() => client.List("", "repo", 123, workflowRunJobsRequest, options));
                Assert.Throws<ArgumentException>(() => client.List("fake", "", 123, workflowRunJobsRequest, options));
            }
        }
    }
}
