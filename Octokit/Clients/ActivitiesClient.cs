
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public class ActivitiesClient: ApiClient, IActivitiesClient
    {
        public ActivitiesClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all the public events
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/activity/events/#list-public-events
        /// </remarks>
        /// <returns>All the public <see cref="Activity"/>s for the particular user.</returns>
        public Task<IReadOnlyList<Activity>> GetAll()
        {
            return ApiConnection.GetAll<Activity>(ApiUrls.Events());
        }
    }
}
