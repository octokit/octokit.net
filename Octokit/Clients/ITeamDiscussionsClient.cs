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
        /// Returns newly created <see cref="TeamDiscussion" /> for the current team.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="TeamDiscussion"/></returns>
        Task<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion);
    }
}
