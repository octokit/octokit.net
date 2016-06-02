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

            var newReaction = new NewReaction(ReactionType.Confused);
            var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, newReaction);

            Assert.IsType<Reaction>(reaction);

            Assert.Equal(ReactionType.Confused, reaction.Content);

            Assert.Equal(result.User.Id, reaction.UserId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
