using System;
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
