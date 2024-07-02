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

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net");

                var masterFork = forks.FirstOrDefault(fork => fork.FullName == "TeamBinary/octokit.net");
                Assert.NotNull(masterFork);
                Assert.Equal("TeamBinary", masterFork.Owner.Login);
            }

            [IntegrationTest]
            public async Task ReturnsForksForRepositoryWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var forks = await github.Repository.Forks.GetAll(7528679);

                var masterFork = forks.FirstOrDefault(fork => fork.FullName == "TeamBinary/octokit.net");
                Assert.NotNull(masterFork);
                Assert.Equal("TeamBinary", masterFork.Owner.Login);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithoutStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net", options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithoutStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                var forks = await github.Repository.Forks.GetAll(7528679, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithStart()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net", options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithStartWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var forks = await github.Repository.Forks.GetAll(7528679, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctForksBasedOnStartPage()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", skipStartOptions);

                Assert.Equal(3, firstPage.Count);
                Assert.Equal(3, secondPage.Count);
                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctForksBasedOnStartPageWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll(7528679, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Forks.GetAll(7528679, skipStartOptions);

                Assert.Equal(3, firstPage.Count);
                Assert.Equal(3, secondPage.Count);
                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithoutStartParameterized()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithoutStartParameterizedWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1
                };

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var forks = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithStartParameterized()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var forks = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfForksWithStartParameterizedWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var forks = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, options);

                Assert.Single(forks);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctForksBasedOnStartPageParameterized()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, skipStartOptions);

                Assert.Equal(3, firstPage.Count);
                Assert.Equal(3, secondPage.Count);
                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctForksBasedOnStartPageParameterizedWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Newest };

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, startOptions);

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 2
                };

                var secondPage = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, skipStartOptions);

                Assert.Equal(3, firstPage.Count);
                Assert.Equal(3, secondPage.Count);
                Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
                Assert.NotEqual(firstPage[1].Id, secondPage[1].Id);
                Assert.NotEqual(firstPage[2].Id, secondPage[2].Id);
            }

            [IntegrationTest]
            public async Task ReturnsForksForRepositorySortingTheResultWithOldestFirstWithApiOptions()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Oldest };

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, startOptions);
                var firstPageOrdered = firstPage.OrderBy(r => r.CreatedAt).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var secondPage = await github.Repository.Forks.GetAll("octokit", "octokit.net", repositoryForksListRequest, skipStartOptions);
                var secondPageOrdered = secondPage.OrderBy(r => r.CreatedAt).ToList();

                for (var index = 0; index < firstPage.Count; index++)
                {
                    Assert.Equal(firstPageOrdered[index].FullName, firstPage[index].FullName);
                }

                for (var index = 0; index < firstPage.Count; index++)
                {
                    Assert.Equal(secondPageOrdered[index].FullName, secondPage[index].FullName);
                }
            }

            [IntegrationTest]
            public async Task ReturnsForksForRepositorySortingTheResultWithOldestFirstWithApiOptionsWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var repositoryForksListRequest = new RepositoryForksListRequest { Sort = Sort.Oldest };

                var startOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var firstPage = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, startOptions);
                var firstPageOrdered = firstPage.OrderBy(r => r.CreatedAt).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 3,
                    StartPage = 1
                };

                var secondPage = await github.Repository.Forks.GetAll(7528679, repositoryForksListRequest, skipStartOptions);
                var secondPageOrdered = secondPage.OrderBy(r => r.CreatedAt).ToList();

                for (var index = 0; index < firstPage.Count; index++)
                {
                    Assert.Equal(firstPageOrdered[index].FullName, firstPage[index].FullName);
                }

                for (var index = 0; index < firstPage.Count; index++)
                {
                    Assert.Equal(secondPageOrdered[index].FullName, secondPage[index].FullName);
                }
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

            [IntegrationTest]
            public async Task ReturnsForksForRepositorySortingTheResultWithOldestFirstWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var actualForks = (await github.Repository.Forks.GetAll(7528679, new RepositoryForksListRequest { Sort = Sort.Oldest })).ToArray();
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
                // The fork is created asynchronously by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts instead of after.
                Helper.DeleteRepo(Helper.GetAuthenticatedClient().Connection, Helper.Credentials.Login, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create("octokit", "octokit.net", new NewRepositoryFork());

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.UserName), forkCreated.FullName);
                Assert.True(forkCreated.Fork);
            }

            [IntegrationTest]
            public async Task ForkCreatedForUserLoggedInWithRepositoryId()
            {
                // The fork is created asynchronously by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts instead of after.
                Helper.DeleteRepo(Helper.GetAuthenticatedClient().Connection, Helper.Credentials.Login, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create(7528679, new NewRepositoryFork());

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.UserName), forkCreated.FullName);
                Assert.True(forkCreated.Fork);
            }

            [OrganizationTest]
            public async Task ForkCreatedForOrganization()
            {
                // The fork is created asynchronously by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts.
                Helper.DeleteRepo(Helper.GetAuthenticatedClient().Connection, Helper.Organization, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create("octokit", "octokit.net", new NewRepositoryFork { Organization = Helper.Organization });

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.Organization), forkCreated.FullName);
                Assert.True(forkCreated.Fork);
            }

            [OrganizationTest]
            public async Task ForkCreatedForOrganizationWithRepositoryId()
            {
                // The fork is created asynchronously by github and therefore it cannot 
                // be certain that the repo exists when the test ends. It is therefore deleted
                // before the test starts.
                Helper.DeleteRepo(Helper.GetAuthenticatedClient().Connection, Helper.Organization, "octokit.net");

                var github = Helper.GetAuthenticatedClient();

                var forkCreated = await github.Repository.Forks.Create(7528679, new NewRepositoryFork { Organization = Helper.Organization });

                Assert.NotNull(forkCreated);
                Assert.Equal(string.Format("{0}/octokit.net", Helper.Organization), forkCreated.FullName);
                Assert.True(forkCreated.Fork);
            }
        }
    }
}
