using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryForksClientTests
    {
        public class TheGetAllMethod
        {
            [IntegrationTest]
            public async Task ReturnsForksForRepository()
            {
                var github = Helper.GetAuthenticatedClient();

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net", null);

                var masterFork = forks.FirstOrDefault(fork => fork.FullName == "TeamBinary/octokit.net");
                Assert.NotNull(masterFork);
                Assert.Equal("TeamBinary", masterFork.Owner.Login);
            }

            [IntegrationTest]
            public async Task ReturnsForksForRepositorySortingTheResultWithOldestFirst()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualForks = (await github.Repository.Forks.GetAll("octokit", "octokit.net", new RepositoryForksListRequest { Sort = Sort.Oldest })).ToArray();
                var sortedForks = actualForks.OrderBy(fork => fork.CreatedAt).ToArray();

                for (var index = 0; index < actualForks.Length; index++)
                {
                    Assert.Equal(sortedForks[index].FullName, actualForks[index].FullName);
                }
            }
        }

        public class TheCreateMethod
        {
            [IntegrationTest]
            public async Task ForkCreatedForUserLoggedIn()
            {
                // The fork is created asynchronially by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts instead of after.
                Helper.DeleteRepo(Helper.Credentials.Login, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create("octokit", "octokit.net", new NewRepositoryFork());

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.UserName), forkCreated.FullName);
                Assert.Equal(true, forkCreated.Fork);
            }

            [OrganizationTest]
            public async Task ForkCreatedForOrganization()
            {
                // The fork is created asynchronially by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts.
                Helper.DeleteRepo(Helper.Organization, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create("octokit", "octokit.net", new NewRepositoryFork { Organization = Helper.Organization });

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.Organization), forkCreated.FullName);
                Assert.Equal(true, forkCreated.Fork);
            }
        }
    }
}
