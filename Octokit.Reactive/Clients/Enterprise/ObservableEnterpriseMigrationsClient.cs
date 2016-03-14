using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Threading.Tasks;

namespace Octokit.Reactive
{
    public class ObservableEnterpriseMigrationsClient : IObservableEnterpriseMigrationsClient
    {
        private readonly IEnterpriseMigrationsClient _client;

        public ObservableEnterpriseMigrationsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _client = client.Enterprise.Migration;
        }

        public IObservable<Migration> GetStatus(string org, int id)
        {
            return _client.GetStatus(org, id).ToObservable();
        }

        public IObservable<List<Migration>> GetMigrations(string org)
        {
            return _client.GetMigrations(org).ToObservable();
        }

        public IObservable<Migration> Start(string org, StartMigrationRequest migration)
        {
            return _client.Start(org, migration).ToObservable();
        }

        public IObservable<string> GetArchive(string org, int id)
        {
            return _client.GetArchive(org, id).ToObservable();
        }

        public IObservable<Unit> DeleteArchive(string org, int id)
        {
            return _client.DeleteArchive(org, id).ToObservable();
        }

        public IObservable<Unit> UnlockRepository(string org, int id, string repo)
        {
            return _client.UnlockRepository(org, id, repo).ToObservable();
        }
    }
}
