using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class PackagesTests
    {
        public class TheListClass
        {
            [IntegrationTest]
            public async Task ReturnsAllPackages()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Packages.List(Helper.Organization, PackageType.Container);

                Assert.Empty(result);
            }
        }
    }
}