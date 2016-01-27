using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

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
        /// <param name="git">An array of IP addresses in CIDR format specifying the Git servers for the GitHub server</param>
        /// <param name="pages">An array of IP addresses in CIDR format specifying the A records for GitHub Pages.</param>
        public Meta(
            bool verifiablePasswordAuthentication,
            string gitHubServicesSha,
            IReadOnlyList<string> hooks,
            IReadOnlyList<string> git,
            IReadOnlyList<string> pages)
        {
            VerifiablePasswordAuthentication = verifiablePasswordAuthentication;
            GitHubServicesSha = gitHubServicesSha;
            Hooks = hooks;
            Git = git;
            Pages = pages;
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
        public string GitHubServicesSha { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the addresses that incoming service hooks will
        /// originate from on GitHub.com. Subscribe to the API Changes blog or follow @GitHubAPI on Twitter to get
        /// updated when this list changes.
        /// </summary>
        public IReadOnlyList<string> Hooks { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the Git servers for GitHub.com.
        /// </summary>
        public IReadOnlyList<string> Git { get; private set; }

        /// <summary>
        /// An Array of IP addresses in CIDR format specifying the A records for GitHub Pages.
        /// </summary>
        public IReadOnlyList<string> Pages { get; private set; }

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
