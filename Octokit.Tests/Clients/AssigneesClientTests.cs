using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class AssigneesClientTests
    {
        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/assignees"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, ""));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", null));
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
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee(null, "name", "tweety"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee(null, "", "tweety"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee("owner", null, "tweety"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee("", null, "tweety"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckAssignee("owner", "name", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CheckAssignee("owner", "name", ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new AssigneesClient(null));
            }
        }
    }
}
