using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's SubIssues API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://docs.github.com/en/rest/issues/sub-issues">Sub-Issues API documentation</a> for more information.
    /// </remarks>
    public class SubIssuesClient : ApiClient, ISubIssuesClient
    {
        /// <summary>
        /// Instantiates a new GitHub Sub-Issues API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public SubIssuesClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        [ManualRoute("GET", "/repos/{owner}/{repo}/issues/{issue_number}/parent")]
        public Task<Issue> GetParent(string owner, string name, long issueNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Get<Issue>(ApiUrls.IssueParent(owner, name, issueNumber), null);
        }

        public Task<Issue> Remove(string owner, string name, long issueNumber, long subIssueNumber)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Delete<Issue>(ApiUrls.IssueSubIssue(owner, name, issueNumber), new { SubIssueId = subIssueNumber });
        }

        public Task<IReadOnlyList<Issue>> List(string owner, string name, long issueNumber)
        {
            return List(owner, name, issueNumber, ApiOptions.None);
        }

        public Task<IReadOnlyList<Issue>> List(string owner, string name, long issueNumber, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return ApiConnection.GetAll<Issue>(ApiUrls.IssueSubIssues(owner, name, issueNumber), options);
        }

        public Task<Issue> Add(string owner, string name, long issueNumber, long subIssueNumber)
        {
            return Add(owner, name, issueNumber, subIssueNumber, replaceParent: false);
        }

        public Task<Issue> Add(string owner, string name, long issueNumber, long subIssueNumber, bool replaceParent)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<Issue>(ApiUrls.IssueSubIssues(owner, name, issueNumber), new
            {
                SubIssueId = subIssueNumber,
                ReplaceParent = replaceParent
            });
        }

        public Task<Issue> ReprioritizeAfter(string owner, string name, long issueNumber, long subIssueNumber, long afterId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<Issue>(ApiUrls.IssueSubIssuePriority(owner, name, issueNumber), new
            {
                SubIssueId = subIssueNumber,
                AfterId = afterId
            });
        }

        public Task<Issue> ReprioritizeBefore(string owner, string name, long issueNumber, long subIssueNumber, long beforeId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return ApiConnection.Post<Issue>(ApiUrls.IssueSubIssuePriority(owner, name, issueNumber), new
            {
                SubIssueId = subIssueNumber,
                BeforeId = beforeId
            });
        }
    }
}
