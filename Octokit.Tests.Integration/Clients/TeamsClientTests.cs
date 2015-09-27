using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class TeamsClientTests
{
    public class TheCreateMethod
    {
        [OrganizationTest]
        public async Task FailsWhenNotAuthenticated()
        {
            var github = Helper.GetAnonymousClient();
            var newTeam = new NewTeam("Test");

            var e = await Assert.ThrowsAsync<AuthorizationException>(() => github.Organization.Team.Create(Helper.Organization, newTeam));

            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for the resolution to this failing test")]
        public async Task FailsWhenAuthenticatedWithBadCredentials()
        {
            var github = Helper.GetBadCredentialsClient();

            var newTeam = new NewTeam("Test");

            var e = await Assert.ThrowsAsync<AuthorizationException>(() => github.Organization.Team.Create(Helper.Organization, newTeam));
            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task SucceedsWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();

            var newTeam = new NewTeam(Guid.NewGuid().ToString());

            var team = await github.Organization.Team.Create(Helper.Organization, newTeam);

            Assert.Equal(newTeam.Name, team.Name);
        }
    }

    public class TheGetAllForCurrentMethod
    {
        [IntegrationTest]
        public async Task GetsIsMemberWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();
            var teams = await github.Organization.Team.GetAllForCurrent();
            Assert.NotEmpty(teams);
        }
    }

    public class TheGetMembershipMethod
    {
        readonly Team team;

        public TheGetMembershipMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task FailsWhenAuthenticatedWithBadCredentials()
        {
            var github = Helper.GetBadCredentialsClient();

            var e = await Assert.ThrowsAsync<AuthorizationException>(
                () => github.Organization.Team.GetMembership(team.Id, Helper.UserName));
            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task GetsIsMemberWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();

            var membership = await github.Organization.Team.GetMembership(team.Id, Helper.UserName);

            Assert.Equal(TeamMembership.Active, membership);
        }

        [OrganizationTest]
        public async Task GetsIsMemberFalseForNonMemberWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();

            var membership = await github.Organization.Team.GetMembership(team.Id, "foo");

            Assert.Equal(TeamMembership.NotFound, membership);
        }
    }

    public class TheGetMembersMethod
    {
        readonly Team team;

        public TheGetMembersMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task GetsAllMembersWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();

            var members = await github.Organization.Team.GetAllMembers(team.Id);

            Assert.Contains(Helper.UserName, members.Select(u => u.Login));
        }
    }
}
