using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Based on "#/components/schemas/repository-collaborator-permission:
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorPermissionResponse
    {
        public CollaboratorPermissionResponse() { }

        public CollaboratorPermissionResponse(string permission, string roleName, Collaborator collaborator)
        {
            Permission = permission;
            RoleName = roleName;
            Collaborator = collaborator;
        }

        public string Permission { get; private set; }

        public string RoleName { get; private set; }

        public Collaborator Collaborator { get; private set; }

        internal string DebuggerDisplay => $"Collaborator: {Collaborator.Id} Permission: {Permission} RoleName: {RoleName}";
    }
}
