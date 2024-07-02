using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableFollowersClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            readonly ObservableFollowersClient _followersClient;

            public TheGetAllForCurrentMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _followersClient = new ObservableFollowersClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsFollowers()
            {
                var followers = await _followersClient.GetAllForCurrent().ToList();

                Assert.NotEmpty(followers);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowersWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var followers = await _followersClient.GetAllForCurrent(options).ToList();

                Assert.Single(followers);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowersWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                var followers = await _followersClient.GetAllForCurrent(options).ToList();

                Assert.Single(followers);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstFollowersPage = await _followersClient.GetAllForCurrent(startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondFollowersPage = await _followersClient.GetAllForCurrent(skipStartOptions).ToList();

                Assert.NotEqual(firstFollowersPage[0].Id, secondFollowersPage[0].Id);
            }
        }

        public class TheGetAllMethod
        {
            readonly ObservableFollowersClient _followersClient;
            const string login = "samthedev";

            public TheGetAllMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _followersClient = new ObservableFollowersClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsFollowers()
            {
                var followers = await _followersClient.GetAll(login).ToList();

                Assert.NotEmpty(followers);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowersWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 3,
                    PageCount = 1
                };

                var followers = await _followersClient.GetAll(login, options).ToList();

                Assert.Equal(3, followers.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowersWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 1
                };

                var followers = await _followersClient.GetAll(login, options).ToList();

                Assert.Equal(2, followers.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1
                };

                var firstFollowersPage = await _followersClient.GetAll(login, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 2,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondFollowersPage = await _followersClient.GetAll(login, skipStartOptions).ToList();

                Assert.NotEqual(firstFollowersPage[0].Id, secondFollowersPage[0].Id);
                Assert.NotEqual(firstFollowersPage[1].Id, secondFollowersPage[1].Id);
            }
        }

        public class TheGetAllFollowingForCurrentMethod : IDisposable
        {
            readonly ObservableFollowersClient _followersClient;

            public TheGetAllFollowingForCurrentMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _followersClient = new ObservableFollowersClient(github);

                // Follow someone to set initial state
                _followersClient.Follow("alfhenrik").Wait();
                _followersClient.Follow("ryangribble").Wait();
            }

            [IntegrationTest]
            public async Task ReturnsFollowing()
            {
                var following = await _followersClient.GetAllFollowingForCurrent().ToList();

                Assert.NotEmpty(following);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowingWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var following = await _followersClient.GetAllFollowingForCurrent(options).ToList();

                Assert.Single(following);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowingWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 1
                };

                var following = await _followersClient.GetAllFollowingForCurrent(options).ToList();

                Assert.Single(following);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1
                };

                var firstFollowingPage = await _followersClient.GetAllFollowingForCurrent(startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 1,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondFollowingPage = await _followersClient.GetAllFollowingForCurrent(skipStartOptions).ToList();

                Assert.NotEqual(firstFollowingPage[0].Id, secondFollowingPage[0].Id);
            }

            public void Dispose()
            {
                _followersClient.Unfollow("alfhenrik");
                _followersClient.Unfollow("ryangribble");
            }
        }

        public class TheGetAllFollowingMethod
        {
            readonly ObservableFollowersClient _followersClient;
            const string login = "samthedev";

            public TheGetAllFollowingMethod()
            {
                var github = Helper.GetAuthenticatedClient();

                _followersClient = new ObservableFollowersClient(github);
            }

            [IntegrationTest]
            public async Task ReturnsFollowing()
            {
                var following = await _followersClient.GetAllFollowing(login).ToList();

                Assert.NotEmpty(following);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowingWithoutStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var following = await _followersClient.GetAllFollowing(login, options).ToList();

                Assert.Equal(5, following.Count);
            }

            [IntegrationTest]
            public async Task ReturnsCorrectCountOfFollowingWithStart()
            {
                var options = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 1
                };

                var following = await _followersClient.GetAllFollowing(login, options).ToList();

                Assert.Equal(5, following.Count);
            }

            [IntegrationTest]
            public async Task ReturnsDistinctResultsBasedOnStartPage()
            {
                var startOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1
                };

                var firstFollowingPage = await _followersClient.GetAllFollowing(login, startOptions).ToList();

                var skipStartOptions = new ApiOptions
                {
                    PageSize = 5,
                    PageCount = 1,
                    StartPage = 2
                };

                var secondFollowingPage = await _followersClient.GetAllFollowing(login, skipStartOptions).ToList();

                Assert.NotEqual(firstFollowingPage[0].Id, secondFollowingPage[0].Id);
                Assert.NotEqual(firstFollowingPage[1].Id, secondFollowingPage[1].Id);
                Assert.NotEqual(firstFollowingPage[2].Id, secondFollowingPage[2].Id);
                Assert.NotEqual(firstFollowingPage[3].Id, secondFollowingPage[3].Id);
                Assert.NotEqual(firstFollowingPage[4].Id, secondFollowingPage[4].Id);
            }
        }
    }
}
