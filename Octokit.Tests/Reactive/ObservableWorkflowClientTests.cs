using System;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Reactive.Clients;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class WorkflowClientTests
    {
        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableWorkflowClient(gitHubClient);

                client.GetAllForRepository("owner", "repo");

                gitHubClient.Received().Actions.Workflows.GetAllForRepository("owner", "repo");
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWorkflowClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableWorkflowClient(Substitute.For<IGitHubClient>());
                var dispatchEvent = new WorkflowDispatchEvent
                {
                    Ref = "refs/heads/master"
                };
                Assert.Throws<ArgumentNullException>(() => client.CreateWorkflowDispatchEvent(null, "name", "id", dispatchEvent));
                Assert.Throws<ArgumentNullException>(() => client.CreateWorkflowDispatchEvent("owner", null, "id", dispatchEvent));
                Assert.Throws<ArgumentNullException>(() => client.CreateWorkflowDispatchEvent("owner", "name", null, dispatchEvent));
                Assert.Throws<ArgumentNullException>(() => client.CreateWorkflowDispatchEvent("owner", "name", "id", null));
                Assert.Throws<ArgumentNullException>(() => client.CreateWorkflowDispatchEvent("owner", "name", "id", new WorkflowDispatchEvent()));

            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableWorkflowClient((null)));
            }
        }
    }
}