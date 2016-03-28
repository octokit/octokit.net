using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class MigrationsClientTests
{
    public class TheStartNewMethod
    {
        readonly IGitHubClient _gitHub;

        public TheStartNewMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();

        }

        [IntegrationTest]
        public async Task CanStartNewMigration()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var repos = (await _gitHub.Repository.GetAllForOrg(organization));
            var repoNames = repos.Select(repo => repo.FullName).ToList();
            var migrationRequest = new StartMigrationRequest(repoNames);

            var migration = await _gitHub.Migration.Migrations.Start(organization, migrationRequest);

            Assert.Equal(repos.Count, migration.Repositories.Count);
            Assert.Equal("pending", migration.State);
        }
    }

    public class TheGetAllMethod
    {
        readonly IGitHubClient _gitHub;

        public TheGetAllMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task CanGetAllMigrations()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var migrations = await _gitHub.Migration.Migrations.GetAll(organization);

            Assert.NotNull(migrations);
            Assert.NotEqual(0, migrations.Count);
        }
    }

    public class TheGetMethod
    {
        readonly IGitHubClient _gitHub;

        public TheGetMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task CanGetMigration()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var repos = (await _gitHub.Repository.GetAllForOrg(organization));
            var repoNames = repos.Select(repo => repo.FullName).ToList();
            var migrationRequest = new StartMigrationRequest(repoNames);
            var migration = await _gitHub.Migration.Migrations.Start(organization, migrationRequest);

            var retreivedMigration = await _gitHub.Migration.Migrations.Get(organization, migration.Id);

            Assert.Equal(migration.Guid, retreivedMigration.Guid);
        }
    }

    public class TheGetArchiveMethod
    {
        readonly IGitHubClient _gitHub;

        public TheGetArchiveMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task CanGetArchive()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var repos = (await _gitHub.Repository.GetAllForOrg(organization));
            var repoNames = repos.Select(repo => repo.FullName).ToList();
            var migrationRequest = new StartMigrationRequest(repoNames);
            var migration = await _gitHub.Migration.Migrations.Start(organization, migrationRequest);

            var url = await _gitHub.Migration.Migrations.GetArchive(organization, migration.Id);

            Assert.NotEmpty(url);
        }
    }

    public class TheDeleteArchiveMethod
    {
        readonly IGitHubClient _gitHub;

        public TheDeleteArchiveMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task DeletesArchive()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var repos = (await _gitHub.Repository.GetAllForOrg(organization));
            var repoNames = repos.Select(repo => repo.FullName).ToList();
            var migrationRequest = new StartMigrationRequest(repoNames);
            var migration = await _gitHub.Migration.Migrations.Start(organization, migrationRequest);

            await _gitHub.Migration.Migrations.DeleteArchive(organization, migration.Id);
        }
    }

    public class TheUnlockRepositoryMethod
    {
        readonly IGitHubClient _gitHub;

        public TheUnlockRepositoryMethod()
        {
            _gitHub = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task UnlocksRepository()
        {
            var organization = Environment.GetEnvironmentVariable("OCTOKIT_GITHUBORGANIZATION");
            var repos = (await _gitHub.Repository.GetAllForOrg(organization));
            var repoNames = repos.Select(repo => repo.FullName).ToList();
            var migrationRequest = new StartMigrationRequest(repoNames, true);
            var migration = await _gitHub.Migration.Migrations.Start(organization, migrationRequest);

            await _gitHub.Migration.Migrations.UnlockRepository(organization, migration.Id, migration.Repositories[0].FullName);
        }
    }
}
