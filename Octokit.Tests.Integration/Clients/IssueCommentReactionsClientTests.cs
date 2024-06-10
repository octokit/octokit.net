using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class IssueCommentReactionsClientTests
{
    public class TheCreateReactionMethod : IDisposable
    {
        private readonly RepositoryContext _context;
        private readonly IIssuesClient _issuesClient;
        private readonly IGitHubClient _github;

        public TheCreateReactionMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _issuesClient = _github.Issue;
            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task CanListReactions()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, new NewReaction(ReactionType.Heart));

            var reactions = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id);

            Assert.NotEmpty(reactions);

            Assert.Equal(reactions[0].Id, issueCommentReaction.Id);

            Assert.Equal(reactions[0].Content, issueCommentReaction.Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithoutStart()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, new NewReaction(ReactionType.Heart));

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var reactions = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, options);

            Assert.Single(reactions);

            Assert.Equal(reactions[0].Id, issueCommentReaction.Id);
            Assert.Equal(reactions[0].Content, issueCommentReaction.Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStart()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, new NewReaction(reactionsContent[i]));
                reactions.Add(issueCommentReaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reactionsInfo = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, options);

            Assert.Single(reactionsInfo);

            Assert.Equal(reactionsInfo[0].Id, reactions.Last().Id);
            Assert.Equal(reactionsInfo[0].Content, reactions.Last().Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPage()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, new NewReaction(reactionsContent[i]));
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var secondPage = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[0].Content, secondPage[0].Content);
        }

        [IntegrationTest]
        public async Task CanListReactionsWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.Repository.Id, issueComment.Id, new NewReaction(ReactionType.Heart));

            var reactions = await _github.Reaction.IssueComment.GetAll(_context.Repository.Id, issueComment.Id);

            Assert.NotEmpty(reactions);

            Assert.Equal(reactions[0].Id, issueCommentReaction.Id);

            Assert.Equal(reactions[0].Content, issueCommentReaction.Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithoutStartWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.Repository.Id, issueComment.Id, new NewReaction(ReactionType.Heart));

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var reactions = await _github.Reaction.IssueComment.GetAll(_context.Repository.Id, issueComment.Id, options);

            Assert.Single(reactions);

            Assert.Equal(reactions[0].Id, issueCommentReaction.Id);
            Assert.Equal(reactions[0].Content, issueCommentReaction.Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStartWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.Repository.Id, issueComment.Id, new NewReaction(reactionsContent[i]));
                reactions.Add(issueCommentReaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var reactionsInfo = await _github.Reaction.IssueComment.GetAll(_context.Repository.Id, issueComment.Id, options);

            Assert.Single(reactionsInfo);

            Assert.Equal(reactionsInfo[0].Id, reactions.Last().Id);
            Assert.Equal(reactionsInfo[0].Content, reactions.Last().Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPageWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.Repository.Id, issueComment.Id, new NewReaction(reactionsContent[i]));
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.IssueComment.GetAll(_context.Repository.Id, issueComment.Id, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var secondPage = await _github.Reaction.IssueComment.GetAll(_context.Repository.Id, issueComment.Id, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[0].Content, secondPage[0].Content);
        }

        [IntegrationTest]
        public async Task CanCreateReaction()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReaction);

                Assert.IsType<Reaction>(reaction);
                Assert.Equal(reactionType, reaction.Content);
                Assert.Equal(issueComment.User.Id, reaction.User.Id);
            }
        }

        [IntegrationTest]
        public async Task CanCreateReactionWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var issueCommentReaction = await _github.Reaction.IssueComment.Create(_context.Repository.Id, issueComment.Id, new NewReaction(ReactionType.Heart));

            Assert.NotNull(issueCommentReaction);

            Assert.IsType<Reaction>(issueCommentReaction);

            Assert.Equal(ReactionType.Heart, issueCommentReaction.Content);

            Assert.Equal(issueComment.User.Id, issueCommentReaction.User.Id);
        }

        [IntegrationTest]
        public async Task CanDeleteReaction()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReaction);

                await _github.Reaction.IssueComment.Delete(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, reaction.Id);
            }

            var allReactions = await _github.Reaction.IssueComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id);

            Assert.Empty(allReactions);
        }

        [IntegrationTest]
        public async Task CanDeleteReactionWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryId, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryId, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.IssueComment.Create(_context.RepositoryId, issueComment.Id, newReaction);
                await _github.Reaction.IssueComment.Delete(_context.RepositoryId, issueComment.Id, reaction.Id);
            }

            var allReactions = await _github.Reaction.IssueComment.GetAll(_context.RepositoryId, issueComment.Id);

            Assert.Empty(allReactions);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
