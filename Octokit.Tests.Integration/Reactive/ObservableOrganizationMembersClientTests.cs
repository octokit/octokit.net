using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationMembersClientTests
    {
        public class TheGetAllPendingInvitationsMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationMembersClient _client;

            public TheGetAllPendingInvitationsMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationMembersClient(_gitHub);
            }

            [OrganizationTest]
            public async Task ReturnsNoPendingInvitations()
            {
                var pendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization).ToList();

                Assert.Empty(pendingInvitations);
            }

            [OrganizationTest]
            public async Task ReturnsPendingInvitations()
            {
                using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
                {
                    teamContext.InviteMember("octokitnet-test1");
                    teamContext.InviteMember("octokitnet-test2");

                    var pendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization).ToList();
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

                    var pendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization, options).ToList();
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

                    var firstPagePendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization, firstPageOptions).ToList();
                    Assert.NotEmpty(firstPagePendingInvitations);
                    Assert.Equal(1, firstPagePendingInvitations.Count);


                    var secondPageOptions = new ApiOptions
                    {
                        PageSize = 1,
                        PageCount = 1,
                        StartPage = 2
                    };

                    var secondPagePendingInvitations = await _client.GetAllPendingInvitations(Helper.Organization, secondPageOptions).ToList();
                    Assert.NotEmpty(secondPagePendingInvitations);
                    Assert.Equal(1, secondPagePendingInvitations.Count);

                    Assert.NotEqual(firstPagePendingInvitations[0].Login, secondPagePendingInvitations[0].Login);
                }
            }
        }
    }
}
