using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Feed
    {
        public string TimelineUrl { get; set; }
        public string UserUrl { get; set; }
        public string CurrentUserPublic { get; set; }
        public string CurrentUserUrl { get; set; }
        public string CurrentUserActorUrl { get; set; }
        public string CurrentUserOrganizationUrl { get; set; }
        public FeedLinks Links { get; set; }
    }
}