using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Enterprise Admin Stats API
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
    ///</remarks>
    public class EnterpriseAdminStatsClient : ApiClient, IEnterpriseAdminStatsClient
    {
        public EnterpriseAdminStatsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets GitHub Enterprise statistics (must be Site Admin user).
        /// </summary>
        /// <remarks>
        ///https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection for the requested <see cref="AdminStatsType"/> type.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public async Task<AdminStats> GetStatistics(AdminStatsType type)
        {
            var endpoint = ApiUrls.EnterpriseAdminStats(type.ToString().ToLowerInvariant());

            if (type == AdminStatsType.All)
            {
                return await ApiConnection.Get<AdminStats>(endpoint)
                                          .ConfigureAwait(false);
            }

            AdminStatsRepos repos = null;
            AdminStatsHooks hooks = null;
            AdminStatsPages pages = null;
            AdminStatsOrgs orgs = null;
            AdminStatsUsers users = null;
            AdminStatsPulls pulls = null;
            AdminStatsIssues issues = null;
            AdminStatsMilestones milestones = null;
            AdminStatsGists gists = null;
            AdminStatsComments comments = null;

            switch (type)
            {
                case AdminStatsType.Repos:
                    {
                        repos = await ApiConnection.Get<AdminStatsRepos>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Hooks:
                    {
                        hooks = await ApiConnection.Get<AdminStatsHooks>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Pages:
                    {
                        pages = await ApiConnection.Get<AdminStatsPages>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Orgs:
                    {
                        orgs = await ApiConnection.Get<AdminStatsOrgs>(endpoint)
                                                  .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Users:
                    {
                        users = await ApiConnection.Get<AdminStatsUsers>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Pulls:
                    {
                        pulls = await ApiConnection.Get<AdminStatsPulls>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Issues:
                    {
                        issues = await ApiConnection.Get<AdminStatsIssues>(endpoint)
                                                    .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Milestones:
                    {
                        milestones = await ApiConnection.Get<AdminStatsMilestones>(endpoint)
                                                        .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Gists:
                    {
                        gists = await ApiConnection.Get<AdminStatsGists>(endpoint)
                                                   .ConfigureAwait(false);
                        break;
                    }
                case AdminStatsType.Comments:
                    {
                        comments = await ApiConnection.Get<AdminStatsComments>(endpoint)
                                                      .ConfigureAwait(false);
                        break;
                    }
            }

            return new AdminStats(repos, hooks, pages, orgs, users, pulls, issues, milestones, gists, comments);
        }
    }
}
