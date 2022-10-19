using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class PackagesClientTests
    {
        public class TheGetAllMethod
        {
            [IntegrationTest(Skip = "Cannot create packages as part of this test, so can never succeed")]
            public async Task ReturnsAllPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.GetAllForOrg(Helper.Organization, PackageType.Container);

                Assert.NotEmpty(result);
            }
        }

        public class TheGetMethod
        {
            [IntegrationTest(Skip = "Cannot create packages as part of this test, so can never succeed")]
            public async Task ReturnsAPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.GetForOrg(Helper.Organization, PackageType.Container, "asd");

                Assert.NotNull(result);
            }
        }
    }
}