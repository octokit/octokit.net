#if NET_45
using System.Collections.Generic;
using System.Threading.Tasks;
#endif

namespace Octokit
{
    public class IssueTimelineClient: ApiClient, IIssueTimelineClient
    {
        public IssueTimelineClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<TimelineEventInfo>> GetAllForIssue(string owner, string repo, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            return ApiConnection.GetAll<TimelineEventInfo>(ApiUrls.IssueTimeline(owner, repo, number), ApiOptions.None);
        }
    }
}
