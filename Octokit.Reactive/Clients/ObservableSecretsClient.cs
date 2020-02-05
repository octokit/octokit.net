using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableSecretsClient : IObservableSecretsClient
    {
        readonly IConnection _connection;
        readonly ISecretsClient _client;

        public ObservableSecretsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Secret;
        }

        public IObservable<Unit> Create(string owner, string name, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return _client.Create(owner, name, secretName, secretRequest).ToObservable();
        }

        public IObservable<Unit> Create(long repositoryId, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Create(repositoryId, secretName, secretRequest).ToObservable();
        }

        public IObservable<Unit> Delete(string owner, string name, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Delete(owner, name, secretName).ToObservable();
        }

        public IObservable<Unit> Delete(long repositoryId, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Delete(repositoryId, secretName).ToObservable();
        }

        public IObservable<Secret> Get(string owner, string name, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Get(owner, name, secretName).ToObservable();
        }

        public IObservable<Secret> Get(long repositoryId, string secretName)
        {
            Ensure.ArgumentNotNullOrEmptyString(secretName, nameof(secretName));

            return _client.Get(repositoryId, secretName).ToObservable();
        }

        public IObservable<SecretsResponse> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return GetAll(owner, name, ApiOptions.None);
        }

        public IObservable<SecretsResponse> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public IObservable<SecretsResponse> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _connection.GetAndFlattenAllPages<SecretsResponse>(ApiUrls.Secrets(owner, name), options);
        }

        public IObservable<SecretsResponse> GetAll(long repositoryId, ApiOptions options)
        {
            return _connection.GetAndFlattenAllPages<SecretsResponse>(ApiUrls.Secrets(repositoryId), options);
        }

        public IObservable<PublicKey> GetPublicKey(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetPublicKey(owner, name).ToObservable();
        }

        public IObservable<PublicKey> GetPublicKey(long repositoryId)
        {
            return _client.GetPublicKey(repositoryId).ToObservable();
        }

        public IObservable<Unit> Update(string owner, string name, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return _client.Update(owner, name, secretName, secretRequest).ToObservable();
        }

        public IObservable<Unit> Update(long repositoryId, string secretName, SecretRequest secretRequest)
        {
            Ensure.ArgumentNotNull(secretRequest, nameof(secretRequest));

            return _client.Update(repositoryId, secretName, secretRequest).ToObservable();
        }
    }
}
