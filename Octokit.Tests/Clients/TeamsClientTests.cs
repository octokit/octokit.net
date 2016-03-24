using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class TeamsClientTests
    {
        public class TheConstructor
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
            public void RequestsTheCorrectlUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.Get(1);

                connection.Received().Get<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"));
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

                connection.Received().GetAll<Team>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll(null));
            }
        }

        public class TheGetMembersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllMembers(1);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "teams/1/members"));
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

                connection.Received().Post<Team>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"), team);
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

                client.Update(1, team);

                connection.Received().Patch<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"), team);
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
                client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheAddMembershipMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();

                var client = new TeamsClient(connection);

                await client.AddMembership(1, "user");

                connection.Received().Put<Dictionary<string, string>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"),
                    Args.Object);
            }

            [Fact]
            public async Task AllowsEmptyBody()
            {
                var connection = Substitute.For<IConnection>();

                var apiConnection = new ApiConnection(connection);

                var client = new TeamsClient(apiConnection);

                await client.AddMembership(1, "user");

                connection.Received().Put<Dictionary<string, string>>(
                    Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"),
                    Arg.Is<object>(u => u == RequestBody.Empty));
            }

            [Fact]
            public async Task EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddMembership(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddMembership(1, ""));
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

                connection.Received().GetAll<Team>(Arg.Is<Uri>(u => u.ToString() == "user/teams"));
            }
        }

        public class TheGetMembershipMethod
        {
            [Fact]
            public void EnsuresNonNullLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetMembership(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                Assert.ThrowsAsync<ArgumentException>(() => client.GetMembership(1, ""));
            }
        }

        public class TheRRemoveMembershipMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.RemoveMembership(1, "user");

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
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
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.GetAllRepositories(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"));

                client.GetAllRepositories(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"));
            }
        }

        public class TheRemoveRepositoryMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.RemoveRepository(1, "org", "repo");

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
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.AddRepository(1, "org", "repo");

                connection.Connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }

            [Fact]
            public void EnsureNonNullOrg()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                Assert.ThrowsAsync<ArgumentException>(() => client.AddRepository(1, null, "Repo Name"));
            }
        }

        public class TheIsRepositoryManagedByTeamMethod
        {
            [Fact]
            public void EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                Assert.ThrowsAsync<ArgumentException>(() => client.AddRepository(1, "org name", null));

                // Check owner arguments.
                Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, null, "repoName"));
                Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "", "repoName"));

                // Check repo arguments.
                Assert.ThrowsAsync<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", null));
                Assert.ThrowsAsync<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", ""));
            }
        }
    }
}
