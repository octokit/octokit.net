using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class GistsClientTests
{
    readonly IGistsClient _fixture;
    const string testGistId = "6305249";

    public GistsClientTests()
    {
        var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
        {
            Credentials = Helper.Credentials
        };

        _fixture = client.Gist;
    }

    [IntegrationTest]
    public async Task CanGetGist()
    {
        var retrieved = await _fixture.Get(testGistId);
        Assert.NotNull(retrieved);
    }

    [IntegrationTest]
    public async Task CanCreateEditAndDeleteAGist()
    {
        var newGist = new NewGist { Description = "my new gist", Public = true };

        newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");

        var createdGist = await _fixture.Create(newGist);

        Assert.NotNull(createdGist);
        Assert.Equal(newGist.Description, createdGist.Description);
        Assert.Equal(newGist.Public, createdGist.Public);

        var gistUpdate = new GistUpdate { Description = "my newly updated gist" };
        var gistFileUpdate = new GistFileUpdate
        {
            NewFileName = "myNewGistTestFile.cs",
            Content = "new GistsClient(connection).Edit();"
        };

        gistUpdate.Files.Add("myGistTestFile.cs", gistFileUpdate);

        var updatedGist = await _fixture.Edit(createdGist.Id, gistUpdate);

        Assert.NotNull(updatedGist);
        Assert.Equal(updatedGist.Description, gistUpdate.Description);

        Assert.DoesNotThrow(async () => { await _fixture.Delete(createdGist.Id); });
    }

    [IntegrationTest]
    public async Task CanStarAndUnstarAGist()
    {
        await _fixture.Star(testGistId);

        var isStarredTrue = await _fixture.IsStarred(testGistId);
        Assert.True(isStarredTrue);

        await _fixture.Unstar(testGistId);

        var isStarredFalse = await _fixture.IsStarred(testGistId);
        Assert.False(isStarredFalse);
    }

    [IntegrationTest]
    public async Task CanForkAGist()
    {
        var forkedGist = await _fixture.Fork(testGistId);

        Assert.NotNull(forkedGist);

        await _fixture.Delete(forkedGist.Id);
    }

    [IntegrationTest]
    public async Task CanListGists()
    {
        // Time is tricky between local and remote, be lenient
        var startTime = DateTimeOffset.Now.Subtract(TimeSpan.FromHours(1));
        var newGist = new NewGist { Description = "my new gist", Public = true };

        newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");

        var createdGist = await _fixture.Create(newGist);

        // Test get all Gists
        var gists = await _fixture.GetAll();
        Assert.NotNull(gists);

        // Test get all Gists since startTime
        gists = await _fixture.GetAll(startTime);

        Assert.NotNull(gists);
        Assert.True(gists.Count > 0);

        // Make sure we can successfully request gists for another user
        Assert.DoesNotThrow(async () => { await _fixture.GetAllForUser("FakeHaacked"); });
        Assert.DoesNotThrow(async () => { await _fixture.GetAllForUser("FakeHaacked", startTime); });

        // Test public gists
        var publicGists = await _fixture.GetAllPublic();
        Assert.True(publicGists.Count > 1);

        var publicGistsSinceStartTime = await _fixture.GetAllPublic(startTime);
        Assert.True(publicGistsSinceStartTime.Count > 0);

        // Test starred gists
        await _fixture.Star(createdGist.Id);
        var starredGists = await _fixture.GetAllStarred();

        Assert.NotNull(starredGists);
        Assert.True(starredGists.Any(x => x.Id == createdGist.Id));

        var starredGistsSinceStartTime = await _fixture.GetAllStarred(startTime);
        Assert.NotNull(starredGistsSinceStartTime);
        Assert.True(starredGistsSinceStartTime.Any(x => x.Id == createdGist.Id));

        await _fixture.Delete(createdGist.Id);
    }
}
