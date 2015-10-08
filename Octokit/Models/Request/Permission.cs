using System.Diagnostics.CodeAnalysis;

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
        Admin,

        /// <summary>
        /// team members can pull and push, but not administer these repositories
        /// </summary>
        Push,

        /// <summary>
        /// team members can pull, but not push to or administer these repositories
        /// </summary>
        Pull
    }
}