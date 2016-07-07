using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;
using Octokit.Tests.Integration.Helpers;
using System;

public class IssueCommentsClientTests
{
    public class TheGetMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const int id = 12067722;
        const int repositoryId = 7528679;

        public TheGetMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = github.Issue.Comment;
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
    }

    public class TheGetAllForRepositoryMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const int repositoryId = 7528679;

        public TheGetAllForRepositoryMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = github.Issue.Comment;
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
    }

    public class TheGetAllForIssueMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;

        const string owner = "octokit";
        const string name = "octokit.net";
        const int number = 1115;
        const int repositoryId = 7528679;

        public TheGetAllForIssueMethod()
        {
            var github = Helper.GetAuthenticatedClient();

            _issueCommentsClient = github.Issue.Comment;
        }

        [IntegrationTest]
        public async Task ReturnsIssueComments()
        {
            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, number);

            Assert.NotEmpty(issueComments);
        }

        [IntegrationTest]
        public async Task ReturnsIssueCommentsWithRepositoryId()
        {
            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, number);

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

            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, number, options);

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

            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, number, options);

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

            var issueComments = await _issueCommentsClient.GetAllForIssue(owner, name, number, options);

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

            var issueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, number, options);

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

            var firstPageIssueComments = await _issueCommentsClient.GetAllForIssue(owner, name, number, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForIssue(owner, name, number, skipStartOptions);

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

            var firstPageIssueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, number, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var secondPageIssueComments = await _issueCommentsClient.GetAllForIssue(repositoryId, number, skipStartOptions);

            Assert.NotEqual(firstPageIssueComments[0].Id, secondPageIssueComments[0].Id);
            Assert.NotEqual(firstPageIssueComments[1].Id, secondPageIssueComments[1].Id);
            Assert.NotEqual(firstPageIssueComments[2].Id, secondPageIssueComments[2].Id);
            Assert.NotEqual(firstPageIssueComments[3].Id, secondPageIssueComments[3].Id);
            Assert.NotEqual(firstPageIssueComments[4].Id, secondPageIssueComments[4].Id);
        }
    }

    public class TheCreateMethod
    {
        readonly IIssueCommentsClient _issueCommentsClient;
        readonly RepositoryContext _context;
        readonly IIssuesClient _issuesClient;
        readonly IReactionsClient _reactionsClient;

        public TheCreateMethod()
        {
            var gitHubClient = Helper.GetAuthenticatedClient();

            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = gitHubClient.CreateRepositoryContext(new NewRepository(repoName)).Result;

            _issuesClient = gitHubClient.Issue;
            _issueCommentsClient = gitHubClient.Issue.Comment;
            _reactionsClient = gitHubClient.Reaction;
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issueCommentsClient.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _reactionsClient.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReaction);

                Assert.IsType<Reaction>(reaction);
                Assert.Equal(reactionType, reaction.Content);
                Assert.Equal(issueComment.User.Id, reaction.User.Id);
            }

            var retrieved = await _issueCommentsClient.Get(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id);

            Assert.True(retrieved.Id > 0);
            Assert.Equal(6, retrieved.Reactions.TotalCount);
            Assert.Equal(1, retrieved.Reactions.Plus1);
            Assert.Equal(1, retrieved.Reactions.Hooray);
            Assert.Equal(1, retrieved.Reactions.Heart);
            Assert.Equal(1, retrieved.Reactions.Laugh);
            Assert.Equal(1, retrieved.Reactions.Confused);
            Assert.Equal(1, retrieved.Reactions.Minus1);
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
}
