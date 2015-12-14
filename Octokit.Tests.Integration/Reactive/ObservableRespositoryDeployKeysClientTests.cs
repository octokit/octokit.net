using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Reactive;
using Octokit.Tests.Integration;
using Xunit;

public class ObservableRespositoryDeployKeysClientTests : IDisposable
{
    const string _key = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABAQDB8IE5+RppLpeW+6lqo0fpfvMunKg6W4bhYCfVJIOYbpKoHP95nTUMZPBT++9NLeB4/YsuNTCrrpnpjc4f2IVpGvloRiVXjAzoJk9QIL6uzn1zRFdvaxSJ3Urhe9LcLHcIgccgZgSdWGzaZI3xtMvGC4diwWNsPjvVc/RyDM/MPqAim0X5XVOQwEFsSsUSraezJ+VgYMYzLYBcKWW0B86HVVhL4ZtmcY/RN2544bljnzw2M3aQvXNPTvkuiUoqLOI+5/qzZ8PfkruO55YtweEd0lkY6oZvrBPMD6dLODEqMHb4tD6htx60wSipNqjPwpOMpzp0Bk3G909unVXi6Fw5";
    const string _keyTitle = "octokit@github";
    ObservableRepositoryDeployKeysClient _client;
    Repository _repository;
    string _owner;

    public ObservableRespositoryDeployKeysClientTests()
    {
        var github = Helper.GetAuthenticatedClient();

        _client = new ObservableRepositoryDeployKeysClient(github);
        var repoName = Helper.MakeNameWithTimestamp("public-repo");
        var result = github.Repository.Create(new NewRepository(repoName) { AutoInit = true }).Result;
        _repository = result;
        _owner = _repository.Owner.Login;
    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for investigating this failing test")]
    public async Task CanCreateADeployKey()
    {
        var deployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };

        var observable = _client.Create(_owner, _repository.Name, deployKey);
        var createdDeployKey = await observable;

        Assert.NotNull(createdDeployKey);
        Assert.Equal(_key, createdDeployKey.Key);
        Assert.Equal(_keyTitle, createdDeployKey.Title);
    }

    [IntegrationTest(Skip = "See https://github.com/octokit/octokit.net/issues/1003 for investigating this failing test")]
    public async Task CanRetrieveAllDeployKeys()
    {
        var deployKeys = await _client.GetAll(_owner, _repository.Name).ToList();
        Assert.Empty(deployKeys);

        var deployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };
        await _client.Create(_owner, _repository.Name, deployKey);

        deployKeys = await _client.GetAll(_owner, _repository.Name).ToList();
        Assert.Equal(1, deployKeys.Count);
        Assert.Equal(_key, deployKeys[0].Key);
        Assert.Equal(_keyTitle, deployKeys[0].Title);
    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for investigating this failing test")]
    public async Task CanRetrieveADeployKey()
    {
        var newDeployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };

        var createdDeployKey = await _client.Create(_owner, _repository.Name, newDeployKey);

        var deployKey = await _client.Get(_owner, _repository.Name, createdDeployKey.Id);
        Assert.NotNull(deployKey);
        Assert.Equal(createdDeployKey.Id, deployKey.Id);
        Assert.Equal(_key, deployKey.Key);
        Assert.Equal(_keyTitle, deployKey.Title);
    }

    [IntegrationTest(Skip = "see https://github.com/octokit/octokit.net/issues/533 for investigating this failing test")]
    public async Task CanRemoveADeployKey()
    {
        var newDeployKey = new NewDeployKey()
        {
            Key = _key,
            Title = _keyTitle
        };
        await _client.Create(_owner, _repository.Name, newDeployKey);

        var deployKeys = await _client.GetAll(_owner, _repository.Name).ToList();
        Assert.Equal(1, deployKeys.Count);
        Assert.Equal(_key, deployKeys[0].Key);
        Assert.Equal(_keyTitle, deployKeys[0].Title);

        await _client.Delete(_owner, _repository.Name, deployKeys[0].Id);
        deployKeys = await _client.GetAll(_owner, _repository.Name).ToList();
        Assert.Empty(deployKeys);
    }

    public void Dispose()
    {
        Helper.DeleteRepo(_repository);
    }
}
