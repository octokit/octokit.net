using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

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
    }
}