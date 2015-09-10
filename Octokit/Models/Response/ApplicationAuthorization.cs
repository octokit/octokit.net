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

        public ApplicationAuthorization(string token)
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