using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Describes an update to an existing pre-receive environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdatePreReceiveEnvironment : NewPreReceiveEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewPreReceiveEnvironment"/> class.
        /// </summary>
        /// <param name="name">The name of the environment as displayed in the UI.</param>
        /// <param name="imageUrl">URL to the tarball that will be downloaded and extracted.</param>
        public UpdatePreReceiveEnvironment(string name, string imageUrl)
            : base(name, imageUrl)
        { }
    }
}