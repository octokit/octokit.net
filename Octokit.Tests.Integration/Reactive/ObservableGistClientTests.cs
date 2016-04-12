using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
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
                var since=new DateTimeOffset(new DateTime(2016,1,1));
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
    }
}
