using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableGistClientTests
{
    public class TheGetAllMethod
    {
        readonly ObservableGistsClient _gistsClient;

        public TheGetAllMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsGists()
        {
            var gists = await _gistsClient.GetAll().ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var gists = await _gistsClient.GetAll(options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var gists = await _gistsClient.GetAll(options).ToList();

            Assert.Equal(4, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1
            };

            var firstGistsPage = await _gistsClient.GetAll(startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var secondGistsPage = await _gistsClient.GetAll(skipStartOptions).ToList();

            Assert.NotEqual(firstGistsPage[0].Id, secondGistsPage[0].Id);
            Assert.NotEqual(firstGistsPage[1].Id, secondGistsPage[1].Id);
            Assert.NotEqual(firstGistsPage[2].Id, secondGistsPage[2].Id);
            Assert.NotEqual(firstGistsPage[3].Id, secondGistsPage[3].Id);
        }

        [IntegrationTest]
        public async Task ReturnsGistsSince()
        {
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAll(since).ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistsSinceWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAll(since, options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistsSinceWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAll(since, options).ToList();

            Assert.Equal(4, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctGistsSinceBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var firstGistsPage = await _gistsClient.GetAll(since, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var secondGistsPage = await _gistsClient.GetAll(since, skipStartOptions).ToList();

            Assert.NotEqual(firstGistsPage[0].Id, secondGistsPage[0].Id);
            Assert.NotEqual(firstGistsPage[1].Id, secondGistsPage[1].Id);
            Assert.NotEqual(firstGistsPage[2].Id, secondGistsPage[2].Id);
            Assert.NotEqual(firstGistsPage[3].Id, secondGistsPage[3].Id);
        }
    }

    public class TheGetAllPublicMethod
    {
        readonly ObservableGistsClient _gistsClient;

        public TheGetAllPublicMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsPublicGists()
        {
            var gists = await _gistsClient.GetAllPublic().ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfPublicGistsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var gists = await _gistsClient.GetAllPublic(options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfPublicGistsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var gists = await _gistsClient.GetAllPublic(options).ToList();

            Assert.Equal(4, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1
            };

            var firstPublicGistsPage = await _gistsClient.GetAllPublic(startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var secondPublicGistsPage = await _gistsClient.GetAllPublic(skipStartOptions).ToList();

            Assert.NotEqual(firstPublicGistsPage[0].Id, secondPublicGistsPage[0].Id);
            Assert.NotEqual(firstPublicGistsPage[1].Id, secondPublicGistsPage[1].Id);
            Assert.NotEqual(firstPublicGistsPage[2].Id, secondPublicGistsPage[2].Id);
            Assert.NotEqual(firstPublicGistsPage[3].Id, secondPublicGistsPage[3].Id);
        }

        [IntegrationTest]
        public async Task ReturnsPublicGistsSince()
        {
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllPublic(since).ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfPublicGistsSinceWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllPublic(since, options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfPublicGistsSinceWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllPublic(since, options).ToList();

            Assert.Equal(4, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctPublicGistsSinceBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var firstPublicGistsPage = await _gistsClient.GetAllPublic(since, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 4,
                PageCount = 1,
                StartPage = 2
            };

            var secondPublicGistsPage = await _gistsClient.GetAllPublic(since, skipStartOptions).ToList();

            Assert.NotEqual(firstPublicGistsPage[0].Id, secondPublicGistsPage[0].Id);
            Assert.NotEqual(firstPublicGistsPage[1].Id, secondPublicGistsPage[1].Id);
            Assert.NotEqual(firstPublicGistsPage[2].Id, secondPublicGistsPage[2].Id);
            Assert.NotEqual(firstPublicGistsPage[3].Id, secondPublicGistsPage[3].Id);
        }
    }

    public class TheGetAllStarredMethod
    {
        readonly ObservableGistsClient _gistsClient;

        public TheGetAllStarredMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsStartedGists()
        {
            var gists = await _gistsClient.GetAllStarred().ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfStartedGistsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var gists = await _gistsClient.GetAllStarred(options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfStartedGistsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var gists = await _gistsClient.GetAllStarred(options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstStartedGistsPage = await _gistsClient.GetAllStarred(startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondStartedGistsPage = await _gistsClient.GetAllStarred(skipStartOptions).ToList();

            Assert.NotEqual(firstStartedGistsPage[0].Id, secondStartedGistsPage[0].Id);
            Assert.NotEqual(firstStartedGistsPage[1].Id, secondStartedGistsPage[1].Id);
            Assert.NotEqual(firstStartedGistsPage[2].Id, secondStartedGistsPage[2].Id);
            Assert.NotEqual(firstStartedGistsPage[3].Id, secondStartedGistsPage[3].Id);
            Assert.NotEqual(firstStartedGistsPage[4].Id, secondStartedGistsPage[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsStartedGistsSince()
        {
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllStarred(since).ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfStartedGistsSinceWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllStarred(since, options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfStartedGistsSinceWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllStarred(since, options).ToList();

            Assert.Equal(5, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctStartedGistsSinceBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var firstStartedGistsPage = await _gistsClient.GetAllStarred(since, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondStartedGistsPage = await _gistsClient.GetAllStarred(since, skipStartOptions).ToList();

            Assert.NotEqual(firstStartedGistsPage[0].Id, secondStartedGistsPage[0].Id);
            Assert.NotEqual(firstStartedGistsPage[1].Id, secondStartedGistsPage[1].Id);
            Assert.NotEqual(firstStartedGistsPage[2].Id, secondStartedGistsPage[2].Id);
            Assert.NotEqual(firstStartedGistsPage[3].Id, secondStartedGistsPage[3].Id);
            Assert.NotEqual(firstStartedGistsPage[4].Id, secondStartedGistsPage[4].Id);
        }
    }

    public class TheGetAllForUserMethod
    {
        readonly ObservableGistsClient _gistsClient;
        const string user = "shiftkey";

        public TheGetAllForUserMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsUserGists()
        {
            var gists = await _gistsClient.GetAllForUser(user).ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfUserGistsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };

            var gists = await _gistsClient.GetAllForUser(user, options).ToList();

            Assert.Equal(3, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfUserGistsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };

            var gists = await _gistsClient.GetAllForUser(user, options).ToList();

            Assert.Equal(3, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };

            var firstUsersGistsPage = await _gistsClient.GetAllForUser(user, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };

            var secondUsersGistsPage = await _gistsClient.GetAllForUser(user, skipStartOptions).ToList();

            Assert.NotEqual(firstUsersGistsPage[0].Id, secondUsersGistsPage[0].Id);
            Assert.NotEqual(firstUsersGistsPage[1].Id, secondUsersGistsPage[1].Id);
            Assert.NotEqual(firstUsersGistsPage[2].Id, secondUsersGistsPage[2].Id);
        }

        [IntegrationTest]
        public async Task ReturnsUserGistsSince()
        {
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllForUser(user, since).ToList();

            Assert.NotEmpty(gists);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfUserGistsSinceWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllForUser(user, since, options).ToList();

            Assert.Equal(3, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfUserGistsSinceWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var gists = await _gistsClient.GetAllForUser(user, since, options).ToList();

            Assert.Equal(3, gists.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctUserGistsSinceBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };
            var since = new DateTimeOffset(new DateTime(2016, 1, 1));
            var firstUserGistsPage = await _gistsClient.GetAllForUser(user, since, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };

            var secondUserGistsPage = await _gistsClient.GetAllForUser(user, since, skipStartOptions).ToList();

            Assert.NotEqual(firstUserGistsPage[0].Id, secondUserGistsPage[0].Id);
            Assert.NotEqual(firstUserGistsPage[1].Id, secondUserGistsPage[1].Id);
            Assert.NotEqual(firstUserGistsPage[2].Id, secondUserGistsPage[2].Id);
        }
    }

    public class TheGetAllCommitsMethod
    {
        readonly ObservableGistsClient _gistsClient;
        const string gistId = "670c22f3966e662d2f83";

        public TheGetAllCommitsMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsGistCommits()
        {
            var gistCommits = await _gistsClient.GetAllCommits(gistId).ToList();

            Assert.NotEmpty(gistCommits);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistCommisWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };

            var gistCommits = await _gistsClient.GetAllCommits(gistId, options).ToList();

            Assert.Equal(3, gistCommits.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountGistCommitsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };

            var gistCommits = await _gistsClient.GetAllCommits(gistId, options).ToList();

            Assert.Equal(3, gistCommits.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };

            var firstGistCommitsPage = await _gistsClient.GetAllCommits(gistId, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1,
                StartPage = 2
            };

            var secondGistCommitsPage = await _gistsClient.GetAllCommits(gistId, skipStartOptions).ToList();

            Assert.NotEqual(firstGistCommitsPage[0].Url, secondGistCommitsPage[0].Url);
            Assert.NotEqual(firstGistCommitsPage[1].Url, secondGistCommitsPage[1].Url);
            Assert.NotEqual(firstGistCommitsPage[2].Url, secondGistCommitsPage[2].Url);
        }
    }

    public class TheGetAllForksMethod
    {
        readonly ObservableGistsClient _gistsClient;
        const string gistId = "670c22f3966e662d2f83";

        public TheGetAllForksMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _gistsClient = new ObservableGistsClient(github);
        }

        [IntegrationTest]
        public async Task ReturnsGistCommits()
        {
            var gistForks = await _gistsClient.GetAllForks(gistId).ToList();

            Assert.NotEmpty(gistForks);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfGistForksWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var gistForks = await _gistsClient.GetAllForks(gistId, options).ToList();

            Assert.Equal(5, gistForks.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountGistForksWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var gistForks = await _gistsClient.GetAllForks(gistId, options).ToList();

            Assert.Equal(5, gistForks.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstGistForksPage = await _gistsClient.GetAllForks(gistId, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondGistForksPage = await _gistsClient.GetAllForks(gistId, skipStartOptions).ToList();

            Assert.NotEqual(firstGistForksPage[0].Url, secondGistForksPage[0].Url);
            Assert.NotEqual(firstGistForksPage[1].Url, secondGistForksPage[1].Url);
            Assert.NotEqual(firstGistForksPage[2].Url, secondGistForksPage[2].Url);
            Assert.NotEqual(firstGistForksPage[3].Url, secondGistForksPage[3].Url);
            Assert.NotEqual(firstGistForksPage[4].Url, secondGistForksPage[4].Url);
        }
    }
}

