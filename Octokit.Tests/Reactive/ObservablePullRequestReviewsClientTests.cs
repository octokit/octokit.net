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
    public class ObservablePullRequestReviewsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservablePullRequestReviewsClient(null));
            }
        }

        static IResponse CreateResponseWithApiInfo(IDictionary<string, Uri> links)
        {
            var response = Substitute.For<IResponse>();
            response.ApiInfo.Returns(new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>())));
            return response;
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.GetAll("fake", "repo", 1);

                gitHubClient.Received().PullRequest.Review.GetAll("fake", "repo", 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.GetAll(1, 1);

                gitHubClient.Received().PullRequest.Review.GetAll(1, 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("fake", "repo", 1, options);

                gitHubClient.Received().PullRequest.Review.GetAll("fake", "repo", 1, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll(1, 1, options);

                gitHubClient.Received().PullRequest.Review.GetAll(1, 1, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlMulti()
            {
                var firstPageUrl = new Uri("repos/owner/name/pulls/7/reviews", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(1),
                        new PullRequestReview(2),
                        new PullRequestReview(3)
                    });
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(4),
                        new PullRequestReview(5),
                        new PullRequestReview(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(7)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReview>>(firstPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReview>>(secondPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReview>>(thirdPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var results = await client.GetAll("owner", "name", 7).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(firstPageUrl, Args.EmptyDictionary, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(secondPageUrl, Args.EmptyDictionary, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(thirdPageUrl, Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task RequestsCorrectUrlMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls/7/reviews", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponseWithApiInfo(firstPageLinks),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(1),
                        new PullRequestReview(2),
                        new PullRequestReview(3)
                    });
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponseWithApiInfo(secondPageLinks),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(4),
                        new PullRequestReview(5),
                        new PullRequestReview(6)
                    }
                );
                var lastPageResponse = new ApiResponse<List<PullRequestReview>>
                (
                    CreateResponse(HttpStatusCode.OK),
                    new List<PullRequestReview>
                    {
                        new PullRequestReview(7)
                    }
                );

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReview>>(firstPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReview>>(secondPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReview>>(thirdPageUrl, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult<IApiResponse<List<PullRequestReview>>>(lastPageResponse));

                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var results = await client.GetAll(1, 7).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(firstPageUrl, Args.EmptyDictionary, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(secondPageUrl, Args.EmptyDictionary, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReview>>(thirdPageUrl, Args.EmptyDictionary, null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

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

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.Get("owner", "name", 53, 2);

                gitHubClient.Received().PullRequest.Review.Get("owner", "name", 53, 2);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.Get(1, 53, 2);

                gitHubClient.Received().PullRequest.Review.Get(1, 53, 2);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestReviewsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1, 1));
                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1, 1));

                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1, 1));
                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1, 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var comment = new DraftPullRequestReviewComment("Comment content", "file.css", 7);
                var review = new PullRequestReviewCreate()
                {
                    CommitId = "commit",
                    Body = "body",
                    Event = PullRequestReviewEvent.Approve
                };

                review.Comments.Add(comment);

                client.Create("owner", "name", 53, review);

                gitHubClient.Received().PullRequest.Review.Create("owner", "name", 53, review);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var comment = new DraftPullRequestReviewComment("Comment content", "file.css", 7);
                var review = new PullRequestReviewCreate()
                {
                    CommitId = "commit",
                    Body = "body",
                    Event = PullRequestReviewEvent.Approve
                };

                review.Comments.Add(comment);

                client.Create(1, 13, review);

                gitHubClient.Received().PullRequest.Review.Create(1, 53, review);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservablePullRequestReviewsClient(Substitute.For<IGitHubClient>());

                string body = "Comment content";
                string path = "file.css";
                int position = 7;

                var comment = new DraftPullRequestReviewComment(body, path, position);

                var review = new PullRequestReviewCreate()
                {
                    CommitId = "commit",
                    Body = "body",
                    Event = PullRequestReviewEvent.Approve
                };

                review.Comments.Add(comment);

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "fakeRepoName", 1, review));
                Assert.Throws<ArgumentNullException>(() => client.Create("fakeOwner", null, 1, review));
                Assert.Throws<ArgumentNullException>(() => client.Create("fakeOwner", "fakeRepoName", 1, null));

                Assert.Throws<ArgumentException>(() => client.Create("", "fakeRepoName", 1, review));
                Assert.Throws<ArgumentException>(() => client.Create("fakeOwner", "", 1, review));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.Delete("owner", "name", 13, 13);

                gitHubClient.Received().PullRequest.Review.Delete("owner", "name", 13, 13);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.Delete(1, 13, 13);

                gitHubClient.Received().PullRequest.Review.Delete(1, 13, 13);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1, 1));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1, 1));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1, 1));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 1, 1));
            }
        }

        public class TheDismissMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var dismissMessage = new PullRequestReviewDismiss()
                {
                    Message = "test message"
                };

                client.Dismiss("owner", "name", 13, 13, dismissMessage);

                gitHubClient.Received().PullRequest.Review.Dismiss("owner", "name", 13, 13, dismissMessage);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var dismissMessage = new PullRequestReviewDismiss()
                {
                    Message = "test message"
                };

                client.Dismiss(1, 13, 13, dismissMessage);

                gitHubClient.Received().PullRequest.Review.Dismiss(1, 13, 13, dismissMessage);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var dismissMessage = new PullRequestReviewDismiss()
                {
                    Message = "test message"
                };

                Assert.Throws<ArgumentNullException>(() => client.Dismiss(null, "name", 1, 1, dismissMessage));
                Assert.Throws<ArgumentNullException>(() => client.Dismiss("owner", null, 1, 1, dismissMessage));
                Assert.Throws<ArgumentNullException>(() => client.Dismiss("owner", "name", 1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Dismiss("", "name", 1, 1, dismissMessage));
                Assert.Throws<ArgumentException>(() => client.Dismiss("owner", "", 1, 1, dismissMessage));
            }
        }

        public class TheGetAllCommentsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.GetAllComments("fake", "repo", 1, 1);

                gitHubClient.Received().PullRequest.Review.GetAllComments("fake", "repo", 1, 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                client.GetAllComments(1, 1, 1);

                gitHubClient.Received().PullRequest.Review.GetAllComments(1, 1, 1);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllComments("fake", "repo", 1, 1, options);

                gitHubClient.Received().PullRequest.Review.GetAllComments("fake", "repo", 1, 1, options);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAllComments(1, 1, 1, options);

                gitHubClient.Received().PullRequest.Review.GetAllComments(1, 1, 1, options);
            }

            [Fact]
            public async Task RequestsCorrectUrlMulti()
            {
                var firstPageUrl = new Uri("repos/owner/name/pulls/7/reviews/1/comments", UriKind.Relative);
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

                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var results = await client.GetAllComments("owner", "name", 7, 1).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary);
            }

            [Fact]
            public async Task RequestsCorrectUrlMultiWithRepositoryId()
            {
                var firstPageUrl = new Uri("repositories/1/pulls/7/reviews/1/comments", UriKind.Relative);
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

                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var results = await client.GetAllComments(1, 7, 1).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, Args.EmptyDictionary);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, Args.EmptyDictionary);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllComments(null, "name", 1, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllComments("owner", null, 1, 1));

                Assert.Throws<ArgumentNullException>(() => client.GetAllComments(null, "name", 1, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllComments("owner", null, 1, 1, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllComments("owner", "name", 1, 1, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllComments(1, 1, 1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllComments("", "name", 1, 1));
                Assert.Throws<ArgumentException>(() => client.GetAllComments("owner", "", 1, 1));

                Assert.Throws<ArgumentException>(() => client.GetAllComments("", "name", 1, 1, ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllComments("owner", "", 1, 1, ApiOptions.None));
            }
        }

        public class TheSubmitMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var submitMessage = new PullRequestReviewSubmit()
                {
                    Body = "string",
                    Event = PullRequestReviewEvent.Approve
                };

                client.Submit("owner", "name", 13, 13, submitMessage);

                gitHubClient.Received().PullRequest.Review.Submit("owner", "name", 13, 13, submitMessage);
            }

            [Fact]
            public void PostsToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var submitMessage = new PullRequestReviewSubmit()
                {
                    Body = "string",
                    Event = PullRequestReviewEvent.Approve
                };

                client.Submit(1, 13, 13, submitMessage);

                gitHubClient.Received().PullRequest.Review.Submit(1, 13, 13, submitMessage);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewsClient(gitHubClient);

                var submitMessage = new PullRequestReviewSubmit()
                {
                    Body = "string",
                    Event = PullRequestReviewEvent.Approve
                };

                Assert.Throws<ArgumentNullException>(() => client.Submit(null, "name", 1, 1, submitMessage));
                Assert.Throws<ArgumentNullException>(() => client.Submit("owner", null, 1, 1, submitMessage));
                Assert.Throws<ArgumentNullException>(() => client.Submit("owner", "name", 1, 1, null));

                Assert.Throws<ArgumentException>(() => client.Submit("", "name", 1, 1, submitMessage));
                Assert.Throws<ArgumentException>(() => client.Submit("owner", "", 1, 1, submitMessage));
            }
        }
    }
}
