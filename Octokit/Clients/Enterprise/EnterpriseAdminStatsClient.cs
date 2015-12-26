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
        public Task<AdminStats> GetStatistics(AdminStatsType type)
        {
            var endpoint = ApiUrls.EnterpriseAdminStats(type.ToString().ToLowerInvariant());

            if (type == AdminStatsType.All)
            {
                return ApiConnection.Get<AdminStats>(endpoint);
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
                        repos = ApiConnection.Get<AdminStatsRepos>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Hooks:
                    {
                        hooks = ApiConnection.Get<AdminStatsHooks>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Pages:
                    {
                        pages = ApiConnection.Get<AdminStatsPages>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Orgs:
                    {
                        orgs = ApiConnection.Get<AdminStatsOrgs>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Users:
                    {
                        users = ApiConnection.Get<AdminStatsUsers>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Pulls:
                    {
                        pulls = ApiConnection.Get<AdminStatsPulls>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Issues:
                    {
                        issues = ApiConnection.Get<AdminStatsIssues>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Milestones:
                    {
                        milestones = ApiConnection.Get<AdminStatsMilestones>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Gists:
                    {
                        gists = ApiConnection.Get<AdminStatsGists>(endpoint).Result;
                        break;
                    }
                case AdminStatsType.Comments:
                    {
                        comments = ApiConnection.Get<AdminStatsComments>(endpoint).Result;
                        break;
                    }
            }

            //return new AdminStats(repos, hooks, pages, orgs, users, pulls, issues, milestones, gists, comments);

            return Task.Run(() =>
            { 
                return new AdminStats(repos, hooks, pages, orgs, users, pulls, issues, milestones, gists, comments);
            });
        }
    }
}
