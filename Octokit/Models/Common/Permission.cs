using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

// Permission.cs file houses all permission-related enums / classes
// This file was created based on this suggestion https://github.com/octokit/octokit.net/issues/2323#issuecomment-1322766701

namespace Octokit
{
    public enum InstallationReadWritePermissionLevel
    {
        [Parameter(Value = "read")]
        Read,
        [Parameter(Value = "write")]
        Write
    }

    public enum InstallationReadWriteAdminPermissionLevel
    {
        [Parameter(Value = "read")]
        Read,
        [Parameter(Value = "write")]
        Write,
        [Parameter(Value = "admin")]
        Admin
    }

    public enum InstallationWritePermissionLevel
    {
        [Parameter(Value = "write")]
        Write
    }

    public enum InstallationReadPermissionLevel
    {
        [Parameter(Value = "read")]
        Read
    }

    /// <summary>
    /// The permission to grant the team on this repository(Legacy).
    /// </summary>
    public enum TeamPermissionLegacy
    {
        [Parameter(Value = "pull")]
        Pull,
        [Parameter(Value = "push")]
        Push,
        [Parameter(Value = "admin")]
        Admin
    }

    /// <summary>
    /// Deprecated. The permission that new repositories will be added to the team with when none is specified
    /// Default: pull
    /// Can be one of: pull, push
    /// </summary>
    public enum TeamPermission
    {
        /// <summary>
        /// team members can pull, but not push to these repositories
        /// </summary>
        [Parameter(Value = "pull")]
        Pull,

        /// <summary>
        /// team members can pull and push to these repositories
        /// </summary>
        [Parameter(Value = "push")]
        Push
    }

    /// <summary>
    /// The permission associated with the invitation for a collaborator in a repository
    /// </summary>
    public enum InvitationPermissionType
    {
        [Parameter(Value = "read")]
        Read,
        [Parameter(Value = "write")]
        Write,
        [Parameter(Value = "admin")]
        Admin,
        [Parameter(Value = "triage")]
        Triage,
        [Parameter(Value = "maintain")]
        Maintain
    }

    public enum CollaboratorPermission
    {
        [Parameter(Value = "pull")]
        Pull,
        [Parameter(Value = "triage")]
        Triage,
        [Parameter(Value = "push")]
        Push,
        [Parameter(Value = "maintain")]
        Maintain,
        [Parameter(Value = "admin")]
        Admin
    }

    /// <summary>
    /// Object for team repository permissions
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TeamRepositoryPermissions
    {
        public TeamRepositoryPermissions() { }
        public TeamRepositoryPermissions(bool pull, bool triage, bool push, bool maintain, bool admin)
        {
            Pull = pull;
            Triage = triage;
            Push = push;
            Maintain = maintain;
            Admin = admin;
        }

        /// <summary>
        /// Can read and clone repository.
        /// Can also open and comment on issues and pull requests.
        /// Required
        /// </summary>
        public bool Pull { get; private set; }

        /// <summary>
        /// Can read and clone repository.
        /// Can also manage issues and pull requests.
        /// Required
        /// </summary>
        public bool Triage { get; private set; }

        /// <summary>
        /// Can read, clone, and push to repository.
        /// Can also manage issues and pull requests.
        /// Required
        /// </summary>
        public bool Push { get; private set; }

        /// <summary>
        /// Can read, clone, and push to repository.
        /// They can also manage issues, pull requests, and some repository settings.
        /// Required
        /// </summary>
        public bool Maintain { get; private set; }

        /// <summary>
        /// Can read, clone, and push to repository.
        /// Can also manage issues, pull requests, and repository settings, including adding collaborators.
        /// Required
        /// </summary>
        public bool Admin { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
                    $"Permissions: Pull: {Pull}, Triage: {Triage}, Push: {Push}, Maintain: {Maintain}, Admin: {Admin}");
    }

    /// <summary>
    /// Object for collaborator permissions
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class CollaboratorPermissions
    {
        public CollaboratorPermissions() { }
        public CollaboratorPermissions(bool pull, bool? triage, bool push, bool? maintain, bool admin)
        {
            Pull = pull;
            Triage = triage;
            Push = push;
            Maintain = maintain;
            Admin = admin;
        }

        public bool Pull { get; private set; }

        public bool? Triage { get; private set; }

        public bool Push { get; private set; }

        public bool? Maintain { get; private set; }

        public bool Admin { get; private set; }

        internal string DebuggerDisplay => string.Format(CultureInfo.InvariantCulture,
                    $"Permissions: Pull: {Pull}, Triage: {Triage}, Push: {Push}, Maintain: {Maintain}, Admin: {Admin}");
    }
}
