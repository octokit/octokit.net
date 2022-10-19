using System.Diagnostics.CodeAnalysis;
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
        ///  team members can pull, push and administer these repositories.
        /// </summary>
        [Parameter(Value = "admin")]
        Admin,
                
        /// <summary>
        ///  team members can manage the repository without access to sensitive or destructive actions. Recommended for project managers. Only applies to repositories owned by organizations.
        /// </summary>
        [Parameter(Value = "maintain")]
        Maintain,
        
        /// <summary>
        ///   team members can proactively manage issues and pull requests without write access. Recommended for contributors who triage a repository. Only applies to repositories owned by organizations.
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
}
