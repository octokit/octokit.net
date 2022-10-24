using Octokit.Internal;

namespace Octokit
{
    public enum PackageType
    {
        /// <summary>
        /// Npm repository packages
        /// </summary>
        [Parameter(Value = "npm")]
        Npm,

        /// <summary>
        /// Gradle registry packages
        /// </summary>
        [Parameter(Value = "maven")]
        Maven,

        /// <summary>
        /// RubyGems packages
        /// </summary>
        [Parameter(Value = "rubygems")]
        RubyGems,

        /// <summary>
        /// Docker container registry packages
        /// </summary>
        [Parameter(Value = "docker")]
        Docker,

        /// <summary>
        /// Nuget registry packages
        /// </summary>
        [Parameter(Value = "nuget")]
        Nuget,

        /// <summary>
        /// Container registry packages
        /// </summary>
        [Parameter(Value = "container")]
        Container,
    }
}
