using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorRequest
    {
        public CollaboratorRequest()
        {
            Permission = Permission.Push;
        }

        public CollaboratorRequest(Permission permissions)
        {
            Permission = permissions;
        }

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
