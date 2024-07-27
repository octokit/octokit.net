using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents a GitHub application from a manifest.
    /// https://docs.github.com/rest/apps/apps#create-a-github-app-from-a-manifest
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitHubAppFromManifest : GitHubApp
    {
        public GitHubAppFromManifest() { }

        public GitHubAppFromManifest(long id, string slug, string nodeId, string name, User owner, string description, string externalUrl, string htmlUrl, DateTimeOffset createdAt, DateTimeOffset updatedAt, InstallationPermissions permissions, IReadOnlyList<string> events, string clientId, string clientSecret, string webhookSecret, string pem)
            : base(id, slug, nodeId, name, owner, description, externalUrl, htmlUrl, createdAt, updatedAt, permissions, events)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            WebhookSecret = webhookSecret;
            Pem = pem;
        }

        /// <summary>
        /// The Client Id of the GitHub App.
        /// </summary>
        public string ClientId { get; private set; }

        /// <summary>
        /// The Client Secret of the GitHub App.
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// The Webhook Secret of the GitHub App.
        /// </summary>
        public string WebhookSecret { get; private set; }

        /// <summary>
        /// The PEM of the GitHub App.
        /// </summary>
        public string Pem { get; private set; }
    }
}
