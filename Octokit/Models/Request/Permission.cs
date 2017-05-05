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