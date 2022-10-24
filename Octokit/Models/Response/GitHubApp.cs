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

        public GitHubApp(long id, string slug, string name, User owner, string description, string externalUrl, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt)
        {
            Id = id;
            Slug = slug;
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
        public long Id { get; private set; }

        /// <summary>
        /// The url-friendly version of the GitHub App name.
        /// </summary>
        public string Slug { get; private set; }

        /// <summary>
        /// The Name of the GitHub App.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The Owner of the GitHub App.
        /// </summary>
        public User Owner { get; private set; }

        /// <summary>
        /// The Description of the GitHub App.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The URL to the GitHub App's external website.
        /// </summary>
        public string ExternalUrl { get; private set; }

        /// <summary>
        /// The URL to view the GitHub App on GitHub.
        /// </summary>
        public string HtmlUrl { get; private set; }

        /// <summary>
        /// Date the GitHub App was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Date the GitHub App was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name); }
        }
    }
}
