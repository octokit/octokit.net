using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a new release.
    /// </summary>
    /// <remarks>
    /// API: https://developer.github.com/v3/repos/releases/#create-a-release
    /// </remarks>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewRelease
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewRelease"/> class.
        /// </summary>
        /// <param name="tagName">Name of the tag to create in the repository for this release.</param>
        public NewRelease(string tagName)
        {
            Ensure.ArgumentNotNullOrEmptyString(tagName, "tagName");
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
        /// Specifies the commitish value that determines where the Git tag is created from. Can be any branch or
        /// commit SHA. Unused if the Git tag already exists. Default: the repository’s default branch
        /// (usually master).
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
        /// Gets or sets a value indicating whether this <see cref="NewRelease"/> is a draft (unpublished).
        /// Default: false
        /// </summary>
        /// <value>
        ///   <c>true</c> if draft; otherwise, <c>false</c>.
        /// </value>
        public bool Draft { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NewRelease"/> is prerelease.
        /// </summary>
        /// <value>
        ///   <c>true</c> if prerelease; otherwise, <c>false</c>.
        /// </value>
        public bool Prerelease { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Name: {0} TagName: {1}", Name, TagName);
            }
        }
    }
}