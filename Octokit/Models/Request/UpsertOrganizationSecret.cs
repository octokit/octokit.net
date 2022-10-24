using Octokit.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents request to change the value of a secret for an organization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpsertOrganizationSecret : UpsertRepositorySecret
    {
        public UpsertOrganizationSecret() { }

        public UpsertOrganizationSecret(string encryptedValue, string encryptionKeyId, string visibility, IEnumerable<long> selectedRepositoriesIds)
        {
            EncryptedValue = encryptedValue;
            KeyId = encryptionKeyId;
            Visibility = visibility;
            SelectedRepositoriesIds = selectedRepositoriesIds;
        }

        /// <summary>
        /// The visibility level of the secret
        /// </summary>
        [Parameter(Key = "visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// The list of ids for the repositories with selected visibility to the secret
        /// </summary>
        [Parameter(Key = "selected_repository_ids")]
        public IEnumerable<long> SelectedRepositoriesIds { get; set; }

        internal new string DebuggerDisplay => string.Format(CultureInfo.CurrentCulture, "UpsertOrganizationSecret: Key ID: {0}", KeyId);
    }
}
