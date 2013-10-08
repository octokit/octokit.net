using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;
using Octokit.Internal;

namespace Octokit
{
    public class RepositoriesClient : ApiClient<Repository>, IRepositoriesClient
    {
        public RepositoriesClient(IApiConnection<Repository> client) : base(client)
        {
        }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>A <see cref="Repository"/> instance for the created repository</returns>
        public async Task<Repository> Create(NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            var endpoint = new Uri("user/repos", UriKind.Relative);
            return await Client.Create(endpoint, newRepository);
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}".FormatUri(owner, name);
            return await Client.Get(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            var endpoint = new Uri("user/repos", UriKind.Relative);
            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = "/users/{0}/repos".FormatUri(login);
            
            return await Client.GetAll(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            var endpoint = "/orgs/{0}/repos".FormatUri(organization);
            
            return await Client.GetAll(endpoint);
        }

        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            var readmeInfo = await Client.GetItem<ReadmeResponse>(endpoint, null);
            return new Readme(readmeInfo, Client);
        }
    }
}
