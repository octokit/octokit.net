using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Models
{
    public class OrganizationInvitationRequestTests
    {
        public class TheConstructor
        {
            [Fact]
            public void CreatesOrganizationInvitationRequestByUserId()
            {
                const int userId = 1;
                var organizationInvitationRequest = new OrganizationInvitationRequest(userId);

                Assert.Equal(userId, organizationInvitationRequest.InviteeId);
                Assert.Equal(OrganizationMembershipRole.DirectMember, organizationInvitationRequest.Role);
                Assert.Null(organizationInvitationRequest.Email);
                Assert.Null(organizationInvitationRequest.TeamIds);
            }

            [Fact]
            public async Task CreatesOrganizationInvitationRequestByUserEmail()
            {
                const string email = "testemail";
                var organizationInvitationRequest = new OrganizationInvitationRequest(email);

                Assert.Equal(email, organizationInvitationRequest.Email);
                Assert.Equal(OrganizationMembershipRole.DirectMember, organizationInvitationRequest.Role);
                Assert.Null(organizationInvitationRequest.InviteeId);
                Assert.Null(organizationInvitationRequest.TeamIds);
            }
        }
    }
}