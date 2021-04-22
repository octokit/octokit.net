using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Org Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/v3/actions/"> Actions API documentation</a> for more information.
    /// </remarks>
    public class OrganizationActionsClient : ApiClient, IOrganizationActionsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs Actions API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public OrganizationActionsClient(IApiConnection apiConnection) : base(apiConnection)
        {
            Secrets = new OrganizationSecretsClient(apiConnection);
        }

        /// <summary>
        /// Returns a client to manage organization secrets.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/v3/actions#secrets"> Secrets API documentation</a> for more information.
        /// </remarks>
        public IOrganizationSecretsClient Secrets { get; private set; }
    }
}
