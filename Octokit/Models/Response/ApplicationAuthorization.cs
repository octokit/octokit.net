using System;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Represents an oauth access given to a particular application.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class ApplicationAuthorization : Authorization
    {
        // TODO: I'd love to not need this
        public ApplicationAuthorization()
        {
        }

        public ApplicationAuthorization(int id, string url, Application application, string tokenLastEight, string hashedToken, string fingerprint, string note, string noteUrl, DateTimeOffset createdAt, DateTimeOffset updateAt, string[] scopes, string token)
            : base(id, url, application, tokenLastEight, hashedToken, fingerprint, note, noteUrl, createdAt, updateAt, scopes)
        {
            Token = token;
        }

        /// <summary>
        /// The oauth token (be careful with these, they are like passwords!).
        /// </summary>
        /// <remarks>
        /// This will return only return a value the first time
        /// the authorization is created. All subsequent API calls
        /// (for example, 'get' for an authorization) will return `null`
        /// </remarks>
        public string Token { get; private set; }
    }
}