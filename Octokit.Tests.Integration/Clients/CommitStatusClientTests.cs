using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;
using Octokit.Tests.Integration.Helpers;

public class CommitStatusClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveStatuses()
        {
            // Figured it was easier to grab the public status of a public repository for now than
            // to go through the rigamarole of creating it all. But ideally, that's exactly what we'd do.

            var github = Helper.GetAuthenticatedClient();
            var statuses = await github.Repository.Status.GetAll(
            "rails",
            "rails",
            "94b857899506612956bb542e28e292308accb908");
            Assert.Equal(2, statuses.Count);
            Assert.Equal(CommitState.Failure, statuses[0].State);
            Assert.Equal(CommitState.Pending, statuses[1].State);
        }
    }

    public class TheGetCombinedMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveCombinedStatus()
        {
            var github = Helper.GetAuthenticatedClient();
            var status = await github.Repository.Status.GetCombined(
            "libgit2",
            "libgit2sharp",
            "f54529997b6ad841be524654d9e9074ab8e7d41d");
            Assert.Equal(CommitState.Success, status.State);
            Assert.Equal("f54529997b6ad841be524654d9e9074ab8e7d41d", status.Sha);
            Assert.Equal(2, status.TotalCount);
            Assert.Equal(2, status.Statuses.Count);
            Assert.True(status.Statuses.All(x => x.State == CommitState.Success));
            Assert.Equal("The Travis CI build passed", status.Statuses[0].Description);
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
        public async Task CanAssignPendingToCommit()
        {
            var commit = await SetupCommitForRepository(_github);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            var result = await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            Assert.Equal(CommitState.Pending, result.State);
        }

        [IntegrationTest]
        public async Task CanRetrievePendingStatus()
        {
            var commit = await SetupCommitForRepository(_github);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            var statuses = await _github.Repository.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, commit.Sha);

            Assert.Equal(1, statuses.Count);
            Assert.Equal(CommitState.Pending, statuses[0].State);
        }

        [IntegrationTest]
        public async Task CanUpdatePendingStatusToSuccess()
        {
            var commit = await SetupCommitForRepository(_github);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            status.State = CommitState.Success;

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            var statuses = await _github.Repository.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, commit.Sha);

            Assert.Equal(2, statuses.Count);
            Assert.Equal(CommitState.Success, statuses[0].State);
        }

        [IntegrationTest]
        public async Task CanProvideACommitStatusWithoutRequiringAContext()
        {
            var commit = await SetupCommitForRepository(_github);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            var statuses = await _github.Repository.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, commit.Sha);

            Assert.Equal(1, statuses.Count);
            Assert.Equal("default", statuses[0].Context);
        }

        [IntegrationTest]
        public async Task CanCreateStatusesForDifferentContexts()
        {
            var commit = await SetupCommitForRepository(_github);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status",
                Context = "System A"
            };

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            status.Context = "System B";

            await _github.Repository.Status.Create(_context.RepositoryOwner, _context.RepositoryName, commit.Sha, status);

            var statuses = await _github.Repository.Status.GetAll(_context.RepositoryOwner, _context.RepositoryName, commit.Sha);

            Assert.Equal(2, statuses.Count);
            Assert.Equal("System B", statuses[0].Context);
            Assert.Equal("System A", statuses[1].Context);
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
