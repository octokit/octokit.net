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
    public class Committer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Committer"/> class.
        /// </summary>
        public Committer() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Committer"/> class.
        /// </summary>
        /// <param name="name">The full name of the author or committer.</param>
        /// <param name="email">The email.</param>
        /// <param name="date">The date.</param>
        public Committer(string name, string email, DateTimeOffset date)
        {
            Name = name;
            Email = email;
            Date = date;
        }

        /// <summary>
        /// Gets the name of the author or committer.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the email of the author or committer.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; protected set; }

        /// <summary>
        /// Gets the date of the author or contributor's contributions.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTimeOffset Date { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Name: {0} Email: {1} Date: {2}", Name, Email, Date); }
        }
    }
}
