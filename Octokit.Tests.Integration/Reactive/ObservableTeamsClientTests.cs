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
        private readonly Team _team;

        public TheGetAllPendingInvitationsMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
            _team = _gitHub.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task ReturnsNoPendingInvitations()
        {
            var client = new ObservableTeamsClient(_gitHub);
            var observable = client.GetAllPendingInvitations(_team.Id);
            var pendingInvitations = await observable.ToList();

            Assert.NotNull(pendingInvitations);
            Assert.Empty(pendingInvitations);
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithoutStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam("team")))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var client = new ObservableTeamsClient(_gitHub);
                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 2
                };
                var observable = client.GetAllPendingInvitations(teamContext.TeamId, options);
                var pendingInvitations = await observable.ToList();
                Assert.NotEmpty(pendingInvitations);
                Assert.Equal(2, pendingInvitations.Count);
            }
        }

        [OrganizationTest]
        public async Task ReturnsCorrectCountOfPendingInvitationsWithStart()
        {
            using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam("team")))
            {
                teamContext.InviteMember("octokitnet-test1");
                teamContext.InviteMember("octokitnet-test2");

                var client = new ObservableTeamsClient(_gitHub);
                var firstPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };
                var firstObservable = client.GetAllPendingInvitations(teamContext.TeamId, firstPageOptions);
                var firstPagePendingInvitations = await firstObservable.ToList();
                Assert.NotEmpty(firstPagePendingInvitations);
                Assert.Equal(1, firstPagePendingInvitations.Count);

                var secondPageOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 2
                };
                var secondObservable = client.GetAllPendingInvitations(teamContext.TeamId, secondPageOptions);
                var secondPagePendingInvitations = await secondObservable.ToList();
                Assert.NotEmpty(secondPagePendingInvitations);
                Assert.Equal(1, secondPagePendingInvitations.Count);
            }
        }
    }
}
