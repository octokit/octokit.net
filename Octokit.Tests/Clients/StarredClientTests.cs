using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Clients
{
    public class StarredClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new StarredClient(null));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllForCurrent();

                connection.Received().GetAll<Repository>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForCurrent(options);

                connection.Received().GetAll<Repository>(endpoint, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlParametrized()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForCurrent(request);

                connection.Received().GetAll<Repository>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlParametrizedWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForCurrent(request, options);

                connection.Received().GetAll<Repository>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllForCurrentWithTimestamps();

                connection.Received().GetAll<RepositoryStar>(endpoint, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForCurrentWithTimestamps(options);

                connection.Received().GetAll<RepositoryStar>(endpoint, null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsParametrized()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForCurrentWithTimestamps(request);

                connection.Received().GetAll<RepositoryStar>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsParametrizedWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForCurrentWithTimestamps(request, options);

                connection.Received().GetAll<RepositoryStar>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new StarredClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent((StarredRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps((ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps((StarredRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrent(new StarredRequest(), null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps(null, new ApiOptions()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps(new StarredRequest(), null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllForUser("banana");

                connection.Received().GetAll<Repository>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForUser("banana", options);

                connection.Received().GetAll<Repository>(endpoint, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlParametrized()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForUser("banana", starredRequest);

                connection.Received().GetAll<Repository>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlParametrizedWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForUser("banana", starredRequest, options);

                connection.Received().GetAll<Repository>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllForUserWithTimestamps("banana");

                connection.Received().GetAll<RepositoryStar>(endpoint, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllForUserWithTimestamps("banana", options);

                connection.Received().GetAll<RepositoryStar>(endpoint, null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsParametrized()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForUserWithTimestamps("banana", starredRequest);

                connection.Received().GetAll<RepositoryStar>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsParametrizedWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                await client.GetAllForUserWithTimestamps("banana", starredRequest, options);

                connection.Received().GetAll<RepositoryStar>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new StarredClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("banana", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, new StarredRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("banana", (StarredRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, new StarredRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("banana", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("banana", new StarredRequest(), null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, new StarredRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", (StarredRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, new StarredRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", new StarredRequest(), null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", new StarredRequest(), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUserWithTimestamps(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUserWithTimestamps("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUserWithTimestamps("", new StarredRequest(), ApiOptions.None));
            }
        }

        public class TheGetAllStargazersForRepoMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllStargazers("fight", "club");

                connection.Received().GetAll<User>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllStargazers(1);

                connection.Received().GetAll<User>(endpoint, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllStargazers("fight", "club", options);

                connection.Received().GetAll<User>(endpoint, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllStargazers(1, options);

                connection.Received().GetAll<User>(endpoint, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("repos/fake/repo/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllStargazersWithTimestamps("fake", "repo");

                connection.Received().GetAll<UserStar>(endpoint, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                await client.GetAllStargazersWithTimestamps(1);

                connection.Received().GetAll<UserStar>(endpoint, null, Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("repos/fake/repo/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllStargazersWithTimestamps("fake", "repo", options);

                connection.Received().GetAll<UserStar>(endpoint, null, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithTimestampsWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAllStargazersWithTimestamps(1, options);

                connection.Received().GetAll<UserStar>(endpoint, null, options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new StarredClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers(null, "club"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers("fight", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers(null, "club", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers("fight", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers("fight", "club", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(null, "club"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(null, "club", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", "club", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazers(1, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazers("", "club"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazers("fight", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazers("", "club", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazers("fight", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazersWithTimestamps("", "club"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazersWithTimestamps("fight", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazersWithTimestamps("", "club", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllStargazersWithTimestamps("fight", "", ApiOptions.None));
            }
        }

        public class TheCheckStarredMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task ReturnsCorrectResultBasedOnStatus(HttpStatusCode status, bool expected)
            {
                var responseTask = CreateApiResponse(status);

                var connection = Substitute.For<IConnection>();
                connection.Get<object>(Arg.Is<Uri>(u => u.ToString() == "user/starred/yes/no"), null, null)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new StarredClient(apiConnection);
                var result = await client.CheckStarred("yes", "no");

                Assert.Equal(expected, result);
            }
        }

        public class TheStarRepoMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            [InlineData(HttpStatusCode.OK, false)]
            public async Task ReturnsCorrectResultBasedOnStatus(HttpStatusCode status, bool expected)
            {
                var responseTask = CreateApiResponse(status);

                var connection = Substitute.For<IConnection>();
                connection.Put<object>(Arg.Is<Uri>(u => u.ToString() == "user/starred/yes/no"), Args.Object, Args.String)
                          .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new StarredClient(apiConnection);
                var result = await client.StarRepo("yes", "no");

                Assert.Equal(expected, result);
            }
        }

        public class TheRemoveStarFromRepoMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            [InlineData(HttpStatusCode.OK, false)]
            public async Task ReturnsCorrectResultBasedOnStatus(HttpStatusCode status, bool expected)
            {
                var response = Task.FromResult(status);

                var connection = Substitute.For<IConnection>();
                connection.Delete(Arg.Is<Uri>(u => u.ToString() == "user/starred/yes/no"))
                    .Returns(response);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new StarredClient(apiConnection);
                var result = await client.RemoveStarFromRepo("yes", "no");

                Assert.Equal(expected, result);
            }
        }
    }
}
