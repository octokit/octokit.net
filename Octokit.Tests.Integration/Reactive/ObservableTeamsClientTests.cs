﻿using System;
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
            Assert.ThrowsAsync<NotFoundException>(async () => await _github.Organization.Team.GetMembershipDetails(_teamContext.TeamId, "foo"));
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

            using (var repositoryContext = await github.CreateRepositoryContext(Helper.Organization, new NewRepository(Helper.MakeNameWithTimestamp("teamrepo"))))
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
                Assert.Equal(1, pendingInvitations.Count);
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
                Assert.Equal(1, firstPagePendingInvitations.Count);

                var secondPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 2
                };
                var secondObservable = _client.GetAllPendingInvitations(teamContext.TeamId, secondPageOptions);
                var secondPagePendingInvitations = await secondObservable.ToList();
                Assert.NotEmpty(secondPagePendingInvitations);
                Assert.Equal(1, secondPagePendingInvitations.Count);

                Assert.NotEqual(firstPagePendingInvitations[0].Login, secondPagePendingInvitations[0].Login);
            }
        }
    }
}
