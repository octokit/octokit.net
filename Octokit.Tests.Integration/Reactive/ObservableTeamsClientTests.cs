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
using Octokit.Reactive;
using Octokit.Tests.Integration.Clients;

public class ObservableTeamsClientTests
{
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

            var client = new ObservableTeamsClient(github);

            var member = await client.GetMembers(team.Id);

            Assert.Equal(Helper.UserName, member.Login);
        }
    }
}
