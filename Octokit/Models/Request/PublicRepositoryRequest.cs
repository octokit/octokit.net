using System;
using System.Diagnostics;
using System.Globalization;

namespace Octokit
{
    /// <summary>
    /// Used as part of the request to retrieve all public repositories.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class PublicRepositoryRequest : RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRepositoryRequest"/> class.
        /// </summary>
        /// <param name="since">The integer Id of the last Repository that you’ve seen.</param>
        [Obsolete("Please use the alternative constructor taking a long, rather than int, typed parameter.  This constructor will be removed in a future release.")]
        public PublicRepositoryRequest(int since)
        {
            Ensure.ArgumentNotNull(since, "since");

            Since = since;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PublicRepositoryRequest"/> class.
        /// </summary>
        /// <param name="since">The Id of the last Repository that you’ve seen.</param>
        public PublicRepositoryRequest(long since)
        {
            Since = since;
        }

        /// <summary>
        /// Gets or sets the integer Id of the last Repository that you’ve seen.
        /// </summary>
        /// <value>
        /// The since.
        /// </value>
        public long Since { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Since: {0} ", Since);
            }
        }
    }
}
