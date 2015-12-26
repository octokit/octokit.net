using System;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseAdminStatsClientTest
    {
        public class TheGetStatisticsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseAdminStatsClient(connection);

                foreach (AdminStatsType type in Enum.GetValues(typeof(AdminStatsType)))
                {
                    client.GetStatistics(type);

                    string expectedUri = String.Format("enterprise/stats/{0}", type.ToString().ToLowerInvariant());
                    switch (type)
                    {
                        case AdminStatsType.All:
                            {
                                connection.Received().Get<AdminStats>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Repos:
                            {
                                connection.Received().Get<AdminStatsRepos>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Hooks:
                            {
                                connection.Received().Get<AdminStatsHooks>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Pages:
                            {
                                connection.Received().Get<AdminStatsPages>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Orgs:
                            {
                                connection.Received().Get<AdminStatsOrgs>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Users:
                            {
                                connection.Received().Get<AdminStatsUsers>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Pulls:
                            {
                                connection.Received().Get<AdminStatsPulls>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Issues:
                            {
                                connection.Received().Get<AdminStatsIssues>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Milestones:
                            {
                                connection.Received().Get<AdminStatsMilestones>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Gists:
                            {
                                connection.Received().Get<AdminStatsGists>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                        case AdminStatsType.Comments:
                            {
                                connection.Received().Get<AdminStatsComments>(Arg.Is<Uri>(u => u.ToString() == expectedUri), null);
                                break;
                            }
                    }
                }
            }
        }
    }
}