using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class AssigneesClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/assignees"),
                    null,
                    AcceptHeaders.StableVersion,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees"),
                    null,
                    AcceptHeaders.StableVersion,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository("fake", "repo", options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/assignees"),
                    null,
                    AcceptHeaders.StableVersion,
                    options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAllForRepository(1, options);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees"),
                    null,
                    AcceptHeaders.StableVersion,
                    options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
            }
        }

        public class TheCheckAssigneeMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                var result = await client.CheckAssignee("foo", "bar", "cody");

                Assert.Equal(expected, result);
            }

            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCodeWithRepositoryId(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                var result = await client.CheckAssignee(1, "cody");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.CheckAssignee("foo", "bar", "cody"));
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryId()
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(HttpStatusCode.Conflict, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.CheckAssignee(1, "cody"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee(null, "name", "tweety"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee("owner", null, "tweety"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee("", "name", "tweety"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee("owner", "", "tweety"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee(1, ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AssigneesClient(null));
            }
        }
    }
}
