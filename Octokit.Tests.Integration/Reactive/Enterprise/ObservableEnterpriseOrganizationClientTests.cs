using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseOrganizationClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableEnterpriseOrganizationClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        [GitHubEnterpriseTest]
        public async Task CanCreateOrganization()
        {
            string orgLogin = Helper.MakeNameWithTimestamp("MyOrganization");
            string orgName = String.Concat(orgLogin, " Display Name");

            var newOrganization = new NewOrganization(orgLogin, EnterpriseHelper.UserName, orgName);
            var observable = _github.Enterprise.Organization.Create(newOrganization);
            var organization = await observable;

            Assert.NotNull(organization);

            // Get organization and check login/name
            var checkOrg = await _github.Organization.Get(orgLogin);
            Assert.Equal(checkOrg.Login, orgLogin);
            Assert.Equal(checkOrg.Name, orgName);
        }
    }
}