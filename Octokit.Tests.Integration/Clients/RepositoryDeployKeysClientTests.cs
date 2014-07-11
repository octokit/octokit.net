using System;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class RepositoryDeployKeysClientTests : IDisposable
{
    const string _key = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDB8IE5+RppLpeW+6lqo0fpfvMunKg6W4bhYCfVJIOYbpKoHP95nTUMZPBT++9NLeB4/YsuNTCrrpnpjc4f2IVpGvloRiVXjAzoJk9QIL6uzn1zRFdvaxSJ3Urhe9LcLHcIgccgZgSdWGzaZI3xtMvGC4diwWNsPjvVc/RyDM/MPqAim0X5XVOQwEFsSsUSraezJ+VgYMYzLYBcKWW0B86HVVhL4ZtmcY/RN2544bljnzw2M3aQvXNPTvkuiUoqLOI+5/qzZ8PfkruO55YtweEd0lkY6oZvrBPMD6dLODEqMHb4tD6htx60wSipNqjPwpOMpzp0Bk3G909unVXi6Fw5";
    const string _keyTitle = "octokit@github";
    readonly IGitHubClient _client;
    IRepositoryDeployKeysClient _fixture;
    Repository _repository;
    string _owner;

    public RepositoryDeployKeysClientTests()
    {
        _client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        _fixture = _client.Repository.DeployKeys;
        _repository = _client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true }).Result;
        _owner = _repository.Owner.Login;

    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for the resolution to this failing test")]
    public async Task CanCreateADeployKey()
    {
        var deployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };

        var deployKeyResult = await _fixture.Create(_owner, _repository.Name, deployKey);
        Assert.NotNull(deployKeyResult);
        Assert.Equal(_key, deployKeyResult.Key);
        Assert.Equal(_keyTitle, deployKeyResult.Title);
    }


    [IntegrationTest]
    public async Task CanRetrieveAllDeployKeys()
    {
        var deployKeys = await _fixture.GetAll(_owner, _repository.Name);
        Assert.Equal(0, deployKeys.Count);

        var deployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };

        await _fixture.Create(_owner, _repository.Name, deployKey);

        deployKeys = await _fixture.GetAll(_owner, _repository.Name);
        Assert.Equal(1, deployKeys.Count);
        Assert.Equal(_key, deployKeys[0].Key);
        Assert.Equal(_keyTitle, deployKeys[0].Title);
    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for the resolution to this failing test")]
    public async Task CanRetrieveADeployKey()
    {
        var newDeployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };
        var deployKeyResult = await _fixture.Create(_owner, _repository.Name, newDeployKey);

        var deployKey = await _fixture.Get(_owner, _repository.Name, deployKeyResult.Id);
        Assert.NotNull(deployKey);
        Assert.Equal(deployKeyResult.Id, deployKey.Id);
        Assert.Equal(_key, deployKey.Key);
        Assert.Equal(_keyTitle, deployKey.Title);
    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for the resolution to this failing test")]
    public async Task CanRemoveADeployKey()
    {
        var newDeployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };

        await _fixture.Create(_owner, _repository.Name, newDeployKey);

        var deployKeys = await _fixture.GetAll(_owner, _repository.Name);
        Assert.Equal(1, deployKeys.Count);
        Assert.Equal(_key, deployKeys[0].Key);
        Assert.Equal(_keyTitle, deployKeys[0].Title);

        await _fixture.Delete(_owner, _repository.Name, deployKeys[0].Id);
        deployKeys = await _fixture.GetAll(_owner, _repository.Name);
        Assert.Equal(0, deployKeys.Count);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
