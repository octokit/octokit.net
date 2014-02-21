using System;
using System.Diagnostics;

namespace Octokit
{
    /// <summary>
    /// Collection of feeds including both url and type
    /// </summary>
    public class FeedLinks
    {
        /// <summary>
        /// The GitHub global public timeline
        /// </summary>
        public FeedLink Timeline { get; set; }

        /// <summary>
        /// The public timeline for any user, using URI template
        /// </summary>
        public FeedLink User { get; set; }

        /// <summary>
        /// The public timeline for the authenticated user
        /// </summary>
        public FeedLink CurrentUserPublic { get; set; }

        /// <summary>
        /// The private timeline for the authenticated user
        /// </summary>
        public FeedLink CurrentUser { get; set; }

        /// <summary>
        /// The private timeline for activity created by the authenticated user
        /// </summary>
        public FeedLink CurrentUserActor { get; set; }

        /// <summary>
        /// The private timeline for the authenticated user for a given organization, using URI template
        /// </summary>  
        public FeedLink CurrentUserOrganization { get; set; }
    }

    /// <summary>
    /// Feed information including feed url and feed type
    /// </summary>
    public class FeedLink
    {
        /// <summary>
        /// Link to feed
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Feed type, e.g. application/atom+xml
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }
    }
}