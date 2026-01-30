using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface ISubIssuesClient
    {
        /// <summary>
        /// Get the parent issue of a sub-issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#get-parent-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        Task<Issue> GetParent(string owner, string name, long issueNumber);

        /// <summary>
        /// Remove a sub-issue from an issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#remove-sub-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="subIssueNumber">The sub-issue number to remove</param>
        Task<Issue> Remove(string owner, string name, long issueNumber, long subIssueNumber);

        /// <summary>
        /// List the sub-issues on an issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#list-sub-issues
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        Task<IReadOnlyList<Issue>> List(string owner, string name, long issueNumber);

        /// <summary>
        /// List the sub-issues on an issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#list-sub-issues
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        Task<IReadOnlyList<Issue>> List(string owner, string name, long issueNumber, ApiOptions options);

        /// <summary>
        /// Add sub-issues to an issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#add-sub-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="subIssueNumber">The sub-issue number to remove</param>
        Task<Issue> Add(string owner, string name, long issueNumber, long subIssueNumber);

        /// <summary>
        /// Add sub-issues to an issue.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#add-sub-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="subIssueNumber">The sub-issue number to remove</param>
        /// <param name="replaceParent">When true, instructs the operation to replace the sub-issues current parent issue</param>
        Task<Issue> Add(string owner, string name, long issueNumber, long subIssueNumber, bool replaceParent);

        /// <summary>
        /// Reprioritize a sub-issue to a different position in the parent list.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#reprioritize-sub-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="subIssueNumber">The sub-issue number to remove</param>
        /// <param name="afterId">The id of the sub-issue to be prioritized after</param>
        Task<Issue> ReprioritizeAfter(string owner, string name, long issueNumber, long subIssueNumber, long afterId);

        /// <summary>
        /// Reprioritize a sub-issue to a different position in the parent list.
        /// </summary>
        /// <remarks>
        /// https://docs.github.com/en/rest/issues/sub-issues?apiVersion=2022-11-28#reprioritize-sub-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="issueNumber">The issue number</param>
        /// <param name="subIssueNumber">The sub-issue number to remove</param>
        /// <param name="beforeId">The id of the sub-issue to be prioritized before</param>
        Task<Issue> ReprioritizeBefore(string owner, string name, long issueNumber, long subIssueNumber, long beforeId);
    }
}
