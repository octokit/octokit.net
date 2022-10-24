using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Org Actions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/reference/actions"> Actions API documentation</a> for more information.
    /// </remarks>
    public interface IOrganizationActionsClient
    {
        /// <summary>
        /// Returns a client to manage organization secrets.
        /// </summary>
        /// <remarks>
        /// See the <a href="https://docs.github.com/en/rest/reference/actions#secrets"> Secrets API documentation</a> for more information.
        /// </remarks>
        IOrganizationSecretsClient Secrets { get; }
    }
}
