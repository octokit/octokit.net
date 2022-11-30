using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Response from the /meta endpoint that provides information about GitHub.com or a GitHub Enterprise instance.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Meta
    {
        /// <summary>
        /// Create an instance of the Meta
        /// </summary>
        public Meta()
        {
        }

        /// <summary>
        /// Create an instance of the Meta
        /// </summary>
        /// <param name="verifiablePasswordAuthentication">Whether authentication with username and password is supported.</param>
        /// <param name="gitHubServicesSha">The currently-deployed SHA of github-services.</param>
        /// <param name="hooks">An array of IP addresses in CIDR format specifying the addresses that incoming service hooks will originate from on GitHub.com.</param>
        /// <param name="web">An array of IP addresses in CIDR format specifying the Web servers for GitHub</param>
        /// <param name="api">An array of IP addresses in CIDR format specifying the Api servers for GitHub</param>
        /// <param name="git">An array of IP addresses in CIDR format specifying the Git servers for the GitHub server</param>
        /// <param name="packages">An array of IP addresses in CIDR format specifying the Packages servers for GitHub</param>
        /// <param name="pages">An array of IP addresses in CIDR format specifying the A records for GitHub Pages.</param>
        /// <param name="importer">An Array of IP addresses specifying the addresses that source imports will originate from on GitHub.com.</param>
        /// <param name="actions">An array of IP addresses in CIDR format specifying the Actions servers for GitHub</param>
        /// <param name="dependabot">An array of IP addresses in CIDR format specifying the Dependabot servers for GitHub</param>
        /// <param name="installedVersion">The installed version of GitHub Enterprise Server</param>
        public Meta(
            bool verifiablePasswordAuthentication,
            string gitHubServicesSha,
            IReadOnlyList<string> hooks,
            IReadOnlyList<string> web,
            IReadOnlyList<string> api,
            IReadOnlyList<string> git,
            IReadOnlyList<string> packages,
            IReadOnlyList<string> pages,
            IReadOnlyList<string> importer,
            IReadOnlyList<string> actions,
            IReadOnlyList<string> dependabot,
            string installedVersion)
        {
            VerifiablePasswordAuthentication = verifiablePasswordAuthentication;
#pragma warning disable CS0618 // Type or member is obsolete
            GitHubServicesSha = gitHubServicesSha;
#pragma warning restore CS0618 // Type or member is obsolete
            Hooks = hooks;
            Web = web;
            Api = api;
            Git = git;
            Packages = packages;
            Pages = pages;
            Importer = importer;
            Actions = actions;
            Dependabot = dependabot;
            InstalledVersion = installedVersion;
        }

        /// <summary>
        /// Whether authentication with username and password is supported. (GitHub Enterprise instances using CAS or
        /// OAuth for authentication will return false. Features like Basic Authentication with a username and
        ///  password, sudo mode, and two-factor authentication are not supported on these servers.)
        /// </summary>
        public bool VerifiablePasswordAuthentication { get; private set; }

        /// <summary>
        /// The currently-deployed SHA of github-services.
        /// </summary>
        [Parameter(Key = "github_services_sha")]
        [Obsolete("No longer returned so always null")]
        public string GitHubServicesSha { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the addresses that incoming service hooks will
        /// originate from on GitHub.com. Subscribe to the API Changes blog or follow @GitHubAPI on Twitter to get
        /// updated when this list changes.
        /// </summary>
        public IReadOnlyList<string> Hooks { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Web servers for GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Web { get; private set; }


        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Api servers for GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Api { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Git servers for GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Git { get; private set; }


        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Packages servers for GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Packages { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the A records for GitHub Pages.
        /// </summary>
        public IReadOnlyList<string> Pages { get; private set; }

        /// <summary>
        /// An Array of IP addresses specifying the addresses that source imports will originate from on GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Importer { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Actions servers.
        /// </summary>
        public IReadOnlyList<string> Actions { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Dependabot servers.
        /// </summary>
        public IReadOnlyList<string> Dependabot { get; private set; }

        /// <summary>
        /// The installed version of GitHub Enterprise Server.
        /// </summary>
        public string InstalledVersion { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(
                    CultureInfo.InvariantCulture,
                    "GitHubServicesSha: {0}, VerifiablePasswordAuthentication: {1} ",
                    GitHubServicesSha,
                    VerifiablePasswordAuthentication);
            }
        }
    }
}
