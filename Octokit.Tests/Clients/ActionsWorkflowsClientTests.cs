using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ActionsWorkflowsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ActionsWorkflowsClient(null));
            }
        }

        public class TheClientProperties
        {
            [Fact]
            public void AreNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                Assert.NotNull(client.Jobs);
                Assert.NotNull(client.Runs);
            }
        }

        public class TheCreateDispatchMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowIdRepoSlug()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.CreateDispatch("fake", "repo", 123, createDispatch);

                connection.Received().Post<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/dispatches"),
                    createDispatch);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileNameRepoSlug()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.CreateDispatch("fake", "repo", "main.yaml", createDispatch);

                connection.Received().Post<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yaml/dispatches"),
                    createDispatch);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowIdRepoId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.CreateDispatch(321, 123, createDispatch);

                connection.Received().Post<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/321/actions/workflows/123/dispatches"),
                    createDispatch);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileNameRepoId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await client.CreateDispatch(321, "main.yaml", createDispatch);

                connection.Received().Post<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/321/actions/workflows/main.yaml/dispatches"),
                    createDispatch);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch(null, "repo", 123, createDispatch));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch("fake", null, 123, createDispatch));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", 123, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch(null, "repo", "main.yaml", createDispatch));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch("fake", null, "main.yaml", createDispatch));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", null, createDispatch));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch("fake", "repo", "main.yaml", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch(4321, 123, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateDispatch(4321, null, createDispatch));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var createDispatch = new CreateWorkflowDispatch("ref");

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch("", "repo", 123, createDispatch));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch("fake", "", 123, createDispatch));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch("", "repo", "main.yaml", createDispatch));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch("fake", "", "main.yaml", createDispatch));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch("fake", "repo", "", createDispatch));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateDispatch(4321, "", createDispatch));
            }
        }

        public class TheDisableMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Disable("fake", "repo", 123);

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/disable"));
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Disable("fake", "repo", "main.yaml");

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yaml/disable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Disable(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Disable("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Disable(null, "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Disable("fake", null, "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Disable("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Disable("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Disable("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Disable("", "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Disable("fake", "", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Disable("fake", "repo", ""));
            }
        }

        public class TheEnableMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Enable("fake", "repo", 123);

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/enable"));
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Enable("fake", "repo", "main.yaml");

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yaml/enable"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Enable(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Enable("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Enable(null, "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Enable("fake", null, "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Enable("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Enable("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Enable("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Enable("", "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Enable("fake", "", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Enable("fake", "repo", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Get("fake", "repo", 123);

                connection.Received().Get<Workflow>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123"),
                    null);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.Get("fake", "repo", "main.yaml");

                connection.Received().Get<Workflow>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yaml"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "repo", ""));
            }
        }

        public class TheGetUsageMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlByWorkflowId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.GetUsage("fake", "repo", 123);

                connection.Received().Get<WorkflowUsage>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/123/timing"),
                    null);
            }

            [Fact]
            public async Task RequestsCorrectUrlByWorkflowFileName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.GetUsage("fake", "repo", "main.yaml");

                connection.Received().Get<WorkflowUsage>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/main.yaml/timing"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage(null, "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage("fake", null, "main.yaml"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetUsage("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("", "repo", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("fake", "", "main.yaml"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetUsage("fake", "repo", ""));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await client.List("fake", "repo");

                connection.Received().GetAll<WorkflowsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                var options = new ApiOptions { PageSize = 1 };

                await client.List("fake", "repo", options);

                connection.Received().GetAll<WorkflowsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows"),
                    null,
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", "", ApiOptions.None));
            }
        }
    }
}
