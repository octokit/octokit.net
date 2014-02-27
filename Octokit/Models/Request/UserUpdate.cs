using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents updatable fields on a user. Values that are null will not be sent in the request.
    /// Use string.empty if you want to clear clear a value.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class UserUpdate
    {
        /// <summary>
        /// This user's bio.
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// URL for this user's blog.
        /// </summary>
        public string Blog { get; set; }

        /// <summary>
        /// The company this user's works for.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// This user's email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The geographic location of this user.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// This user's full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tells if this user is currently hireable.
        /// </summary>
        public bool? Hireable { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Name: {0}", Name);
            }
        }
    }
}