using System;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Organization Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Organization Teams API documentation</a> for more information.
    /// </remarks>
    public class TeamDiscussionsClient : ApiClient, ITeamDiscussionsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs Team API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public TeamDiscussionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            var endpoint = ApiUrls.TeamDiscussions(teamId);
            return ApiConnection.Post<TeamDiscussion>(endpoint, teamDiscussion, AcceptHeaders.Concat(AcceptHeaders.ProjectsApiPreview, AcceptHeaders.NestedTeamsPreview, AcceptHeaders.TeamDiscussionsApiPreview));
        }
    }
}
