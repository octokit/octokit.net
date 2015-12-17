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
        // TODO: I'd love to not need this
        public Authorization() { }

        public Authorization(int id, string url, Application application, string note, string noteUrl, DateTimeOffset createdAt, DateTimeOffset updateAt, string[] scopes)
        {
            Id = id;
            Url = url;
            Application = application;

            // TODO: testable ctor for new values
            //Token = token;

            Note = note;
            NoteUrl = noteUrl;
            CreatedAt = createdAt;
            UpdateAt = updateAt;
            Scopes = scopes;
        }

        /// <summary>
        /// The Id of this <see cref="Authorization"/>.
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// The API URL for this <see cref="Authorization"/>.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// The <see cref="Application"/> that created this <see cref="Authorization"/>.
        /// </summary>
        public Application Application { get; protected set; }

        /// <summary>
        /// The last eight characters of the user's token
        /// </summary>
        public string TokenLastEight { get; protected set; }

        /// <summary>
        /// Base-64 encoded representation of the SHA-256 digest of the token
        /// </summary>
        public string HashedToken { get; protected set; }

        /// <summary>
        /// Optional parameter that allows an OAuth application to create
        /// multiple authorizations for a single user
        /// </summary>
        public string Fingerprint { get; protected set; }

        /// <summary>
        /// Notes about this particular <see cref="Authorization"/>.
        /// </summary>
        public string Note { get; protected set; }

        /// <summary>
        /// A url for more information about notes.
        /// </summary>
        public string NoteUrl { get; protected set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; protected set; }

        /// <summary>
        /// When this <see cref="Authorization"/> was last updated.
        /// </summary>
        public DateTimeOffset UpdateAt { get; protected set; }

        /// <summary>
        /// The scopes that this <see cref="Authorization"/> has. This is the kind of access that the token allows.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Special type of model object that only updates none-null fields.")]
        public string[] Scopes { get; protected set; }

        public string ScopesDelimited
        {
            get { return string.Join(",", Scopes); }
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Id: {0} CreatedAt: {1} ", Id, CreatedAt);
            }
        }
    }
}