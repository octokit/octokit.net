using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class ObservableTeamsClientTests
{
    public class TheGetAllChildTeamsMethod
    {
        private readonly IObservableGitHubClient _github;

        public TheGetAllChildTeamsMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
        }

        [OrganizationTest]
        public async Task GetsChildTeams()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            {
                var team1 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });
                var team2 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });

                var teams = await _github.Organization.Team.GetAllChildTeams(parentTeamContext.TeamId).ToList();

                Assert.Equal(2, teams.Count);
                Assert.Contains(teams, x => x.Id == team1.Id);
                Assert.Contains(teams, x => x.Id == team2.Id);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfChildTeamsWithoutStart()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            {
                var team1 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });
                var team2 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var teams = await _github.Organization.Team.GetAllChildTeams(parentTeamContext.TeamId, options).ToList();

                Assert.Single(teams);
                Assert.Equal(team1.Id, teams[0].Id);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfChildTeamsWithStart()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            {
                var team1 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });
                var team2 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var teams = await _github.Organization.Team.GetAllChildTeams(parentTeamContext.TeamId, options).ToList();

                Assert.Single(teams);
                Assert.Equal(team2.Id, teams[0].Id);
            }
        }

        [OrganizationTest]
        public async Task ReturnsDistinctChildTeamsBasedOnStartPage()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            {
                var team1 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });
                var team2 = await _github.Organization.Team.Create(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("child-team")) { Privacy = TeamPrivacy.Closed, ParentTeamId = parentTeamContext.TeamId });

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await _github.Organization.Team.GetAllChildTeams(parentTeamContext.TeamId, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _github.Organization.Team.GetAllChildTeams(parentTeamContext.TeamId, skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            }
        }
    }

    public class TheAddOrEditMembershipMethod : IDisposable
    {
        private readonly IObservableGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheAddOrEditMembershipMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

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

    public class TheGetMembershipDetailsMethod : IDisposable
    {
        private readonly IObservableGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheGetMembershipDetailsMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

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
            await Assert.ThrowsAsync<NotFoundException>(async () => await _github.Organization.Team.GetMembershipDetails(_teamContext.TeamId, "foo"));
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
        private readonly IObservableGitHubClient _github;
        private readonly TeamContext _teamContext;

        public TheGetAllMembersMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

            var newTeam = new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"));
            newTeam.Maintainers.Add(Helper.UserName);

            _teamContext = _github.CreateTeamContext(Helper.Organization, newTeam).Result;
        }

        [OrganizationTest]
        public async Task GetsAllMembers()
        {
            var members = await _github.Organization.Team.GetAllMembers(_teamContext.TeamId).ToList();

            Assert.Contains(Helper.UserName, members.Select(u => u.Login));
        }

        [OrganizationTest]
        public async Task GetsAllMembersWithRequest()
        {
            var members = await _github.Organization.Team.GetAllMembers(_teamContext.TeamId, new TeamMembersRequest(TeamRoleFilter.Member)).ToList();

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
            var client = new ObservableTeamsClient(github);

            using (var repositoryContext = await github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                client.AddRepository(_team.Id, Helper.Organization, repositoryContext.RepositoryName);

                var observable = client.GetAllRepositories(_team.Id, ApiOptions.None);
                var repos = await observable.ToList();

                Assert.True(repos.Count() > 0);
                Assert.NotNull(repos[0].Permissions);
            }
        }
    }

    public class TheGetAllPendingInvitationsMethod
    {
        private readonly IGitHubClient _gitHub;
        private readonly IObservableTeamsClient _client;

        public TheGetAllPendingInvitationsMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
            _client = new ObservableTeamsClient(_gitHub);
        }

        [OrganizationTest]
        public async Task ReturnsNoPendingInvitations()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            {
                var team = teamContext.Team;

                var observable = _client.GetAllPendingInvitations(team.Id);
                var pendingInvitations = await observable.ToList();
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

                var observable = _client.GetAllPendingInvitations(teamContext.TeamId);
                var pendingInvitations = await observable.ToList();
                Assert.NotEmpty(pendingInvitations);
                Assert.Equal(2, pendingInvitations.Count);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithoutStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam("team")))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };
                var observable = _client.GetAllPendingInvitations(teamContext.TeamId, options);
                var pendingInvitations = await observable.ToList();
                Assert.NotEmpty(pendingInvitations);
                Assert.Single(pendingInvitations);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam("team")))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var firstPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var firstObservable = _client.GetAllPendingInvitations(teamContext.TeamId, firstPageOptions);
                var firstPagePendingInvitations = await firstObservable.ToList();
                Assert.NotEmpty(firstPagePendingInvitations);
                Assert.Single(firstPagePendingInvitations);

                var secondPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 2
                };
                var secondObservable = _client.GetAllPendingInvitations(teamContext.TeamId, secondPageOptions);
                var secondPagePendingInvitations = await secondObservable.ToList();
                Assert.NotEmpty(secondPagePendingInvitations);
                Assert.Single(secondPagePendingInvitations);

                Assert.NotEqual(firstPagePendingInvitations[0].Login, secondPagePendingInvitations[0].Login);
            }
        }
    }

    public class TheUpdateMethod
    {
        private readonly IObservableGitHubClient _github;

        public TheUpdateMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
        }

        [OrganizationTest]
        public async Task UpdatesTeam()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"))))
            {
                var teamName = Helper.MakeNameWithTimestamp("updated-team");
                var teamDescription = Helper.MakeNameWithTimestamp("updated description");
                var update = new UpdateTeam(teamName)
                {
                    Description = teamDescription,
                    Privacy = TeamPrivacy.Closed,
                    ParentTeamId = parentTeamContext.TeamId
                };

                var team = await _github.Organization.Team.Update(Helper.Organization, teamContext.Team.Slug, update);

                Assert.Equal(teamName, team.Name);
                Assert.Equal(teamDescription, team.Description);
                Assert.Equal(TeamPrivacy.Closed, team.Privacy);
                Assert.Equal(parentTeamContext.TeamId, team.Parent.Id);

                _github.Organization.Team.Delete(Helper.Organization, team.Slug);
            }
        }
    }

    public class TheUpdateLegacyMethod
    {
        private readonly IObservableGitHubClient _github;

        public TheUpdateLegacyMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
        }

        [OrganizationTest]
        public async Task UpdatesTeamLegacy()
        {
            using (var parentTeamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("parent-team"))))
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team-fixture"))))
            {
                var teamName = Helper.MakeNameWithTimestamp("updated-team");
                var teamDescription = Helper.MakeNameWithTimestamp("updated description");
                var update = new UpdateTeam(teamName)
                {
                    Description = teamDescription,
                    Privacy = TeamPrivacy.Closed,
                    ParentTeamId = parentTeamContext.TeamId
                };

                var team = await _github.Organization.Team.Update(teamContext.TeamId, update);

                Assert.Equal(teamName, team.Name);
                Assert.Equal(teamDescription, team.Description);
                Assert.Equal(TeamPrivacy.Closed, team.Privacy);
                Assert.Equal(parentTeamContext.TeamId, team.Parent.Id);

                _github.Organization.Team.Delete(teamContext.TeamId);
            }
        }
    }

    public class TheCheckTeamPermissionsForARepositoryMethod
    {
        private readonly IObservableGitHubClient _github;
        public TheCheckTeamPermissionsForARepositoryMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

        }

        [OrganizationTest]
        public async Task ChecksTeamPermissions()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repositoryContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                _github.Organization.Team.AddRepository(teamContext.TeamId, Helper.Organization, repositoryContext.RepositoryName);

                var teamPermissionResponse = await _github.Organization.Team.CheckTeamPermissionsForARepository(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repositoryContext.RepositoryOwner,
                    repositoryContext.RepositoryName);

                Assert.True(teamPermissionResponse);
            }
        }

        [OrganizationTest]
        public async Task ChecksTeamPermissionsReturnsFalseOnNonTeamRepository()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repositoryContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                var response = await _github.Organization.Team.CheckTeamPermissionsForARepository(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repositoryContext.RepositoryOwner,
                    repositoryContext.RepositoryName);

                Assert.False(response);
            }
        }
    }

    public class TheCheckTeamPermissionsForARepositoryWithCustomAcceptHeaderMethod
    {
        private readonly IObservableGitHubClient _github;
        public TheCheckTeamPermissionsForARepositoryWithCustomAcceptHeaderMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

        }

        [OrganizationTest]
        public async Task ChecksTeamPermissions()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repositoryContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                _github.Organization.Team.AddRepository(teamContext.TeamId, Helper.Organization, repositoryContext.RepositoryName);

                var teamPermissionResponse = await _github.Organization.Team.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repositoryContext.RepositoryOwner,
                    repositoryContext.RepositoryName);

                Assert.NotNull(teamPermissionResponse);
            }
        }

        [OrganizationTest]
        public async Task ChecksTeamPermissionsThrowsNotFoundException()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repositoryContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
            {
                await Assert.ThrowsAsync<NotFoundException>(async () =>
                    await _github.Organization.Team.CheckTeamPermissionsForARepositoryWithCustomAcceptHeader(
                        Helper.Organization,
                        teamContext.Team.Slug,
                        repositoryContext.RepositoryOwner,
                        repositoryContext.RepositoryName));
            }
        }
    }
    
    public class TheAddOrUpdateTeamRepositoryPermissionsMethod
    {
        private readonly IObservableGitHubClient _github;

        public TheAddOrUpdateTeamRepositoryPermissionsMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
        }

        [OrganizationTest]
        public async Task AddsTeamRepositoryPermissions()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repoContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("team-repository"))))
            {
                var teamRepository = await _github.Organization.Team
                    .GetAllRepositories(teamContext.TeamId)
                    .FirstOrDefaultAsync(x => x.Id == repoContext.RepositoryId);

                Assert.Null(teamRepository);

                await _github.Organization.Team.AddOrUpdateTeamRepositoryPermissions(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    "admin");

                teamRepository = await _github.Organization.Team
                    .GetAllRepositories(teamContext.TeamId)
                    .FirstOrDefaultAsync(x => x.Id == repoContext.RepositoryId);

                Assert.NotNull(teamRepository);
            }
        }

        [OrganizationTest]
        public async Task UpdatesTeamRepositoryPermissions()
        {
            using (var teamContext = await _github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repoContext = await _github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("team-repository"))))
            {
                await _github.Organization.Team.AddOrUpdateTeamRepositoryPermissions(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    "admin");

                var teamRepository = await _github.Organization.Team
                    .GetAllRepositories(teamContext.TeamId)
                    .FirstOrDefaultAsync(x => x.Id == repoContext.RepositoryId);

                Assert.True(teamRepository.Permissions.Admin);

                await _github.Organization.Team.AddOrUpdateTeamRepositoryPermissions(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    "maintain");

                teamRepository = await _github.Organization.Team
                    .GetAllRepositories(teamContext.TeamId)
                    .FirstOrDefaultAsync(x => x.Id == repoContext.RepositoryId);
                
                Assert.True(teamRepository.Permissions.Maintain);
                Assert.False(teamRepository.Permissions.Admin);
            }
        }
    }

    public class TheRemoveRepositoryFromATeamMethod
    {
        [OrganizationTest]
        public async Task RemovesRepositoryFromATeam()
        {
            var github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());

            using (var teamContext = await github.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
            using (var repoContext = await github.CreateOrganizationRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("team-repository"))))
            {
                await github.Organization.Team.AddOrUpdateTeamRepositoryPermissions(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName,
                    "admin");

                await github.Organization.Team.RemoveRepositoryFromATeam(
                    Helper.Organization,
                    teamContext.Team.Slug,
                    repoContext.RepositoryOwner,
                    repoContext.RepositoryName);

                var addedRepo = await github.Organization.Team.GetAllRepositories(teamContext.TeamId).ToList();

                Assert.Empty(addedRepo);
            }
        }
    }
}
