using System.Collections;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationClientTests
    {
        public class TheGetAllMethod
        {
            readonly IGitHubClient _github;
            readonly IOrganizationsClient _organizationsClient;

            public TheGetAllMethod()
            {
                _github = EnterpriseHelper.GetAuthenticatedClient();

                _organizationsClient = _github.Organization;
            }

            [GitHubEnterpriseTest]
            public async Task CanListAllOrganizations()
            {
                string orgLogin1 = Helper.MakeNameWithTimestamp("MyOrganization1");
                string orgName1 = string.Concat(orgLogin1, " Display Name 1");
                string orgLogin2 = Helper.MakeNameWithTimestamp("MyOrganization2");
                string orgName2 = string.Concat(orgLogin2, " Display Name 2");

                var newOrganization1 = new NewOrganization(orgLogin1, EnterpriseHelper.UserName, orgName1);
                var newOrganization2 = new NewOrganization(orgLogin2, EnterpriseHelper.UserName, orgName2);
                await _github.Enterprise.Organization.Create(newOrganization1);
                await _github.Enterprise.Organization.Create(newOrganization2);

                var organizations = await _organizationsClient.GetAll();

                Assert.Contains(organizations, (org => org.Login == orgLogin1));
                Assert.Contains(organizations, (org => org.Login == orgLogin2));
            }

            [GitHubEnterpriseTest]
            public async Task ReturnsCorrectOrganizationsWithSince()
            {
                string orgLogin1 = Helper.MakeNameWithTimestamp("MyOrganization1");
                string orgName1 = string.Concat(orgLogin1, " Display Name 1");
                string orgLogin2 = Helper.MakeNameWithTimestamp("MyOrganization2");
                string orgName2 = string.Concat(orgLogin2, " Display Name 2");
                string orgLogin3 = Helper.MakeNameWithTimestamp("MyOrganization3");
                string orgName3 = string.Concat(orgLogin3, " Display Name 3");

                var newOrganization1 = new NewOrganization(orgLogin1, EnterpriseHelper.UserName, orgName1);
                var newOrganization2 = new NewOrganization(orgLogin2, EnterpriseHelper.UserName, orgName2);
                var newOrganization3 = new NewOrganization(orgLogin3, EnterpriseHelper.UserName, orgName3);

                var createdOrganization1 = await _github.Enterprise.Organization.Create(newOrganization1);
                var createdOrganization2 = await _github.Enterprise.Organization.Create(newOrganization2);
                var createdOrganization3 = await _github.Enterprise.Organization.Create(newOrganization3);

                var requestParameter = new OrganizationRequest(createdOrganization1.Id);

                var organizations = await _organizationsClient.GetAll(requestParameter);

                Assert.DoesNotContain(organizations, (org => org.Login == orgLogin1));
                Assert.Contains(organizations, (org => org.Login == orgLogin2));
                Assert.Contains(organizations, (org => org.Login == orgLogin3));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            readonly IGitHubClient _github;
            readonly IOrganizationsClient _organizationsClient;

            public TheGetAllForCurrentMethod()
            {
                _github = EnterpriseHelper.GetAuthenticatedClient();

                _organizationsClient = _github.Organization;
            }

            [GitHubEnterpriseTest]
            public async Task CanListUserOrganizations()
            {
                string orgLogin = Helper.MakeNameWithTimestamp("MyOrganization");
                string orgName = string.Concat(orgLogin, " Display Name");

                var newOrganization = new NewOrganization(orgLogin, EnterpriseHelper.UserName, orgName);
                var organization = await _github.Enterprise.Organization.Create(newOrganization);

                Assert.NotNull(organization);

                var organizations = await _organizationsClient.GetAllForCurrent();

                Assert.NotEmpty(organizations);
            }

            [GitHubEnterpriseTest]
            public async Task ReturnsCorrectCountOfUserOrganizationsWithoutStart()
            {
                string orgLogin1 = Helper.MakeNameWithTimestamp("MyOrganization1");
                string orgName1 = string.Concat(orgLogin1, " Display Name 1");
                string orgLogin2 = Helper.MakeNameWithTimestamp("MyOrganization2");
                string orgName2 = string.Concat(orgLogin2, " Display Name 2");

                var newOrganization1 = new NewOrganization(orgLogin1, EnterpriseHelper.UserName, orgName1);
                var newOrganization2 = new NewOrganization(orgLogin2, EnterpriseHelper.UserName, orgName2);
                await _github.Enterprise.Organization.Create(newOrganization1);
                await _github.Enterprise.Organization.Create(newOrganization2);

                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var organizations = await _organizationsClient.GetAllForCurrent(options);

                Assert.Equal(2, organizations.Count);
            }

            [GitHubEnterpriseTest]
            public async Task ReturnsCorrectCountOfUserOrganizationsWithStart()
            {
                string orgLogin1 = Helper.MakeNameWithTimestamp("MyOrganization1");
                string orgName1 = string.Concat(orgLogin1, " Display Name 1");
                string orgLogin2 = Helper.MakeNameWithTimestamp("MyOrganization2");
                string orgName2 = string.Concat(orgLogin2, " Display Name 2");

                var newOrganization1 = new NewOrganization(orgLogin1, EnterpriseHelper.UserName, orgName1);
                var newOrganization2 = new NewOrganization(orgLogin2, EnterpriseHelper.UserName, orgName2);
                await _github.Enterprise.Organization.Create(newOrganization1);
                await _github.Enterprise.Organization.Create(newOrganization2);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2,
                };

                var organizations = await _organizationsClient.GetAllForCurrent(options);

                Assert.Equal(1, organizations.Count);
            }

            [GitHubEnterpriseTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                string orgLogin1 = Helper.MakeNameWithTimestamp("MyOrganization1");
                string orgName1 = string.Concat(orgLogin1, " Display Name 1");
                string orgLogin2 = Helper.MakeNameWithTimestamp("MyOrganization2");
                string orgName2 = string.Concat(orgLogin2, " Display Name 2");
                string orgLogin3 = Helper.MakeNameWithTimestamp("MyOrganization3");
                string orgName3 = string.Concat(orgLogin3, " Display Name 3");

                var newOrganization1 = new NewOrganization(orgLogin1, EnterpriseHelper.UserName, orgName1);
                var newOrganization2 = new NewOrganization(orgLogin2, EnterpriseHelper.UserName, orgName2);
                var newOrganization3 = new NewOrganization(orgLogin3, EnterpriseHelper.UserName, orgName3);
                await _github.Enterprise.Organization.Create(newOrganization1);
                await _github.Enterprise.Organization.Create(newOrganization2);
                await _github.Enterprise.Organization.Create(newOrganization3);

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await _organizationsClient.GetAllForCurrent(startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _organizationsClient.GetAllForCurrent(skipStartOptions);

                Assert.NotEqual(firstPage[0].Login, secondPage[0].Login);
            }
        }
    }
}
