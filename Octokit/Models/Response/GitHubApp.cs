using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a GitHub application.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubApp
    {
        /// <summary>
        /// Initialize a new empty GitHubApp object ready for deserialization.
        /// </summary>
        public GitHubApp() { }

        /// <summary>
        /// The GitHubApp Id. Should be the same as in the GitHubApp -> Settings -> About page.
        /// </summary>
        public long Id { get; protected set; }

        public string Name { get; protected set; }

        public User Owner { get; protected set; }

        public string Description { get; protected set; }

        public string ExternalUrl { get; protected set; }

        public string HtmlUrl { get; protected set; }

        public DateTimeOffset CreatedAt { get; protected set; }

        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name); }
        }
    }
}
