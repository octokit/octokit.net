using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class IssueCommentsClientTests
{
    public class TheCreateMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly RepositoryContext _context;

        public TheCreateMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _github.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueComment = await _github.Issue.Comment.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, "A test comment");

            Assert.NotNull(issueComment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.IssueComment.Create(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id, newReaction);

                Assert.IsType<Reaction>(reaction);
                Assert.Equal(reactionType, reaction.Content);
                Assert.Equal(issueComment.User.Id, reaction.User.Id);
            }

            var retrieved = await _github.Issue.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, issueComment.Id);

            Assert.True(retrieved.Id > 0);
            Assert.Equal(6, retrieved.Reactions.TotalCount);
            Assert.Equal(1, retrieved.Reactions.Plus1);
            Assert.Equal(1, retrieved.Reactions.Hooray);
            Assert.Equal(1, retrieved.Reactions.Heart);
            Assert.Equal(1, retrieved.Reactions.Laugh);
            Assert.Equal(1, retrieved.Reactions.Confused);
            Assert.Equal(1, retrieved.Reactions.Minus1);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
