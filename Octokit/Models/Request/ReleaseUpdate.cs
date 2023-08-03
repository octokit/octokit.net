using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to update a release.
    /// </summary>
    /// <remarks>
    /// API: https://docs.github.com/rest/releases/releases#update-a-release
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ReleaseUpdate
    {
        /// <summary>
        /// Gets the name of the tag.
        /// </summary>
        /// <value>
        /// The name of the tag.
        /// </value>
        public string TagName { get; set; }

        /// <summary>
        /// Specifies the commitish value that determines where the Git tag is created from. Can be any branch or
        /// commit SHA. Unused if the Git tag already exists. Default: the repository’s default branch
        /// (usually main).
        /// </summary>
        /// <value>
        /// The target commitish.
        /// </value>
        public string TargetCommitish { get; set; }

        /// <summary>
        /// Gets or sets the name of the release.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the text describing the contents of the tag.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReleaseUpdate"/> is a draft (unpublished).
        /// Default: false
        /// </summary>
        /// <value>
        ///   <c>true</c> if draft; otherwise, <c>false</c>.
        /// </value>
        public bool? Draft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ReleaseUpdate"/> is prerelease.
        /// </summary>
        /// <value>
        ///   <c>true</c> if prerelease; otherwise, <c>false</c>.
        /// </value>
        public bool? Prerelease { get; set; }

        /// <summary>
        /// Specifies whether this release should be set as the latest release for the repository.
        /// Drafts and prereleases cannot be set as latest. Defaults to true for newly published releases.
        /// </summary>
        /// <value>
        ///   <c>True</c> set release as latest;
        ///   <c>Legacy</c> specifies that the latest release should be determined based on the release creation date and higher semantic version.
        /// </value>
        public MakeLatestQualifier? MakeLatest { get; set; }
        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} TagName: {1}", Name, TagName);
            }
        }
    }
}