using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IIssuesLabelsClient
    {
        /// <summary>
        /// Gets all  labels for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:name
        /// </remarks>
        /// <returns></returns>
        Task<IReadOnlyList<Label>> GetForRepository(string owner, string repo);

        /// <summary>
        /// Gets a single Label by name.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:name
        /// </remarks>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get",
        Justification = "Method makes a network request")]
        Task<Label> Get(string owner, string repo, string name);

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:label
        /// </remarks>
        /// <returns></returns>
        Task Delete(string owner, string repo, string name);

        /// <summary>
        /// Deletes a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:label
        /// </remarks>
        /// <returns></returns>
        Task<Label> Create(string owner, string repo, NewLabel newLabel);

        /// <summary>
        /// Updates a label.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/:owner/:repo/labels/:label
        /// </remarks>
        /// <returns></returns>
        Task<Label> Create(string owner, string repo, string name, LabelUpdate labelUpdate);
    }
}