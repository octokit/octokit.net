using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorPermission
    {
        public CollaboratorPermission() { }

        public CollaboratorPermission(PermissionLevel permission, User user)
        {
            Permission = permission;
            User = user;
        }

        public StringEnum<PermissionLevel> Permission { get; private set; }
        public User User { get; private set; }

        internal string DebuggerDisplay => $"User: {User.Id} Permission: {Permission}";
    }
}
