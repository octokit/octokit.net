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
    public class WorkflowJobsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new WorkflowJobsClient(null));
            }
        }


        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<WorkflowJob>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/jobs/1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<WorkflowJob>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/jobs/2"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await client.GetAll("fake", "repo", 1);

                connection.Received().GetAll<WorkflowJobsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/jobs"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await client.GetAll(1, 2);

                connection.Received().GetAll<WorkflowJobsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/jobs"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", 1, options);

                connection.Received().GetAll<WorkflowJobsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/jobs"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, 2, options);

                connection.Received().GetAll<WorkflowJobsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/jobs"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", 1, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, 2, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", 1, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", 1, ApiOptions.None));
            }
        }

        public class TheGetLogsUrlMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/jobs/1/logs"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowJobsClient(apiConnection);
                var result = await client.LogsUrl("fake", "repo", 1);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/jobs/2/logs"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowJobsClient(apiConnection);
                var result = await client.LogsUrl(1, 2);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowJobsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl(null, "name", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.LogsUrl("owner", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("", "name", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.LogsUrl("owner", "", 1));

            }

        }



    }
}
