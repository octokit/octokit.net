using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Xunit;
using Xunit.Extensions;

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

                connection.Received().Get<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"), null);
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

        public class TheAddMemberMethod
        {
#warning TODO: implement RequestsTheCorrectUrl test for TheAddMemberMethod

#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheAddMemberMethod

            [Fact]
            public void EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.AddMember(1, null));
                AssertEx.Throws<ArgumentException>(() => client.AddMember(1, ""));
            }
        }

        public class TheRemoveMemberMethod
        {
#warning TODO: implement RequestsTheCorrectUrl test for TheRemoveMemberMethod

#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheRemoveMemberMethod

            [Fact]
            public void EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.RemoveMember(1, null));
                AssertEx.Throws<ArgumentException>(() => client.RemoveMember(1, ""));
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

        public class TheGetRepositoriesMethod
        {
#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheGetRepositoriesMethod

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetRepositories(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"));
            }
        }

        public class TheAddRepositoryMethod
        {
#warning TODO: implement RequestsTheCorrectUrl test for TheAddRepositoryMethod

#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheAddRepositoryMethod

            [Fact]
            public void EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check org arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.AddRepository(1, null, "repoName"));
                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, "", "repoName"));

                // Check repo arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.AddRepository(1, "orgName", null));
                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, "orgName", ""));
            }
        }

        public class TheRemoveRepositoryMethod
        {
#warning TODO: implement RequestsTheCorrectUrl test for TheRemoveRepositoryMethod

#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheRemoveRepositoryMethod

            [Fact]
            public void EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check owner arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.RemoveRepository(1, null, "repoName"));
                AssertEx.Throws<ArgumentException>(() => client.RemoveRepository(1, "", "repoName"));

                // Check repo arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.RemoveRepository(1, "ownerName", null));
                AssertEx.Throws<ArgumentException>(() => client.RemoveRepository(1, "ownerName", ""));
            }
        }

        public class TheIsRepositoryManagedByTeamMethod
        {
#warning TODO: implement RequestsTheCorrectUrl test for TheIsRepositoryManagedByTeamMethod

#warning TODO: implement ReturnsCorrectResultBasedOnStatus test for TheIsRepositoryManagedByTeamMethod

            [Fact]
            public void EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check owner arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, null, "repoName"));
                AssertEx.Throws<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "", "repoName"));

                // Check repo arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", null));
                AssertEx.Throws<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", ""));
            }
        }
    }
}
