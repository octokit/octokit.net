using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

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
                var responseTask = CreateApiResponse(status);
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"),
                    null, null).Returns(responseTask);
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
                var responseTask = CreateApiResponse(status);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees/cody"), null, null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                var result = await client.CheckAssignee(1, "cody");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var responseTask = CreateApiResponse(HttpStatusCode.Conflict);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"), null, null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.CheckAssignee("foo", "bar", "cody"));
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryId()
            {
                var response = CreateResponse(HttpStatusCode.Conflict);
                var responseTask = Task.FromResult<IApiResponse<object>>(new ApiResponse<object>(response));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees/cody"),
                    null, null).Returns(responseTask);
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

        public class TheAddAssigneesMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                await client.AddAssignees("fake", "repo", 2, newAssignees);

                connection.Received().Post<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/2/assignees"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAssignees("", "name", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAssignees("owner", "", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAssignees("owner", "name", 2, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAssignees(null, "name", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAssignees("owner", null, 2, newAssignees));
            }
        }

        public class TheRemoveAssigneesMethod
        {
            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var removeAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                await client.RemoveAssignees("fake", "repo", 2, removeAssignees);

                connection.Received().Delete<Issue>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/issues/2/assignees"), Arg.Any<object>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAssignees("", "name", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveAssignees("owner", "", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAssignees("owner", "name", 2, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAssignees(null, "name", 2, newAssignees));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveAssignees("owner", null, 2, newAssignees));
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
