using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class PackageVersionsClientTests
    {
        public class TheGetAllMethod
        {
            [IntegrationTest(Skip = "Cannot create packages as part of this test, so can never succeed")]
            public async Task ReturnsAllPackageVersions()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.PackageVersions.GetAllForOrg(Helper.Organization, PackageType.Container, "asd");

                Assert.NotEmpty(result);
            }
        }

        public class TheGetMethod
        {
            [IntegrationTest(Skip = "Cannot create packages as part of this test, so can never succeed")]
            public async Task ReturnsAPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.PackageVersions.GetForOrg(Helper.Organization, PackageType.Container, "asd", 1);

                Assert.NotNull(result);
            }
        }
    }
}