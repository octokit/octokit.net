using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IIssueTimelineClient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get")]
        Task<IReadOnlyList<TimelineEventInfo>> Get(string owner, string repo, int number);
    }
}
