using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Helpers;
using Octokit.Tests.Integration;
using Xunit;

public class ReferencesClientTests
{
    readonly IReferencesClient _fixture;

    public ReferencesClientTests()
    {
        var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };
        _fixture = client.GitDatabase.Reference;
    }

    [IntegrationTest]
    public async Task CanGetAReference()
    {
        var @ref = await _fixture.Get("octokit", "octokit.net", "heads/master");

        // validate the top-level properties
        Assert.Equal("refs/heads/master", @ref.Ref);
        Assert.Equal("https://api.github.com/repos/octokit/octokit.net/git/refs/heads/master", @ref.Url);

        // validate the git reference
        Assert.Equal(TaggedType.Commit, @ref.Object.Type);
        Assert.False(String.IsNullOrWhiteSpace(@ref.Object.Sha));
    }

    [IntegrationTest]
    public async Task CanGetListOfReferences()
    {
        var list = await _fixture.GetAll("octokit", "octokit.net");
        Assert.NotEmpty(list);
    }

    [IntegrationTest]
    public async Task CanGetListOfReferencesInNamespace()
    {
        var list = await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "heads");
        Assert.NotEmpty(list);
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanGetErrorForInvalidNamespace()
    {
        await AssertEx.Throws<Exception>(
            async () => { await _fixture.GetAllForSubNamespace("octokit", "octokit.net", "666"); });
    }

    [IntegrationTest(Skip = "TODO")]
    public async Task CanCreateAReference()
    {
        // TODO: create a blob
        // TODO: create a tree
        // TODO: create a commit
        // TODO: use the SHA to create a reference
        var newReference = new NewReference("heads/develop", "sha");
        var result = await _fixture.Create("owner", "repo", newReference);

        Assert.NotNull(result);
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanUpdateAReference()
    {
        // TODO: create a blob
        // TODO: create a tree
        // TODO: create a commit
        // TODO: use the SHA to create a reference

        var newReference = new NewReference("heads/develop", "sha");
        await _fixture.Create("owner", "repo", newReference);

        var referenceUpdate = new ReferenceUpdate("sha");

        var result = await _fixture.Update("owner", "repo", "heads/develop", referenceUpdate);

        Assert.NotNull(result);
    }

    [IntegrationTest(Skip="TODO")]
    public async Task CanDeleteAReference()
    {
        // TODO: create a blob
        // TODO: create a tree
        // TODO: create a commit
        // TODO: use the SHA to create a reference

        // TODO: can we validate the response here?
        // TODO: otherwise, fire off a GetAll and validate it's not in the list

        await _fixture.Delete("owner", "repo", "heads/develop");
    }
}
