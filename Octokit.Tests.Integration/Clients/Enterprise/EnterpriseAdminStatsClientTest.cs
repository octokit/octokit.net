using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class EnterpriseAdminStatsClientTest
{
    readonly IGitHubClient _github;

    public EnterpriseAdminStatsClientTest()
    {
        _github = Helper.GetAuthenticatedClient();
    }

    [GitHubEnterpriseTest]
    public async Task CanGetAllStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.All);

        Assert.NotNull(adminStats);
        Assert.NotNull(adminStats.Repos);
        Assert.NotNull(adminStats.Hooks);
        Assert.NotNull(adminStats.Pages);
        Assert.NotNull(adminStats.Orgs);
        Assert.NotNull(adminStats.Users);
        Assert.NotNull(adminStats.Pulls);
        Assert.NotNull(adminStats.Issues);
        Assert.NotNull(adminStats.Milestones);
        Assert.NotNull(adminStats.Gists);
        Assert.NotNull(adminStats.Comments);
    }
}