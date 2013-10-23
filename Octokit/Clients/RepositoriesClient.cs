using System;
#if NET_45
using System.Collections.Generic;
#endif
using System.Threading.Tasks;

namespace Octokit
{
    public class RepositoriesClient : ApiClient, IRepositoriesClient
    {
        public RepositoriesClient(IApiConnection apiConnection) : base(apiConnection)
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

            return await ApiConnection.Post<Repository>(ApiUrls.Repositories(), newRepository);
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

            return await ApiConnection.Post<Repository>(ApiUrls.OrganizationRepositories(organizationLogin), newRepository);
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
            await ApiConnection.Delete(endpoint);
        }

        public async Task<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}".FormatUri(owner, name);
            return await ApiConnection.Get<Repository>(endpoint);
        }

        public async Task<IReadOnlyList<Repository>> GetAllForCurrent()
        {
            return await ApiConnection.GetAll<Repository>(ApiUrls.Repositories());
        }

        public async Task<IReadOnlyList<Repository>> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return await ApiConnection.GetAll<Repository>(ApiUrls.Repositories(login));
        }

        public async Task<IReadOnlyList<Repository>> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return await ApiConnection.GetAll<Repository>(ApiUrls.OrganizationRepositories(organization));
        }

        public async Task<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            var readmeInfo = await ApiConnection.Get<ReadmeResponse>(endpoint, null);
            return new Readme(readmeInfo, ApiConnection);
        }

        public async Task<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            var endpoint = "/repos/{0}/{1}/readme".FormatUri(owner, name);
            return await ApiConnection.GetHtml(endpoint, null);
        }
    }
}
