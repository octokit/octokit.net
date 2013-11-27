using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class TreeClientTests : IDisposable
{
    ITreesClient _fixture;
    Repository _repository;
    string _owner;

    public TreeClientTests()
    {
        var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = client.GitDatabase.Tree;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _repository = client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanCreateATree()
    {
        // TODO: create a blob
        // TODO: create a tree

    }

    [IntegrationTest]
    public async Task CanGetATree()
    {
        var tree = await _fixture.Get("octokit", "octokit.net", "master");

        Assert.NotNull(tree);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
