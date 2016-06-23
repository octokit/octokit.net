using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

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
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";
        const int repositoryId = 1296269;

        public TheGetAllForRepositoryMethod()
        {
            var client = Helper.GetAuthenticatedClient();

            _fixture = client.Repository.Comment;
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
        public async Task CanCreateComment()
        {
            var commit = await SetupCommitForRepository(_github);

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
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.Repository.Id,
                commit.Sha, comment);

            Assert.NotNull(result);

            var retrieved = await _github.Repository.Comment.Get(_context.Repository.Id, result.Id);

            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanGetReactionPayload()
        {
            var commit = await SetupCommitForRepository(_github);

            var comment = new NewCommitComment("test");

            var result = await _github.Repository.Comment.Create(_context.Repository.Id,
                commit.Sha, comment);

            var reaction = await _github.Reaction.CommitComment.Create(_context.RepositoryOwner, _context.RepositoryName, result.Id, new NewReaction(ReactionType.Confused));
            var retrieved = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.True(retrieved.Id > 0);
            Assert.Equal(1, retrieved.Reactions.TotalCount);
            Assert.Equal(0, retrieved.Reactions.Plus1);
            Assert.Equal(0, retrieved.Reactions.Hooray);
            Assert.Equal(0, retrieved.Reactions.Heart);
            Assert.Equal(0, retrieved.Reactions.Laugh);
            Assert.Equal(1, retrieved.Reactions.Confused);
            Assert.Equal(0, retrieved.Reactions.Minus1);
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
        public async Task CanUpdateComment()
        {
            var commit = await SetupCommitForRepository(_github);

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
            var commit = await SetupCommitForRepository(_github);

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
        public async Task CanDeleteComment()
        {
            var commit = await SetupCommitForRepository(_github);

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
            var commit = await SetupCommitForRepository(_github);

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
}
