using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents response of secrets for an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class OrganizationSecretsCollection
    {
        public OrganizationSecretsCollection()
        {
        }

        public OrganizationSecretsCollection(int count, IReadOnlyList<OrganizationSecret> secrets)
        {
            Count = count;
            Secrets = secrets;
        }

        /// <summary>
        /// The total count of secrets for the organization
        /// </summary>
        [Parameter(Key = "total_count")]
        public int Count { get; private set; }

        /// <summary>
        /// The list of secrets for the organization
        /// </summary>
        [Parameter(Key = "secrets")]
        public IReadOnlyList<OrganizationSecret> Secrets { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "OrganizationSecretCollection: Count: {0}", Count);
    }
}
