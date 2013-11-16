using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;
using System.Net;
using Xunit.Extensions;
using Octokit.Internal;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepoCollaboratorsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new RepoCollaboratorsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.GetAll("owner", "test");
                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null,"test"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("", "test"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", null));
                AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", ""));
            }
        }

        public class TheGetMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepoCollaboratorsClient(apiConnection);

                var result = await client.IsCollaborator("owner", "test", "user1");

                Assert.Equal(expected, result);
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCode()
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = HttpStatusCode.Conflict });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "repos/foo/bar/assignees/cody"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new AssigneesClient(apiConnection);

                AssertEx.Throws<ApiException>(async () => await client.CheckAssignee("foo", "bar", "cody"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator(null, "test", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator("", "test", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator("owner", null, "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator("owner", "", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator("owner", "test", ""));
                AssertEx.Throws<ArgumentNullException>(async () => await client.IsCollaborator("owner", "test", null));
            }
        }

        public class TheAddMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.Add("owner", "test", "user1");
                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.Add(null, "test","user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Add("", "test", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Add("owner", null, "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Add("owner", "", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Add("owner", "test", ""));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Add("owner", "test", null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.Delete("owner", "test", "user1");
                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "test", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("", "test", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", null, "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", "", "user1"));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", "test", ""));
                AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", "test", null));
            }
        }
    }
}
