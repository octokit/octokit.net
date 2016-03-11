using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IEnterpriseMigrationsClient
    {
        Task<Migration> GetStatus(
            string org,
            int id);

        Task<List<Migration>> GetMigrations(
            string org);

        Task<Migration> Start(
            string org,
            StartMigrationRequest migration);

        Task<string> GetArchive(
            string org,
            int id);

        Task<string> DeleteArchive(
            string org,
            int id);
    }
}