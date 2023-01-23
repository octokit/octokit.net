using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Used to describe a permission level.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public enum Permission
    {
        /// <summary>
        /// team members can pull, push and administer these repositories.
        /// </summary>
        [Parameter(Value = "admin")]
        Admin,
                
        /// <summary>
        /// team members can manage the repository without access to sensitive or destructive actions. Recommended for project managers. Only applies to repositories owned by organizations.
        /// </summary>
        [Parameter(Value = "maintain")]
        Maintain,
        
        /// <summary>
        /// team members can proactively manage issues and pull requests without write access. Recommended for contributors who triage a repository. Only applies to repositories owned by organizations.
        /// </summary>
        [Parameter(Value = "triage")]
        Triage,
        
        /// <summary>
        /// team members can pull and push, but not administer these repositories
        /// </summary>
        [Parameter(Value = "push")]
        Push,

        /// <summary>
        /// team members can pull, but not push to or administer these repositories
        /// </summary>
        [Parameter(Value = "pull")]
        Pull
    }

    /// <summary>
    /// Deprecated. The permission that new repositories will be added to the team with when none is specified
    /// Default: pull
    /// Can be one of: pull, push
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
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

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    $"Permissions: Pull: {Pull}, Triage: {Triage}, Push: {Push}, Maintain: {Maintain}, Admin: {Admin}");
            }
        }
    }
}
