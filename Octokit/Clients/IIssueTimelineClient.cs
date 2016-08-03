#if NET_45
using System.Collections.Generic;
using System.Threading.Tasks;
#endif

namespace Octokit
{
    public interface IIssueTimelineClient
    {
        Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number);
    }
}
