using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class IssuesEventsClient : ApiClient, IIssuesEventsClient
    {
        public IssuesEventsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public Task<IReadOnlyList<EventInfo>> GetForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<EventInfo>(ApiUrls.IssuesEvents(owner, name, number));
        }

        public Task<IReadOnlyList<IssueEvent>> GetForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name));
        }

        public Task<IssueEvent> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(owner, name, number));
        }
    }
}