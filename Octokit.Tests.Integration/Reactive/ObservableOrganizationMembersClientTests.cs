using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationMembersClientTests
    {
        public class TheGetOrganizationMembershipMethod
        {
            readonly IGitHubClient _gitHub;
            readonly ObservableOrganizationMembersClient _client;

            public TheGetOrganizationMembershipMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
                _client = new ObservableOrganizationMembersClient(_gitHub);
            }

            [OrganizationTest]
            public async Task ReturnsUsersMembershipOrganizationMembership()
            {
                using (var teamContext = await _gitHub.CreateTeamContext(Helper.Organization, new NewTeam(Helper.MakeNameWithTimestamp("team"))))
                {
                    teamContext.InviteMember("alfhenrik-test-2");
                    
                    var organizationMemberhip = await _client.GetOrganizationMembership(Helper.Organization, "alfhenrik-test-2");
                    Assert.Equal(MembershipState.Pending, organizationMemberhip.State);
                    Assert.Equal(MembershipRole.Member, organizationMemberhip.Role);
                    await _client.RemoveOrganizationMembership(Helper.Organization, "alfhenrik-test-2");
                }
            }
        }
        
        public class TheAddOrUpdateOrganizationMembershipMethod
        {
            readonly ObservableOrganizationMembersClient _client;

            public TheAddOrUpdateOrganizationMembershipMethod()
            {
                _client = new ObservableOrganizationMembersClient(Helper.GetAuthenticatedClient());
            }

            [OrganizationTest]
            public async Task ReturnsUsersPendingMemberOrganizationMembership()
            {
                var organizationMembership = await _client.AddOrUpdateOrganizationMembership(Helper.Organization, "alfhenrik-test-2", new OrganizationMembershipUpdate());
                Assert.Equal(MembershipState.Pending, organizationMembership.State);
                Assert.Equal(MembershipRole.Member, organizationMembership.Role);
                await _client.RemoveOrganizationMembership(Helper.Organization, "alfhenrik-test-2");
            }

            [OrganizationTest]
            public async Task ReturnsUsersPendingAdminOrganizationMembership()
            {
                var organizationMembership = await _client.AddOrUpdateOrganizationMembership(Helper.Organization, "alfhenrik-test-2", new OrganizationMembershipUpdate { Role = MembershipRole.Admin});
                Assert.Equal(MembershipState.Pending, organizationMembership.State);
                Assert.Equal(MembershipRole.Admin, organizationMembership.Role);
                await _client.RemoveOrganizationMembership(Helper.Organization, "alfhenrik-test-2");
            }
        }

        public class TheRemoveOrganizationMembershipMethod
        {
            readonly ObservableOrganizationMembersClient _client;
            
            public TheRemoveOrganizationMembershipMethod()
            {
                _client = new ObservableOrganizationMembersClient(Helper.GetAuthenticatedClient());
            }
            
            [OrganizationTest]
            public async Task RemovesOrganizationMembership()
            {
                await _client.AddOrUpdateOrganizationMembership(Helper.Organization, "alfhenrik-test-2", new OrganizationMembershipUpdate());
                await _client.RemoveOrganizationMembership(Helper.Organization, "alfhenrik-test-2");
                await Assert.ThrowsAsync<NotFoundException>(() => _client.GetOrganizationMembership(Helper.Organization, "alfhenrik-test-2").ToTask());
            }
        }

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
