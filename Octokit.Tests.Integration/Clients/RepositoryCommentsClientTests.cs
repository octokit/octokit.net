using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;
using System.Collections.Generic;
using System.Linq;

public class RepositoryCommentsClientTests
{
    public class TheGetMethod
    {
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";
        const int repositoryId = 1296269;

        public TheGetMethod()
        {
            var client = Helper.GetAuthenticatedClient();

            _fixture = client.Repository.Comment;
        }

        [IntegrationTest]
        public async Task CanGetComment()
        {
            var commit = await _fixture.Get(owner, name, 1467023);
            Assert.NotNull(commit);
        }

        [IntegrationTest]
        public async Task CanGetCommentWithRepositoryId()
        {
            var commit = await _fixture.Get(repositoryId, 1467023);
            Assert.NotNull(commit);
        }
    }

    public class TheGetAllForRepositoryMethod
    {
        readonly IGitHubClient _client;
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";
        const int repositoryId = 1296269;

        public TheGetAllForRepositoryMethod()
        {
            _client = Helper.GetAuthenticatedClient();

            _fixture = _client.Repository.Comment;
        }

        [IntegrationTest]
        public async Task CanGetListOfCommentsForRepository()
        {
            var list = await _fixture.GetAllForRepository(owner, name);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommentsForRepositoryWithRepositoryId()
        {
            var list = await _fixture.GetAllForRepository(repositoryId);
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAllForRepository(owner, name, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithoutStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAllForRepository(repositoryId, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAllForRepository(owner, name, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithStartWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAllForRepository(repositoryId, options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStart()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAllForRepository(owner, name, startOptions);
            var secondCommit = await _fixture.GetAllForRepository(owner, name, skipStartOptions);

            Assert.NotEqual(firstCommit[0].Id, secondCommit[0].Id);
            Assert.NotEqual(firstCommit[1].Id, secondCommit[1].Id);
            Assert.NotEqual(firstCommit[2].Id, secondCommit[2].Id);
            Assert.NotEqual(firstCommit[3].Id, secondCommit[3].Id);
            Assert.NotEqual(firstCommit[4].Id, secondCommit[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAllForRepository(repositoryId, startOptions);
            var secondCommit = await _fixture.GetAllForRepository(repositoryId, skipStartOptions);

            Assert.NotEqual(firstCommit[0].Id, secondCommit[0].Id);
            Assert.NotEqual(firstCommit[1].Id, secondCommit[1].Id);
            Assert.NotEqual(firstCommit[2].Id, secondCommit[2].Id);
            Assert.NotEqual(firstCommit[3].Id, secondCommit[3].Id);
            Assert.NotEqual(firstCommit[4].Id, secondCommit[4].Id);
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var numberToCreate = 2;
            using (var context = await _client.CreateRepositoryContext("public-repo"))
            {
                var commentIds = new List<int>();

                // Create multiple test commits
                for (int count = 1; count <= numberToCreate; count++)
                {
                    var commit = await SetupCommitForRepository(_client, context.RepositoryOwner, context.RepositoryName);

                    // Each with a comment with reactions
                    var commentId = await HelperCreateCommitCommentWithReactions(_client, context.RepositoryOwner, context.RepositoryName, commit.Sha);
                    commentIds.Add(commentId);
                }
                Assert.Equal(numberToCreate, commentIds.Count);

                // Retrieve all comments for the commit
                var commitComments = await _fixture.GetAllForRepository(context.RepositoryOwner, context.RepositoryName);

                // Check the reactions
                foreach (var commentId in commentIds)
                {
                    var retrieved = commitComments.FirstOrDefault(x => x.Id == commentId);

                    Assert.NotNull(retrieved);
                    Assert.Equal(6, retrieved.Reactions.TotalCount);
                    Assert.Equal(1, retrieved.Reactions.Plus1);
                    Assert.Equal(1, retrieved.Reactions.Hooray);
                    Assert.Equal(1, retrieved.Reactions.Heart);
                    Assert.Equal(1, retrieved.Reactions.Laugh);
                    Assert.Equal(1, retrieved.Reactions.Confused);
                    Assert.Equal(1, retrieved.Reactions.Minus1);
                }
            }
        }
    }

    public class TheGetAllForCommitMethod
    {
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";
        const int repositoryId = 1296269;

        public TheGetAllForCommitMethod()
        {
            var client = Helper.GetAuthenticatedClient();

            _fixture = client.Repository.Comment;
        }

        [IntegrationTest]
        public async Task CanGetListOfCommentsForCommit()
        {
            var list = await _fixture.GetAllForCommit(owner, name, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d");
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetListOfCommentsForCommitWithRepositoryId()
        {
            var list = await _fixture.GetAllForCommit(repositoryId, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d");
            Assert.NotEmpty(list);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithoutStartForCommit()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAllForCommit(owner, name, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithoutStartForCommitWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var commits = await _fixture.GetAllForCommit(repositoryId, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithStartForCommit()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAllForCommit(owner, name, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task CanGetCorrectCountOfCommentsWithStartForCommitWithRepositoryId()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var commits = await _fixture.GetAllForCommit(repositoryId, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", options);
            Assert.Equal(5, commits.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartForCommit()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAllForCommit(owner, name, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", startOptions);
            var secondCommit = await _fixture.GetAllForCommit(owner, name, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", skipStartOptions);

            Assert.NotEqual(firstCommit[0].Id, secondCommit[0].Id);
            Assert.NotEqual(firstCommit[1].Id, secondCommit[1].Id);
            Assert.NotEqual(firstCommit[2].Id, secondCommit[2].Id);
            Assert.NotEqual(firstCommit[3].Id, secondCommit[3].Id);
            Assert.NotEqual(firstCommit[4].Id, secondCommit[4].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctResultsBasedOnStartForCommitWithRepositoryId()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var skipStartOptions = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var firstCommit = await _fixture.GetAllForCommit(repositoryId, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", startOptions);
            var secondCommit = await _fixture.GetAllForCommit(repositoryId, "7fd1a60b01f91b314f59955a4e4d4e80d8edf11d", skipStartOptions);

            Assert.NotEqual(firstCommit[0].Id, secondCommit[0].Id);
            Assert.NotEqual(firstCommit[1].Id, secondCommit[1].Id);
            Assert.NotEqual(firstCommit[2].Id, secondCommit[2].Id);
            Assert.NotEqual(firstCommit[3].Id, secondCommit[3].Id);
            Assert.NotEqual(firstCommit[4].Id, secondCommit[4].Id);
        }
    }

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
        public async Task CanCreateComment()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrieved = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanCreateCommentWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.Repository.Id,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrieved = await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);

            Assert.NotNull(retrieved);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheUpdateMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly RepositoryContext _context;

        public TheUpdateMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanUpdateComment()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrievedBefore = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.NotNull(retrievedBefore);

            await _github.Repository.Comment.Update(_context.RepositoryOwner, _context.RepositoryName, result.Id, "new comment");

            var retrievedAfter = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.Equal("new comment", retrievedAfter.Body);
        }

        [IntegrationTest]
        public async Task CanUpdateCommentWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.Repository.Id,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrievedBefore = await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);

            Assert.NotNull(retrievedBefore);

            await _github.Repository.Comment.Update(_context.Repository.Id, result.Id, "new comment");

            var retrievedAfter = await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);

            Assert.Equal("new comment", retrievedAfter.Body);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    public class TheDeleteMethod : IDisposable
    {
        private readonly IGitHubClient _github;
        private readonly RepositoryContext _context;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _context = _github.CreateRepositoryContext("public-repo").Result;
        }

        [IntegrationTest]
        public async Task CanDeleteComment()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.RepositoryOwner, _context.RepositoryName,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrievedBefore = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.NotNull(retrievedBefore);

            await _github.Repository.Comment.Delete(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            var notFound = false;

            try
            {
                await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);
            }
            catch (NotFoundException)
            {
                notFound = true;
            }

            Assert.True(notFound);
        }

        [IntegrationTest]
        public async Task CanDeleteCommentWithRepositoryId()
        {
            var commit = await SetupCommitForRepository(_github, _context.RepositoryOwner, _context.RepositoryName);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.Repository.Id,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrievedBefore = await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);

            Assert.NotNull(retrievedBefore);

            await _github.Repository.Comment.Delete(_context.Repository.Id, result.Id);

            var notFound = false;

            try
            {
                await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);
            }
            catch (NotFoundException)
            {
                notFound = true;
            }

            Assert.True(notFound);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

    async static Task<Commit> SetupCommitForRepository(IGitHubClient client, string owner, string name)
    {
        var blob = new NewBlob
        {
            Content = "Hello World!",
            Encoding = EncodingType.Utf8
        };
        var blobResult = await client.Git.Blob.Create(owner, name, blob);

        var newTree = new NewTree();
        newTree.Tree.Add(new NewTreeItem
        {
            Type = TreeType.Blob,
            Mode = FileMode.File,
            Path = "README.md",
            Sha = blobResult.Sha
        });

        var treeResult = await client.Git.Tree.Create(owner, name, newTree);

        var newCommit = new NewCommit("test-commit", treeResult.Sha);

        return await client.Git.Commit.Create(owner, name, newCommit);
    }

    async static Task<int> HelperCreateCommitCommentWithReactions(IGitHubClient client, string owner, string name, string sha)
    {
        var newCommitComment = new NewCommitComment("test comment");

        var commitComment = await client.Repository.Comment.Create(owner, name, sha, newCommitComment);

        Assert.NotNull(commitComment);

        foreach (ReactionType reactionType in Enum.GetValues(typeof(ReactionType)))
        {
            var newReaction = new NewReaction(reactionType);

            var reaction = await client.Reaction.CommitComment.Create(owner, name, commitComment.Id, newReaction);

            Assert.IsType<Reaction>(reaction);
            Assert.Equal(reactionType, reaction.Content);
            Assert.Equal(commitComment.User.Id, reaction.User.Id);
        }

        return commitComment.Id;
    }
}
