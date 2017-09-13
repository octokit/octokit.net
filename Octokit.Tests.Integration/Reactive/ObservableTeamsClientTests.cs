﻿using System.Linq;
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
                Assert.True(teams.Any(x => x.Id == team1.Id));
                Assert.True(teams.Any(x => x.Id == team2.Id));
            }
        }

        [IntegrationTest]
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

                Assert.Equal(1, teams.Count);
                Assert.Equal(team1.Id, teams[0].Id);
            }
        }

        [IntegrationTest]
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

                Assert.Equal(1, teams.Count);
                Assert.Equal(team2.Id, teams[0].Id);
            }
        }

        [IntegrationTest]
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

    public class TheGetAllMembersMethod
    {
        readonly Team _team;

        public TheGetAllMembersMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task GetsAllMembersWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();
            var client = new ObservableTeamsClient(github);

            var observable = client.GetAllMembers(_team.Id, ApiOptions.None);
            var members = await observable.ToList();

            Assert.True(members.Count > 0);
            Assert.True(members.Any(x => x.Login == Helper.UserName));
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
