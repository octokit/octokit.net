using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    public class EnterpriseAdminStatsClient : ApiClient, IEnterpriseAdminStatsClient
    {
        public EnterpriseAdminStatsClient(IApiConnection apiConnection)
            : base(apiConnection)
        { }

        /// <summary>
        /// Gets all email addresses for the authenticated user.
        /// </summary>
        /// <remarks>
        ///https://developer.github.com/v3/enterprise/admin_stats/#get-statistics
        /// </remarks>
        /// <returns>The <see cref="AdminStats"/> collection for the requested <see cref="AdminStatsType"/> type.</returns>
        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase")]
        public Task<AdminStats> GetStatistics(AdminStatsType type)
        {
            return ApiConnection.Get<AdminStats>(ApiUrls.EnterpriseAdminStats(type.ToString().ToLowerInvariant()));
        }
    }
}
