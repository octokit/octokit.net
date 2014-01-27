using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class StatisticsClientTests
    {
        public class TheContributorStatistics
        {
            readonly IGitHubClient _client;
            readonly Repository _repository;
            readonly ICommitsClient _fixture;
            readonly string _owner;
            readonly string _repoName;

            public TheContributorStatistics()
            {
                _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                _repoName = Helper.MakeNameWithTimestamp("public-repo");
                _fixture = _client.GitDatabase.Commit;
                _repository = _client.Repository.Create(new NewRepository { Name = _repoName, AutoInit = true }).Result;
                _owner = _repository.Owner.Login;
            }


            [IntegrationTest]
            public async Task CanCreateAndRetrieveCommit()
            {
                await SeedRepository();
                var contributors = await _client.Statistics.GetContributors(_owner, _repoName);
                Assert.NotNull(contributors);
            }

            async Task<Commit> SeedRepository()
            {
                var blob = new NewBlob
                {
                    Content = "Hello World!",
                    Encoding = EncodingType.Utf8
                };
                var blobResult = await _client.GitDatabase.Blob.Create(_owner, _repository.Name, blob);

                var newTree = new NewTree();
                newTree.Tree.Add(new NewTreeItem
                {
                    Type = TreeType.Blob,
                    Mode = FileMode.File,
                    Path = "README.md",
                    Sha = blobResult.Sha
                });

                var treeResult = await _client.GitDatabase.Tree.Create(_owner, _repository.Name, newTree);

                var newCommit = new NewCommit("test-commit", treeResult.Sha);

                var commit = await _fixture.Create(_owner, _repository.Name, newCommit);
                return commit;
            }
        }
    }
}
