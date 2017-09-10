﻿using System;
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

    public class TheAddOrEditMembershipMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheAddOrEditMembershipMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            var newTeam = new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"));
            newTeam.Maintainers.Add(Helper.UserName);

            _teamContext = _github.CreateTeamContext(Helper.Organization, newTeam).Result;
        }

        [OrganizationTest]
        public async Task AddsMembership()
        {
            var login = "octokitnet-test1";

            var membership = await _github.Organization.Team.AddOrEditMembership(_teamContext.TeamId, login, new UpdateTeamMembership(TeamRole.Member));

            Assert.Equal(TeamRole.Member, membership.Role);
            Assert.Equal(MembershipState.Pending, membership.State);
        }

        [OrganizationTest]
        public async Task EditsMembership()
        {
            var login = "octokitnet-test1";

            // Add as member
            await _github.Organization.Team.AddOrEditMembership(_teamContext.TeamId, login, new UpdateTeamMembership(TeamRole.Member));

            // Update to maintainer
            var membership = await _github.Organization.Team.AddOrEditMembership(_teamContext.TeamId, login, new UpdateTeamMembership(TeamRole.Maintainer));

            Assert.Equal(TeamRole.Maintainer, membership.Role);
            Assert.Equal(MembershipState.Pending, membership.State);
        }

        public void Dispose()
        {
            if (_teamContext != null)
            {
                _teamContext.Dispose();
            }
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

    public class TheGetMembershipDetailsMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheGetMembershipDetailsMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            var newTeam = new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"));
            newTeam.Maintainers.Add(Helper.UserName);

            _teamContext = _github.CreateTeamContext(Helper.Organization, newTeam).Result;
        }

        [OrganizationTest]
        public async Task GetsMembershipDetails()
        {
            var membership = await _github.Organization.Team.GetMembershipDetails(_teamContext.TeamId, Helper.UserName);

            Assert.Equal(TeamRole.Maintainer, membership.Role);
            Assert.Equal(MembershipState.Active, membership.State);
        }

        [OrganizationTest]
        public async Task ThrowsWhenNotAMember()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => _github.Organization.Team.GetMembershipDetails(_teamContext.TeamId, "foo"));
        }

        public void Dispose()
        {
            if (_teamContext != null)
            {
                _teamContext.Dispose();
            }
        }
    }

    public class TheGetAllMembersMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheGetAllMembersMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            var newTeam = new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"));
            newTeam.Maintainers.Add(Helper.UserName);

            _teamContext = _github.CreateTeamContext(Helper.Organization, newTeam).Result;
        }

        [OrganizationTest]
        public async Task GetsAllMembers()
        {
            var members = await _github.Organization.Team.GetAllMembers(_teamContext.TeamId);

            Assert.Contains(Helper.UserName, members.Select(u => u.Login));
        }

        [OrganizationTest]
        public async Task GetsAllMembersWithRoleFilter()
        {
            var members = await _github.Organization.Team.GetAllMembers(_teamContext.TeamId, new TeamMembersRequest(TeamRoleFilter.Member));

            Assert.Empty(members);
        }

        public void Dispose()
        {
            if (_teamContext != null)
            {
                _teamContext.Dispose();
            }
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

    public class TheGetAllPendingInvitationsMethod
    {
        private readonly IGitHubClient _gitHub;

        public TheGetAllPendingInvitationsMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [OrganizationTest]
        public async Task ReturnsNoPendingInvitations()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var team = teamContext.Team;

                var pendingInvitations = await _gitHub.Organization.Team.GetAllPendingInvitations(team.Id);
                Assert.NotNull(pendingInvitations);
                Assert.Empty(pendingInvitations);
            }
        }

        [OrganizationTest]
        public async Task ReturnsPendingInvitations()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var pendingInvitations = await _gitHub.Organization.Team.GetAllPendingInvitations(teamContext.TeamId);
                Assert.NotEmpty(pendingInvitations);
                Assert.Equal(2, pendingInvitations.Count);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithoutStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var pendingInvitations = await _gitHub.Organization.Team.GetAllPendingInvitations(teamContext.TeamId, options);
                Assert.NotEmpty(pendingInvitations);
                Assert.Equal(1, pendingInvitations.Count);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var firstPageOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                var firstPagePendingInvitations = await _gitHub.Organization.Team.GetAllPendingInvitations(teamContext.TeamId, firstPageOptions);
                Assert.NotEmpty(firstPagePendingInvitations);
                Assert.Equal(1, firstPagePendingInvitations.Count);

                var secondPageOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPagePendingInvitations = await _gitHub.Organization.Team.GetAllPendingInvitations(teamContext.TeamId, secondPageOptions);
                Assert.NotEmpty(secondPagePendingInvitations);
                Assert.Equal(1, secondPagePendingInvitations.Count);

                Assert.NotEqual(firstPagePendingInvitations[0].Login, secondPagePendingInvitations[0].Login);
            }
        }
    }
}
