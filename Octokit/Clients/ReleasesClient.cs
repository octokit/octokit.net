using System.Threading.Tasks;
using Octokit.Http;

namespace Octokit.Clients
{
    public class ReleasesClient : ApiClient<Release>, IReleasesClient
    {
        public ReleasesClient(IApiConnection<Release> client) : base(client)
        {
        }

        public async Task<IReadOnlyCollection<Release>> GetAll(string owner, string repository)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(repository, "repository");

            var endpoint = "/repos/{0}/{1}/releases".FormatUri(owner, repository);
            return await Client.GetAll(endpoint);
        }
    }
}
