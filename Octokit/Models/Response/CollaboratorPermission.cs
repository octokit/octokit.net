using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorPermissionResponse
    {
        public CollaboratorPermissionResponse() { }

        public CollaboratorPermissionResponse(CollaboratorPermission permission, User user)
        {
            Permission = permission;
            User = user;
        }

        public StringEnum<CollaboratorPermission> Permission { get; private set; }
        public User User { get; private set; }

        internal string DebuggerDisplay => $"User: {User.Id} Permission: {Permission}";
    }
}
