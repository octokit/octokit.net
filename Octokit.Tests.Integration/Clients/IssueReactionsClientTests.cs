using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task ReturnsCorrectCountOfReactionsWithoutStart()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

            Assert.Single(issueReactions);

            Assert.Equal(issueReaction.Id, issueReactions[0].Id);
            Assert.Equal(issueReaction.Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStart()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(reactionsContent[i]));
                reactions.Add(issueReaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number, options);

            Assert.Single(issueReactions);

            Assert.Equal(reactions.Last().Id, issueReactions[0].Id);
            Assert.Equal(reactions.Last().Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPage()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(reactionsContent[i]));
                reactions.Add(issueReaction);
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var secondPage = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number, skipStartOptions);

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

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number);

            Assert.NotEmpty(issueReactions);

            Assert.Equal(issueReaction.Id, issueReactions[0].Id);
            Assert.Equal(issueReaction.Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithoutStartWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(ReactionType.Heart));

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number, options);

            Assert.Single(issueReactions);

            Assert.Equal(issueReaction.Id, issueReactions[0].Id);
            Assert.Equal(issueReaction.Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStartWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(reactionsContent[i]));
                reactions.Add(issueReaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var issueReactions = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number, options);

            Assert.Single(issueReactions);

            Assert.Equal(reactions.Last().Id, issueReactions[0].Id);
            Assert.Equal(reactions.Last().Content, issueReactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPageWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Heart, ReactionType.Plus1 };
            for (var i = 0; i < 2; i++)
            {
                var issueReaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, new NewReaction(reactionsContent[i]));
                reactions.Add(issueReaction);
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var secondPage = await _github.Reaction.Issue.GetAll(_context.Repository.Id, issue.Number, skipStartOptions);

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

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, newReaction);

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

        [IntegrationTest]
        public async Task CanDeleteReaction()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, newReaction);
                await _github.Reaction.Issue.Delete(_context.RepositoryOwner, _context.RepositoryName, issue.Number, reaction.Id);
            }

            var allReactions = await _github.Reaction.Issue.GetAll(_context.RepositoryOwner, _context.RepositoryName, issue.Number);

            Assert.Empty(allReactions);
        }

        [IntegrationTest]
        public async Task CanDeleteReactionWithRepositoryId()
        {
            var newIssue = new NewIssue("a test issue") { Body = "A new unassigned issue" };
            var issue = await _issuesClient.Create(_context.RepositoryOwner, _context.RepositoryName, newIssue);

            Assert.NotNull(issue);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.Issue.Create(_context.RepositoryOwner, _context.RepositoryName, issue.Number, newReaction);
                await _github.Reaction.Issue.Delete(_context.RepositoryId, issue.Number, reaction.Id);
            }

            var allReactions = await _github.Reaction.Issue.GetAll(_context.RepositoryId, issue.Number);

            Assert.Empty(allReactions);

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
