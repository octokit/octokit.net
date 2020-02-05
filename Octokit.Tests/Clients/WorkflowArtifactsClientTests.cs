using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using System.Net;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class WorkflowArtifactsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new WorkflowArtifactsClient(null));
            }
        }


        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<WorkflowArtifact>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/artifacts/1"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<WorkflowArtifact>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/artifacts/2"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

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
                var client = new WorkflowArtifactsClient(connection);

                await client.GetAll("fake", "repo", 1);

                connection.Received().GetAll<WorkflowArtifactsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/artifacts"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                await client.GetAll(1, 2);

                connection.Received().GetAll<WorkflowArtifactsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/artifacts"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", 1, options);

                connection.Received().GetAll<WorkflowArtifactsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/runs/1/artifacts"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, 2, options);

                connection.Received().GetAll<WorkflowArtifactsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/runs/2/artifacts"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

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


        public class TheDownloadUrlMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/artifacts/1/zip"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowArtifactsClient(apiConnection);
                var result = await client.DownloadUrl("fake", "repo", 1, "zip");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Found, null, new Dictionary<string, string>(), "application/json")));
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/artifacts/2/zip"), null, null)
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new WorkflowArtifactsClient(apiConnection);
                var result = await client.DownloadUrl(1, 2, "zip");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DownloadUrl(null, "name", 1, "zip"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DownloadUrl("owner", null, 1, "zip"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DownloadUrl("owner", "name", 1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DownloadUrl("", "name", 1, "zip"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DownloadUrl("owner", "", 1, "zip"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DownloadUrl("owner", "", 1, ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                client.Delete("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/artifacts/1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                client.Delete(1, 2);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/artifacts/2"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new WorkflowArtifactsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("fake", null, 1));
            }
        }
    }
}
