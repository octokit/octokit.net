using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservablePullRequestReviewCommentsClientTests
    {
        static ApiInfo CreateApiInfo(IDictionary<string, Uri> links)
        {
            return new ApiInfo(links, new List<string>(), new List<string>(), "etag", new RateLimit(new Dictionary<string, string>()));
        }

        public class TheGetForPullRequestMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var firstPageUrl = new Uri("repos/fakeOwner/fakeRepoName/pulls/7/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 1},
                        new PullRequestReviewComment {Id = 2},
                        new PullRequestReviewComment {Id = 3}
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 4},
                        new PullRequestReviewComment {Id = 5},
                        new PullRequestReviewComment {Id = 6}
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 7}
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };

                var gitHubClient = Substitute.For<IGitHubClient>();
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetAll("fakeOwner", "fakeRepoName", 7).ToArray();

                Assert.Equal(7, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null, "name", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("", "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("owner", "", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null, null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("", "", 1));
            }
        }

        public class TheGetForRepositoryMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var firstPageUrl = new Uri("repos/fakeOwner/fakeRepoName/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 1},
                        new PullRequestReviewComment {Id = 2},
                        new PullRequestReviewComment {Id = 3}
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 4},
                        new PullRequestReviewComment {Id = 5},
                        new PullRequestReviewComment {Id = 6}
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 7},
                        new PullRequestReviewComment {Id = 8},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"), null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var request = new PullRequestReviewCommentRequest
                {
                    Direction = SortDirection.Descending,
                    Since = new DateTimeOffset(2013, 11, 15, 11, 43, 01, 00, new TimeSpan()),
                    Sort = PullRequestReviewCommentSort.Updated,
                };

                var results = await client.GetForRepository("fakeOwner", "fakeRepoName", request).ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 3
                        && d["direction"] == "desc"
                        && d["since"] == "2013-11-15T11:43:01Z"
                        && d["sort"] == "updated"), null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithoutSelectedSortingArguments()
            {
                var firstPageUrl = new Uri("repos/fakeOwner/fakeRepoName/pulls/comments", UriKind.Relative);
                var secondPageUrl = new Uri("https://example.com/page/2");
                var firstPageLinks = new Dictionary<string, Uri> { { "next", secondPageUrl } };
                var firstPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 1},
                        new PullRequestReviewComment {Id = 2},
                        new PullRequestReviewComment {Id = 3}
                    },
                    ApiInfo = CreateApiInfo(firstPageLinks)
                };
                var thirdPageUrl = new Uri("https://example.com/page/3");
                var secondPageLinks = new Dictionary<string, Uri> { { "next", thirdPageUrl } };
                var secondPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 4},
                        new PullRequestReviewComment {Id = 5},
                        new PullRequestReviewComment {Id = 6}
                    },
                    ApiInfo = CreateApiInfo(secondPageLinks)
                };
                var lastPageResponse = new ApiResponse<List<PullRequestReviewComment>>
                {
                    BodyAsObject = new List<PullRequestReviewComment>
                    {
                        new PullRequestReviewComment {Id = 7},
                        new PullRequestReviewComment {Id = 8},
                    },
                    ApiInfo = CreateApiInfo(new Dictionary<string, Uri>())
                };

                var gitHubClient = Substitute.For<IGitHubClient>();

                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"), null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => firstPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(secondPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => secondPageResponse));
                gitHubClient.Connection.Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null)
                    .Returns(Task.Factory.StartNew<IResponse<List<PullRequestReviewComment>>>(() => lastPageResponse));

                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var results = await client.GetForRepository("fakeOwner", "fakeRepoName").ToArray();

                Assert.Equal(8, results.Length);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(firstPageUrl,
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2
                        && d["direction"] == "asc"
                        && d["sort"] == "created"), null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(secondPageUrl, null, null);
                gitHubClient.Connection.Received(1).Get<List<PullRequestReviewComment>>(thirdPageUrl, null, null);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var client = new ObservablePullRequestReviewCommentsClient(Substitute.For<IGitHubClient>());

                var request = new PullRequestReviewCommentRequest();

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository(null, "name", request));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("", "name", request));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", null, request));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetForRepository("owner", "", request));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetForRepository("owner", "name", null));
            }
        }

        public class TheGetCommentMethod
        {
            [Fact]
            public void GetsFromClientPullRequestComment()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.GetComment("fakeOwner", "fakeRepoName", 53);

                gitHubClient.PullRequest.Comment.Received().GetComment("fakeOwner", "fakeRepoName", 53);
            }

            [Fact]
            public async Task EnsuresArgumentsNonNull()
            {
                var client = new ObservablePullRequestReviewCommentsClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetComment(null, "name", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetComment("", "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetComment("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetComment("owner", "", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetComment(null, null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetComment("", "", 1));
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

                client.Create("fakeOwner", "fakeRepoName", 13, comment);

                gitHubClient.PullRequest.Comment.Received().Create("fakeOwner", "fakeRepoName", 13, comment);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                string body = "Comment content";
                string commitId = "qe3dsdsf6";
                string path = "file.css";
                int position = 7;

                var comment = new PullRequestReviewCommentCreate(body, commitId, path, position);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", 1, null));
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

                client.CreateReply("fakeOwner", "fakeRepoName", 13, comment);

                gitHubClient.PullRequest.Comment.Received().CreateReply("fakeOwner", "fakeRepoName", 13, comment);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                string body = "Comment content";
                int inReplyTo = 7;

                var comment = new PullRequestReviewCommentReplyCreate(body, inReplyTo);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.CreateReply(null, "name", 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.CreateReply("", "name", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CreateReply("owner", null, 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.CreateReply("owner", "", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.CreateReply("owner", "name", 1, null));
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

                client.Edit("fakeOwner", "fakeRepoName", 13, comment);

                gitHubClient.PullRequest.Comment.Received().Edit("fakeOwner", "fakeRepoName", 13, comment);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                var body = "New comment content";

                var comment = new PullRequestReviewCommentEdit(body);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit(null, "name", 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.Edit("", "name", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit("owner", null, 1, comment));
                await AssertEx.Throws<ArgumentException>(async () => await client.Edit("owner", "", 1, comment));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Edit("owner", "name", 1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                client.Delete("fakeOwner", "fakeRepoName", 13);

                gitHubClient.PullRequest.Comment.Received().Delete("fakeOwner", "fakeRepoName", 13);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservablePullRequestReviewCommentsClient(gitHubClient);

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "name", 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "name", 1));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", null, 1));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("owner", "", 1));
            }
        }
    }
}
