using System.Threading.Tasks;

namespace Octokit
{
    public class InstallationId
    {
        public InstallationId() { }

        public InstallationId(long id)
        {
            Id = id;
        }

        /// <summary>
        /// The Installation Id.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Create a time bound access token for this GitHubApp Installation that can be used to access other API endpoints.
        /// </summary>
        /// <remarks>
        /// https://developer.github.com/v3/apps/#create-a-new-installation-token
        /// https://developer.github.com/apps/building-github-apps/authentication-options-for-github-apps/#authenticating-as-an-installation
        /// https://developer.github.com/v3/apps/available-endpoints/
        /// </remarks>
        /// <param name="client">The client to use</param>
        public Task<AccessToken> CreateAccessToken(IGitHubAppsClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            return client.CreateInstallationToken(Id);
        }
    }
}