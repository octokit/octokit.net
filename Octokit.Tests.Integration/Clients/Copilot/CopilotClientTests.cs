using System.Threading.Tasks;
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
                var billingSettings = await _gitHub.Copilot.Get(Helper.Organization);

                Assert.True(string.IsNullOrEmpty(billingSettings.SeatManagementSetting));
                Assert.True(string.IsNullOrEmpty(billingSettings.PublicCodeSuggestions));
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
                var licenses = await _gitHub.Copilot.License.GetAll(Helper.Organization,  new CopilotApiOptions());

                Assert.True(licenses.Count > 0);
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
                var allocation = await _gitHub.Copilot.License.Assign(Helper.Organization, Helper.UserName);

                Assert.True(allocation.SeatsCreated > 0);
            }
            
            [OrganizationTest]
            public async Task AddsLicenseForUsers()
            {
                var seatAllocation = new UserSeatAllocation() { SelectedUsernames = new[] { Helper.UserName } };

                var allocation = await _gitHub.Copilot.License.Assign(Helper.Organization, seatAllocation);

                Assert.True(allocation.SeatsCreated > 0);
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
                var allocation = await _gitHub.Copilot.License.Remove(Helper.Organization, Helper.UserName);

                Assert.True(allocation.SeatsCancelled > 0);
            }

            [OrganizationTest]
            public async Task RemovesLicenseForUsers()
            {
                var seatAllocation = new UserSeatAllocation() { SelectedUsernames = new[] { Helper.UserName } };

                var allocation = await _gitHub.Copilot.License.Remove(Helper.Organization, seatAllocation);

                Assert.True(allocation.SeatsCancelled > 0);
            }
        }
    }
}
