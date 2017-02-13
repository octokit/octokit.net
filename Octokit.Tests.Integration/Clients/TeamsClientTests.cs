using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
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
        public async Task GetsAllForCurrentWhenAuthenticated()
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

    public class TheGetAllMembersMethod
    {
        readonly Team team;

        public TheGetAllMembersMethod()
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

    public class TheGetAllRepositoriesMethod
    {
        readonly Team _team;

        public TheGetAllRepositoriesMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task GetsAllRepositories()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var repositoryContext = await github.CreateRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                github.Organization.Team.AddRepository(_team.Id, Helper.Organization, repositoryContext.RepositoryName);

                var repos = await github.Organization.Team.GetAllRepositories(_team.Id);

                Assert.True(repos.Count > 0);
                Assert.NotNull(repos[0].Permissions);
            }
        }
    }

    public class TheAddOrUpdateTeamRepositoryMethod
    {
        private readonly IGitHubClient _github;

        public TheAddOrUpdateTeamRepositoryMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task CanAddRepository()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repoContext = await _github.CreateRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("team-repository"))))
            {
                var team = teamContext.Team;
                var repo = repoContext.Repository;

                var addRepo = await _github.Organization.Team.AddRepository(team.Id, team.Organization.Login, repo.Name, new RepositoryPermissionRequest(Permission.Admin));

                Assert.True(addRepo);

                var addedRepo = await _github.Organization.Team.GetAllRepositories(team.Id);

                //Check if permission was correctly applied
                Assert.True(addedRepo.First(x => x.Id == repo.Id).Permissions.Admin == true);
            }
        }
    }
}
