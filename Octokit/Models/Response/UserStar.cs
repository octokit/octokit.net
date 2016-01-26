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
    public class UserStar
    {
        public UserStar() { }

        public UserStar(DateTimeOffset starredAt, User user)
        {
            StarredAt = starredAt;
            User = user;
        }

        /// <summary>
        /// The date the star was created.
        /// </summary>
        public DateTimeOffset StarredAt { get; protected set; }

        /// <summary>
        /// The user associated with the star.
        /// </summary>
        public User User { get; protected set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, User.DebuggerDisplay);
            }
        }
    }
}