using System;
using System.Diagnostics;

namespace Octokit
{
    public class FeedLinks
    {
        public FeedLink Timeline { get; set; }
        public FeedLink User { get; set; }
        public FeedLink CurrentUserPublic { get; set; }
        public FeedLink CurrentUser { get; set; }
        public FeedLink CurrentUserActor { get; set; }
        public FeedLink CurrentUserOrganization { get; set; }
    }

    public class FeedLink
    {
        public string Href { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public string Type { get; set; }
    }
}