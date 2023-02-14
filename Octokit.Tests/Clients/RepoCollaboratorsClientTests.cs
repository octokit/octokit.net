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

                connection.Received().GetAll<Collaborator>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.GetAll(1);

                connection.Received().GetAll<Collaborator>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                    Arg.Any<Dictionary<string, string>>(),
                    Args.ApiOptions);
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
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                        Arg.Any<Dictionary<string, string>>(),
                        options);
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "all"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "direct"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "outside"),
                        Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithPermissionFilter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => !d.ContainsKey("permission")),
                                          Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Permission = CollaboratorPermission.Admin
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => d["permission"] == "admin"),
                                          Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Permission = CollaboratorPermission.Maintain
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => d["permission"] == "maintain"),
                                          Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilterAndPermissionFilter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "all" && !d.ContainsKey("permission")),
                                          Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct,
                    Permission = CollaboratorPermission.Admin
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "direct" && d["permission"] == "admin"),
                                          Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside,
                    Permission = CollaboratorPermission.Pull
                };

                client.GetAll("owner", "test", request);

                connection.Received()
                    .GetAll<Collaborator>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators"),
                                          Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "outside" && d["permission"] == "pull"),
                                          Args.ApiOptions);
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
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Any<Dictionary<string, string>>(),
                        options);
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilterAndRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "all"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "direct"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "outside"),
                        Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithPermissionFilterAndRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => !d.ContainsKey("permission")),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Permission = CollaboratorPermission.Triage
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["permission"] == "triage"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Permission = CollaboratorPermission.Push
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["permission"] == "push"),
                        Args.ApiOptions);
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilterPermissionFilterAndRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var request = new RepositoryCollaboratorListRequest();

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "all" && !d.ContainsKey("permission")),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct,
                    Permission = CollaboratorPermission.Triage
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "direct" && d["permission"] == "triage"),
                        Args.ApiOptions);

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside,
                    Permission = CollaboratorPermission.Push
                };

                client.GetAll(1, request);

                connection.Received()
                    .GetAll<Collaborator>(
                        Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators"),
                        Arg.Is<Dictionary<string, string>>(d => d["affiliation"] == "outside" && d["permission"] == "push"),
                        Args.ApiOptions);
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
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "test", options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "test", request: null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, options: null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, request: null));
            }
        }

        public class TheIsCollaboratorMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var responseTask = CreateApiResponse(status);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"), null, null)
                          .Returns(responseTask);

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
                var responseTask = CreateApiResponse(status);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators/user1"), null, null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new RepoCollaboratorsClient(apiConnection);

                var result = await client.IsCollaborator(1, "user1");

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
                var client = new RepoCollaboratorsClient(apiConnection);

                await Assert.ThrowsAsync<ApiException>(() => client.IsCollaborator("foo", "bar", "cody"));
            }

            [Fact]
            public async Task ThrowsExceptionForInvalidStatusCodeWithRepositoryId()
            {
                var responseTask = TestSetup.CreateApiResponse(HttpStatusCode.Conflict);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/assignees/cody"), null, null)
                          .Returns(responseTask);

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

        public class TheReviewPermissionMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.ReviewPermission("owner", "test", "user1");
                connection.Received().Get<CollaboratorPermissionResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1/permission"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                client.ReviewPermission(1L, "user1");
                connection.Received().Get<CollaboratorPermissionResponse>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/collaborators/user1/permission"),
                    Arg.Any<Dictionary<string, string>>());
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPermission(null, "test", "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPermission("", "test", "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPermission("owner", null, "user1"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPermission("owner", "", "user1"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPermission("owner", "test", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPermission("owner", "test", ""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.ReviewPermission(1L, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.ReviewPermission(1L, ""));
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

            [Fact]
            public async Task SurfacesAuthorizationException()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                connection.Put(Arg.Any<Uri>()).Returns(x => { throw new AuthorizationException(); });

                await Assert.ThrowsAsync<AuthorizationException>(() => client.Add("owner", "test", "user1"));
                await Assert.ThrowsAsync<AuthorizationException>(() => client.Add(1, "user1"));
            }

            [Fact]
            public async Task SurfacesAuthorizationExceptionWhenSpecifyingCollaboratorRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                connection.Put<RepositoryInvitation>(Arg.Any<Uri>(), Arg.Any<object>()).ThrowsAsync(new AuthorizationException());

                await Assert.ThrowsAsync<AuthorizationException>(() => client.Add("owner", "test", "user1", new CollaboratorRequest("pull")));
                await Assert.ThrowsAsync<AuthorizationException>(() => client.Add(1, "user1", new CollaboratorRequest("pull")));
            }
        }

        public class TheInviteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepoCollaboratorsClient(connection);

                var permission = new CollaboratorRequest("push");

                client.Invite("owner", "test", "user1", permission);
                connection.Received().Put<RepositoryInvitation>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/test/collaborators/user1"), Arg.Is<CollaboratorRequest>(permission));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                var permission = new CollaboratorRequest("push");

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
