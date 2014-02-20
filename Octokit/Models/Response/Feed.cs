using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace Octokit
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Feed
    {
        public string TimelineUrl { get; set; }
        public string UserUrl { get; set; }
        public string CurrentUserPublicUrl { get; set; }
        public string CurrentUserUrl { get; set; }
        public string CurrentUserActorUrl { get; set; }
        public string CurrentUserOrganizationUrl { get; set; }

        // TODO: Note, the deserializer didn't work when this was named Links
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "links"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores")]
        public FeedLinks _links { get; set; }

        internal string DebuggerDisplay
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, "Public Url: {0} ", CurrentUserPublicUrl);
            }
        }
    }
}