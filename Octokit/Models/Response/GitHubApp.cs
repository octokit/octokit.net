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
        public GitHubApp() { }

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
