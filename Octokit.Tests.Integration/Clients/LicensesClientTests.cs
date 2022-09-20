using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class LicensesClientTests
    {
        public class TheGetLicensesMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveListOfLicenses()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Licenses.GetAllLicenses();

                Assert.True(result.Count > 2);
                Assert.Contains(result, license => license.Key == "mit");
            }

            [IntegrationTest]
            public async Task CanRetrieveListOfLicensesWithPagination()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 5,
                };

                var result = await github.Licenses.GetAllLicenses(options);

                Assert.Equal(5, result.Count);
            }

            [IntegrationTest]
            public async Task CanRetrieveDistinctListOfLicensesBasedOnPageStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstPage = await github.Licenses.GetAllLicenses(startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await github.Licenses.GetAllLicenses(skipStartOptions);

                Assert.NotEqual(firstPage[0].Key, secondPage[0].Key);
            }
        }

        public class TheGetLicenseMethod
        {
            [IntegrationTest]
            public async Task CanRetrieveListOfLicenses()
            {
                var github = Helper.GetAuthenticatedClient();

                var result = await github.Licenses.GetLicense("mit");

                Assert.Equal("mit", result.Key);
                Assert.Equal("MIT License", result.Name);
            }

            [IntegrationTest]
            public async Task ReportsErrorWhenInvalidLicenseProvided()
            {
                var github = Helper.GetAuthenticatedClient();

                await Assert.ThrowsAsync<NotFoundException>(() => github.Licenses.GetLicense("purple-monkey-dishwasher"));
            }
        }
    }
}