using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorRequest
    {
        /// <summary>
        /// Used to set the permission for a collaborator.
        /// </summary>
        public CollaboratorRequest(string permission)
        {
            Permission = permission;
        }

        /// <summary>
        /// The permission to grant the collaborator on this repository.
        /// Only valid on organization-owned repositories. We accept the following permissions to be set: 
        /// pull, triage, push, maintain, admin and you can also specify a custom repository role name, 
        /// if the owning organization has defined any.
        /// </summary>
        public string Permission { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture, $"Permission: {Permission}");
    }
}
