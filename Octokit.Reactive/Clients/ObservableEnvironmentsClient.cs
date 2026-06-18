using System;
using System.Reactive.Threading.Tasks;
using Octokit.Models.Response;
using Octokit.Reactive.Internal;
using System.Threading;

namespace Octokit.Reactive.Clients
{
    public class ObservableEnvironmentsClient : IObservableRepositoryDeployEnvironmentsClient
    {
        readonly IRepositoryDeployEnvironmentsClient _client;
        readonly IConnection _connection;

        public ObservableEnvironmentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _client = client.Repository.Environment;
            _connection = client.Connection;
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None, cancellationToken);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, CancellationToken cancellationToken = default)
        {
            return GetAll(repositoryId, ApiOptions.None, cancellationToken);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeploymentEnvironmentsResponse>(
                ApiUrls.DeploymentEnvironments(owner, name), options, cancellationToken);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options, CancellationToken cancellationToken = default)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeploymentEnvironmentsResponse>(
                ApiUrls.DeploymentEnvironments(repositoryId), options);
        }
    }
}
