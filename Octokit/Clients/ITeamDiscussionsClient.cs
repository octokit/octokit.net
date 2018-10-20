using System;
using System.Diagnostics.CodeAnalysis;
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
        /// Gets a single <see cref="TeamDiscussion"/> by identifier.
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
        /// Returns all <see cref="TeamDiscussion" />s for the team.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/teams/discussions/#list-discussions
        /// </remarks>
        /// <param name="teamId">The team identifier.</param>
        /// <returns>A list of discussions in the team.</returns>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        Task<IReadOnlyList<TeamDiscussion>> GetAll(int teamId);

        /// <summary>
        /// Returns all <see cref="TeamDiscussion" />s for the team.
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
        /// Returns newly created <see cref="TeamDiscussion" /> for the current team.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="TeamDiscussion"/></returns>
        Task<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion);
    }
}
