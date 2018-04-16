using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes an update to an existing pre-receive environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UpdatePreReceiveEnvironment
    {
        /// <summary>
        /// The name of the environment as displayed in the UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// URL to the tarball that will be downloaded and extracted.
        /// </summary>
        public string ImageUrl { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} ImageUrl: {1}", Name, ImageUrl); }
        }
    }
}
