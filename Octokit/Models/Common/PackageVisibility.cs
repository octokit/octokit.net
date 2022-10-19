using Octokit.Internal;

namespace Octokit
{
    public enum PackageVisibility
    {
        /// <summary>
        /// Only public packages
        /// </summary>
        [Parameter(Value = "public")]
        Public,

        /// <summary>
        /// Only private packages
        /// </summary>
        [Parameter(Value = "private")]
        Private,

        /// <summary>
        /// Only supported by container package types, otherwise the same as private
        /// </summary>
        [Parameter(Value = "internal")]
        Internal
    }
}
