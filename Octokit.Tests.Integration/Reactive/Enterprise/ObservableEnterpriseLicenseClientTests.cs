using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseLicenseClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableEnterpriseLicenseClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        [GitHubEnterpriseTest]
        public async Task CanGet()
        {
            var observable = _github.Enterprise.License.Get();
            var licenseInfo = await observable;

            Assert.NotNull(licenseInfo);
        }
    }
}