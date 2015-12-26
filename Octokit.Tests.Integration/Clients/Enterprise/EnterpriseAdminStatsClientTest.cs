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

    [GitHubEnterpriseTest]
    public async Task CanGetReposStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Repos);

        Assert.NotNull(adminStats);
        Assert.NotNull(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetHooksStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Hooks);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.NotNull(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetPagesStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Pages);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.NotNull(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetOrgsStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Orgs);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.NotNull(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetUsersStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Users);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.NotNull(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetPullsStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Pulls);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.NotNull(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetIssuesStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Issues);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.NotNull(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetMilstonesStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Milestones);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.NotNull(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetGistsStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Gists);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.NotNull(adminStats.Gists);
        Assert.Null(adminStats.Comments);
    }

    [GitHubEnterpriseTest]
    public async Task CanGetCommentsStatistics()
    {
        var adminStats = await
            _github.Enterprise.AdminStats.GetStatistics(AdminStatsType.Comments);

        Assert.NotNull(adminStats);
        Assert.Null(adminStats.Repos);
        Assert.Null(adminStats.Hooks);
        Assert.Null(adminStats.Pages);
        Assert.Null(adminStats.Orgs);
        Assert.Null(adminStats.Users);
        Assert.Null(adminStats.Pulls);
        Assert.Null(adminStats.Issues);
        Assert.Null(adminStats.Milestones);
        Assert.Null(adminStats.Gists);
        Assert.NotNull(adminStats.Comments);
    }
}