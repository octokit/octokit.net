using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Base class for a GitHub account, most often either a <see cref="User"/> or <see cref="Organization"/>.
    /// </summary>
    public abstract class Account
    {
        /// <summary>
        /// URL of the account's avatar.
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// The account's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// URL of the account's blog.
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// Number of collaborators the account has.
        /// </summary>
        public int Collaborators { get; set; }

        /// <summary>
        /// Company the account works for.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Date the account was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Amount of disk space the account is using.
        /// </summary>
        public int DiskUsage { get; set; }

        /// <summary>
        /// The account's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Number of follwers the account has.
        /// </summary>
        public int Followers { get; set; }

        /// <summary>
        /// Number of other users the account is following.
        /// </summary>
        public int Following { get; set; }

        /// <summary>
        /// Indicates whether the account is currently hireable.
        /// </summary>
        /// <value>True if the account is hirable; otherwise, false.</value>
        public bool Hireable { get; set; }

        /// <summary>
        /// The HTML URL for the account on github.com (or GitHub Enterprise).
        /// </summary>
        public string HtmlUrl { get; set; }

        /// <summary>
        /// The account's system-wide unique ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The account's geographic location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// The account's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// The account's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of private repos owned by the account.
        /// </summary>
        public int OwnedPrivateRepos { get; set; }

        /// <summary>
        /// Plan the account pays for.
        /// </summary>
        public Plan Plan { get; set; }

        /// <summary>
        /// Number of private gists the account has created.
        /// </summary>
        public int PrivateGists { get; set; }

        /// <summary>
        /// Number of public gists the account has created.
        /// </summary>
        public int PublicGists { get; set; }

        /// <summary>
        /// Number of public repos the account owns.
        /// </summary>
        public int PublicRepos { get; set; }

        /// <summary>
        /// Total number of private repos the account owns.
        /// </summary>
        public int TotalPrivateRepos { get; set; }

        /// <summary>
        /// The account's API URL.
        /// </summary>
        public string Url { get; set; }
    }
}