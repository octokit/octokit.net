using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class StarredClientTests : IDisposable
    {
        private readonly IStarredClient _fixture;
        private readonly RepositoryContext _repositoryContext;

        public StarredClientTests()
        {
            var client = Helper.GetAuthenticatedClient();
            _fixture = client.Activity.Starring;

            var github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo1");

            _repositoryContext = github.CreateRepositoryContext(new NewRepository(repoName)).Result;

            _fixture.RemoveStarFromRepo(_repositoryContext.RepositoryOwner, _repositoryContext.RepositoryName).Wait();
            _fixture.RemoveStarFromRepo("octokit", "octokit.net").Wait();
            _fixture.StarRepo(_repositoryContext.RepositoryOwner, _repositoryContext.RepositoryName).Wait();
            _fixture.StarRepo("octokit", "octokit.net").Wait();
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrent()
        {
            var repositories = await _fixture.GetAllForCurrent();
            Assert.NotEmpty(repositories);

            var repo = repositories.FirstOrDefault(repository => repository.Owner.Login == _repositoryContext.RepositoryOwner && repository.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForCurrent()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var repositories = await _fixture.GetAllForCurrent(options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForCurrent()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var repositories = await _fixture.GetAllForCurrent(options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForCurrent()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForCurrent(startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForCurrent(skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrentParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var repositories = await _fixture.GetAllForCurrent(starredRequest);
            Assert.NotEmpty(repositories);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForCurrentParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var repositories = await _fixture.GetAllForCurrent(starredRequest, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForCurrentParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var repositories = await _fixture.GetAllForCurrent(starredRequest, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForCurrentParameterized()
        {
            var starredRequestFirstPage = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };
            var starredRequestSecondPage = new StarredRequest { SortDirection = SortDirection.Descending, SortProperty = StarredSort.Updated };

            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForCurrent(starredRequestFirstPage, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForCurrent(starredRequestSecondPage, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrentWithTimestamps()
        {
            var stars = await _fixture.GetAllForCurrentWithTimestamps();
            Assert.NotEmpty(stars);

            var repo = stars.FirstOrDefault(star => star.Repo.Owner.Login == _repositoryContext.RepositoryOwner && star.Repo.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForCurrentWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var repositories = await _fixture.GetAllForCurrentWithTimestamps(options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForCurrentWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var repositories = await _fixture.GetAllForCurrentWithTimestamps(options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForCurrentWithTimestamps()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForCurrentWithTimestamps(startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForCurrentWithTimestamps(skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Repo.Id, secondPage.First().Repo.Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrentWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var stars = await _fixture.GetAllForCurrentWithTimestamps(starredRequest);
            Assert.NotEmpty(stars);

            var repo = stars.FirstOrDefault(star => star.Repo.Owner.Login == _repositoryContext.RepositoryOwner && star.Repo.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForCurrentWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var stars = await _fixture.GetAllForCurrentWithTimestamps(starredRequest, options);
            Assert.Equal(1, stars.Count);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForCurrentWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var stars = await _fixture.GetAllForCurrentWithTimestamps(starredRequest, options);
            Assert.Equal(1, stars.Count);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForCurrentWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Descending, SortProperty = StarredSort.Created };

            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForCurrentWithTimestamps(starredRequest, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForCurrentWithTimestamps(starredRequest, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Repo.Id, secondPage.First().Repo.Id);

            for (int i = 1; i < firstPage.Count; i++)
            {
                Assert.True(firstPage[i].StarredAt >= secondPage[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task CanGetAllForUser()
        {
            var repositories = await _fixture.GetAllForUser(Helper.UserName);
            Assert.NotEmpty(repositories);

            var repo = repositories.FirstOrDefault(repository => repository.Owner.Login == _repositoryContext.RepositoryOwner && repository.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForUser()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var repositories = await _fixture.GetAllForUser(Helper.UserName, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForUser()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var repositories = await _fixture.GetAllForUser(Helper.UserName, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForUser()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForUser(Helper.UserName, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForUser(Helper.UserName, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForUserParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var repositories = await _fixture.GetAllForUser(Helper.UserName, starredRequest);
            Assert.NotEmpty(repositories);

            var repo = repositories.FirstOrDefault(repository => repository.Owner.Login == _repositoryContext.RepositoryOwner && repository.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForUserParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var repositories = await _fixture.GetAllForUser(Helper.UserName, starredRequest, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForUserParameterized()
        {
            var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var repositories = await _fixture.GetAllForUser(Helper.UserName, starredRequest, options);
            Assert.Equal(1, repositories.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForUserParameterized()
        {
            var starredRequestFirstPage = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };
            var starredRequestSecondPage = new StarredRequest { SortDirection = SortDirection.Ascending, SortProperty = StarredSort.Created };

            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForUser(Helper.UserName, starredRequestFirstPage, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForUser(Helper.UserName, starredRequestSecondPage, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForUserWithTimestamps()
        {
            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName);
            Assert.NotEmpty(stars);

            var star = stars.FirstOrDefault(repositoryStar => repositoryStar.Repo.Owner.Login == _repositoryContext.RepositoryOwner && repositoryStar.Repo.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(star);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForUserWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, options);
            Assert.Equal(1, stars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForUserWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, options);
            Assert.Equal(1, stars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForUserWithTimestamps()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Repo.Id, secondPage.First().Repo.Id);
        }

        [IntegrationTest]
        public async Task CanGetAllForUserWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, starredRequest);
            Assert.NotEmpty(stars);

            var repo = stars.FirstOrDefault(repository => repository.Repo.Owner.Login == _repositoryContext.RepositoryOwner && repository.Repo.Name == _repositoryContext.RepositoryName);
            Assert.NotNull(repo);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartForUserWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, starredRequest, options);
            Assert.Equal(1, stars.Count);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartForUserWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Ascending };

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var stars = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, starredRequest, options);
            Assert.Equal(1, stars.Count);

            for (int i = 1; i < stars.Count; i++)
            {
                Assert.True(stars[i - 1].StarredAt >= stars[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageForUserWithTimestampsParameterized()
        {
            var starredRequest = new StarredRequest { SortProperty = StarredSort.Created, SortDirection = SortDirection.Descending };

            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, starredRequest, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllForUserWithTimestamps(Helper.UserName, starredRequest, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Repo.Id, secondPage.First().Repo.Id);

            for (int i = 0; i < firstPage.Count; i++)
            {
                Assert.True(firstPage[i].StarredAt >= secondPage[i].StarredAt);
            }
        }

        [IntegrationTest]
        public async Task CanGetAllStargazers()
        {
            var users = await _fixture.GetAllStargazers(_repositoryContext.RepositoryOwner, _repositoryContext.RepositoryName);
            Assert.NotEmpty(users);

            var user = users.FirstOrDefault(u => u.Login == Helper.UserName);
            Assert.NotNull(user);
        }

        [IntegrationTest]
        public async Task CanGetAllStargazersWithRepositoryId()
        {
            var users = await _fixture.GetAllStargazers(_repositoryContext.Repository.Id);
            Assert.NotEmpty(users);

            var user = users.FirstOrDefault(u => u.Login == Helper.UserName);
            Assert.NotNull(user);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartAllStargazers()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var users = await _fixture.GetAllStargazers("octokit", "octokit.net", options);
            Assert.Equal(1, users.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartAllStargazersWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var users = await _fixture.GetAllStargazers(7528679, options);
            Assert.Equal(1, users.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartAllStargazers()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var users = await _fixture.GetAllStargazers("octokit", "octokit.net", options);
            Assert.Equal(1, users.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartAllStargazersWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var users = await _fixture.GetAllStargazers(7528679, options);
            Assert.Equal(1, users.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageAllStargazers()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllStargazers("octokit", "octokit.net", startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllStargazers("octokit", "octokit.net", skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageAllStargazersWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllStargazers(7528679, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllStargazers(7528679, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }

        [IntegrationTest]
        public async Task CanGetAllStargazersWithTimestamps()
        {
            var users = await _fixture.GetAllStargazersWithTimestamps(_repositoryContext.RepositoryOwner, _repositoryContext.RepositoryName);
            Assert.NotEmpty(users);

            var userStar = users.FirstOrDefault(star => star.User.Login == _repositoryContext.RepositoryOwner);
            Assert.NotNull(userStar);

            Assert.True(DateTimeOffset.UtcNow.Subtract(userStar.StarredAt) < TimeSpan.FromMinutes(5));
        }

        [IntegrationTest]
        public async Task CanGetAllStargazersWithTimestampsWithRepositoryId()
        {
            var users = await _fixture.GetAllStargazersWithTimestamps(_repositoryContext.Repository.Id);
            Assert.NotEmpty(users);

            var userStar = users.FirstOrDefault(star => star.User.Login == _repositoryContext.RepositoryOwner);
            Assert.NotNull(userStar);

            Assert.True(DateTimeOffset.UtcNow.Subtract(userStar.StarredAt) < TimeSpan.FromMinutes(5));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartAllStargazersWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var userStars = await _fixture.GetAllStargazersWithTimestamps("octokit", "octokit.net", options);
            Assert.Equal(1, userStars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithoutStartAllStargazersWithTimestampsWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var userStars = await _fixture.GetAllStargazersWithTimestamps(7528679, options);
            Assert.Equal(1, userStars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartAllStargazersWithTimestamps()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var userStars = await _fixture.GetAllStargazersWithTimestamps("octokit", "octokit.net", options);
            Assert.Equal(1, userStars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfRepositoriesWithStartAllStargazersWithTimestampsWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 2
            };

            var userStars = await _fixture.GetAllStargazersWithTimestamps(7528679, options);
            Assert.Equal(1, userStars.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageAllStargazersWithTimestamps()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllStargazersWithTimestamps("octokit", "octokit.net", startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllStargazersWithTimestamps("octokit", "octokit.net", skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().StarredAt, secondPage.First().StarredAt);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctRepositoriesBasedOnStartPageAllStargazersWithTimestampsWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _fixture.GetAllStargazersWithTimestamps(7528679, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _fixture.GetAllStargazersWithTimestamps(7528679, skipStartOptions);

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().StarredAt, secondPage.First().StarredAt);
        }

        public void Dispose()
        {
            _repositoryContext.Dispose();
        }
    }
}
