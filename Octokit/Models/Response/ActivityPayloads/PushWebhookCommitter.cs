using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents the author or committer to a Git commit. This is the information stored in Git and should not be
    /// confused with GitHub account information.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PushWebhookCommitter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PushWebhookCommitter"/> class.
        /// </summary>
        public PushWebhookCommitter() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PushWebhookCommitter"/> class.
        /// </summary>
        /// <param name="name">The full name of the author or committer.</param>
        /// <param name="email">The email.</param>
        /// <param name="username">The username associated with the account.</param>
        public PushWebhookCommitter(string name, string email, string username)
        {
            Name = name;
            Email = email;
            Username = username;
        }

        /// <summary>
        /// Gets the name of the author or committer.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the email of the author or committer.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the GitHub username associated with the commit
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username { get; private set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Email: {1} Username: {2}", Name, Email, Username); }
        }
    }
}
