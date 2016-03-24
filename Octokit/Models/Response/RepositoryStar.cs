using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents additional information about a star (such as creation time)
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryStar
    {
        public RepositoryStar() { }

        public RepositoryStar(DateTimeOffset starredAt, Repository repo)
        {
            StarredAt = starredAt;
            Repo = repo;
        }

        /// <summary>
        /// The date the star was created.
        /// </summary>
        public DateTimeOffset StarredAt { get; protected set; }

        /// <summary>
        /// The repository associated with the star.
        /// </summary>
        public Repository Repo { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, Repo.DebuggerDisplay);
            }
        }
    }
}