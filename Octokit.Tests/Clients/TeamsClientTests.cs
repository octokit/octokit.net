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
            public void EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => teams.GetAll(null));
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
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("superstars");

                Assert.Throws<ArgumentNullException>(() => client.Create(null, team));
                Assert.Throws<ArgumentException>(() => client.Create("", team));
                Assert.Throws<ArgumentNullException>(() => client.Create("name", null));
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
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                Assert.Throws<ArgumentNullException>(() => client.Update(1, null));
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
    }
}
