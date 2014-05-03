using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used to create a new authorization.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class AuthorizationUpdate
    {
        /// <summary>
        /// Replaces the authorization scopes with this list.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }

        /// <summary>
        /// A list of scopes to add to this authorization.
        /// </summary>
        public IEnumerable<string> AddScopes { get; set; }

        /// <summary>
        /// A list of scopes to remove from this authorization.
        /// </summary>
        public IEnumerable<string> RemoveScopes { get; set; }

        /// <summary>
        /// An optional note to remind you what the OAuth token is for.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        // An optional URL to remind you what app the OAuth token is for.
        /// </summary>
        public string NoteUrl { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                var scopes = Scopes ?? new List<string>();
                return String.Format(CultureInfo.InvariantCulture, "Scopes: {0} ", string.Join(",", scopes));
            }
        }
    }
}