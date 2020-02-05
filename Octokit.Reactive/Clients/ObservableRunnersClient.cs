using System;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Reactive.Linq;
using Octokit.Reactive.Internal;

namespace Octokit.Reactive
{
    public class ObservableRunnersClient : IObservableRunnersClient
    {
        readonly IConnection _connection;
        readonly IRunnersClient _client;

        public ObservableRunnersClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, nameof(client));

            _connection = client.Connection;
            _client = client.Action.Runner;
        }

        public IObservable<RunnerRegistrationToken> CreateRegistrationToken(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.CreateRegistrationToken(owner, name).ToObservable();
        }

        public IObservable<RunnerRegistrationToken> CreateRegistrationToken(long repositoryId)
        {
            return _client.CreateRegistrationToken(repositoryId).ToObservable();
        }

        public IObservable<RunnerRemoveToken> CreateRemoveToken(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.CreateRemoveToken(owner, name).ToObservable();
        }

        public IObservable<RunnerRemoveToken> CreateRemoveToken(long repositoryId)
        {
            return _client.CreateRemoveToken(repositoryId).ToObservable();
        }

        public IObservable<Runner> Get(string owner, string name, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Get(owner, name, runnerId).ToObservable();
        }

        public IObservable<Runner> Get(long repositoryId, long runnerId)
        {
            return _client.Get(repositoryId, runnerId).ToObservable();
        }

        public IObservable<Runner> GetAll(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            return GetAll(owner, name, ApiOptions.None);
        }

        public IObservable<Runner> GetAll(long repositoryId)
        {
            return GetAll(repositoryId, ApiOptions.None);
        }

        public IObservable<Runner> GetAll(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Runner>(ApiUrls.Runners(owner, name), options);
        }

        public IObservable<Runner> GetAll(long repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, nameof(options));

            return _connection.GetAndFlattenAllPages<Runner>(ApiUrls.Runners(repositoryId), options);
        }

        public IObservable<RunnerDownload> GetAllDownloads(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.GetAllDownloads(owner, name).ToObservable().SelectMany(e => e);
        }

        public IObservable<RunnerDownload> GetAllDownloads(long repositoryId)
        {
            return _client.GetAllDownloads(repositoryId).ToObservable().SelectMany(e => e);
        }

        public IObservable<Unit> Remove(string owner, string name, long runnerId)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, nameof(owner));
            Ensure.ArgumentNotNullOrEmptyString(name, nameof(name));

            return _client.Remove(owner, name, runnerId).ToObservable();
        }

        public IObservable<Unit> Remove(long repositoryId, long runnerId)
        {
            return _client.Remove(repositoryId, runnerId).ToObservable();
        }
    }
}
