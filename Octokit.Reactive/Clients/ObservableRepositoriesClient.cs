using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Helpers;

namespace Octokit.Reactive.Clients
{
    public class ObservableRepositoriesClient : IObservableRepositoriesClient
    {
        readonly IRepositoriesClient _client;
        readonly IConnection _connection;

        public ObservableRepositoriesClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");
            
            _client = client.Repository;
            _connection = client.Connection;
        }

        /// <summary>
        /// Creates a new repository for the current user.
        /// </summary>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(newRepository).ToObservable();
        }

        /// <summary>
        /// Creates a new repository in the specified organization.
        /// </summary>
        /// <param name="organizationLogin">The login of the organization in which to create the repostiory</param>
        /// <param name="newRepository">A <see cref="NewRepository"/> instance describing the new repository to create</param>
        /// <returns>An <see cref="IObservable{Repository}"/> instance for the created repository</returns>
        public IObservable<Repository> Create(string organizationLogin, NewRepository newRepository)
        {
            Ensure.ArgumentNotNull(organizationLogin, "organizationLogin");
            Ensure.ArgumentNotNull(newRepository, "newRepository");
            if (string.IsNullOrEmpty(newRepository.Name))
                throw new ArgumentException("The new repository's name must not be null.");

            return _client.Create(organizationLogin, newRepository).ToObservable();
        }

        /// <summary>
        /// Deletes a repository for the specified owner and name.
        /// </summary>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <remarks>Deleting a repository requires admin access. If OAuth is used, the `delete_repo` scope is required.</remarks>
        /// <returns>An <see cref="IObservable{Unit}"/> for the operation</returns>
        public IObservable<Unit> Delete(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Delete(owner, name).ToObservable();
        }

        public IObservable<Repository> Get(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.Get(owner, name).ToObservable();
        }

        public IObservable<Repository> GetAllForCurrent()
        {
            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories());
        }

        public IObservable<Repository> GetAllForUser(string login)
        {
            Ensure.ArgumentNotNullOrEmptyString(login, "login");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.Repositories(login));
        }

        public IObservable<Repository> GetAllForOrg(string organization)
        {
            Ensure.ArgumentNotNullOrEmptyString(organization, "organization");

            return _connection.GetAndFlattenAllPages<Repository>(ApiUrls.OrganizationRepositories(organization));
        }

        public IObservable<Readme> GetReadme(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetReadme(owner, name).ToObservable();
        }

        public IObservable<string> GetReadmeHtml(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _client.GetReadmeHtml(owner, name).ToObservable();
        }
    }
}
