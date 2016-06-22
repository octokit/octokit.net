using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
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
        public async Task CanCreateReaction()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _issuesClient.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            var newReactionConfused = new NewReaction(ReactionType.Confused);
            var newReactionHeart = new NewReaction(ReactionType.Heart);
            var newReactionHooray = new NewReaction(ReactionType.Hooray);
            var newReactionLaugh = new NewReaction(ReactionType.Laugh);
            var newReactionMinus1 = new NewReaction(ReactionType.Minus1);
            var newReactionPlus1 = new NewReaction(ReactionType.Plus1);
            var reactionConfused = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionConfused);
            var reactionHeart = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionHeart);
            var reactionHooray = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionHooray);
            var reactionLaugh = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionLaugh);
            var reactionMinus1 = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionMinus1);
            var reactionPlus1 = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReactionPlus1);

            Assert.IsType<Reaction>(reactionConfused);
            Assert.Equal(ReactionType.Confused, reactionConfused.Content);
            Assert.Equal(issueComment.User.Id, reactionConfused.User.Id);

            Assert.IsType<Reaction>(reactionHeart);
            Assert.Equal(ReactionType.Heart, reactionHeart.Content);
            Assert.Equal(issueComment.User.Id, reactionHeart.User.Id);

            Assert.IsType<Reaction>(reactionHooray);
            Assert.Equal(ReactionType.Hooray, reactionHooray.Content);
            Assert.Equal(issueComment.User.Id, reactionHooray.User.Id);

            Assert.IsType<Reaction>(reactionLaugh);
            Assert.Equal(ReactionType.Laugh, reactionLaugh.Content);
            Assert.Equal(issueComment.User.Id, reactionLaugh.User.Id);

            Assert.IsType<Reaction>(reactionMinus1);
            Assert.Equal(ReactionType.Minus1, reactionMinus1.Content);
            Assert.Equal(issueComment.User.Id, reactionMinus1.User.Id);

            Assert.IsType<Reaction>(reactionPlus1);
            Assert.Equal(ReactionType.Plus1, reactionPlus1.Content);
            Assert.Equal(issueComment.User.Id, reactionPlus1.User.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
