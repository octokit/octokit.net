using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorRequest
    {
        public CollaboratorRequest(Permission permissions)
        {
            Permissions = permissions;
        }

        public Permission Permissions { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Permission: {0}", Permissions);
            }
        }
    }
}
