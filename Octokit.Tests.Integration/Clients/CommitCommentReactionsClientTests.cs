using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
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

            _context = _github.CreateRepositoryContext("public-repo").Result;
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
        public async Task CanCreateReaction()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var newReactionConfused = new NewReaction(ReactionType.Confused);
            var newReactionHeart = new NewReaction(ReactionType.Heart);
            var newReactionHooray = new NewReaction(ReactionType.Hooray);
            var newReactionLaugh = new NewReaction(ReactionType.Laugh);
            var newReactionMinus1 = new NewReaction(ReactionType.Minus1);
            var newReactionPlus1 = new NewReaction(ReactionType.Plus1);
            var reactionConfused = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionConfused);
            var reactionHeart = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionHeart);
            var reactionHooray = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionHooray);
            var reactionLaugh = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionLaugh);
            var reactionMinus1 = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionMinus1);
            var reactionPlus1 = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReactionPlus1);

            Assert.IsType<Reaction>(reactionConfused);
            Assert.Equal(ReactionType.Confused, reactionConfused.Content);
            Assert.Equal(result.User.Id, reactionConfused.User.Id);

            Assert.IsType<Reaction>(reactionHeart);
            Assert.Equal(ReactionType.Heart, reactionHeart.Content);
            Assert.Equal(result.User.Id, reactionHeart.User.Id);

            Assert.IsType<Reaction>(reactionHooray);
            Assert.Equal(ReactionType.Hooray, reactionHooray.Content);
            Assert.Equal(result.User.Id, reactionHooray.User.Id);

            Assert.IsType<Reaction>(reactionLaugh);
            Assert.Equal(ReactionType.Laugh, reactionLaugh.Content);
            Assert.Equal(result.User.Id, reactionLaugh.User.Id);

            Assert.IsType<Reaction>(reactionMinus1);
            Assert.Equal(ReactionType.Minus1, reactionMinus1.Content);
            Assert.Equal(result.User.Id, reactionMinus1.User.Id);

            Assert.IsType<Reaction>(reactionPlus1);
            Assert.Equal(ReactionType.Plus1, reactionPlus1.Content);
            Assert.Equal(result.User.Id, reactionPlus1.User.Id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
