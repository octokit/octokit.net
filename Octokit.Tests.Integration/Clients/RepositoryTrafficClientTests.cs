using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class RepositoryTrafficClientTests
{
    readonly IRepositoryTrafficClient _fixture;
    IGitHubClient _github;

    public RepositoryTrafficClientTests()
    {
        _github = Helper.GetAuthenticatedClient();
        _fixture = _github.Repository.Traffic;
    }

    [IntegrationTest(Skip = "Skipped due to requiring admin permissions")]
    public async Task TheGetAllRefererrsMethod()
    {
        var refererrs = await _fixture.GetAllReferrers("octokit", "ocotkit.net");

        Assert.Equal(1, refererrs.Count);
    }

    [IntegrationTest(Skip = "Skipped due to requiring admin permissions")]
    public async Task TheGetAllPathsMethod()
    {
        var paths = await _fixture.GetAllPaths("octokit", "ocotkit.net");

        Assert.Equal(1, paths.Count);
    }

    [IntegrationTest(Skip = "Skipped due to requiring admin permissions")]
    public async Task TheGetClonesMethod()
    {
        var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
        var clones = await _fixture.GetClones("octokit", "ocotkit.net", request);

        Assert.Equal(2, clones.Count);
        Assert.Equal(clones.Count, clones.Clones.Count);
        Assert.Equal(3, clones.Uniques);

        var uniques = clones.Clones.Where(x => x.Uniques > 0);

        Assert.Equal(clones.Uniques, uniques.Count());
    }

    [IntegrationTest(Skip = "Skipped due to requiring admin permissions")]
    public async Task TheGetViewsMethod()
    {
        var request = new RepositoryTrafficRequest(TrafficDayOrWeek.Day);
        var views = await _fixture.GetViews("octokit", "ocotkit.net", request);

        Assert.Equal(2, views.Count);
        Assert.Equal(1, views.Views.Count);
        Assert.Equal(3, views.Uniques);

        var uniques = views.Views.Where(x => x.Uniques > 0);

        Assert.Equal(views.Uniques, uniques.Count());
    }
}
