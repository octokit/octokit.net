using Octokit.Internal;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Octokit
{
    public enum PagesBuildStatus
    {
        /// <summary>
        ///  The site has yet to be built
        /// </summary>
        Null,

        /// <summary>
        /// The build is in progress
        /// </summary>
        Building,

        /// <summary>
        /// The site has been built
        /// </summary>
        Built,

        /// <summary>
        /// An error occurred during the build
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Errored")]
        Errored,

        /// <summary>
        /// Used as a placeholder for unknown fields
        /// </summary>
        Unknown
    }

    ///<summary>
    /// Information about your GitHub Pages configuration
    ///</summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Page
    {
        public Page() { }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "cname")]
        public Page(string url, string cname, bool custom404)
        {
            Url = url;
            CName = cname;
            Custom404 = custom404;
        }

        /// <summary>
        /// The pages's API URL.
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Absolute URL to the rendered site.
        /// </summary>
        public string HtmlUrl { get; protected set; }

        /// <summary>
        /// Build status of the pages site.
        /// </summary>
        [Parameter(Key = "IgnoreThisField")]
        public PagesBuildStatus? Status { get { return StatusText.ParseEnumWithDefault(PagesBuildStatus.Unknown); } }

        [Parameter(Key = "status")]
        public string StatusText { get; protected set; }

        /// <summary>
        /// CName of the pages site. Will be null if no CName was provided by the user.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "CName")]
        public string CName { get; protected set; }

        /// <summary>
        /// Is a custom 404 page provided.
        /// </summary>
        public bool Custom404 { get; protected set; }

        internal string DebuggerDisplay
        {
            get { return string.Format(CultureInfo.InvariantCulture, "CName: {0}, Status: {1}", CName, Status.ToString()); }
        }
    }
}
