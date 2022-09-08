using Octokit.Internal;

namespace Octokit
{
    public enum PackageVersionState
    {
        /// <summary>
        /// Package version which is active
        /// </summary>
        [Parameter(Value = "active")]
        Active,
        
        /// <summary>
        /// Package version whic is deleted
        /// </summary>
        [Parameter(Value = "deleted")]
        Deleted
    }
}
