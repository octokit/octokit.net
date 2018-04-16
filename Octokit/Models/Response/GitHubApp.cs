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

        public GitHubApp(long id, string name, User owner, string description, string externalUrl, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Description = description;
            ExternalUrl = externalUrl;
            HtmlUrl = htmlUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        /// <summary>
        /// The Id of the GitHub App.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The Name of the GitHub App.
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The Owner of the GitHub App.
        /// </summary>
        public User Owner { get; protected set; }

        /// <summary>
        /// The Description of the GitHub App.
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// The URL to the GitHub App's external website.
        /// </summary>
        public string ExternalUrl { get; protected set; }

        /// <summary>
        /// The URL to view the GitHub App on GitHub
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// Date the GitHub App was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// Date the GitHub App was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name); }
        }
    }
}
