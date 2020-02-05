using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;
using System.Reactive.Linq;

namespace Octokit.Tests.Reactive
{
    public class ObservableWorkflowsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableWorkflowsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", options).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", options).ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);
                var options = new ApiOptions();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", options).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, options).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);
                var options = new ApiOptions();

                client.GetAll("fake", "repo", options);
                githubClient.Received().Action.Workflow.GetAll("fake", "repo", options);
            }

            [Fact]
            public void GetsCorrectUrlWithRepositoryId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);
                var options = new ApiOptions();

                client.GetAll(1, options);
                githubClient.Received().Action.Workflow.GetAll(1, options);
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);

                // get by workflowFileName
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "workflow.yml").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "workflow.yml").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", "").ToTask());

                // get by workflowId
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, "").ToTask());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);

                // get by workflowFileName
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "workflow.yml").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "workflow.yml").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null).ToTask());

                // get by workflowId
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null).ToTask());
            }

            [Fact]
            public void GetsCorrectUrlWithWorkflowId()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);

                client.Get("fake", "repo", 1);
                githubClient.Received().Action.Workflow.Get("fake", "repo", 1);
            }

            [Fact]
            public void GetsCorrectUrlWithWorkflowFileName()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowsClient(githubClient);

                client.Get("fake", "repo", "workflow.yml");
                githubClient.Received().Action.Workflow.Get("fake", "repo", "workflow.yml");
            }
        }
    }
}
