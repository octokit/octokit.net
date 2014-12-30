using System.Diagnostics.CodeAnalysis;

namespace Octokit
{
    /// <summary>
    /// The possible repository content types.
    /// </summary>
    public enum ContentType
    {
        File,
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dir", Justification = "Matches the value returned by the API")]
        Dir,
        Symlink,
        Submodule
    }
}