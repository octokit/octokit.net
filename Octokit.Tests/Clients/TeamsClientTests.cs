using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class TeamsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TeamsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.Get(1);

                connection.Received().Get<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAll("orgName");

                connection.Received().GetAll<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll("orgName", null));
            }
        }

        public class TheGetAllChildTeamsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllChildTeams(1);

                connection.Received().GetAll<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/teams"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllChildTeams(1, null));
            }
        }

        public class TheGetAllMembersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllMembers(1);

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/members"),
                    Args.ApiOptions);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRequest()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllMembers(1, new TeamMembersRequest(TeamRoleFilter.Maintainer));

                connection.Received().GetAll<User>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/members"),
                    Arg.Is<Dictionary<string, string>>(d => d["role"] == "maintainer"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllMembers(1, (TeamMembersRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllMembers(1, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllMembers(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllMembers(1, new TeamMembersRequest(TeamRoleFilter.All), null));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("Octokittens");

                client.Create("orgName", team);

                connection.Received().Post<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"),
                    team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("superstars");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, team));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", team));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("name", null));
            }
        }

        public class TheUpdateTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new UpdateTeam("Octokittens");

                var org = "org";
                var slug = "slug";
                client.Update(org, slug , team);

                connection.Received().Patch<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/teams/slug"),
                    team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "b", new UpdateTeam("update-team")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("a", null, new UpdateTeam("update-team")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("a", "b", null));
            }
        }

        public class TheUpdateTeamLegacyMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new UpdateTeam("Octokittens");

                client.Update(1, team);

                connection.Received().Patch<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1"),
                    team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                var org = "org";
                var slug = "slug";

                client.Delete(org, slug);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/teams/slug"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("a", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "a"));
            }
        }

        public class TheDeleteTeamLegacyMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.Delete(1);

                connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheAddOrEditMembershipMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var request = new UpdateTeamMembership(TeamRole.Maintainer);

                await client.AddOrEditMembership(1, "user", request);

                connection.Received().Put<TeamMembershipDetails>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"),
                    Arg.Is<UpdateTeamMembership>(x => x.Role == TeamRole.Maintainer));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var request = new UpdateTeamMembership(TeamRole.Maintainer);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrEditMembership(1, null, request));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrEditMembership(1, "user", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddOrEditMembership(1, "", request));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Team>(
                    Arg.Is<Uri>(u => u.ToString() == "user/teams"),
                    Args.ApiOptions);
            }
        }

        public class TheGetMembershipDetailsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.GetMembershipDetails(1, "user");

                connection.Received().Get<TeamMembershipDetails>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetMembershipDetails(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetMembershipDetails(1, ""));
            }
        }

        public class TheRemoveMembershipMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.RemoveMembership(1, "user");

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveMembership(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveMembership(1, ""));
            }
        }

        public class TheGetAllRepositoriesMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.GetAllRepositories(1);

                connection.Received().GetAll<Repository>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"),
                    Args.ApiOptions);
            }
        }

        public class TheRemoveRepositoryMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.RemoveRepository(1, "org", "repo");

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }
        }

        public class TheAddRepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check owner arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, null, "repoName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "", "repoName"));

                // Check repo arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, "ownerName", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "ownerName", ""));
            }


            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                await client.AddRepository(1, "org", "repo");

                connection.Connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }

            [Fact]
            public async Task AddOrUpdatePermission()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var newPermission = new RepositoryPermissionRequest(TeamPermissionLegacy.Admin);

                await client.AddRepository(1, "org", "repo", newPermission);

                connection.Connection.Received().Put<HttpStatusCode>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"),
                    newPermission);
            }

            [Fact]
            public async Task EnsureNonNullOrg()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepository(1, null, "Repo Name"));
            }
        }

        public class TheIsRepositoryManagedByTeamMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepository(1, "org name", null));

                // Check owner arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, null, "repoName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "", "repoName"));

                // Check repo arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", ""));
            }
        }

        public class TheGetAllPendingInvitationsMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.GetAllPendingInvitations(1);

                connection.Received().GetAll<OrganizationMembershipInvitation>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/invitations"),
                    null,
                    Args.ApiOptions);
            }
        }

        public class TheCheckTeamPermissionsForARepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckTeamPermissionsForARepository(null, "teamSlug", "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckTeamPermissionsForARepository("org", null, "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckTeamPermissionsForARepository("org", "teamSlug", null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckTeamPermissionsForARepository("org", "teamSlug", "owner", null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.CheckTeamPermissionsForARepository("org", "teamSlug", "owner", "repo");

                var expected = "/orgs/org/teams/teamSlug/repos/owner/repo";

                connection.Received().Get<TeamRepository>(Arg.Is<Uri>(u => u.ToString() == expected));
            }
        }

        public class TheCheckTeamPermissionsForARepositoryWithCustomAcceptHeaderMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => 
                    client.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader(null, "teamSlug", "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => 
                    client.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader("org", null, "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => 
                    client.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader("org", "teamSlug", null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => 
                    client.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader("org", "teamSlug", "owner", null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader("org", "teamSlug", "owner", "repo");

                var expected = "/orgs/org/teams/teamSlug/repos/owner/repo";

                connection.Received().Get<TeamRepository>(
                    Arg.Is<Uri>(u => u.ToString() == expected),
                    null,
                    Arg.Is<string>(s => s.Equals("application/vnd.github.v3.repository+json")));
            }
        }

        public class TheAddOrUpdateTeamRepositoryPermissionsMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateTeamRepositoryPermissions(null, "teamSlug", "owner", "repo", "permission"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateTeamRepositoryPermissions("org", null, "owner", "repo", "permission"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateTeamRepositoryPermissions("org", "teamSlug", null, "repo", "permission"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddOrUpdateTeamRepositoryPermissions("org", "teamSlug", "owner", null, "permission"));
            }

            [Fact]
            public async Task EnsuresNullPermissionValueDoesNotThrow()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var exception = await Record.ExceptionAsync(() => client.AddOrUpdateTeamRepositoryPermissions("org", "teamSlug", "owner", "repo", null));

                Assert.Null(exception);
            }


            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var permission = "a";

                await client.AddOrUpdateTeamRepositoryPermissions("org", "teamSlug", "owner", "repo", permission);

                var expected = "/orgs/org/teams/teamSlug/repos/owner/repo";

                connection.Received().Put(
                    Arg.Is<Uri>(u => u.ToString() == expected),
                    Arg.Any<object>());
            }

            [Fact]
            public async Task PassesTheCorrestPermission()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var permission = "a";

                await client.AddOrUpdateTeamRepositoryPermissions("org", "teamSlug", "owner", "repo", permission);

                connection.Received().Put(
                    Arg.Any<Uri>(),
                    Arg.Is<object>(o => o.GetType().GetProperty("permission").GetValue(o).ToString() == "a"));
            }
        }

        public class TheRemoveRepositoryFromATeamMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepositoryFromATeam(null, "teamSlug", "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepositoryFromATeam("org", null, "owner", "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepositoryFromATeam("org", "teamSlug", null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepositoryFromATeam("org", "teamSlug", "owner", null));
            }

            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await client.RemoveRepositoryFromATeam("org", "teamSlug", "owner", "repo");

                var expected = "/orgs/org/teams/teamSlug/repos/owner/repo";

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expected));
            }
        }
    }
}
