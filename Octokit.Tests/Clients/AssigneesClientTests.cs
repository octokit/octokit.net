using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Clients
{
    public class AssignessClientTests
    {
        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new AssigneesClient(connection);

                client.GetForRepository("fake", "repo");

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/assignees"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository(null, "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository(null, ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("", null));
            }
        }

        public class TheCheckAssigneeMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
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
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                await AssertEx.Throws<ApiException>(() => client.CheckAssignee("foo", "bar", "cody"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new AssigneesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckAssignee(null, "name", "tweety"));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckAssignee(null, "", "tweety"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckAssignee("owner", null, "tweety"));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckAssignee("", null, "tweety"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CheckAssignee("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.CheckAssignee("owner", "name", ""));
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
