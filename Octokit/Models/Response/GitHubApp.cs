using System;
using System.Collections.Generic;
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

        public GitHubApp(long id, string slug, string nodeId, string name, User owner, string description, string externalUrl, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, InstallationPermissions permissions, IReadOnlyList<string> events)
        {
            Id = id;
            Slug = slug;
            NodeId = nodeId;
            Name = name;
            Owner = owner;
            Description = description;
            ExternalUrl = externalUrl;
            HtmlUrl = htmlUrl;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Permissions = permissions;
            Events = events;
        }

        /// <summary>
        /// The Id of the GitHub App.
        /// </summary>
        public long Id { get; protected set; }

        /// <summary>
        /// The url-friendly version of the GitHub App name.
        /// </summary>
        public string Slug { get; protected set; }

        /// <summary>
        /// GraphQL Node Id
        /// </summary>
        public string NodeId { get; protected set; }

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
        /// The URL to view the GitHub App on GitHub.
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

        /// <summary>
        /// The Permissions granted to the Installation
        /// </summary>
        public InstallationPermissions Permissions { get; protected set; }

        /// <summary>
        /// The Events subscribed to by the Installation
        /// </summary>
        public IReadOnlyList<string> Events { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Id: {0} Name: {1}", Id, Name); }
        }
    }
}
