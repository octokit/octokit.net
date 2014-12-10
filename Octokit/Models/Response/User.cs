using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents a user on GitHub.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class User : Account
    {
        /// <summary>
        /// Hex Gravatar identifier, now obsolete
        /// </summary>
        /// <remarks>
        /// For more details: https://developer.github.com/changes/2014-09-05-removing-gravatar-id/
        /// </remarks>
        [Obsolete("This property is now obsolete, use AvatarUrl instead")]
        public string GravatarId { get; set; }

        /// <summary>
        /// Whether or not the user is an administrator of the site
        /// </summary>
        public bool SiteAdmin { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture,
                    "User: Id: {0} Login: {1}", Id, Login);
            }
        }
    }
}