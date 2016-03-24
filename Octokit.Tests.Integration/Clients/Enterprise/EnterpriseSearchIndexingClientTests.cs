using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class EnterpriseSearchIndexingClientTests
{
    readonly IGitHubClient _github;

    public EnterpriseSearchIndexingClientTests()
    {
        _github = EnterpriseHelper.GetAuthenticatedClient();
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueOwner()
    {
        var response = await
            _github.Enterprise.SearchIndexing.Queue(EnterpriseHelper.UserName);

        Assert.NotNull(response);
        Assert.NotNull(response.Message);
        Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueRepository()
    {
        var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
        using (var context = await _github.CreateRepositoryContext(newRepository))
        {
            var response = await
                _github.Enterprise.SearchIndexing.Queue(EnterpriseHelper.UserName, context.RepositoryName);

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
        }
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueAll()
    {
        var response = await
            _github.Enterprise.SearchIndexing.QueueAll(EnterpriseHelper.UserName);

        Assert.NotNull(response);
        Assert.NotNull(response.Message);
        Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueAllCodeOwner()
    {
        var response = await
            _github.Enterprise.SearchIndexing.QueueAllCode(EnterpriseHelper.UserName);

        Assert.NotNull(response);
        Assert.NotNull(response.Message);
        Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueAllCodeRepository()
    {
        var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
        using (var context = await _github.CreateRepositoryContext(newRepository))
        {
            var response = await
                _github.Enterprise.SearchIndexing.QueueAllCode(EnterpriseHelper.UserName, context.RepositoryName);

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("was added to the indexing queue")));
        }
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueAllIssuesOwner()
    {
        var response = await
            _github.Enterprise.SearchIndexing.QueueAllIssues(EnterpriseHelper.UserName);

        Assert.NotNull(response);
        Assert.NotNull(response.Message);
        Assert.True(response.Message.All(m => m.Contains("were added to the indexing queue")));
    }

    [GitHubEnterpriseTest]
    public async Task CanQueueAllIssuesRepository()
    {
        var newRepository = new NewRepository(Helper.MakeNameWithTimestamp("public-repo"));
        using (var context = await _github.CreateRepositoryContext(newRepository))
        {
            var response = await
                _github.Enterprise.SearchIndexing.QueueAllIssues(EnterpriseHelper.UserName, context.RepositoryName);

            Assert.NotNull(response);
            Assert.NotNull(response.Message);
            Assert.True(response.Message.All(m => m.Contains("were added to the indexing queue")));
        }
    }
}
