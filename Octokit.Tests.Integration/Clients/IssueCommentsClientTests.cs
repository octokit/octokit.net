using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;

public class IssueCommentsClientTests
{
    public class TheGetMethod
    {
        readonly IGitHubClient _github;
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const long id = 12067722;
        const long repositoryId = 7528679;

        public TheGetMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = _github.Issue.Comment;
        }

        [IntegrationTest]
        public async Task ReturnsIssueComment()
        {
            var comment = await _issueCommentsClient.Get(owner, name, id);

            Assert.NotNull(comment);
        }

        [IntegrationTest]
        public async Task ReturnsIssueCommentWithRepositoryId()
        {
            var comment = await _issueCommentsClient.Get(repositoryId, id);

            Assert.NotNull(comment);
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            using (var context = await _github.CreateRepositoryContextWithAutoInit(Helper.MakeNameWithTimestamp("IssueCommentsReactionTests")))
            {
                // Create a test issue
                var issueNumber = await HelperCreateIssue(context.RepositoryOwner, context.RepositoryName);

                // Create a test comment with reactions
                var commentId = await HelperCreateIssueCommentWithReactions(context.RepositoryOwner, context.RepositoryName, issueNumber);

                // Retrieve the comment
                var retrieved = await _issueCommentsClient.Get(context.RepositoryOwner, context.RepositoryName, commentId);

                // Check the reactions
                Assert.True(retrieved.Id > 0);
                Assert.Equal(6, retrieved.Reactions.TotalCount);
                Assert.Equal(1, retrieved.Reactions.Plus1);
                Assert.Equal(1, retrieved.Reactions.Hooray);
                Assert.Equal(1, retrieved.Reactions.Heart);
                Assert.Equal(1, retrieved.Reactions.Laugh);
                Assert.Equal(1, retrieved.Reactions.Confused);
                Assert.Equal(1, retrieved.Reactions.Minus1);
            }
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        readonly IGitHubClient _github;
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const long repositoryId = 7528679;

        public TheGetAllForRepositoryMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = _github.Issue.Comment;
        }

        [IntegrationTest]
        public async Task ReturnsIssueComments()
        {
            var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name);

            Assert.NotEmpty(issueComments);
        }

        [IntegrationTest]
        public async Task ReturnsIssueCommentsWithRepositoryId()
        {
            var issueComments = await _issueCommentsClient.GetAllForRepository(repositoryId);

            Assert.NotEmpty(issueComments);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithRepositoryIdWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var issueComments = await _issueCommentsClient.GetAllForRepository(repositoryId, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var issueComments = await _issueCommentsClient.GetAllForRepository(owner, name, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithRepositoryIdWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var issueComments = await _issueCommentsClient.GetAllForRepository(repositoryId, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPageIssueComments = await _issueCommentsClient.GetAllForRepository(owner, name, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForRepository(owner, name, skipStartOptions);

            Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
            Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
            Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
            Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
            Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPageIssueComments = await _issueCommentsClient.GetAllForRepository(repositoryId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForRepository(repositoryId, skipStartOptions);

            Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
            Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
            Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
            Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
            Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var numberToCreate = 2;
            using (var context = await _github.CreateRepositoryContextWithAutoInit(Helper.MakeNameWithTimestamp("IssueCommentsReactionTests")))
            {
                var commentIds = new List<long>();

                // Create multiple test issues
                for (int count = 1; count <= numberToCreate; count++)
                {
                    var issueNumber = await HelperCreateIssue(context.RepositoryOwner, context.RepositoryName);

                    // Each with a comment with reactions
                    var commentId = await HelperCreateIssueCommentWithReactions(context.RepositoryOwner, context.RepositoryName, issueNumber);
                    commentIds.Add(commentId);
                }
                Assert.Equal(numberToCreate, commentIds.Count);

                // Retrieve all issue comments for the repo
                var issueComments = await _issueCommentsClient.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

                // Check the reactions
                foreach (var commentId in commentIds)
                {
                    var retrieved = issueComments.FirstOrDefault(x => x.Id == commentId);

                    Assert.NotNull(retrieved);
                    Assert.Equal(6, retrieved.Reactions.TotalCount);
                    Assert.Equal(1, retrieved.Reactions.Plus1);
                    Assert.Equal(1, retrieved.Reactions.Hooray);
                    Assert.Equal(1, retrieved.Reactions.Heart);
                    Assert.Equal(1, retrieved.Reactions.Laugh);
                    Assert.Equal(1, retrieved.Reactions.Confused);
                    Assert.Equal(1, retrieved.Reactions.Minus1);
                }
            }
        }
    }

    public class TheGetAllForIssueMethod
    {
        readonly IGitHubClient _github;
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const long issueNumber = 1115;
        const long repositoryId = 7528679;

        public TheGetAllForIssueMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = _github.Issue.Comment;
        }

        [IntegrationTest]
        public async Task ReturnsIssueComments()
        {
            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, issueNumber);

            Assert.NotEmpty(issueComments);
        }

        [IntegrationTest]
        public async Task ReturnsIssueCommentsWithRepositoryId()
        {
            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, issueNumber);

            Assert.NotEmpty(issueComments);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, issueNumber, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithRepositoryIdWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, issueNumber, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, issueNumber, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfIssueCommentsWithRepositoryIdWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, issueNumber, options);

            Assert.Equal(5, issueComments.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPageIssueComments = await _issueCommentsClient.GetAllForIssue(owner, name, issueNumber, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForIssue(owner, name, issueNumber, skipStartOptions);

            Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
            Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
            Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
            Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
            Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartPageWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var firstPageIssueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, issueNumber, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, issueNumber, skipStartOptions);

            Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
            Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
            Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
            Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
            Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var numberToCreate = 2;
            using (var context = await _github.CreateRepositoryContextWithAutoInit(Helper.MakeNameWithTimestamp("IssueCommentsReactionTests")))
            {
                var commentIds = new List<long>();

                // Create a single test issue
                var issueNumber = await HelperCreateIssue(context.RepositoryOwner, context.RepositoryName);

                // With multiple comments with reactions
                for (int count = 1; count <= numberToCreate; count++)
                {
                    var commentId = await HelperCreateIssueCommentWithReactions(context.RepositoryOwner, context.RepositoryName, issueNumber);
                    commentIds.Add(commentId);
                }
                Assert.Equal(numberToCreate, commentIds.Count);

                // Retrieve all comments for the issue
                var issueComments = await _issueCommentsClient.GetAllForIssue(context.RepositoryOwner, context.RepositoryName, issueNumber);

                // Check the reactions
                foreach (var commentId in commentIds)
                {
                    var retrieved = issueComments.FirstOrDefault(x => x.Id == commentId);

                    Assert.NotNull(retrieved);
                    Assert.Equal(6, retrieved.Reactions.TotalCount);
                    Assert.Equal(1, retrieved.Reactions.Plus1);
                    Assert.Equal(1, retrieved.Reactions.Hooray);
                    Assert.Equal(1, retrieved.Reactions.Heart);
                    Assert.Equal(1, retrieved.Reactions.Laugh);
                    Assert.Equal(1, retrieved.Reactions.Confused);
                    Assert.Equal(1, retrieved.Reactions.Minus1);
                }
            }
        }
    }

    public class TheCreateMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;
        readonly RepositoryContext _context;
        readonly IIssuesClient _issuesClient;

        public TheCreateMethod()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = gitHubClient.CreateRepositoryContext(new NewRepository(repoName)).Result;

            _issuesClient = gitHubClient.Issue;
            _issueCommentsClient = gitHubClient.Issue.Comment;
        }

        [IntegrationTest]
        public async Task ReturnsIssueComment()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 1"));

            var comment = await _issueCommentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "test comment 1");

            var loadedComment = await _issueCommentsClient.Get(_context.RepositoryOwner, _context.RepositoryName, comment.Id);

            Assert.Equal(comment.Id, loadedComment.Id);
            Assert.Equal(comment.Body, loadedComment.Body);
        }

        [IntegrationTest]
        public async Task ReturnsIssueCommentWithRepositoryId()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 2"));

            var comment = await _issueCommentsClient.Create(_context.Repository.Id, issue.Number, "test comment 2");

            var loadedComment = await _issueCommentsClient.Get(_context.Repository.Id, comment.Id);

            Assert.Equal(comment.Id, loadedComment.Id);
            Assert.Equal(comment.Body, loadedComment.Body);
        }
    }

    public class TheUpdateMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;
        readonly RepositoryContext _context;
        readonly IIssuesClient _issuesClient;

        public TheUpdateMethod()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = gitHubClient.CreateRepositoryContext(new NewRepository(repoName)).Result;

            _issuesClient = gitHubClient.Issue;
            _issueCommentsClient = gitHubClient.Issue.Comment;
        }

        [IntegrationTest]
        public async Task UpdateIssueComment()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 1"));

            var comment = await _issueCommentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "test comment 1");
            var commentId = comment.Id;

            var beforeComment = await _issueCommentsClient.Get(_context.RepositoryOwner, _context.RepositoryName, commentId);

            await _issueCommentsClient.Update(_context.RepositoryOwner, _context.RepositoryName, commentId, "test comment 2");

            var afterComment = await _issueCommentsClient.Get(_context.RepositoryOwner, _context.RepositoryName, commentId);

            Assert.Equal(beforeComment.Id, afterComment.Id);
            Assert.NotEqual(beforeComment.Body, afterComment.Body);
            Assert.Equal("test comment 2", afterComment.Body);
        }

        [IntegrationTest]
        public async Task UpdateIssueWithRepositoryId()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 1"));

            var comment = await _issueCommentsClient.Create(_context.Repository.Id, issue.Number, "test comment 1");
            var commentId = comment.Id;

            var beforeComment = await _issueCommentsClient.Get(_context.Repository.Id, commentId);

            await _issueCommentsClient.Update(_context.Repository.Id, commentId, "test comment 2");

            var afterComment = await _issueCommentsClient.Get(_context.Repository.Id, commentId);

            Assert.Equal(beforeComment.Id, afterComment.Id);
            Assert.NotEqual(beforeComment.Body, afterComment.Body);
            Assert.Equal("test comment 2", afterComment.Body);
        }
    }

    public class TheDeleteMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;
        readonly RepositoryContext _context;
        readonly IIssuesClient _issuesClient;

        public TheDeleteMethod()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = gitHubClient.CreateRepositoryContext(new NewRepository(repoName)).Result;

            _issuesClient = gitHubClient.Issue;
            _issueCommentsClient = gitHubClient.Issue.Comment;
        }

        [IntegrationTest]
        public async Task DeleteIssueComment()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 1"));

            var comment = await _issueCommentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "test comment 1");

            await _issueCommentsClient.Delete(_context.RepositoryOwner, _context.RepositoryName, comment.Id);

            await Assert.ThrowsAsync<NotFoundException>(() => _issueCommentsClient.Get(_context.RepositoryOwner, _context.RepositoryName, comment.Id));
        }

        [IntegrationTest]
        public async Task DeleteIssueWithRepositoryId()
        {
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, new NewIssue("Super Issue 1"));

            var comment = await _issueCommentsClient.Create(_context.Repository.Id, issue.Number, "test comment 1");

            await _issueCommentsClient.Delete(_context.Repository.Id, comment.Id);

            await Assert.ThrowsAsync<NotFoundException>(() => _issueCommentsClient.Get(_context.Repository.Id, comment.Id));
        }
    }

    async static Task<int> HelperCreateIssue(string owner, string repo)
    {
        var github = Helper.GetAuthenticatedClient();

        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var issue = await github.Issue.Create(owner, repo, newIssue);
        Assert.NotNull(issue);

        return issue.Number;
    }

    async static Task<long> HelperCreateIssueCommentWithReactions(string owner, string repo, long issueNumber)
    {
        var github = Helper.GetAuthenticatedClient();

        var issueComment = await github.Issue.Comment.Create(owner, repo, issueNumber, "A test issue comment");
        Assert.NotNull(issueComment);

        foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
        {
            var newReaction = new NewReaction(reactionType);

            var reaction = await github.Reaction.IssueComment.Create(owner, repo, issueComment.Id, newReaction);

            Assert.IsType<Reaction>(reaction);
            Assert.Equal(reactionType, reaction.Content);
            Assert.Equal(issueComment.User.Id, reaction.User.Id);
        }

        return issueComment.Id;
    }
}
