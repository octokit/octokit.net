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
                var response = Task.Factory.StartNew<IApiResponse<Repository>>(() =>
                    new ApiResponse<Repository>(new Response(), repository));
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
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<Repository>>(
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<Repository>
                    {
                        new Repository(1),
                        new Repository(2),
                        new Repository(3)
                    });
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<Repository>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<Repository>
                    {
                        new Repository(4),
                        new Repository(5),
                        new Repository(6)
                    });
                var lastPageResponse = new ApiResponse<List<Repository>>(
                    new Response(),
                    new List<Repository>
                    {
                        new Repository(7)
                    });
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetResponse<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllForCurrent().ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(thirdPageUrl, null, null);
            }

            [Fact(Skip = "See https://github.com/octokit/octokit.net/issues/1011 for issue to investigate this further")]
            public async Task StopsMakingNewRequestsWhenTakeIsFulfilled()
            {
                var firstPageUrl = new Uri("user/repos", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<Repository>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<Repository>
                    {
                        new Repository(1),
                        new Repository(2),
                        new Repository(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<Repository>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<Repository>
                    {
                        new Repository(4),
                        new Repository(5),
                        new Repository(6)
                    }
                );
                var fourthPageUrl = new Uri("https://example.com/page/4");
                var thirdPageLinks = new Dictionary<string, Uri> { { "next", fourthPageUrl } };
                var thirdPageResponse = new ApiResponse<List<Repository>>
                (

                    CreateResponseWithApiInfo(thirdPageLinks),
                    new List<Repository>
                    {
                        new Repository(7)
                    }
                );
                var lastPageResponse = new ApiResponse<List<Repository>>
                (
                    new Response(),
                    new List<Repository>
                    {
                        new Repository(8)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetResponse<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => thirdPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(fourthPageUrl)
                    .Returns(Task.Factory.StartNew<IApiResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllForCurrent().Take(4).ToArray();

                Assert.Equal(4, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(0).Get<List<Repository>>(thirdPageUrl, null, null);
                gitHubClient.Connection.Received(0).Get<List<Repository>>(fourthPageUrl, null, null);
            }
        }

        public class TheGetAllPublicRepositoriesSinceMethod
        {
            [Fact]
            public async Task ReturnsEveryPageOfRepositories()
            {
                var firstPageUrl = new Uri("/repositories?since=364", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                IApiResponse<List<Repository>> firstPageResponse = new ApiResponse<List<Repository>>(
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<Repository>
                    {
                        new Repository(364),
                        new Repository(365),
                        new Repository(366)
                    });

                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                IApiResponse<List<Repository>> secondPageResponse = new ApiResponse<List<Repository>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<Repository>
                    {
                        new Repository(367),
                        new Repository(368),
                        new Repository(369)
                    });

                IApiResponse<List<Repository>> lastPageResponse = new ApiResponse<List<Repository>>(
                    new Response(),
                    new List<Repository>
                    {
                        new Repository(370)
                    });

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Repository>>(firstPageUrl, null, null)
                    .Returns(Task.FromResult(firstPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(secondPageUrl, null, null)
                    .Returns(Task.FromResult(secondPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(thirdPageUrl, null, null)
                    .Returns(Task.FromResult(lastPageResponse));

                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllPublic(new PublicRepositoryRequest(364)).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(thirdPageUrl, null, null);
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

        public class TheGetCommitMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Commit.Get(null, "repo", "reference"));
                Assert.Throws<ArgumentNullException>(() => client.Commit.Get("owner", null, "reference"));
                Assert.Throws<ArgumentNullException>(() => client.Commit.Get("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.Commit.Get("", "repo", "reference"));
                Assert.Throws<ArgumentException>(() => client.Commit.Get("owner", "", "reference"));
                Assert.Throws<ArgumentException>(() => client.Commit.Get("owner", "repo", ""));
            }

            [Fact]
            public void CallsCorrectApi()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);

                client.Commit.Get("owner", "repo", "reference");

                github.Repository.Commit.Received(1).Get("owner", "repo", "reference");
            }
        }

        public class TheGetAllCommitsMethod
        {
            [Fact]
            public void EnsuresArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll("owner", "repo", null));
                Assert.Throws<ArgumentException>(() => client.Commit.GetAll("", "repo"));
                Assert.Throws<ArgumentException>(() => client.Commit.GetAll("owner", ""));
            }

            [Fact]
            public void GetsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/commits", UriKind.Relative);

                client.Commit.GetAll("owner", "repo");

                github.Connection.Received(1).Get<List<GitHubCommit>>(expected, Arg.Any<IDictionary<string, string>>(), null);
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
                    .Get<List<RepositoryContributor>>(expected,
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

        public class TheEditBranchMethod
        {
            [Fact]
            public async Task EnsuresArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var nonreactiveClient = new RepositoriesClient(Substitute.For<IApiConnection>());
                github.Repository.Returns(nonreactiveClient);
                var client = new ObservableRepositoriesClient(github);
                var update = new BranchUpdate();

                Assert.Throws<ArgumentNullException>(() => client.EditBranch(null, "repo", "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.EditBranch("owner", null, "branch", update));
                Assert.Throws<ArgumentNullException>(() => client.EditBranch("owner", "repo", null, update));
                Assert.Throws<ArgumentNullException>(() => client.EditBranch("owner", "repo", "branch", null));
                Assert.Throws<ArgumentException>(() => client.EditBranch("", "repo", "branch", update));
                Assert.Throws<ArgumentException>(() => client.EditBranch("owner", "", "branch", update));
                Assert.Throws<ArgumentException>(() => client.EditBranch("owner", "repo", "", update));
            }

            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var update = new BranchUpdate();

                client.EditBranch("owner", "repo", "branch", update);

                github.Repository.Received(1).EditBranch("owner", "repo", "branch", update);
            }
        }

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }
    }
}
