using System;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Users API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/users/">Users API documentation</a> for more information.
    /// </remarks>
    public class EnterpriseClient : ApiClient, IEnterpriseClient
    {
        //static readonly Uri _enterpriseEndpoint = new Uri("enterprise", UriKind.Relative);

        /// <summary>
        /// Instantiates a new GitHub Enterprise API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public EnterpriseClient(IApiConnection apiConnection) : base(apiConnection)
        {
            AdminStats = new EnterpriseAdminStatsClient(apiConnection);
        }

        /// <summary>
        /// A client for GitHub's Enterprise AdminStats API
        /// </summary>
        /// <remarks>
        /// See the <a href="http://developer.github.com/v3/enterprise/admin_stats/">Enterprise Admin Stats API documentation</a> for more information.
        ///</remarks>
        public IEnterpriseAdminStatsClient AdminStats { get; private set; }
    }
}
