using System;
using System.Collections.Generic;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

using static Octokit.Internal.TestSetup;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestReviewCommentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservablePullRequestReviewCommentsClient(null));
            }
        }

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }

        public class TheGetForPullRequestMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetAll("fake", "repo", 1);

                gitHubClient.Received().PullRequest.ReviewComment.GetAll("fake", "repo", 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetAll(1, 1);

                gitHubClient.Received().PullRequest.ReviewComment.GetAll(1, 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("fake", "repo", 1, options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAll("fake", "repo", 1, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(1, 1, options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAll(1, 1, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlMulti()
            {
                var firstPageUrl = new Uri("repos/owner/name/pulls/7/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    });
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetAll("owner", "name", 7).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls/7/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    });
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetAll(1, 7).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAll(null, "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", null, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAll("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAll(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetAll("", "name", 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAll("owner", "", 1, ApiOptions.None));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                client.GetAllForRepository("fake", "repo", request);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository("fake", "repo", request);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                client.GetAllForRepository(1, request);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository(1, request);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", request, options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository("fake", "repo", request, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository(1, request, options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository(1, request, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlMulti()
            {
                var firstPageUrl = new Uri("repos/owner/name/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    });
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7),
                        new PullRequestReviewComment(8)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                var results = await client.GetAllForRepository("owner", "name", request).ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
            }

            [Fact]
            public async Task RequestsCorrectUrlMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    });
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7),
                        new PullRequestReviewComment(8)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated
                };

                var results = await client.GetAllForRepository(1, request).ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"));
            }

            [Fact]
            public void RequestsCorrectUrlWithoutSelectedSortingArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository("fake", "repo");
            }

            [Fact]
            public void RequestsCorrectUrlWithoutSelectedSortingArgumentsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository(1);
            }

            [Fact]
            public void RequestsCorrectUrlWithoutSelectedSortingArgumentsWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository("fake", "repo", options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository("fake", "repo", options);
            }

            [Fact]
            public void RequestsCorrectUrlWithoutSelectedSortingArgumentsWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllForRepository(1, options);

                gitHubClient.Received().PullRequest.ReviewComment.GetAllForRepository(1, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithoutSelectedSortingArgumentsMulti()
            {
                var firstPageUrl = new Uri("repos/owner/name/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    });
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7),
                        new PullRequestReviewComment(8)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetAllForRepository("owner", "name").ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithoutSelectedSortingArgumentsMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(1),
                        new PullRequestReviewComment(2),
                        new PullRequestReviewComment(3)
                    }
                );
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(4),
                        new PullRequestReviewComment(5),
                        new PullRequestReviewComment(6)
                    });
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment(7),
                        new PullRequestReviewComment(8)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"))
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReviewComment>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetAllForRepository(1).ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestReviewCommentsClient(Substitute.For<IGitHubClient>());

                var request = new PullRequestReviewCommentRequest();

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (ApiOptions)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", (PullRequestReviewCommentRequest)null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name", request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null, request, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", "name", request, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, (PullRequestReviewCommentRequest)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, request, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", request));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", request));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name", request, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", "", request, ApiOptions.None));
            }
        }

        public class TheGetCommentMethod
        {
            [Fact]
            public void GetsFromClientPullRequestComment()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetComment("owner", "name", 53);

                gitHubClient.PullRequest.ReviewComment.Received().GetComment("owner", "name", 53);
            }

            [Fact]
            public void GetsFromClientPullRequestCommentWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetComment(1, 53);

                gitHubClient.PullRequest.ReviewComment.Received().GetComment(1, 53);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestReviewCommentsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetComment(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.GetComment("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.GetComment("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.GetComment("owner", "", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

                client.Create("owner", "name", 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().Create("owner", "name", 13, comment);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentCreate("Comment content", "qe3dsdsf6", "file.css", 7);

                client.Create(1, 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().Create(1, 13, comment);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                string body = "Comment content";
                string commitId = "qe3dsdsf6";
                string path = "file.css";
                int position = 7;

                var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.Create(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", 1, comment));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", 1, comment));
            }
        }

        public class TheCreateReplyMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentReplyCreate("Comment content", 9);

                client.CreateReply("owner", "name", 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().CreateReply("owner", "name", 13, comment);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentReplyCreate("Comment content", 9);

                client.CreateReply(1, 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().CreateReply(1, 13, comment);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                string body = "Comment content";
                int inReplyTo = 7;

                var comment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);

                Assert.Throws<ArgumentNullException>(() => client.CreateReply(null, "name", 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.CreateReply("owner", null, 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.CreateReply("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.CreateReply(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.CreateReply("", "name", 1, comment));
                Assert.Throws<ArgumentException>(() => client.CreateReply("owner", "", 1, comment));
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentEdit("New comment content");

                client.Edit("owner", "name", 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().Edit("owner", "name", 13, comment);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var comment = new PullRequestReviewCommentEdit("New comment content");

                client.Edit(1, 13, comment);

                gitHubClient.PullRequest.ReviewComment.Received().Edit(1, 13, comment);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var body = "New comment content";

                var comment = new PullRequestReviewCommentEdit(body);

                Assert.Throws<ArgumentNullException>(() => client.Edit(null, "name", 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", null, 1, comment));
                Assert.Throws<ArgumentNullException>(() => client.Edit("owner", "name", 1, null));

                Assert.Throws<ArgumentNullException>(() => client.Edit(1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Edit("", "name", 1, comment));
                Assert.Throws<ArgumentException>(() => client.Edit("owner", "", 1, comment));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.Delete("owner", "name", 13);

                gitHubClient.PullRequest.ReviewComment.Received().Delete("owner", "name", 13);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.Delete(1, 13);

                gitHubClient.PullRequest.ReviewComment.Received().Delete(1, 13);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }
    }
}
