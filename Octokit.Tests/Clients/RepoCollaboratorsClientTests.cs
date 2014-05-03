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
            public void EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null,"test"));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "test"));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", ""));
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
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"),
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
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(() => client.IsCollaborator(null, "test", "user1"));
                await AssertEx.Throws<ArgumentException>(() => client.IsCollaborator("", "test", "user1"));
                await AssertEx.Throws<ArgumentNullException>(() => client.IsCollaborator("owner", null, "user1"));
                await AssertEx.Throws<ArgumentException>(() => client.IsCollaborator("owner", "", "user1"));
                await AssertEx.Throws<ArgumentException>(() => client.IsCollaborator("owner", "test", ""));
                await AssertEx.Throws<ArgumentNullException>(() => client.IsCollaborator("owner", "test", null));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.Add(null, "test","user1"));
                Assert.Throws<ArgumentException>(() => client.Add("", "test", "user1"));
                Assert.Throws<ArgumentNullException>(() => client.Add("owner", null, "user1"));
                Assert.Throws<ArgumentException>(() => client.Add("owner", "", "user1"));
                Assert.Throws<ArgumentException>(() => client.Add("owner", "test", ""));
                Assert.Throws<ArgumentNullException>(() => client.Add("owner", "test", null));
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
            public void EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "test", "user1"));
                Assert.Throws<ArgumentException>(() => client.Delete("", "test", "user1"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, "user1"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", "user1"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "test", ""));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", "test", null));
            }
        }
    }
}
