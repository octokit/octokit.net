using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class CodespacesClientTests
{
    readonly ICodespacesClient _fixture;

    public CodespacesClientTests()
    {
        var github = Helper.GetAuthenticatedClient();
        _fixture = github.Codespaces;
    }

    [IntegrationTest]
    public async Task CanGetCodespaces()
    {
        var retrieved = await _fixture.GetAll();
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanGetCodespacesForRepo()
    {
        var retrieved = await _fixture.GetForRepository(Helper.UserName, Helper.RepositoryWithCodespaces);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanGetCodespaceByName()
    {
        var collection = await _fixture.GetForRepository(Helper.UserName, Helper.RepositoryWithCodespaces);
        var codespaceName = collection.Codespaces.First().Name;
        var retrieved = await _fixture.Get(codespaceName);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanStartCodespace()
    {
        var collection = await _fixture.GetForRepository(Helper.UserName, Helper.RepositoryWithCodespaces);
        var codespaceName = collection.Codespaces.First().Name;
        var retrieved = await _fixture.Start(codespaceName);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanStopCodespace()
    {
        var collection = await _fixture.GetForRepository(Helper.UserName, Helper.RepositoryWithCodespaces);
        var codespaceName = collection.Codespaces.First().Name;
        var retrieved = await _fixture.Stop(codespaceName);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanGetAvailableMachinesForRepo()
    {
        var retrieved = await _fixture.GetAvailableMachinesForRepo(Helper.UserName, Helper.RepositoryWithCodespaces);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanCreateCodespace()
    {
        MachinesCollection machinesCollection = (await _fixture.GetAvailableMachinesForRepo(Helper.UserName, Helper.RepositoryWithCodespaces));
        var retrieved = await _fixture.Create(Helper.UserName, Helper.RepositoryWithCodespaces, new NewCodespace(machinesCollection.Machines.First()));
        Assert.NotNull(retrieved);
    }
}
