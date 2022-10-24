using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to generate release notes for a given tag.
    /// </summary>
    /// <remarks>
    /// API: https://docs.github.com/rest/releases/releases#generate-release-notes-content-for-a-release
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GenerateReleaseNotesRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateReleaseNotesRequest"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag to create in the repository for this release.</param>
        public GenerateReleaseNotesRequest(string tagName)
        {
            Ensure.ArgumentNotNullOrEmptyString(tagName, nameof(tagName));
            TagName = tagName;
        }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; private set; }

        /// <summary>
        /// The name of the previous tag to use as the starting point for the release notes.
        /// Use to manually specify the range for the set of changes considered as part this release.
        /// </summary>
        /// <value>
        /// The target commitish.
        /// </value>
        public string TargetCommitish { get; set; }

        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the previous tag.
        /// </value>
        public string PreviousTagName { get; set; }

        /// <summary>
        /// Specifies a path to a file in the repository containing configuration settings used for generating the
        /// release notes. If unspecified, the configuration file located in the repository at '.github/release.yml' or
        /// '.github/release.yaml' will be used. If that is not present, the default configuration will be used.
        /// </summary>
        /// <value>
        /// The path to the configuration file.
        /// </value>
        public string ConfigurationFilePath { get; set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "TagName: {0} PreviousTagName: {1}", TagName, PreviousTagName); }
        }

    }
}
