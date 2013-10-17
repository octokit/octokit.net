﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive.Clients;

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
                connection.GetAsync<Repository>(Args.Uri, null, null).Returns(response);
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoriesClient(gitHubClient);
                var observable = client.Get("stark", "ned");
                connection.Received(0).GetAsync<Repository>(Args.Uri);

                var result = await observable;
                connection.Received(1).GetAsync<Repository>(Args.Uri, null, null);
                var result2 = await observable;
                // TODO: If we change this to a warm observable, we'll need to change this to Received(2)
                connection.Received(1).GetAsync<Repository>(Args.Uri, null, null);

                Assert.Same(repository, result);
                Assert.Same(repository, result2);
            }
        }

        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void ReturnsEveryPageOfRepositories()
            {
                var firstPageUrl = new Uri("user/repos", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> {{"next", secondPageUrl}};
                var scopes = new List<string>();
                var firstPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 1},
                        new Repository {Id = 2},
                        new Repository {Id = 3}
                    },
                    ApiInfo = new ApiInfo(firstPageLinks, scopes, scopes, "etag", 100, 100)
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
                    ApiInfo = new ApiInfo(secondPageLinks, scopes, scopes, "etag", 100, 100)
                };
                var lastPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 7}
                    },
                    ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), scopes, scopes, "etag", 100, 100)
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetAsync<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetAsync<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetAsync<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = repositoriesClient.GetAllForCurrent().ToArray().Wait();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).GetAsync<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).GetAsync<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).GetAsync<List<Repository>>(thirdPageUrl, null, null);
            }

            [Fact]
            public void StopsMakingNewRequestsWhenTakeIsFulfilled()
            {
                var firstPageUrl = new Uri("user/repos", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var scopes = new List<string>();
                var firstPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 1},
                        new Repository {Id = 2},
                        new Repository {Id = 3}
                    },
                    ApiInfo = new ApiInfo(firstPageLinks, scopes, scopes, "etag", 100, 100)
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
                    ApiInfo = new ApiInfo(secondPageLinks, scopes, scopes, "etag", 100, 100)
                };
                var fourthPageUrl = new Uri("https://example.com/page/4");
                var thirdPageLinks = new Dictionary<string, Uri> { { "next", fourthPageUrl } };
                var thirdPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 7}
                    },
                    ApiInfo = new ApiInfo(thirdPageLinks, scopes, scopes, "etag", 100, 100)
                };
                var lastPageResponse = new ApiResponse<List<Repository>>
                {
                    BodyAsObject = new List<Repository>
                    {
                        new Repository {Id = 8}
                    },
                    ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), scopes, scopes, "etag", 100, 100)
                };
                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.GetAsync<List<Repository>>(firstPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => firstPageResponse));
                gitHubClient.Connection.GetAsync<List<Repository>>(secondPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => secondPageResponse));
                gitHubClient.Connection.GetAsync<List<Repository>>(thirdPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => thirdPageResponse));
                gitHubClient.Connection.GetAsync<List<Repository>>(fourthPageUrl)
                    .Returns(Task.Factory.StartNew<IResponse<List<Repository>>>(() => lastPageResponse));
                var repositoriesClient = new ObservableRepositoriesClient(gitHubClient);

                var results = repositoriesClient.GetAllForCurrent().Take(4).ToArray().Wait();

                Assert.Equal(4, results.Length);
                gitHubClient.Connection.Received(1).GetAsync<List<Repository>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).GetAsync<List<Repository>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(0).GetAsync<List<Repository>>(thirdPageUrl, null, null);
                gitHubClient.Connection.Received(0).GetAsync<List<Repository>>(fourthPageUrl, null, null);
            }
        }
    }
}
