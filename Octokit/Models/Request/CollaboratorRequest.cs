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
        public CollaboratorRequest(Permission permissions)
        {
            Permission = permissions;
        }

        /// <summary>
        /// The permission to grant the collaborator on this repository.
        /// </summary>
        public Permission Permission { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Permission: {0}", Permission);
            }
        }
    }
}
