using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class PullRequestsClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly ICommitsClient _commitsClient;
    readonly IPullRequestsClient _pullRequestsClient;
    readonly Repository _modifiedRepository;
    readonly string _modifiedRepositoryOwner;
    readonly string _modifiedRepositoryName;
    readonly Repository _targetRepository;
    readonly string _targetRepositoryOwner;
    readonly string _targetRepositoryName;

    public PullRequestsClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _commitsClient = _gitHubClient.GitDatabase.Commit;
        _pullRequestsClient = _gitHubClient.Repository.PullRequest;

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        var targetRepoName = Helper.MakeNameWithTimestamp("source-repo");

        _modifiedRepository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _modifiedRepositoryOwner = _modifiedRepository.Owner.Login;
        _modifiedRepositoryName = _modifiedRepository.Name;

        _targetRepository = _gitHubClient.Repository.Create(new NewRepository { Name = targetRepoName, AutoInit = true}).Result;
        _targetRepositoryOwner = _targetRepository.Owner.Login;
        _targetRepositoryName = _targetRepository.Name;
        
        // add a new commit to the modified repository
        var author = new Signature { Name = "author", Email = "test-author@example.com", Date = DateTime.UtcNow };
        var commiter = new Signature { Name = "commiter", Email = "test-commiter@example.com", Date = DateTime.Today };

        var newCommit = new NewCommit("test-commit", "", Enumerable.Empty<string>())
        {
            Author = author,
            Committer = commiter
        };

        _commitsClient.Create(_modifiedRepositoryOwner, _modifiedRepositoryName, newCommit);
    }

    [IntegrationTest(Skip = "Requires Tree Api implementation to create a commit")]
    public async Task CanRetrieveOnePullRequest() {
        var baseRef = _targetRepositoryOwner + ":master";
        var headRef = _modifiedRepositoryOwner + ":master";

        var newPullRequest = new NewPullRequest("a pull request", headRef, baseRef);
        var result = await _pullRequestsClient.Create(_targetRepositoryOwner, _targetRepositoryName, newPullRequest);

        Assert.Equal("a pull request", result.Title);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_modifiedRepository);
        Helper.DeleteRepo(_targetRepository);
    }
}
