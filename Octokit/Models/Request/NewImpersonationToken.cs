using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Describes a new Impersonation Token to create via the <see cref="IUserAdministrationClient.CreateImpersonationToken(string, NewImpersonationToken)"/> method.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class NewImpersonationToken
    {
        public NewImpersonationToken() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NewImpersonationToken"/> class.
        /// </summary>
        /// <param name="scopes">The scopes for the token.</param>
        public NewImpersonationToken(IEnumerable<string> scopes)
        {
            Scopes = scopes;
        }

        /// <summary>
        /// The scopes for the token
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Scopes: {0}", string.Join("\r\n", Scopes));
            }
        }
    }
}
