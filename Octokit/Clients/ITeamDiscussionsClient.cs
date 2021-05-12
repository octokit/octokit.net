using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Team Discussions API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/teams/discussions/">Team Discussions API documentation</a> for more information.
    /// </remarks>
    public interface ITeamDiscussionsClient
    {
        /// <summary>
        /// List all discussions on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#list-discussions
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>A list of discussions in the team.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<TeamDiscussion>> GetAll(int teamId);

        /// <summary>
        /// List all discussions on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#list-discussions
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="options">Options to change API behaviour.</param>
        /// <returns>A list of discussions in the team.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<TeamDiscussion>> GetAll(int teamId, ApiOptions options);
        
        /// <summary>
        /// Get a specific discussion on a team's page.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#get-a-single-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <returns>The <see cref="TeamDiscussion"/> with the given identifier.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<TeamDiscussion> Get(int teamId, int number);

        /// <summary>
        /// Creates a new discussion post on a team's page.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#create-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="newTeamDiscussion">New team discussion.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="TeamDiscussion"/> object.</returns>
        Task<TeamDiscussion> Create(int teamId, NewTeamDiscussion newTeamDiscussion);

        /// <summary>
        /// Edits the title and body text of a discussion post.
        /// Only the parameters you provide are updated.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#edit-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <param name="updateTeamDiscussion">New values for the discussion.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="TeamDiscussion"/> object.</returns>
        Task<TeamDiscussion> Update(int teamId, int number, UpdateTeamDiscussion updateTeamDiscussion);

        /// <summary>
        /// Delete a discussion from a team's page.
        /// </summary>
        /// <remarks>
        /// OAuth access tokens require the write:discussion scope.
        /// https://developer.github.com/v3/teams/discussions/#delete-a-discussion
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <param name="number">The team discussion number.</param>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task Delete(int teamId, int number);
    }
}
