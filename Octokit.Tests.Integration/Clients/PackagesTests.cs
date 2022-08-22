using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class PackagesTests
    {
        public class TheGetAllMethod
        {
            [IntegrationTest]
            public async Task ReturnsAllPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.GetAll(Helper.Organization, PackageType.Container);

                Assert.Empty(result);
            }
        }

        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task ReturnsAPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.Get(Helper.Organization, PackageType.Container, "asd");

                Assert.NotNull(result);
            }
        }
    }

    public class PackageVersionTests
    {
        public class TheGetAllMethod
        {
            [IntegrationTest]
            public async Task ReturnsAllPackageVersions()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.PackageVersions.GetAll(Helper.Organization, PackageType.Container, "asd");

                Assert.Empty(result);
            }
        }
    }
}