using System.Diagnostics.CodeAnalysis;

namespace Nocto
{
    /// <summary>
    /// Marker interface for GitHub API Entities.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces",
        Justification = "Need a marker interface to attach extension methods to.")]
    public interface IGitHubEntity
    {
    }
}
