﻿using System.Diagnostics;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorPermission
    {
        public StringEnum<CollaboratorPermissions> Permission { get; protected set; }
        public User User { get; protected set; }

        internal string DebuggerDisplay => $"User: {User.Id} Permission: {Permission}";
    }
}
