using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ActionsWorkflowJobsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ActionsWorkflowJobsClient(null));
            }
        }

        public class TheRerunMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await client.Rerun("fake", "repo", 123);

                connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/jobs/123/rerun"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerun(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Rerun("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerun("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Rerun("fake", "", 123));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await client.Get("fake", "repo", 123);

                connection.Received().Get<WorkflowJob>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/jobs/123"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("fake", "", 123));
            }
        }

        public class TheGetLogsMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await client.GetLogs("fake", "repo", 123);

                connection.Connection.Received().Get<string>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/jobs/123/logs"),
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLogs(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetLogs("fake", null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLogs("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetLogs("fake", "", 123));
            }
        }

        public class TheListMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await client.List("fake", "repo", 123);

                connection.Received().GetAll<WorkflowJobsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/jobs"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);

                await client.List("fake", "repo", 123, 456);

                connection.Received().GetAll<WorkflowJobsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/attempts/456/jobs"),
                    null,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                var request = new WorkflowRunJobsRequest
                {
                    Filter = WorkflowRunJobsFilter.All,
                };

                await client.List("fake", "repo", 123, request);

                connection.Received().GetAll<WorkflowJobsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/jobs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["filter"] == "all"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                var request = new WorkflowRunJobsRequest { Filter = WorkflowRunJobsFilter.Latest };
                var options = new ApiOptions { PageSize = 1 };

                await client.List("fake", "repo", 123, request, options);

                connection.Received().GetAll<WorkflowJobsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/jobs"),
                    Arg.Is<Dictionary<string, string>>(x =>
                            x.Count == 1
                            && x["filter"] == "latest"),
                    options);

                await client.List("fake", "repo", 123, 456, options);

                connection.Received().GetAll<WorkflowJobsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/123/attempts/456/jobs"),
                    null,
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo", 123));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null, 123));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List(null, "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.List("fake", null, 123, 456));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ActionsWorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo", 123));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", "", 123));

                await Assert.ThrowsAsync<ArgumentException>(() => client.List("", "repo", 123, 456));
                await Assert.ThrowsAsync<ArgumentException>(() => client.List("fake", "", 123, 456));
            }
        }
    }
}
