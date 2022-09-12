using System.Diagnostics;
using System.Globalization;
using Octokit.Internal;

namespace Octokit
{
    /// <summary>
    /// Lists all the feeds available to the authenticating user:
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Feed
    {
        public Feed() { }

        public Feed(string timelineUrl, string userUrl, string currentUserPublicUrl, string currentUserUrl, string currentUserActorUrl, string currentUserOrganizationUrl, FeedLinks links)
        {
            TimelineUrl = timelineUrl;
            UserUrl = userUrl;
            CurrentUserPublicUrl = currentUserPublicUrl;
            CurrentUserUrl = currentUserUrl;
            CurrentUserActorUrl = currentUserActorUrl;
            CurrentUserOrganizationUrl = currentUserOrganizationUrl;
            Links = links;
        }

        /// <summary>
        /// The GitHub global public timeline
        /// </summary>
        public string TimelineUrl { get; private set; }

        /// <summary>
        /// The public timeline for any user, using URI template
        /// </summary>
        public string UserUrl { get; private set; }

        /// <summary>
        /// The public timeline for the authenticated user
        /// </summary>
        public string CurrentUserPublicUrl { get; private set; }

        /// <summary>
        /// The private timeline for the authenticated user
        /// </summary>
        public string CurrentUserUrl { get; private set; }

        /// <summary>
        /// The private timeline for activity created by the authenticated user
        /// </summary>
        public string CurrentUserActorUrl { get; private set; }

        /// <summary>
        /// The private timeline for the authenticated user for a given organization, using URI template
        /// </summary>
        public string CurrentUserOrganizationUrl { get; private set; }

        /// <summary>
        /// List of feed urls including feed url and feed type, e.g. application/atom+xml
        /// </summary>
        [Parameter(Key = "_links")]
        public FeedLinks Links { get; private set; }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "Public Url: {0} ", CurrentUserPublicUrl);
            }
        }
    }
}
