using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class CommitStatusClientTests
{
    public class TheGetAllMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveStatuses()
        {
            // Figured it was easier to grab the public status of a public repository for now than
            // to go through the rigamarole of creating it all. But ideally, that's exactly what we'd do.

            var githubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var statuses = await githubClient.Repository.CommitStatus.GetAll(
            "rails",
            "rails",
            "94b857899506612956bb542e28e292308accb908");
            Assert.Equal(2, statuses.Count);
            Assert.Equal(CommitState.Failure, statuses[0].State);
            Assert.Equal(CommitState.Pending, statuses[1].State);
        }
    }

    public class TheCreateMethod : IDisposable
    {
        readonly IGitHubClient _client;
        readonly Repository _repository;
        readonly string _owner;

        public TheCreateMethod()
        {
            _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
            _owner = _repository.Owner.Login;
        }

        [IntegrationTest]
        public async Task CanAssignPendingToCommit()
        {
            var commit = await SetupCommitForRepository(_client);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            var result = await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            Assert.Equal(CommitState.Pending, result.State);
        }

        [IntegrationTest]
        public async Task CanRetrievePendingStatus()
        {
            var commit = await SetupCommitForRepository(_client);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            var statuses = await _client.Repository.CommitStatus.GetAll(_owner, _repository.Name, commit.Sha);

            Assert.Equal(1, statuses.Count);
            Assert.Equal(CommitState.Pending, statuses[0].State);
        }

        [IntegrationTest]
        public async Task CanUpdatePendingStatusToSuccess()
        {
            var commit = await SetupCommitForRepository(_client);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            status.State = CommitState.Success;

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            var statuses = await _client.Repository.CommitStatus.GetAll(_owner, _repository.Name, commit.Sha);

            Assert.Equal(2, statuses.Count);
            Assert.Equal(CommitState.Success, statuses[0].State);
        }

        [IntegrationTest]
        public async Task CanProvideACommitStatusWithoutRequiringAContext()
        {
            var commit = await SetupCommitForRepository(_client);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status"
            };

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            var statuses = await _client.Repository.CommitStatus.GetAll(_owner, _repository.Name, commit.Sha);

            Assert.Equal(1, statuses.Count);
            Assert.Null(statuses[0].Context);
        }

        [IntegrationTest]
        public async Task CanCreateStatusesForDifferentContexts()
        {
            var commit = await SetupCommitForRepository(_client);

            var status = new NewCommitStatus
            {
                State = CommitState.Pending,
                Description = "this is a test status",
                Context = "System A"
            };

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            status.Context = "System B";

            await _client.Repository.CommitStatus.Create(_owner, _repository.Name, commit.Sha, status);

            var statuses = await _client.Repository.CommitStatus.GetAll(_owner, _repository.Name, commit.Sha);

            Assert.Equal(2, statuses.Count);
            Assert.Equal("System B", statuses[0].Context);
            Assert.Equal("System A", statuses[1].Context);
        }

        async Task<Commit> SetupCommitForRepository(IGitHubClient client)
        {
            var blob = new NewBlob
            {
                Content = "Hello World!",
                Encoding = EncodingType.Utf8
            };
            var blobResult = await client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

            var newTree = new NewTree();
            newTree.Tree.Add(new NewTreeItem
            {
                Type = TreeType.Blob,
                Mode = FileMode.File,
                Path = "README.md",
                Sha = blobResult.Sha
            });

            var treeResult = await client.GitDatabase.Tree.Create(_owner, _repository.Name, newTree);

            var newCommit = new NewCommit("test-commit", treeResult.Sha);

            return await client.GitDatabase.Commit.Create(_owner, _repository.Name, newCommit);
        }

        public void Dispose()
        {
            _client.Repository.Delete(_owner, _repository.Name).Wait();
        }
    }
}
