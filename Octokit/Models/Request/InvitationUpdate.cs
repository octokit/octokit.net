using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Used to update a gist and its contents.
    /// </summary>
    /// <remarks>
    /// Note: All files from the previous version of the gist are carried over by default if not included in the
    ///  object. Deletes can be performed by including the filename with a null object.
    /// API docs: https://developer.github.com/v3/gists/
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class InvitationUpdate
    {

    }
}
