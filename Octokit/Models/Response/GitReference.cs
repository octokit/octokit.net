using System;
using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class GitReference
    {
        public GitReference() { }

        public GitReference(string url, string label, string @ref, string sha, User user, Repository repository)
        {
            Url = url;
            Label = label;
            Ref = @ref;
            Sha = sha;
            User = user;
            Repository = repository;
        }

        /// <summary>
        /// The URL associated with this reference.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The reference label.
        /// </summary>
        public string Label { get; protected set; }

        /// <summary>
        /// The reference identifier.
        /// </summary>
        public string Ref { get; protected set; }

        /// <summary>
        /// The sha value of the reference.
        /// </summary>
        public string Sha { get; protected set; }

        /// <summary>
        /// The user associated with this reference.
        /// </summary>
        public User User { get; protected set; }

        /// <summary>
        /// The repository associated with this reference.
        /// </summary>
        [Parameter(Key = "repo")]
        public Repository Repository { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Sha: {0}", Sha);
            }
        }
    }
}