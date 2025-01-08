using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableActionsWorkflowsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableActionsWorkflowsClient(null));
            }
        }

        public class TheClientProperties
        {
            [Fact]
            public void AreNotNull()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.NotNull(client.Jobs);
                Assert.NotNull(client.Runs);
            }
        }

        public class TheCreateDispatchMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowIdRepoSlug()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                client.CreateDispatch("fake", "repo", 123, createDispatch);

                connection.Received().Actions.Workflows.CreateDispatch("fake", "repo", 123, createDispatch);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileNameRepoSlug()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                client.CreateDispatch("fake", "repo", "main.yaml", createDispatch);

                connection.Received().Actions.Workflows.CreateDispatch("fake", "repo", "main.yaml", createDispatch);
            }
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowIdRepoId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                client.CreateDispatch(1234, 123, createDispatch);

                connection.Received().Actions.Workflows.CreateDispatch(1234, 123, createDispatch);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileNameRepoId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                client.CreateDispatch(1234, "main.yaml", createDispatch);

                connection.Received().Actions.Workflows.CreateDispatch(1234, "main.yaml", createDispatch);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch(null, "repo", 123, createDispatch));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch("fake", null, 123, createDispatch));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", 123, null));

                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch(null, "repo", "main.yaml", createDispatch));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch("fake", null, "main.yaml", createDispatch));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", null, createDispatch));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", "main.yaml", null));

                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch(4321, 123, null));
                Assert.Throws<ArgumentNullException>(() => client.CreateDispatch(4321, null, createDispatch));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                Assert.Throws<ArgumentException>(() => client.CreateDispatch("", "repo", 123, createDispatch));
                Assert.Throws<ArgumentException>(() => client.CreateDispatch("fake", "", 123, createDispatch));

                Assert.Throws<ArgumentException>(() => client.CreateDispatch("", "repo", "main.yaml", createDispatch));
                Assert.Throws<ArgumentException>(() => client.CreateDispatch("fake", "", "main.yaml", createDispatch));
                Assert.Throws<ArgumentException>(() => client.CreateDispatch("fake", "repo", "", createDispatch));

                Assert.Throws<ArgumentException>(() => client.CreateDispatch(4321, "", createDispatch));
            }
        }

        public class TheDisableMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Disable("fake", "repo", 123);

                connection.Received().Actions.Workflows.Disable("fake", "repo", 123);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Disable("fake", "repo", "main.yaml");

                connection.Received().Actions.Workflows.Disable("fake", "repo", "main.yaml");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Disable(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Disable("fake", null, 123));

                Assert.Throws<ArgumentNullException>(() => client.Disable(null, "repo", "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Disable("fake", null, "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Disable("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Disable("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Disable("fake", "", 123));

                Assert.Throws<ArgumentException>(() => client.Disable("", "repo", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Disable("fake", "", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Disable("fake", "repo", ""));
            }
        }

        public class TheEnableMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Enable("fake", "repo", 123);

                connection.Received().Actions.Workflows.Enable("fake", "repo", 123);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Enable("fake", "repo", "main.yaml");

                connection.Received().Actions.Workflows.Enable("fake", "repo", "main.yaml");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Enable(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Enable("fake", null, 123));

                Assert.Throws<ArgumentNullException>(() => client.Enable(null, "repo", "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Enable("fake", null, "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Enable("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Enable("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Enable("fake", "", 123));

                Assert.Throws<ArgumentException>(() => client.Enable("", "repo", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Enable("fake", "", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Enable("fake", "repo", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Get("fake", "repo", 123);

                connection.Received().Actions.Workflows.Get("fake", "repo", 123);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.Get("fake", "repo", "main.yaml");

                connection.Received().Actions.Workflows.Get("fake", "repo", "main.yaml");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, 123));

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "repo", "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", null, "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.Get("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", 123));

                Assert.Throws<ArgumentException>(() => client.Get("", "repo", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.Get("fake", "repo", ""));
            }
        }

        public class TheGetUsageMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.GetUsage("fake", "repo", 123);

                connection.Received().Actions.Workflows.GetUsage("fake", "repo", 123);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.GetUsage("fake", "repo", "main.yaml");

                connection.Received().Actions.Workflows.GetUsage("fake", "repo", "main.yaml");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.GetUsage(null, "repo", 123));
                Assert.Throws<ArgumentNullException>(() => client.GetUsage("fake", null, 123));

                Assert.Throws<ArgumentNullException>(() => client.GetUsage(null, "repo", "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.GetUsage("fake", null, "main.yaml"));
                Assert.Throws<ArgumentNullException>(() => client.GetUsage("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentException>(() => client.GetUsage("", "repo", 123));
                Assert.Throws<ArgumentException>(() => client.GetUsage("fake", "", 123));

                Assert.Throws<ArgumentException>(() => client.GetUsage("", "repo", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.GetUsage("fake", "", "main.yaml"));
                Assert.Throws<ArgumentException>(() => client.GetUsage("fake", "repo", ""));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                client.List("fake", "repo");

                connection.Received().Actions.Workflows.List("fake", "repo");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                var options = new ApiOptions { PageSize = 1 };

                client.List("fake", "repo", options);

                connection.Received().Actions.Workflows.List("fake", "repo", options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null));

                Assert.Throws<ArgumentNullException>(() => client.List(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.List("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IGitHubClient>();
                var client = new ObservableActionsWorkflowsClient(connection);

                Assert.Throws<ArgumentException>(() => client.List("", "repo"));
                Assert.Throws<ArgumentException>(() => client.List("fake", ""));

                Assert.Throws<ArgumentException>(() => client.List("", "repo", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.List("fake", "", ApiOptions.None));
            }
        }
    }
}
