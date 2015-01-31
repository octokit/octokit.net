using System;
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

                client.GetMembers(1);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "teams/1/members"));
            }
        }

        public class TheCreateTeamMethod
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

        public class TheIsMemberMethod
        {
            [Fact]
            public void EnsuresNonNullLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.IsMember(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.IsMember(1, ""));
            }
        }

        public class TheAddMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.AddMember(1, "user");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/members/user"));
            }
        }

        public class TheRemoveMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.RemoveMember(1, "user");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/members/user"));
            }
        }

        public class TheGetRepositoriesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.GetRepositories(1);

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

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }
        }

        public class TheAddRepositoryMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.AddRepository(1, "org", "repo");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }

            [Fact]
            public void EnsureNonNullOrg()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, null, "Repo Name"));
            }

            [Fact]
            public void EnsureNonNullRepo()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, "org name", null));

            }
        }
    }
}
