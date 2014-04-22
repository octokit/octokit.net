using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Internal;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;
using System;
using System.Collections.Generic;
using Xunit.Sdk;

public class TeamsClientTests
{
    public class TheCreateMethod
    {
        [OrganizationTest]
        public async Task FailsWhenNotAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"));
            var newTeam = new NewTeam("Test");

            var e = await AssertEx.Throws<AuthorizationException>(async
                () => await github.Organization.Team.Create(Helper.Organization, newTeam));

            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task FailsWhenAuthenticatedWithBadCredentials()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = new Credentials(Helper.Credentials.Login, "bad-password")
            };
            var newTeam = new NewTeam("Test");

            var e = await AssertEx.Throws<AuthorizationException>(async
                () => await github.Organization.Team.Create(Helper.Organization, newTeam));
            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task SucceedsWhenAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var newTeam = new NewTeam(Guid.NewGuid().ToString());

            var team = await github.Organization.Team.Create(Helper.Organization, newTeam);

            Assert.Equal(newTeam.Name, team.Name);
        }
    }

    public class TheIsMemberMethod
    {
        readonly Team team;

        public TheIsMemberMethod()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests")) { Credentials = Helper.Credentials };

            team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        //TODO: seems like a bug in Github: it's actually returning the membership information!
        //Maybe because it's a public organization?
        //[OrganizationTest]
        public async Task FailsWhenNotAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"));

            var e = await AssertEx.Throws<AuthorizationException>(async
                () => await github.Organization.Team.IsMember(team.Id, Helper.UserName));

            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task FailsWhenAuthenticatedWithBadCredentials()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = new Credentials(Helper.Credentials.Login, "bad-password")
            };

            var e = await AssertEx.Throws<AuthorizationException>(async
                () => await github.Organization.Team.IsMember(team.Id, Helper.UserName));
            Assert.Equal(HttpStatusCode.Unauthorized, e.StatusCode);
        }

        [OrganizationTest]
        public async Task GetsIsMemberWhenAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var isMember = await github.Organization.Team.IsMember(team.Id, Helper.UserName);

            Assert.True(isMember);
        }

        [OrganizationTest]
        public async Task GetsIsMemberFalseForNonMemberWhenAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var isMember = await github.Organization.Team.IsMember(team.Id, "foo");

            Assert.False(isMember);
        }
    }

    public class TheGetMembersMethod
    {
        readonly Team team;

        public TheGetMembersMethod()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests")) { Credentials = Helper.Credentials };

            team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task GetsAllMembersWhenAuthenticated()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var members = await github.Organization.Team.GetMembers(team.Id);

            Assert.Contains(Helper.UserName, members.Select(u => u.Login));
        }
    }
}
