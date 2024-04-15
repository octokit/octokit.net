using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class CommitCommentReactionsClientTests
{
    public class TheCreateReactionMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly RepositoryContext _context;

        public TheCreateReactionMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _context = _github.CreateRepositoryContextWithAutoInit("public-repo").Result;
        }

        private async Task<Commit> SetupCommitForRepository(IGitHubClient client)
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await client.Git.Blob.Create(_context.RepositoryOwner, _context.RepositoryName, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await client.Git.Tree.Create(_context.RepositoryOwner, _context.RepositoryName, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            return await client.Git.Commit.Create(_context.RepositoryOwner, _context.RepositoryName, newCommit);
        }

        [IntegrationTest]
        public async Task CanListReactions()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var confusedReaction = new NewReaction(ReactionType.Confused);
            var firstReaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, confusedReaction);

            var rocketReaction = new NewReaction(ReactionType.Rocket);
            var secondReaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, rocketReaction);

            var reactions = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.NotEmpty(reactions);

            Assert.Equal(firstReaction.Id, reactions[0].Id);
            Assert.Equal(firstReaction.Content, reactions[0].Content);

            Assert.Equal(secondReaction.Id, reactions[1].Id);
            Assert.Equal(secondReaction.Content, reactions[1].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithoutStart()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var newReaction = new NewReaction(ReactionType.Confused);
            var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };
            var reactions = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id, options);

            Assert.Single(reactions);

            Assert.Equal(reaction.Id, reactions[0].Id);
            Assert.Equal(reaction.Content, reactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStart()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Confused, ReactionType.Hooray };
            for (var i = 0; i < 2; i++)
            {
                var newReaction = new NewReaction(reactionsContent[i]);
                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);
                reactions.Add(reaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var reactionsInfo = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id, options);

            Assert.Single(reactionsInfo);

            Assert.Equal(reactions.Last().Id, reactionsInfo[0].Id);
            Assert.Equal(reactions.Last().Content, reactionsInfo[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPage()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var reactionsContent = new[] { ReactionType.Confused, ReactionType.Hooray };
            for (var i = 0; i < 2; i++)
            {
                var newReaction = new NewReaction(reactionsContent[i]);
                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var secondPage = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[0].Content, secondPage[0].Content);
        }

        [IntegrationTest]
        public async Task CanListReactionsWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var newReaction = new NewReaction(ReactionType.Confused);
            var reaction = await _github.Reaction.CommitComment.Create(_context.Repository.Id, result.Id, newReaction);

            var reactions = await _github.Reaction.CommitComment.GetAll(_context.Repository.Id, result.Id);

            Assert.NotEmpty(reactions);

            Assert.Equal(reaction.Id, reactions[0].Id);

            Assert.Equal(reaction.Content, reactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithoutStartWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var newReaction = new NewReaction(ReactionType.Confused);
            var reaction = await _github.Reaction.CommitComment.Create(_context.Repository.Id, result.Id, newReaction);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };
            var reactions = await _github.Reaction.CommitComment.GetAll(_context.Repository.Id, result.Id, options);

            Assert.Single(reactions);

            Assert.Equal(reaction.Id, reactions[0].Id);
            Assert.Equal(reaction.Content, reactions[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfReactionsWithStartWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var reactions = new List<Reaction>();
            var reactionsContent = new[] { ReactionType.Confused, ReactionType.Hooray };
            for (var i = 0; i < 2; i++)
            {
                var newReaction = new NewReaction(reactionsContent[i]);
                var reaction = await _github.Reaction.CommitComment.Create(_context.Repository.Id, result.Id, newReaction);
                reactions.Add(reaction);
            }

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };
            var reactionsInfo = await _github.Reaction.CommitComment.GetAll(_context.Repository.Id, result.Id, options);

            Assert.Single(reactionsInfo);

            Assert.Equal(reactions.Last().Id, reactionsInfo[0].Id);
            Assert.Equal(reactions.Last().Content, reactionsInfo[0].Content);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctReactionsBasedOnStartPageWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var reactionsContent = new[] { ReactionType.Confused, ReactionType.Hooray };
            for (var i = 0; i < 2; i++)
            {
                var newReaction = new NewReaction(reactionsContent[i]);
                var reaction = await _github.Reaction.CommitComment.Create(_context.Repository.Id, result.Id, newReaction);
            }

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var firstPage = await _github.Reaction.CommitComment.GetAll(_context.Repository.Id, result.Id, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };
            var secondPage = await _github.Reaction.CommitComment.GetAll(_context.Repository.Id, result.Id, skipStartOptions);

            Assert.Single(firstPage);
            Assert.Single(secondPage);
            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
            Assert.NotEqual(firstPage[0].Content, secondPage[0].Content);
        }
        [IntegrationTest]
        public async Task CanCreateReaction()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);

                Assert.IsType<Reaction>(reaction);
                Assert.Equal(reactionType, reaction.Content);
                Assert.Equal(result.User.Id, reaction.User.Id);
            }
        }

        [IntegrationTest]
        public async Task CanCreateReactionWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var newReaction = new NewReaction(ReactionType.Confused);
            var reaction = await _github.Reaction.CommitComment.Create(_context.Repository.Id, result.Id, newReaction);

            Assert.IsType<Reaction>(reaction);

            Assert.Equal(ReactionType.Confused, reaction.Content);

            Assert.Equal(result.User.Id, reaction.User.Id);
        }


        [IntegrationTest]
        public async Task CanDeleteReaction()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);

                await _github.Reaction.CommitComment.Delete(_context.RepositoryOwner, _context.RepositoryName, result.Id, reaction.Id);
            }

            var finalComments = await _github.Reaction.CommitComment.GetAll(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.Empty(finalComments);
        }

        [IntegrationTest]
        public async Task CanDeleteReactionWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryId, commit.Sha, comment);

            foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
            {
                var newReaction = new NewReaction(reactionType);

                var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryId, result.Id, newReaction);

                await _github.Reaction.CommitComment.Delete(_context.RepositoryId, result.Id, reaction.Id);
            }

            var finalComments = await _github.Reaction.CommitComment.GetAll(_context.RepositoryId, result.Id);

            Assert.Empty(finalComments);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
