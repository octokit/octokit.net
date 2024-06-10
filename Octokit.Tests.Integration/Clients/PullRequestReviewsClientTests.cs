using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class PullRequestReviewsClientTests
{
    public class TheGetAllMethod
    {
        private readonly IGitHubClient _github;

        public TheGetAllMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task GetsAllReviews()
        {
            var reviews = await _github.PullRequest.Review.GetAll("octokit", "octokit.net", 1648);

            Assert.NotNull(reviews);
            Assert.NotEmpty(reviews);
            Assert.True(reviews.Count > 1);
            Assert.False(string.IsNullOrEmpty(reviews[0].Body));
            Assert.False(string.IsNullOrEmpty(reviews[0].CommitId));
            Assert.False(string.IsNullOrEmpty(reviews[0].User.Login));
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var reviews = await _github.PullRequest.Review.GetAll("octokit", "octokit.net", 1648, options);

            Assert.Single(reviews);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReviewsWithStart()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var reviews = await _github.PullRequest.Review.GetAll("octokit", "octokit.net", 1648, options);

            Assert.Single(reviews);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReviewsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _github.PullRequest.Review.GetAll("octokit", "octokit.net", 1648, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.PullRequest.Review.GetAll("octokit", "octokit.net", 1648, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }
    }

    public class TheGetMethod
    {
        private readonly IGitHubClient _github;

        public TheGetMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task GetsReview()
        {
            var review = await _github.PullRequest.Review.Get("octokit", "octokit.net", 1648, 54646850);

            Assert.NotNull(review);
            Assert.False(string.IsNullOrEmpty(review.Body));
            Assert.False(string.IsNullOrEmpty(review.CommitId));
            Assert.False(string.IsNullOrEmpty(review.User.Login));
        }
    }

    public class TheCreateMethod
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewsClient _client;

        private readonly IGitHubClient _github2;

        public TheCreateMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.PullRequest.Review;

            _github2 = Helper.GetAuthenticatedClient(true);
        }

        [DualAccountTest]
        public async Task CanCreatePendingReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                var body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Pending, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreatePendingReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                const string body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryId, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Pending, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateCommentedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                var body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.Comment,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Commented, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateCommentedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                const string body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.Comment,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryId, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Commented, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateChangesRequestedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                var body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.RequestChanges,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.ChangesRequested, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateChangesRequestedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                const string body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.RequestChanges,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryId, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.ChangesRequested, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateApprovedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                var body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.Approve,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Approved, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }

        [DualAccountTest]
        public async Task CanCreateApprovedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);

                const string body = "A review comment message";

                var review = new PullRequestReviewCreate()
                {
                    CommitId = pullRequest.Head.Sha,
                    Body = body,
                    Event = PullRequestReviewEvent.Approve,
                    Comments = new List<DraftPullRequestReviewComment>
                    {
                        new DraftPullRequestReviewComment("comment 1", "README.md", 1),
                        new DraftPullRequestReviewComment("comment 2", "README.md", 2)
                    }
                };

                var createdReview = await _client.Create(context.RepositoryId, pullRequest.Number, review);

                Assert.NotNull(createdReview);
                Assert.Equal(body, createdReview.Body);
                Assert.Equal(PullRequestReviewState.Approved, createdReview.State);
                Assert.Equal(pullRequest.Head.Sha, createdReview.CommitId);
            }
        }
    }

    public class TheDeleteMethod
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewsClient _client;

        private readonly IGitHubClient _github2;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.PullRequest.Review;

            _github2 = Helper.GetAuthenticatedClient(true);
        }

        [DualAccountTest]
        public async Task CanDeleteReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                await _client.Delete(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, createdReview.Id);

                var retrievedReviews = await _client.GetAll(context.RepositoryOwner, context.RepositoryName, pullRequest.Number);

                Assert.DoesNotContain(retrievedReviews, x => x.Id == createdReview.Id);
            }
        }

        [DualAccountTest]
        public async Task CanDeleteReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                await _client.Delete(context.RepositoryId, pullRequest.Number, createdReview.Id);

                var retrievedReviews = await _client.GetAll(context.RepositoryId, pullRequest.Number);

                Assert.DoesNotContain(retrievedReviews, x => x.Id == createdReview.Id);
            }
        }
    }

    public class TheDismissMethod
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewsClient _client;

        private readonly IGitHubClient _github2;

        public TheDismissMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.PullRequest.Review;

            _github2 = Helper.GetAuthenticatedClient(true);
        }

        [DualAccountTest]
        public async Task CanDismissReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review", PullRequestReviewEvent.RequestChanges);

                var dismissedReview = await _client.Dismiss(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, createdReview.Id, new PullRequestReviewDismiss { Message = "No soup for you!" });

                Assert.Equal(PullRequestReviewState.Dismissed, dismissedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanDismissReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review", PullRequestReviewEvent.RequestChanges);

                var dismissedReview = await _client.Dismiss(context.RepositoryId, pullRequest.Number, createdReview.Id, new PullRequestReviewDismiss { Message = "No soup for you!" });

                Assert.Equal(PullRequestReviewState.Dismissed, dismissedReview.State);
            }
        }
    }

    public class TheGetAllCommentsMethod
    {
        private readonly IGitHubClient _github;

        public TheGetAllCommentsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task GetsAllComments()
        {
            var comments = await _github.PullRequest.Review.GetAllComments("octokit", "octokit.net", 1648, 54646850);

            Assert.NotNull(comments);
            Assert.NotEmpty(comments);
            Assert.True(comments.Count > 1);
            foreach (var comment in comments)
            {
                Assert.False(string.IsNullOrEmpty(comment.Body));
                Assert.False(string.IsNullOrEmpty(comment.CommitId));
                Assert.False(string.IsNullOrEmpty(comment.User.Login));
            }
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCommentsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1
            };

            var comments = await _github.PullRequest.Review.GetAllComments("octokit", "octokit.net", 1648, 54646850, options);

            Assert.Single(comments);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCommentsWithStart()
        {
            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var comments = await _github.PullRequest.Review.GetAllComments("octokit", "octokit.net", 1648, 54646850, options);

            Assert.Single(comments);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctCommentsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageCount = 1,
                PageSize = 1,
                StartPage = 1
            };

            var firstPage = await _github.PullRequest.Review.GetAllComments("octokit", "octokit.net", 1648, 54646850, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.PullRequest.Review.GetAllComments("octokit", "octokit.net", 1648, 54646850, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }
    }

    public class TheSubmitMethod
    {
        private readonly IGitHubClient _github;
        private readonly IPullRequestReviewsClient _client;

        private readonly IGitHubClient _github2;

        public TheSubmitMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _client = _github.PullRequest.Review;

            _github2 = Helper.GetAuthenticatedClient(true);
        }

        [DualAccountTest]
        public async Task CanSubmitCommentedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.Comment
                };
                var submittedReview = await _client.Submit(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.Commented, submittedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanSubmitCommentedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.Comment
                };
                var submittedReview = await _client.Submit(context.RepositoryId, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.Commented, submittedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanSubmitChangesRequestedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.RequestChanges
                };
                var submittedReview = await _client.Submit(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.ChangesRequested, submittedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanSubmitChangesRequestedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.RequestChanges
                };
                var submittedReview = await _client.Submit(context.RepositoryId, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.ChangesRequested, submittedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanSubmitApprovedReview()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.Approve
                };
                var submittedReview = await _client.Submit(context.RepositoryOwner, context.RepositoryName, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.Approved, submittedReview.State);
            }
        }

        [DualAccountTest]
        public async Task CanSubmitApprovedReviewWithRepositoryId()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit("test-repo"))
            {
                await _github.CreateTheWorld(context.Repository);
                var pullRequest = await _github2.CreatePullRequest(context.Repository);
                var createdReview = await _github.CreatePullRequestReview(context.Repository, pullRequest.Number, "A pending review");

                var submitMessage = new PullRequestReviewSubmit
                {
                    Body = "Roger roger!",
                    Event = PullRequestReviewEvent.Approve
                };
                var submittedReview = await _client.Submit(context.RepositoryId, pullRequest.Number, createdReview.Id, submitMessage);

                Assert.Equal("Roger roger!", submittedReview.Body);
                Assert.Equal(PullRequestReviewState.Approved, submittedReview.State);
            }
        }
    }
}