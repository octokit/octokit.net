using System.Reactive.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ObservableEnterpriseAdminStatsClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableEnterpriseAdminStatsClientTests()
        {
            _github = new ObservableGitHubClient(EnterpriseHelper.GetAuthenticatedClient());
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsIssues()
        {
            var issueStats = await
                _github.Enterprise.AdminStats.GetStatisticsIssues();

            Assert.NotNull(issueStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsHooks()
        {
            var hookStats = await
                _github.Enterprise.AdminStats.GetStatisticsHooks();

            Assert.NotNull(hookStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsMilestones()
        {
            var milestoneStats = await
                _github.Enterprise.AdminStats.GetStatisticsMilestones();

            Assert.NotNull(milestoneStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsOrgs()
        {
            var orgStats = await
                _github.Enterprise.AdminStats.GetStatisticsOrgs();

            Assert.NotNull(orgStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsComments()
        {
            var commentStats = await
                _github.Enterprise.AdminStats.GetStatisticsComments();

            Assert.NotNull(commentStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsPages()
        {
            var pageStats = await
                _github.Enterprise.AdminStats.GetStatisticsPages();

            Assert.NotNull(pageStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsUsers()
        {
            var userStats = await
                _github.Enterprise.AdminStats.GetStatisticsUsers();

            Assert.NotNull(userStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsGists()
        {
            var gistStats = await
                _github.Enterprise.AdminStats.GetStatisticsGists();

            Assert.NotNull(gistStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsPulls()
        {
            var pullStats = await
                _github.Enterprise.AdminStats.GetStatisticsPulls();

            Assert.NotNull(pullStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsRepos()
        {
            var repoStats = await
                _github.Enterprise.AdminStats.GetStatisticsRepos();

            Assert.NotNull(repoStats);
        }

        [GitHubEnterpriseTest]
        public async Task CanGetStatisticsAll()
        {
            var adminStats = await
                _github.Enterprise.AdminStats.GetStatisticsAll();

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
}