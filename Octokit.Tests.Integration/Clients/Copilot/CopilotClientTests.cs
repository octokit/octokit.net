using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients.Copilot
{
    public class CopilotClientTests
    {
        public class TheGetBillingSettingsMethod
        {
            private readonly IGitHubClient _gitHub;

            public TheGetBillingSettingsMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task ReturnsBillingSettingsData()
            {
                var billingSettings = await _gitHub.Copilot.GetSummaryForOrganization(Helper.Organization);

                Assert.NotNull(billingSettings.SeatManagementSetting);
                Assert.NotNull(billingSettings.PublicCodeSuggestions);
            }
        }
        
        public class TheGetAllLicensesMethod
        {
            private readonly IGitHubClient _gitHub;

            public TheGetAllLicensesMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task ReturnsUserCopilotLicenseDetailsAsList()
            {
                using (var context = await _gitHub.CreateCopilotUserLicenseContext(Helper.Organization, Helper.UserName))
                {
                    var licenses = await _gitHub.Copilot.Licensing.GetAll(Helper.Organization,  new ApiOptions());

                    Assert.True(licenses.Count > 0);                   
                }
            }
        }

        public class TheAddLicenseMethod
        {
            private readonly IGitHubClient _gitHub;

            public TheAddLicenseMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task AddsLicenseForUser()
            {
                using (var context = await _gitHub.CreateCopilotUserLicenseContext(Helper.Organization, Helper.UserName))
                {
                    var allocation = await _gitHub.Copilot.Licensing.Assign(Helper.Organization, Helper.UserName);

                    Assert.True(allocation.SeatsCreated > 0);                    
                }
            }
            
            [OrganizationTest]
            public async Task AddsLicenseForUsers()
            {
                using (var context = await _gitHub.CreateCopilotUserLicenseContext(Helper.Organization, Helper.UserName))
                {
                    var seatAllocation = new UserSeatAllocation() { SelectedUsernames = new[] { Helper.UserName } };

                    var allocation = await _gitHub.Copilot.Licensing.Assign(Helper.Organization, seatAllocation);

                    Assert.True(allocation.SeatsCreated > 0);
                }
            }
        }

        public class TheDeleteLicenseMethod
        {
            private readonly IGitHubClient _gitHub;

            public TheDeleteLicenseMethod()
            {
                _gitHub = Helper.GetAuthenticatedClient();
            }

            [OrganizationTest]
            public async Task RemovesLicenseForUser()
            {
                using (var context = await _gitHub.CreateCopilotUserLicenseContext(Helper.Organization, Helper.UserName))
                {
                    var allocation = await _gitHub.Copilot.Licensing.Remove(Helper.Organization, Helper.UserName);

                    Assert.True(allocation.SeatsCancelled > 0);
                }
            }

            [OrganizationTest]
            public async Task RemovesLicenseForUsers()
            {
                using (var context = await _gitHub.CreateCopilotUserLicenseContext(Helper.Organization, Helper.UserName))
                {
                    var seatAllocation = new UserSeatAllocation() { SelectedUsernames = new[] { Helper.UserName } };

                    var allocation = await _gitHub.Copilot.Licensing.Remove(Helper.Organization, seatAllocation);

                    Assert.True(allocation.SeatsCancelled > 0);                    
                }
            }
        }
    }
}
