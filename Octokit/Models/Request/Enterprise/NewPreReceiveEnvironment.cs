using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new pre-receive environment.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewPreReceiveEnvironment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewPreReceiveEnvironment"/> class.
        /// </summary>
        /// <param name="name">The name of the environment as displayed in the UI.</param>
        /// <param name="imageUrl">URL to the tarball that will be downloaded and extracted.</param>
        public NewPreReceiveEnvironment(string name, string imageUrl)
        {
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(imageUrl, nameof(imageUrl));

            Name = name;
            ImageUrl = imageUrl;
        }

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
