using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;


namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class RepositoryOidcSubjectClaimRequest
    {
        public RepositoryOidcSubjectClaimRequest()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryOidcSubjectClaimRequest"/> class
        /// </summary>
        /// <param name="useDefault">Whether to use the default template or not. If true, the IncludeClaimKeys field is ignored.</param>
        /// <param name="includeClaimKeys">Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.</param>
        public RepositoryOidcSubjectClaimRequest(bool useDefault, List<string> includeClaimKeys)
        {
            this.UseDefault = useDefault;
            this.IncludeClaimKeys = includeClaimKeys;
        }


        /// <summary>
        /// Whether to use the default template or not. If true, the IncludeClaimKeys field is ignored.
        /// </summary>
        public bool UseDefault { get; set; }

        /// <summary>
        /// Array of unique strings. Each claim key can only contain alphanumeric characters and underscores.
        /// </summary>
        public List<string> IncludeClaimKeys { get; set; }


        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "RepositoryOidcSubjectClaimRequest: UseDefault: {0} IncludeClaimKeys {1}", this.UseDefault, this.IncludeClaimKeys);
            }
        }
    }
}
