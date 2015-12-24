using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IEnterpriseAdminStatsClient
    {
        Task<AdminStats> GetStatistics(AdminStatsType type);
    }

    public enum AdminStatsType
    {
        Issues,
        Hooks,
        Milestones,
        Orgs,
        Comments,
        Pages,
        Users,
        Gists,
        Pulls,
        Repos,
        All
    }
}
