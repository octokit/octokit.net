using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// Used to create a new authorization.
    /// </summary>
    public class NewAuthorization
    {
        /// <summary>
        /// Replaces the authorization scopes with this list.
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }

        /// <summary>
        /// An optional note to remind you what the OAuth token is for.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        // An optional URL to remind you what app the OAuth token is for.
        /// </summary>
        public string NoteUrl { get; set; }
    }
}