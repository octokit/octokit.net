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
        private static readonly string AcceptHeader = AcceptHeaders.Concat(AcceptHeaders.TeamDiscussionsApiPreview, AcceptHeaders.ReactionsPreview);

        /// <summary>
        /// Initializes a new GitHub Orgs Team API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public TeamDiscussionsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        public Task<TeamDiscussion> Get(int teamId, int number)
        {
            var endpoint = ApiUrls.TeamDiscussion(teamId, number);

            return ApiConnection.Get<TeamDiscussion>(endpoint, null, AcceptHeader);
        }

        public Task<IReadOnlyList<TeamDiscussion>> GetAll(int teamId)
        {
            return GetAll(teamId, ApiOptions.None);
        }

        public Task<IReadOnlyList<TeamDiscussion>> GetAll(int teamId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            var endpoint = ApiUrls.TeamDiscussions(teamId);
            return ApiConnection.GetAll<TeamDiscussion>(endpoint, null, AcceptHeader, options);
        }

        public Task<TeamDiscussion> Create(int teamId, NewTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            var endpoint = ApiUrls.TeamDiscussions(teamId);
            return ApiConnection.Post<TeamDiscussion>(endpoint, teamDiscussion, AcceptHeader);
        }

        public Task<TeamDiscussion> Update(int teamId, int number, UpdateTeamDiscussion teamDiscussion)
        {
            Ensure.ArgumentNotNull(teamDiscussion, nameof(teamDiscussion));

            var endpoint = ApiUrls.TeamDiscussion(teamId, number);
            return ApiConnection.Patch<TeamDiscussion>(endpoint, teamDiscussion, AcceptHeader);
        }

        public Task Delete(int teamId, int number)
        {
            var endpoint = ApiUrls.TeamDiscussion(teamId, number);
            return ApiConnection.Delete(endpoint, new object(), AcceptHeader);
        }
    }
}
