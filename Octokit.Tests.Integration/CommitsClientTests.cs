using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

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
        this._repository = this._gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;        
    }

    [IntegrationTest]
    public async Task CanCreateAndRetrieveCommit()
    {
        string owner = this._repository.Owner.Login;

        var author = new UserAction { Name = "author", Email = "test-author@example.com", Date = DateTime.UtcNow };
        var commiter = new UserAction { Name = "commiter", Email = "test-commiter@example.com", Date = DateTime.Today };
        var newCommit = new NewCommit("test-commit", "tree", Enumerable.Empty<string>())
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