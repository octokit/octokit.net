using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using System.Net;
using Octokit.Internal;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepoCollaboratorsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
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

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.GetAll(1);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"), Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll("owner", "test", options);

                connection.Received()
                    .GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"), options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsAndRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                client.GetAll(1, options);

                connection.Received()
                    .GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "test"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "test"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "test", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "test", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));
            }
        }

        public class TheIsCollaboratorMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IApiResponse<object>>(() =>
                    new ApiResponse<object>(new Response(status, null, new Dictionary<string, string>(), "application/json")));
                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepoCollaboratorsClient(apiConnection);

                var result = await client.IsCollaborator("owner", "test", "user1");

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
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators/user1"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepoCollaboratorsClient(apiConnection);

                var result = await client.IsCollaborator(1, "user1");

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
                var client = new RepoCollaboratorsClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.IsCollaborator("foo", "bar", "cody"));
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
                var client = new RepoCollaboratorsClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.IsCollaborator(1, "cody"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsCollaborator(null, "test", "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsCollaborator("owner", null, "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsCollaborator("owner", "test", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsCollaborator(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.IsCollaborator("", "test", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsCollaborator("owner", "", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsCollaborator("owner", "test", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsCollaborator(1, ""));
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
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.Add(1, "user1");
                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators/user1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Add(null, "test", "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Add("owner", null, "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Add("owner", "test", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Add(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Add("", "test", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Add("owner", "", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Add("owner", "test", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Add(1, ""));
            }
        }

        public class TheInviteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var permission = new CollaboratorRequest(Permission.Push);

                client.Invite("owner", "test", "user1", permission);
                connection.Received().Put<RepositoryInvitation>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"), Arg.Is<CollaboratorRequest>(permission), Arg.Any<string>(), Arg.Is<string>("application/vnd.github.swamp-thing-preview+json"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                var permission = new CollaboratorRequest(Permission.Push);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Invite(null, "test", "user1", permission));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Invite("", "test", "user1", permission));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Invite("owner", null, "user1", permission));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Invite("owner", "", "user1", permission));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Invite("owner", "test", "", permission));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Invite("owner", "test", null, permission));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Invite("owner", "test", "user1", null));
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
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.Delete(1, "user1");
                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators/user1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "test", "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "test", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "test", "user1")); ;
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "test", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, ""));
            }
        }
    }
}
