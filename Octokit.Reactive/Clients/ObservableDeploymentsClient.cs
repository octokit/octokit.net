using Octokit.Reactive.Internal;
using System;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive.Clients
{
    public class ObservableDeploymentsClient : IObservableDeploymentsClient
    {
        readonly IDeploymentsClient _client;
        readonly IConnection _connection;

        public ObservableDeploymentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Repository.Deployment;
            _connection = client.Connection;

            Status = new ObservableDeploymentStatusClient(client);
        }

        /// <summary>
        /// Gets all the deployments for the specified repository. Any user with pull access
        /// to a repository can view deployments.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#list-deployments
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>All the <see cref="Deployment"/>s for the specified repository.</returns>
        public IObservable<Deployment> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAndFlattenAllPages<Deployment>(
                ApiUrls.Deployments(owner, name), null, "application/vnd.github.cannonball-preview+json");
        }

        /// <summary>
        /// Creates a new deployment for the specified repository.
        /// Users with push access can create a deployment for a given ref.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/repos/deployments/#create-a-deployment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="newDeployment">A <see cref="NewDeployment"/> instance describing the new deployment to create</param>
        /// <returns>The created <see cref="Deployment"/></returns>
        public IObservable<Deployment> Create(string owner, string name, NewDeployment newDeployment)
        {
            return _client.Create(owner, name, newDeployment).ToObservable();
        }

        /// <summary>
        /// 
        /// </summary>
        public IObservableDeploymentStatusClient Status { get; private set; }
    }
}