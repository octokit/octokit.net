using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryForksClientTests
    {
        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task ReturnsForksForProject()
            {
                var github = Helper.GetAuthenticatedClient();

                var forks = await github.Repository.Forks.Get("octokit", "octokit.net");

                var masterFork = forks.FirstOrDefault(fork => fork.FullName == "TeamBinary/octokit.net");
                Assert.NotNull(masterFork);
                Assert.Equal("TeamBinary", masterFork.Owner.Login);
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
                Assert.Equal(String.Format("{0}/octokit.net", Helper.Credentials.Login), forkCreated.FullName);
                Assert.Equal(true, forkCreated.Fork);
            }

            [IntegrationTest]
            public async Task ForkCreatedForOrganization()
            {
                // The fork is created asynchronially by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts.
                Helper.DeleteRepo(Helper.Organization, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create("octokit", "octokit.net", new NewRepositoryFork { Organization = Helper.Organization });

                Assert.NotNull(forkCreated);
                Assert.Equal(String.Format("{0}/octokit.net", Helper.Organization), forkCreated.FullName);
                Assert.Equal(true, forkCreated.Fork);
            }
        }
    }
}
