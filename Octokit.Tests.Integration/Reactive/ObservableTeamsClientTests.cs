﻿using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableTeamsClientTests
{
    public class TheGetMembersMethod
    {
        readonly Team _team;

        public TheGetMembersMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _team = github.Organization.Team.GetAll(Helper.Organization).Result.First();
        }

        [OrganizationTest]
        public async Task GetsAllMembersWhenAuthenticated()
        {
            var github = Helper.GetAuthenticatedClient();

            var client = new ObservableTeamsClient(github);

            var member = await client.GetAllMembers(_team.Id, ApiOptions.None);

            Assert.Equal(Helper.UserName, member.Login);
        }
    }
}
