using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class StatisticsClient : ApiClient, IStatisticsClient
    {
        /// <summary>
        /// Instantiates a new GitHub Statistics API client.
        /// </summary>
        /// <param name="apiConnection">An API connection</param>
        public StatisticsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns a list of <see cref="Contributor"/> for the given repository
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="repositoryName">The name of the repository</param>
        /// <returns>A list of <see cref="Contributor"/></returns>
        public async Task<IEnumerable<Contributor>> GetContributors(string owner, string repositoryName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repositoryName, "repositoryName");

            var endpoint = "/repos/{0}/{1}/stats/contributors".FormatUri(owner, repositoryName);

            var response = await Connection.GetAsync<IList<Contributor>>(endpoint, null, null);

            if (response.StatusCode == HttpStatusCode.Accepted)
            {
                return await GetContributors(owner, repositoryName);
            }
            return response.BodyAsObject;
        }
    }
}