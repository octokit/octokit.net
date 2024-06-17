using Octokit.Internal;

namespace Octokit
{
    public enum Scope
    {
        /// <summary>
        /// The scope of the dependency is not specified or cannot be determined.
        /// </summary>
        [Parameter(Value = "Unknown")]
        Unknown,

        /// <summary>
        /// Dependency is utilized at runtime and in the development environment.
        /// </summary>
        [Parameter(Value = "Runtime")]
        Runtime,

        /// <summary>
        /// Dependency is only utilized in the development environment.
        /// </summary>
        [Parameter(Value = "Development")]
        Development,
    }
}
