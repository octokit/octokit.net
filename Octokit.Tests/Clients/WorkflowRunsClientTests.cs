using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using System.Net;
using Octokit.Tests.Helpers;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class WorkflowRunsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new WorkflowRunsClient(null));
            }
        }


        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<WorkflowRun>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<WorkflowRun>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }

        public class TheGetAllForWorkflowIdMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.GetAllForWorkflowId("fake", "repo", 1);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/1/runs"),
                    Arg.Is<Dictionary<string, string>>(x => true),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.GetAllForWorkflowId(1, 2);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/workflows/2/runs"),
                    Arg.Is<Dictionary<string, string>>(x => true),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithWorkflowRunsRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                await client.GetAllForWorkflowId("fake", "repo", 1, request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/1/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithWorkflowRunsRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                await client.GetAllForWorkflowId(1, 2, request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/workflows/2/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithWorkflowRunsRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForWorkflowId("fake", "repo", 1, request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/workflows/1/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithWorkflowRunsRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForWorkflowId(1, 2, request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/workflows/2/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(null, "name", 1, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", null, 1, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(null, "name", 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", null, 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", "name", 1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId("owner", "name", 1, request, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(1, 2, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForWorkflowId(1, 2, request, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("owner", "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("", "name", 1, request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("owner", "", 1, request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("", "name", 1, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForWorkflowId("owner", "", 1, request, ApiOptions.None));
            }
        }


        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => true),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => true),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithWorkflowRunsRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                await client.GetAllForRepository("fake", "repo", request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithWorkflowRunsRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                await client.GetAllForRepository(1, request);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithWorkflowRunsRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository("fake", "repo", request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithWorkflowRunsRequestWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest()
                {
                    Branch = "master"
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForRepository(1, request, options);

                connection.Received().GetAll<WorkflowRunsResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs"),
                    Arg.Is<Dictionary<string, string>>(x => x["branch"] == "master"),
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);
                var request = new WorkflowRunsRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, request, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", request, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", request, ApiOptions.None));
            }
        }

        public class TheReRunMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Created);
                var client = new WorkflowRunsClient(connection);

                var result = await client.ReRun("fake", "repo", 1);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/rerun"));

                Assert.True(result);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Created);
                var client = new WorkflowRunsClient(connection);

                var result = await client.ReRun(1, 2);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/rerun"));

                Assert.True(result);
            }


            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryName()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Conflict);
                var client = new WorkflowRunsClient(connection);
                await Assert.ThrowsAsync<ApiException>(() => client.ReRun("fake", "repo", 1));
            }


            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryId()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Conflict);
                var client = new WorkflowRunsClient(connection);
                await Assert.ThrowsAsync<ApiException>(() => client.ReRun(1, 2));
            }


            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReRun(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReRun("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.ReRun("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReRun("owner", "", 1));

            }

        }

        public class TheCancelMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Accepted);
                var client = new WorkflowRunsClient(connection);

                var result = await client.Cancel("fake", "repo", 1);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/cancel"));

                Assert.True(result);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Accepted);
                var client = new WorkflowRunsClient(connection);

                var result = await client.Cancel(1, 2);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/cancel"));

                Assert.True(result);
            }


            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryName()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Conflict);
                var client = new WorkflowRunsClient(connection);
                await Assert.ThrowsAsync<ApiException>(() => client.Cancel("fake", "repo", 1));
            }


            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryId()
            {
                var connection = MockedIApiConnection.PostReturnsHttpStatus(HttpStatusCode.Conflict);
                var client = new WorkflowRunsClient(connection);
                await Assert.ThrowsAsync<ApiException>(() => client.Cancel(1, 2));
            }


            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Cancel("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Cancel("owner", "", 1));

            }

        }

        public class TheLogsUrlMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/logs"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowRunsClient(apiConnection);
                var result = await client.LogsUrl("fake", "repo", 1);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/logs"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowRunsClient(apiConnection);
                var result = await client.LogsUrl(1, 2);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowRunsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("owner", "", 1));

            }

        }



    }
}
