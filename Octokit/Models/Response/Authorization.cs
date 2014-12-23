using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Represents an oauth access given to a particular application.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Authorization
    {
        /// <summary>
        /// The Id of this <see cref="Authorization"/>.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The API URL for this <see cref="Authorization"/>.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// The <see cref="Application"/> that created this <see cref="Authorization"/>.
        /// </summary>
        public Application Application { get; set; }

        /// <summary>
        /// The oauth token (be careful with these, they are like passwords!).
        /// </summary>
        [Obsolete("Now returns empty string, to be deprecated for security reasons")]
        public string Token { get; set; }

        /// <summary>
        /// The last eight characters of the user's token
        /// </summary>
        public string TokenLastEight { get; set; }

        /// <summary>
        /// Base-64 encoded representation of the SHA-256 digest of the token
        /// </summary>
        public string HashedToken { get; set; }

        /// <summary>
        /// Optional parameter that allows an OAuth application to create
        /// multiple authorizations for a single user
        /// </summary>
        public string Fingerprint { get; set; }

        /// <summary>
        /// Notes about this particular <see cref="Authorization"/>.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// A url for more information about notes.
        /// </summary>
        public string NoteUrl { get; set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; set; }

        /// <summary>
        /// The scopes that this <see cref="Authorization"/> has. This is the kind of access that the token allows.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Special type of model object that only updates none-null fields.")]
        public string[] Scopes { get; set; }

        public string ScopesDelimited
        {
            get { return string.Join(",", Scopes); }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1} ", Id, CreatedAt);
            }
        }
    }
}