using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/events/">Issue Events API documentation</a> for more information.
    /// </remarks>
    public interface IIssuesEventsClient
    {
        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns></returns>
        Task<IReadOnlyList<EventInfo>> GetForIssue(string owner, string name, int number);

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns></returns>
        Task<IReadOnlyList<IssueEvent>> GetForRepository(string owner, string name);

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The event id</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        Task<IssueEvent> Get(string owner, string name, int number);
    }
}
