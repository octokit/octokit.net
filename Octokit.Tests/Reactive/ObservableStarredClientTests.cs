using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableStarredClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableStarredClient(null));
            }
        }

        public class TheGetAllStargazersMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllStargazers("fight", "club");

                connection.Received().Get<List<User>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllStargazers(1);

                connection.Received().Get<List<User>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllStargazers("fight", "club", options);

                connection.Received().Get<List<User>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllStargazers(1, options);

                connection.Received().Get<List<User>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllStargazersWithTimestamps("fight", "club");

                connection.Received().Get<List<UserStar>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllStargazersWithTimestamps(1);

                connection.Received().Get<List<UserStar>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllStargazersWithTimestamps("fight", "club", options);

                connection.Received().Get<List<UserStar>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsWithApiOptionsWithRepositoryId()
            {
                var endpoint = new Uri("repositories/1/stargazers", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllStargazersWithTimestamps(1, options);

                connection.Received().Get<List<UserStar>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers(null, "club"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers("fight", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers(null, "club", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers("fight", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers("fight", "club", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(null, "club"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(null, "club", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps("fight", "club", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazers(1, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllStargazersWithTimestamps(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllStargazers("", "club"));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazers("fight", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazers("", "club", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazers("fight", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazersWithTimestamps("", "club"));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazersWithTimestamps("fight", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazersWithTimestamps("", "club", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllStargazersWithTimestamps("fight", "", ApiOptions.None));
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForCurrent();

                connection.Received().Get<List<Repository>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrent(options);

                connection.Received().Get<List<Repository>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlParametrized()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForCurrent(request);

                connection.Received().Get<List<Repository>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"));
            }

            [Fact]
            public void RequestsCorrectUrlParametrizedWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForCurrent(request, options);

                connection.Received().Get<List<Repository>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 4 && d["direction"] == "asc" && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForCurrentWithTimestamps();

                connection.Received().Get<List<RepositoryStar>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForCurrentWithTimestamps(options);

                connection.Received().Get<List<RepositoryStar>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsParametrized()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForCurrentWithTimestamps(request);

                connection.Received().Get<List<RepositoryStar>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsParametrizedWithApiOptions()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var request = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForCurrentWithTimestamps(request, options);

                connection.Received().Get<List<RepositoryStar>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 4 && d["direction"] == "asc" && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent((StarredRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps((ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps((StarredRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(null, new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrent(new StarredRequest(), null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps(null, new ApiOptions()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForCurrentWithTimestamps(new StarredRequest(), null));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForUser("banana");

                connection.Received().Get<List<Repository>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForUser("banana", options);

                connection.Received().Get<List<Repository>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlParametrized()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForUser("banana", starredRequest);

                connection.Received().Get<List<Repository>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"));
            }

            [Fact]
            public void RequestsCorrectUrlParametrizedWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForUser("banana", starredRequest, options);

                connection.Received().Get<List<Repository>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 4 && d["direction"] == "asc" && d["direction"] == "asc" && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestamps()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                client.GetAllForUserWithTimestamps("banana");

                connection.Received().Get<List<RepositoryStar>>(endpoint, Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllForUserWithTimestamps("banana", options);

                connection.Received().Get<List<RepositoryStar>>(endpoint,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsParametrized()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForUserWithTimestamps("banana", starredRequest);

                connection.Received().Get<List<RepositoryStar>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["direction"] == "asc"));
            }

            [Fact]
            public void RequestsCorrectUrlWithTimestampsParametrizedWithApiOptions()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IConnection>();
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Returns(connection);
                var client = new ObservableStarredClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                var starredRequest = new StarredRequest { SortDirection = SortDirection.Ascending };

                client.GetAllForUserWithTimestamps("banana", starredRequest, options);

                connection.Received().Get<List<RepositoryStar>>(endpoint, Arg.Is<IDictionary<string, string>>(d => d.Count == 4 && d["direction"] == "asc" && d["direction"] == "asc" && d["per_page"] == "1" && d["page"] == "1"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser("banana", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null, new StarredRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser("banana", (StarredRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser(null, new StarredRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser("banana", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUser("banana", new StarredRequest(), null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, new StarredRequest()));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", (StarredRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps(null, new StarredRequest(), ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForUserWithTimestamps("banana", new StarredRequest(), null));

                Assert.Throws<ArgumentException>(() => client.GetAllForUser(""));
                Assert.Throws<ArgumentException>(() => client.GetAllForUser("", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForUser("", new StarredRequest(), ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForUserWithTimestamps(""));
                Assert.Throws<ArgumentException>(() => client.GetAllForUserWithTimestamps("", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForUserWithTimestamps("", new StarredRequest(), ApiOptions.None));
            }
        }

        public class TheCheckStarredMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckStarred(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CheckStarred("james", null).ToTask());
            }

            [Fact]
            public void ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.CheckStarred("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().CheckStarred("jugglingnutcase", "katiejamie");
            }
        }

        public class TheStarRepoMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.StarRepo(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.StarRepo("james", null).ToTask());
            }

            [Fact]
            public void ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.StarRepo("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().StarRepo("jugglingnutcase", "katiejamie");
            }
        }

        public class TheRemoveStarFromRepoMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var client = new ObservableStarredClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveStarFromRepo(null, "james").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveStarFromRepo("james", null).ToTask());
            }

            [Fact]
            public void ChecksStarredForUser()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableStarredClient(gitHubClient);

                client.RemoveStarFromRepo("jugglingnutcase", "katiejamie");
                gitHubClient.Activity.Starring.Received().RemoveStarFromRepo("jugglingnutcase", "katiejamie");
            }
        }
    }
}