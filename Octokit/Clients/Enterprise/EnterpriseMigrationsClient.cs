using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octokit
{
    public class EnterpriseMigrationsClient : ApiClient, IEnterpriseMigrationsClient
    {
        public EnterpriseMigrationsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        public async Task<Migration> GetStatus(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationById(org, id);

            return await ApiConnection.Get<Migration>(endpoint);
        }

        public async Task<List<Migration>> GetMigrations(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrations(org);

            return await ApiConnection.Get<List<Migration>>(endpoint);
        }

        public async Task<Migration> Start(string org, StartMigrationRequest migration)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNull(migration, "migration");

            var endpoint = ApiUrls.EnterpriseMigrations(org);

            return await ApiConnection.Post<Migration>(endpoint, migration);
        }

        public async Task<string> GetArchive(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationArchive(org, id);

            return await ApiConnection.Get<string>(endpoint);
        }

        public Task DeleteArchive(string org, int id)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = ApiUrls.EnterpriseMigrationArchive(org, id);

            return ApiConnection.Delete(endpoint);
        }

        public Task UnlockRepository(string org, int id, string repo)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");
            Ensure.ArgumentNotNullOrEmptyString(repo, "repo");

            var endpoint = ApiUrls.EnterpriseMigrationUnlockRepository(org, id, repo);

            return ApiConnection.Delete(endpoint);
        }
    }
}