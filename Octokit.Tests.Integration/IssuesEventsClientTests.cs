using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class IssuesEventsClientTests
{
    readonly IGitHubClient _gitHubClient;
    readonly Repository _repository;
    readonly string _owner;

    public IssuesEventsClientTests()
    {
        _gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        var repoName = Helper.MakeNameWithTimestamp("public-repo");

        _repository = _gitHubClient.Repository.Create(new NewRepository { Name = repoName }).Result;
        _owner = _repository.Owner.Login;
    }

    //[IntegrationTest]
    //public async Task CanListEventsForAnIssue()
    //{
    //    throw new NotImplementedException();
    //}

    //[IntegrationTest]
    //public async Task CanListEventsForARepository()
    //{
    //    throw new NotImplementedException();
    //}

    //[IntegrationTest]
    //public async Task CanRetrieveOneEvent()
    //{
    //    throw new NotImplementedException();
    //}

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
