using System;
using System.Reactive.Threading.Tasks;
using Octokit.Models.Response;
using Octokit.Reactive.Internal;

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

        public IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeploymentEnvironmentsResponse>(
                ApiUrls.DeploymentEnvironments(owner, name), options);
        }

        public IObservable<DeploymentEnvironmentsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<DeploymentEnvironmentsResponse>(
                ApiUrls.DeploymentEnvironments(repositoryId), options);
        }
    }
}
