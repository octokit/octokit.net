using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoriesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableRepositoriesClient(null));
            }
        }

        public class TheTransferMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var transfer = new RepositoryTransfer("newOwner");

                Assert.Throws<ArgumentNullException>(
                    () => client.Transfer(null, "name", transfer));
                Assert.Throws<ArgumentNullException>(
                    () => client.Transfer("owner", null, transfer));
                Assert.Throws<ArgumentNullException>(
                    () => client.Transfer("owner", "name", null));
            }

            [Fact]
            public void EnsuresNonNullArgumentsById()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var transfer = new RepositoryTransfer("newOwner");
                var repositoryId = 1;

                Assert.Throws<ArgumentNullException>(
                    () => client.Transfer(repositoryId, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var transfer = new RepositoryTransfer("newOwner");

                Assert.Throws<ArgumentException>(
                    () => client.Transfer("", "name", transfer));
                Assert.Throws<ArgumentException>(
                    () => client.Transfer("owner", "", transfer));
            }

            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var transfer = new RepositoryTransfer("newOwner");

                client.Transfer("owner", "name", transfer);
                gitHubClient.Repository.Received().Transfer("owner", "name", transfer);
            }

            [Fact]
            public void CallsIntoClientById()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var transfer = new RepositoryTransfer("newOwner");

                client.Transfer(1, transfer);
                gitHubClient.Repository.Received().Transfer(1, transfer);
            }
        }

        public class TheIsFollowingMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(githubClient);

                client.AreVulnerabilityAlertsEnabled("owner", "name");
                githubClient.Repository.Received().AreVulnerabilityAlertsEnabled("owner", "name");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AreVulnerabilityAlertsEnabled(null, "name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.AreVulnerabilityAlertsEnabled("", "name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AreVulnerabilityAlertsEnabled("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.AreVulnerabilityAlertsEnabled("owner", "").ToTask());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                await client.Delete("owner", "name");

                gitHubClient.Received().Repository.Delete("owner", "name");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                await client.Delete(1);

                gitHubClient.Received().Repository.Delete(1);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name"));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", ""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                await client.Get("owner", "name");

                gitHubClient.Received().Repository.Get("owner", "name");
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                await client.Get(1);

                gitHubClient.Repository.Get(1);
            }

            // This isn't really a test specific to this method. This is just as good a place as any to test
            // that our API methods returns the right kind of observables.
            [Fact]
            public async Task IsALukeWarmObservable()
            {
                var repository = new Repository();
                var response = Task.FromResult<IApiResponse<Repository>>(new ApiResponse<Repository>(CreateResponse(HttpStatusCode.OK), repository));
                var connection = Substitute.For<IConnection>();
                connection.Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders).Returns(response);
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoriesClient(gitHubClient);
                var observable = client.Get("stark", "ned");

                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);

                var result = await observable;
                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);
                var result2 = await observable;
                // TODO: If we change this to a warm observable, we'll need to change this to Received(2)
                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);

                Assert.Same(repository, result);
                Assert.Same(repository, result2);
            }

            // This isn't really a test specific to this method. This is just as good a place as any to test
            // that our API methods returns the right kind of observables.
            [Fact]
            public async Task IsALukeWarmObservableWithRepositoryId()
            {
                var repository = new Repository();
                var response = Task.FromResult<IApiResponse<Repository>>(new ApiResponse<Repository>(CreateResponse(HttpStatusCode.OK), repository));
                var connection = Substitute.For<IConnection>();
                connection.Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders).Returns(response);
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoriesClient(gitHubClient);
                var observable = client.Get(1);

                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);

                var result = await observable;
                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);
                var result2 = await observable;
                // TODO: If we change this to a warm observable, we'll need to change this to Received(2)
                connection.Received(1).Get<Repository>(Args.Uri, null, Args.AnyAcceptHeaders);

                Assert.Same(repository, result);
                Assert.Same(repository, result2);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null));

                Assert.Throws<ArgumentException>(() => client.Get("", "name"));
                Assert.Throws<ArgumentException>(() => client.Get("owner", ""));
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
                    CreateResponse(HttpStatusCode.OK),
                    new List<Repository>
                    {
                        new Repository(7)
                    });

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Repository>>(firstPageUrl, Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(secondPageUrl, Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(thirdPageUrl, Arg.Any<IDictionary<string, string>>())
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllForCurrent().ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, Arg.Any<IDictionary<string, string>>());
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, Arg.Any<IDictionary<string, string>>());
                gitHubClient.Connection.Received(1).Get<List<Repository>>(thirdPageUrl, Arg.Any<IDictionary<string, string>>());
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
                    CreateResponse(HttpStatusCode.OK),
                    new List<Repository>
                    {
                        new Repository(8)
                    }
                );
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetResponse<List<Repository>>(firstPageUrl)
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(firstPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(secondPageUrl)
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(secondPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(thirdPageUrl)
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(thirdPageResponse));
                gitHubClient.Connection.GetResponse<List<Repository>>(fourthPageUrl)
                    .Returns(Task.FromResult<IApiResponse<List<Repository>>>(lastPageResponse));
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
                var firstPageUrl = new Uri("repositories?since=364", UriKind.Relative);
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
                    CreateResponse(HttpStatusCode.OK),
                    new List<Repository>
                    {
                        new Repository(370)
                    });

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<Repository>>(firstPageUrl, null)
                    .Returns(Task.FromResult(firstPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(secondPageUrl, null)
                    .Returns(Task.FromResult(secondPageResponse));
                gitHubClient.Connection.Get<List<Repository>>(thirdPageUrl, null)
                    .Returns(Task.FromResult(lastPageResponse));

                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = await repositoriesClient.GetAllPublic(new PublicRepositoryRequest(364L)).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<Repository>>(firstPageUrl, Arg.Any<IDictionary<string, string>>());
                gitHubClient.Connection.Received(1).Get<List<Repository>>(secondPageUrl, Arg.Any<IDictionary<string, string>>());
                gitHubClient.Connection.Received(1).Get<List<Repository>>(thirdPageUrl, Arg.Any<IDictionary<string, string>>());
            }
        }

        public class TheGetCommitMethod
        {
            [Fact]
            public void EnsuresNonNullArguments()
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
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.Commit.GetAll("owner", "repo", null, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.Commit.GetAll("", "repo"));
                Assert.Throws<ArgumentException>(() => client.Commit.GetAll("owner", ""));
            }

            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var expected = new Uri("repos/owner/repo/commits", UriKind.Relative);

                client.Commit.GetAll("owner", "repo");

                github.Connection.Received(1).Get<List<GitHubCommit>>(expected, Arg.Any<IDictionary<string, string>>());
            }
        }

        public class TheGetAllContributorsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/contributors", UriKind.Relative);

                client.GetAllContributors("owner", "repo");

                gitHubClient.Connection.Received(1)
                    .Get<List<RepositoryContributor>>(expected,
                        Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/contributors", UriKind.Relative);

                client.GetAllContributors(1);

                gitHubClient.Connection.Received(1)
                    .Get<List<RepositoryContributor>>(expected,
                        Args.EmptyDictionary);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/contributors", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllContributors("owner", "repo", options);

                gitHubClient.Connection.Received(1)
                    .Get<List<RepositoryContributor>>(expected,
                        Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/contributors", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllContributors(1, options);

                gitHubClient.Connection.Received(1)
                    .Get<List<RepositoryContributor>>(expected,
                        Arg.Is<IDictionary<string, string>>(d => d.Count == 2));
            }

            [Fact]
            public void RequestsTheCorrectUrlIncludeAnonymous()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                client.GetAllContributors("owner", "name", true);

                gitHubClient.Connection.Received()
                    .Get<List<RepositoryContributor>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdIncludeAnonymous()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                client.GetAllContributors(1, true);

                gitHubClient.Connection.Received()
                    .Get<List<RepositoryContributor>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"), Arg.Is<IDictionary<string, string>>(d => d["anon"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptionsIncludeAnonymous()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllContributors("owner", "name", true, options);

                gitHubClient.Connection.Received()
                    .Get<List<RepositoryContributor>>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/name/contributors"),
                        Arg.Is<IDictionary<string, string>>(d => d.Count == 3 && d["anon"] == "1" && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptionsIncludeAnonymous()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllContributors(1, true, options);

                gitHubClient.Connection.Received()
                    .Get<List<RepositoryContributor>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contributors"),
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 3 && d["anon"] == "1" && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", "repo", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(null, "repo", false, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", null, false, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors("owner", "repo", false, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(1, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContributors(1, false, null));

                Assert.Throws<ArgumentException>(() => client.GetAllContributors("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("", "repo", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("", "repo", false, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContributors("owner", "", false, ApiOptions.None));
            }
        }

        public class TheGetAllLanguagesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/languages", UriKind.Relative);

                client.GetAllLanguages("owner", "repo");

                gitHubClient.Connection.Received(1).GetResponse<List<Tuple<string, long>>>(expected);
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/languages", UriKind.Relative);

                client.GetAllLanguages(1);

                gitHubClient.Connection.Received(1).GetResponse<List<Tuple<string, long>>>(expected);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllLanguages("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllLanguages("owner", ""));
            }
        }

        public class TheGetAllTeamsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/teams", UriKind.Relative);

                client.GetAllTeams("owner", "repo");

                gitHubClient.Connection.Received(1).Get<List<Team>>(expected,
                    Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/teams", UriKind.Relative);

                client.GetAllTeams(1);

                gitHubClient.Connection.Received(1).Get<List<Team>>(expected,
                    Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/teams", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllTeams("owner", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<Team>>(expected,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/teams", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllTeams(1, options);

                gitHubClient.Connection.Received(1).Get<List<Team>>(expected,
                    Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllTeams(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllTeams("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("", "repo", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllTeams("owner", "", ApiOptions.None));
            }
        }

        public class TheGetAllTagsMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/tags", UriKind.Relative);

                client.GetAllTags("owner", "repo");

                var received = gitHubClient.Connection.ReceivedCalls();
                gitHubClient.Connection.Received(1).Get<List<RepositoryTag>>(expected, Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/tags", UriKind.Relative);

                client.GetAllTags(1);

                gitHubClient.Connection.Received(1).Get<List<RepositoryTag>>(expected, Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repos/owner/repo/tags", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllTags("owner", "repo", options);

                gitHubClient.Connection.Received(1).Get<List<RepositoryTag>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(gitHubClient);
                var expected = new Uri("repositories/1/tags", UriKind.Relative);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAllTags(1, options);

                gitHubClient.Connection.Received(1).Get<List<RepositoryTag>>(expected, Arg.Is<IDictionary<string, string>>(d => d.Count == 2 && d["page"] == "1" && d["per_page"] == "1"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllTags(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags(null, "repo", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllTags("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllTags(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllTags("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("", "repo", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllTags("owner", "", ApiOptions.None));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void PatchsTheCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var update = new RepositoryUpdate(){ Name= "anyreponame" };

                client.Edit("owner", "repo", update);

                github.Repository.Received(1).Edit("owner", "repo", update);
            }

            [Fact]
            public void PatchsTheCorrectUrlWithRepositoryId()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoriesClient(github);
                var update = new RepositoryUpdate(){ Name= "anyreponame" };

                client.Edit(1, update);

                github.Repository.Received(1).Edit(1, update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoriesClient(Substitute.For<IGitHubClient>());
                var update = new RepositoryUpdate() { Name= "anyreponame" };

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "repo", update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, update));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, null));

                Assert.Throws<ArgumentException>(() => client.Edit("", "repo", update));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", update));
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
