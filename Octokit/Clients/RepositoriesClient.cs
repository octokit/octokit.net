using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoriesClient : ApiClient, IRepositoriesClient
    {
        public RepositoriesClient(IApiConnection client) : base(client)
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
            return await Client.Create<Repository>(endpoint, newRepository);
        }

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <param name="organizationLogin">The login of the organization in which to create the repostiory</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>A <see cref="Repository"/> instance for the created repository</returns>
        public async Task<Repository> Create(string organizationLogin, NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(organizationLogin, "organizationLogin");
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            var endpoint = "orgs/{0}/repos".FormatUri(organizationLogin);
            return await Client.Create<Repository>(endpoint, newRepository);
        }

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        public async Task Delete(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}".FormatUri(owner, name);
            await Client.Delete<Repository>(endpoint);
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}".FormatUri(owner, name);
            return await Client.Get<Repository>(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            var endpoint = new Uri("user/repos", UriKind.Relative);
            return await Client.GetAll<Repository>(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            var endpoint = "/users/{0}/repos".FormatUri(login);

            return await Client.GetAll<Repository>(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            var endpoint = "/orgs/{0}/repos".FormatUri(organization);

            return await Client.GetAll<Repository>(endpoint);
        }

        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            var readmeInfo = await Client.Get<ReadmeResponse>(endpoint, null);
            return new Readme(readmeInfo, Client);
        }

        public async Task<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            return await Client.GetHtml(endpoint, null);
        }
    }
}
