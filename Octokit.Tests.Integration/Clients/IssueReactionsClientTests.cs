using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class IssueReactionsClientTests
{
    public class TheCreateReactionMethod : IDisposable
    {
        private readonly RepositoryContext _context;
        private readonly IIssuesClient _issuesClient;
        private readonly IGitHubClient _github;

        public TheCreateReactionMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _issuesClient = _github.Issue;

            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task CanListReactions()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

            Assert.NotEmpty(issueReactions);

            Assert.Equal(issueReaction.Id, issueReactions[0].Id);
            Assert.Equal(issueReaction.Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task CanListReactionsWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number);

            Assert.NotEmpty(issueReactions);

            Assert.Equal(issueReaction.Id, issueReactions[0].Id);
            Assert.Equal(issueReaction.Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task CanCreateReaction()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Id, newReaction);

                Assert.IsType<Reaction>(reaction);
                Assert.Equal(reactionType, reaction.Content);
                Assert.Equal(issue.User.Id, reaction.User.Id);
            }
        }

        [IntegrationTest]
        public async Task CanCreateReactionWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueReaction = await _github.Reaction.Issue.Create(_context.Repository.Id, issue.Number, new NewReaction(ReactionType.Heart));

            Assert.NotNull(issueReaction);

            Assert.IsType<Reaction>(issueReaction);

            Assert.Equal(ReactionType.Heart, issueReaction.Content);

            Assert.Equal(issue.User.Id, issueReaction.User.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
