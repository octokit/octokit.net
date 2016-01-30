using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseAdminStatsClientTests
    {
        public class TheGetStatisticsIssuesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/issues";
                client.GetStatisticsIssues();
                connection.Received().Get<AdminStatsIssues>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsHooksMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/hooks";
                client.GetStatisticsHooks();
                connection.Received().Get<AdminStatsHooks>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsMilestonesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/milestones";
                client.GetStatisticsMilestones();
                connection.Received().Get<AdminStatsMilestones>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsOrgsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/orgs";
                client.GetStatisticsOrgs();
                connection.Received().Get<AdminStatsOrgs>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsCommentsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/comments";
                client.GetStatisticsComments();
                connection.Received().Get<AdminStatsComments>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsPagesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/pages";
                client.GetStatisticsPages();
                connection.Received().Get<AdminStatsPages>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsUsersMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/users";
                client.GetStatisticsUsers();
                connection.Received().Get<AdminStatsUsers>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsGistsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/gists";
                client.GetStatisticsGists();
                connection.Received().Get<AdminStatsGists>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsPullsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/pulls";
                client.GetStatisticsPulls();
                connection.Received().Get<AdminStatsPulls>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsReposMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/repos";
                client.GetStatisticsRepos();
                connection.Received().Get<AdminStatsRepos>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }

        public class TheGetStatisticsAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                string expectedUri = "enterprise/stats/all";
                client.GetStatisticsAll();
                connection.Received().Get<AdminStats>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }
        }
    }
}