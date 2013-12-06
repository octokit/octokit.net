using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class IssuesLabelsClientTests : IDisposable
{
    readonly IGitHubClient _gitHubClient;
    readonly IIssuesLabelsClient _issuesLabelsClient;
    readonly IIssuesClient _issuesClient;
    readonly Repository _repository;
    readonly string _repositoryOwner;
    readonly string _repositoryName;

    public IssuesLabelsClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _issuesLabelsClient= _gitHubClient.Issue.Labels;
        _issuesClient = _gitHubClient.Issue;
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
        _repositoryOwner = _repository.Owner.Login;
        _repositoryName = _repository.Name;
    }

    [IntegrationTest]
    public async Task CanListLabelsForAnIssue()
    {
        var newIssue = new NewIssue("A test issue") { Body = "A new unassigned issue" };
        var newLabel = new NewLabel("test label", "#FFFFFF");

        var label = _issuesLabelsClient.Create(_repositoryOwner, _repository.Name, newLabel).Result;
        var issue = await _issuesClient.Create(_repositoryOwner, _repositoryName, newIssue);

        var issueLabelsInfo = await _issuesLabelsClient.GetForIssue(_repositoryOwner, _repositoryName, issue.Number);
        Assert.Empty(issueLabelsInfo);

        var issueUpdate = new IssueUpdate();
        issueUpdate.Labels.Add(label.Name);
        var updated = _issuesClient.Update(_repositoryOwner, _repository.Name, issue.Number, issueUpdate)
            .Result;
        Assert.NotNull(updated);
        issueLabelsInfo = await _issuesLabelsClient.GetForIssue(_repositoryOwner, _repositoryName, issue.Number);

        Assert.Equal(1, issueLabelsInfo.Count);
        Assert.Equal(newLabel.Color, issueLabelsInfo[0].Color);
    }

    [IntegrationTest]
    public async Task CanListIssueLabelsForARepository()
    {
        // create 2 new issues
        var newIssue1 = new NewIssue("A test issue1") { Body = "Everything's coming up Millhouse" };
        var newLabel1 = new NewLabel("test label 1", "#FFFFFF");
        var newLabel2 = new NewLabel("test label 2", "#FFFFFF");

        var issue1 = await _issuesClient.Create(_repositoryOwner, _repository.Name, newIssue1);

        var label1 = _issuesLabelsClient.Create(_repositoryOwner, _repository.Name, newLabel1).Result;
        var label2 = _issuesLabelsClient.Create(_repositoryOwner, _repository.Name, newLabel2).Result;

        // close and open issue1
        var issueUpdate = new IssueUpdate();
        issueUpdate.Labels.Add(label1.Name);
        issueUpdate.Labels.Add(label2.Name);
        var updated1 = _issuesClient.Update(_repositoryOwner, _repository.Name, issue1.Number, issueUpdate)
            .Result;
        Assert.NotNull(updated1);

        var issueLabels = await _issuesLabelsClient.GetForRepository(_repositoryOwner, _repositoryName);

        Assert.Equal(2, issueLabels.Count);
    }

    [IntegrationTest]
    public async Task CanRetrieveIssueLabelByName()
    {
        var newLabel = new NewLabel("test label 1b", "#FFFFFF");
        var label = _issuesLabelsClient.Create(_repositoryOwner, _repository.Name, newLabel).Result;
        Assert.NotNull(label);

        var issueLabelLookupByName = await _issuesLabelsClient.Get(_repositoryOwner, _repositoryName, label.Name);

        Assert.Equal(label.Name, issueLabelLookupByName.Name);
        Assert.Equal(label.Color, issueLabelLookupByName.Color);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
