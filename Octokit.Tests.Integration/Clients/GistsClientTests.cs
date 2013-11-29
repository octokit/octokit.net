using System.Net.Http.Headers;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class GistsClientTests
{
    readonly IGistsClient _fixture;

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
        var retrieved = await _fixture.Get("6305249");
        Assert.NotNull(retrieved);
    }
}