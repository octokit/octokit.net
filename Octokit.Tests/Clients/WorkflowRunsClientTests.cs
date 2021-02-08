using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
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
    }
}