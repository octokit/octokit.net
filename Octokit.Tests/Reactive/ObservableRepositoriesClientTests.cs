using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoriesClientTests
    {
        public class TheGetMethod
        {
            // This isn't really a test specific to this method. This is just as good a place as any to test
            // that our API methods returns the right kind of observables.
            [Fact]
            public async Task IsALukeWarmObservable()
            {
                var repository = new Repository();
                var response = Task.Factory.StartNew<IResponse<Repository>>(() =>
                    new ApiResponse<Repository> { BodyAsObject = repository });
                var connection = Substitute.For<IConnection>();
                connection.Get<Repository>(Args.Uri, null, null).Returns(response);
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoriesClient(gitHubClient);
                var observable = client.Get("stark", "ned");
                
                connection.Received(1).Get<Repository>(Args.Uri, null, null);

                var result = await observable;
                connection.Received(1).Get<Repository>(Args.Uri, null, null);
                var result2 = await observable;
                // TODO: If we change this to a warm observable, we'll need to change this to Received(2)
                connection.Received(1).Get<Repository>(Args.Uri, null, null);

                Assert.Same(repository, result);
                Assert.Same(repository, result2);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public async Task ReturnsEveryPageOfRepositories()
            {
                var firstPageUrl = new Uri("user/repos", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> {{"next", secondPageUrl}};
                var firstPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 1},
                        new Repository {Id = 2},
                        new Repository {Id = 3}
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> {{"next", thirdPageUrl}};
                var secondPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 4},
                        new Repository {Id = 5},
                        new Repository {Id = 6}
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 7}
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetResponse<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllForCurrent().ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(thirdPageUrl, null, null);
            }

            [Fact]
            public async Task StopsMakingNewRequestsWhenTakeIsFulfilled()
            {
                var firstPageUrl = new Uri("user/repos", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 1},
                        new Repository {Id = 2},
                        new Repository {Id = 3}
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 4},
                        new Repository {Id = 5},
                        new Repository {Id = 6}
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var fourthPageUrl = new Uri("https://example.com/page/4");
                var thirdPageLinks = new Dictionary<string, Uri> { { "next", fourthPageUrl } };
                var thirdPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 7}
                    },
                    ApiInfo = CreateApiInfo(thirdPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 8}
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetResponse<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => thirdPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(fourthPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllForCurrent().Take(4).ToArray();

                Assert.Equal(4, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(0).Get<List<Repository>>(thirdPageUrl, null, null);
                gitHubClient.Connection.Received(0).Get<List<Repository>>(fourthPageUrl, null, null);
            }
        }

        public class TheGetAllBranchesMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllBranches(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllBranches("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllBranches("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllBranches("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/branches", UriKind.Relative);

                client.GetAllBranches("owner", "repo");

                github.Connection.Received(1).GetResponse<List<Branch>>(expected);
            }
        }

        public class TheGetAllContributorsMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/contributors", UriKind.Relative);

                client.GetAllContributors("owner", "repo");

                github.Connection.Received(1)
                    .Get<List<User>>(expected,
                                          Arg.Any<IDictionary<string, string>>(),
                                          Arg.Any<string>());
            }

            // TODO: Needs test for 'includeAnonymous'
        }

        public class TheGetAllLanguagesMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/languages", UriKind.Relative);

                client.GetAllLanguages("owner", "repo");

                github.Connection.Received(1).GetResponse<List<Tuple<string, long>>>(expected);
            }
        }

        public class TheGetAllTeamsMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/teams", UriKind.Relative);

                client.GetAllTeams("owner", "repo");

                github.Connection.Received(1).GetResponse<List<Team>>(expected);
            }
        }

        public class TheGetAllTagsMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTags(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags("owner", null));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/tags", UriKind.Relative);

                client.GetAllTags("owner", "repo");

                github.Connection.Received(1).GetResponse<List<RepositoryTag>>(expected);
            }
        }

        public class TheGetBranchMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var nonreactiveClient = new RepositoriesClient(Substitute.For<IApiConnection>());
                github.Repository.Returns(nonreactiveClient);
                var client = new ObservableRepositoriesClient(github);

                Assert.Throws<ArgumentNullException>(() => client.GetBranch(null, "repo", "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranch("owner", null, "branch"));
                Assert.Throws<ArgumentNullException>(() => client.GetBranch("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.GetBranch("", "repo", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranch("owner", "", "branch"));
                Assert.Throws<ArgumentException>(() => client.GetBranch("owner", "repo", ""));
            }

            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);

                client.GetBranch("owner", "repo", "branch");

                github.Repository.Received(1).GetBranch("owner", "repo", "branch");
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var nonreactiveClient = new RepositoriesClient(Substitute.For<IApiConnection>());
                github.Repository.Returns(nonreactiveClient);
                var client = new ObservableRepositoriesClient(github);
                var update = new RepositoryUpdate();

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "repo", update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.Edit("", "repo", update));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", update));
            }

            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var update = new RepositoryUpdate();

                client.Edit("owner", "repo", update);

                github.Repository.Received(1).Edit("owner", "repo", update);
            }
        }

        static ApiInfo CreateApiInfo(IDictionary<string, Uri> links)
        {
            return new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
        }
    }
}
