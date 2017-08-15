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

            Assert.Equal(1, reviews.Count);
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

            Assert.Equal(1, reviews.Count);
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

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
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

            Assert.Equal(1, comments.Count);
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

            Assert.Equal(1, comments.Count);
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

            Assert.Equal(1, firstPage.Count);
            Assert.Equal(1, secondPage.Count);
            Assert.NotEqual(firstPage.First().Id, secondPage.First().Id);
        }
    }
}