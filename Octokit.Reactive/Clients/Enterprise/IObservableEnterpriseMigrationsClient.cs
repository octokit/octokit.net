using System;
using System.Collections.Generic;
using System.Reactive;

namespace Octokit.Reactive
{
    public interface IObservableEnterpriseMigrationsClient
    {
        IObservable<Migration> GetStatus(
           string org,
           int id);

        IObservable<List<Migration>> GetMigrations(
            string org);

        IObservable<Migration> Start(
            string org,
            StartMigrationRequest migration);

        IObservable<string> GetArchive(
            string org,
            int id);

        IObservable<Unit> DeleteArchive(
            string org,
            int id);

        IObservable<Unit> UnlockRepository(
            string org,
            int id,
            string repo);
    }
}
