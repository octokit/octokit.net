using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class MigrationsClientTests : IDisposable
{
    private readonly IGitHubClient _gitHub;
    private List<RepositoryContext> _repos;
    private Migration _migrationContext;
    private string _orgName;
    private bool isExported = false;

    public MigrationsClientTests()
    {
        _gitHub = Helper.GetAuthenticatedClient();
        _orgName = Helper.Organization;
        _repos = new List<RepositoryContext>();

        CreateTheWorld().Wait();
        StartNewMigration().Wait();
    }

    private async Task CreateTheWorld()
    {
        _repos.Add(await _gitHub.CreateRepositoryContext(_orgName, new NewRepository("migrationtest-repo1")
        {
            GitignoreTemplate = "VisualStudio", LicenseTemplate = "mit"
        }));
        _repos.Add(await _gitHub.CreateRepositoryContext(_orgName, new NewRepository("migrationtest-repo2")
        {
            GitignoreTemplate = "VisualStudio",
            LicenseTemplate = "mit"
        }));
        _repos.Add(await _gitHub.CreateRepositoryContext(_orgName, new NewRepository("migrationtest-repo3")
        {
            GitignoreTemplate = "VisualStudio",
            LicenseTemplate = "mit"
        }));
    }

    [IntegrationTest(Skip = "This will be automatically tested as all other tests call it. See: https://gist.github.com/ryangribble/fc14239ecad3f65d96f5ebd2fecf722e#file-octokit-migrationsclienttests-cs-L40")]
    public async Task StartNewMigration()
    {
        var repoNames = _repos.Select(repo => repo.Repository.FullName).ToList();
        var migrationRequest = new StartMigrationRequest(repoNames);

        _migrationContext = await _gitHub.Migration.Migrations.Start(_orgName, migrationRequest);

        Assert.Equal(3, _migrationContext.Repositories.Count);
        Assert.Equal(Migration.MigrationState.Pending, _migrationContext.State);

        ChecksMigrationCompletion();
    }

    [IntegrationTest]
    public async Task CanGetAllMigrations()
    {
        var migrations = await _gitHub.Migration.Migrations.GetAll(_orgName);

        Assert.NotNull(migrations);
        Assert.NotEqual(0, migrations.Count);
    }

    [IntegrationTest]
    public async Task CanGetMigration()
    {
        var retreivedMigration = await _gitHub.Migration.Migrations.Get(_orgName, _migrationContext.Id);

        Assert.Equal(_migrationContext.Guid, retreivedMigration.Guid);
    }

    [IntegrationTest]
    public async Task CanGetArchive()
    {
        while (!isExported)
        {
            Thread.Sleep(2000);    
        }

        var url = await _gitHub.Migration.Migrations.GetArchive(_orgName, _migrationContext.Id);

        Assert.NotEmpty(url);
    }

    [IntegrationTest]
    public async Task CanDeleteArchive()
    {
        while (!isExported)
        {
            Thread.Sleep(2000);
        }

        await _gitHub.Migration.Migrations.DeleteArchive(_orgName, _migrationContext.Id);
    }

    [IntegrationTest]
    public async Task CanUnlockRepository()
    {
        while (!isExported)
        {
            Thread.Sleep(2000);
        }

        await _gitHub.Migration.Migrations.UnlockRepository(_orgName, _migrationContext.Id, _migrationContext.Repositories[0].Name);
    }

    async Task ChecksMigrationCompletion()
    {
        while (!isExported)
        {
            Thread.Sleep(2000);
            _migrationContext = await _gitHub.Migration.Migrations.Get(_orgName, _migrationContext.Id);

            if (_migrationContext.State == Migration.MigrationState.Exported)
            {
                isExported = true;
            }
        }
    }

    public void Dispose()
    {
        _repos.ForEach( (repo) => repo.Dispose() );
    }
}
