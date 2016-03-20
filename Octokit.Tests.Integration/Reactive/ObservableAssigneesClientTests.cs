using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableAssigneesClientTests
    {
        public class TheGetAllMethod
        {
            readonly ObservableAssigneesClient _assigneesClient;
            const string owner = "octokit";
            const string name = "octokit.net";

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _assigneesClient = new ObservableAssigneesClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsAssignees()
            {
                var assignees = await _assigneesClient.GetAllForRepository(owner, name).ToList();

                Assert.NotEmpty(assignees);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfAssigneesWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var assignees = await _assigneesClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, assignees.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfAssigneesWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var releases = await _assigneesClient.GetAllForRepository(owner, name, options).ToList();

                Assert.Equal(5, releases.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstPage = await _assigneesClient.GetAllForRepository(owner, name, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondPage = await _assigneesClient.GetAllForRepository(owner, name, skipStartOptions).ToList();

                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
                Assert.NotEqual(firstPage[3].Id, secondPage[3].Id);
                Assert.NotEqual(firstPage[4].Id, secondPage[4].Id);
            }
        }
    }
}