using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
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

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

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

