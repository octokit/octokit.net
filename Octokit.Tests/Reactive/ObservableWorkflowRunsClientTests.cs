using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;
using System.Reactive.Linq;

namespace Octokit.Tests.Reactive
{
    public class ObservableWorkflowRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableWorkflowRunsClient(null));
            }
        }

        public class TheGetAllForWorkflowIdMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("", "name", 1, request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("owner", "", 1, request, options).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(null, "name", 1, request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", null, 1, request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", "name", 1, null, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", "name", 1, request, null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(1, 2, null, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(1, 2, request, null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                client.GetAllForWorkflowId("fake", "repo", 1, request, options);
                githubClient.Received().Action.Run.GetAllForWorkflowId("fake", "repo", 1, request, options);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                client.GetAllForWorkflowId(1, 2, request, options);
                githubClient.Received().Action.Run.GetAllForWorkflowId(1, 2, request, options);
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request, options).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, request, null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                client.GetAllForRepository("fake", "repo", request, options);
                githubClient.Received().Action.Run.GetAllForRepository("fake", "repo", request, options);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);
                var request = new WorkflowRunsRequest();
                var options = new ApiOptions();

                client.GetAllForRepository(1, request, options);
                githubClient.Received().Action.Run.GetAllForRepository(1, request, options);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1).ToTask());
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryNameAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.Get("fake", "repo", 1);
                githubClient.Received().Action.Run.Get("fake", "repo", 1);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryIdAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.Get(1, 2);
                githubClient.Received().Action.Run.Get(1, 2);
            }
        }

        public class TheReRunMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentException>(() => client.ReRun("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReRun("owner", "", 1).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReRun(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReRun("owner", null, 1).ToTask());
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryNameAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.ReRun("fake", "repo", 1);
                githubClient.Received().Action.Run.ReRun("fake", "repo", 1);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryIdAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.ReRun(1, 2);
                githubClient.Received().Action.Run.ReRun(1, 2);
            }
        }

        public class TheCancelMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("owner", "", 1).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel("owner", null, 1).ToTask());
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryNameAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.Cancel("fake", "repo", 1);
                githubClient.Received().Action.Run.Cancel("fake", "repo", 1);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryIdAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.Cancel(1, 2);
                githubClient.Received().Action.Run.Cancel(1, 2);
            }
        }

        public class TheLogsUrlMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("", "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("owner", "", 1).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl(null, "name", 1).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl("owner", null, 1).ToTask());
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryNameAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.LogsUrl("fake", "repo", 1);
                githubClient.Received().Action.Run.LogsUrl("fake", "repo", 1);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryIdAndWorkflowRunId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowRunsClient(githubClient);

                client.LogsUrl(1, 2);
                githubClient.Received().Action.Run.LogsUrl(1, 2);
            }
        }
    }
}
