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
    }

    public class TheGetAllForRepositoryMethod
    {
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";

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
    }

    public class TheGetAllForCommitMethod
    {
        readonly IRepositoryCommentsClient _fixture;
        const string owner = "octocat";
        const string name = "Hello-World";

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

            var retrievedAfter = await _github.Repository.Comment.Get(_context.RepositoryOwner, _context.RepositoryName, result.Id);

            Assert.Null(retrievedAfter);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

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

            var newCommitCommentReaction = new NewCommitCommentReaction(Reaction.Confused);
            var reaction = await _github.Repository.Comment.CreateReaction(_context.RepositoryOwner, _context.RepositoryName, result.Id, newCommitCommentReaction);

            Assert.IsType<CommitCommentReaction>(reaction);

            Assert.Equal(Reaction.Confused, reaction.Content);

            Assert.Equal(result.User.Id, reaction.UserId);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
