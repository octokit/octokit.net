using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseAdminStatsClientTests
    {
        public class TheGetStatisticsIssuesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsIssues();
                github.Enterprise.AdminStats.Received(1).GetStatisticsIssues();
            }
        }

        public class TheGetStatisticsHooksMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsHooks();
                github.Enterprise.AdminStats.Received(1).GetStatisticsHooks();
            }
        }

        public class TheGetStatisticsMilestonesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsMilestones();
                github.Enterprise.AdminStats.Received(1).GetStatisticsMilestones();
            }
        }

        public class TheGetStatisticsOrgsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsOrgs();
                github.Enterprise.AdminStats.Received(1).GetStatisticsOrgs();
            }
        }

        public class TheGetStatisticsCommentsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsComments();
                github.Enterprise.AdminStats.Received(1).GetStatisticsComments();
            }
        }

        public class TheGetStatisticsPagesMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsPages();
                github.Enterprise.AdminStats.Received(1).GetStatisticsPages();
            }
        }

        public class TheGetStatisticsUsersMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsUsers();
                github.Enterprise.AdminStats.Received(1).GetStatisticsUsers();
            }
        }

        public class TheGetStatisticsGistsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsGists();
                github.Enterprise.AdminStats.Received(1).GetStatisticsGists();
            }
        }

        public class TheGetStatisticsPullsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsPulls();
                github.Enterprise.AdminStats.Received(1).GetStatisticsPulls();
            }
        }

        public class TheGetStatisticsReposMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsRepos();
                github.Enterprise.AdminStats.Received(1).GetStatisticsRepos();
            }
        }

        public class TheGetStatisticsAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseAdminStatsClient(github);

                client.GetStatisticsAll();
                github.Enterprise.AdminStats.Received(1).GetStatisticsAll();
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableEnterpriseAdminStatsClient(null));
            }
        }
    }
}