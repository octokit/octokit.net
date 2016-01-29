using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class EnterpriseLicenseClientTests
{
    readonly IGitHubClient _github;

    public EnterpriseLicenseClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();
    }

    [GitHubEnterpriseTest]
    public async Task CanGetLicense()
    {
        var licenseInfo = await
            _github.Enterprise.License.Get();

        Assert.NotNull(licenseInfo);
    }
}