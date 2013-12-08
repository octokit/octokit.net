﻿using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class CommitsClientTests : IDisposable
    {
        readonly IGitHubClient _gitHubClient;
        readonly Repository _repository;
        readonly ICommitsClient _commitsClient;

        public CommitsClientTests()
        {
            this._gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            var repoName = Helper.MakeNameWithTimestamp("public-repo");
            this._commitsClient = this._gitHubClient.GitDatabase.Commit;
            this._repository = this._gitHubClient.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        }

        [IntegrationTest(Skip = "Requires Tree Api implementation to create a commit")]
        public async Task CanCreateAndRetrieveCommit()
        {
            string owner = this._repository.Owner.Login;

            var author = new Signature { Name = "author", Email = "test-author@example.com", Date = DateTime.UtcNow };
            var commiter = new Signature { Name = "commiter", Email = "test-commiter@example.com", Date = DateTime.Today };

            var newCommit = new NewCommit("test-commit", "[Change this to tree sha]", Enumerable.Empty<string>())
            {
                Author = author,
                Committer = commiter
            };

            var commit = await this._commitsClient.Create(owner, this._repository.Name, newCommit);

            Assert.NotNull(commit);
            var retrieved = await this._commitsClient.Get(owner, this._repository.Name, commit.Sha);
            Assert.NotNull(retrieved);
        }


        public void Dispose()
        {
            Helper.DeleteRepo(this._repository);
        }
    }
}