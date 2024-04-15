using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

public class MigrationsClientTests : IDisposable
{
    private readonly IGitHubClient _gitHub;
    private readonly List<RepositoryContext> _repos;
    private Migration _migrationContext;
    private readonly string _orgName;
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
        _repos.Add(await _gitHub.CreateOrganizationRepositoryContext(_orgName, new NewRepository(Helper.MakeNameWithTimestamp("migrationtest-repo1"))
        {
            GitignoreTemplate = "VisualStudio",
            LicenseTemplate = "mit"
        }));
        _repos.Add(await _gitHub.CreateOrganizationRepositoryContext(_orgName, new NewRepository(Helper.MakeNameWithTimestamp("migrationtest-repo2"))
        {
            GitignoreTemplate = "VisualStudio",
            LicenseTemplate = "mit"
        }));
        _repos.Add(await _gitHub.CreateOrganizationRepositoryContext(_orgName, new NewRepository(Helper.MakeNameWithTimestamp("migrationtest-repo3"))
        {
            GitignoreTemplate = "VisualStudio",
            LicenseTemplate = "mit"
        }));
    }

    private async Task StartNewMigration()
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
        Assert.NotEmpty(migrations);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMigrationsWithoutStart()
    {
        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1
        };

        var migrations = await _gitHub.Migration.Migrations.GetAll(_orgName, options);

        Assert.Single(migrations);
    }

    [IntegrationTest]
    public async Task ReturnsCorrectCountOfMigrationsWithStart()
    {
        var options = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };

        var migrations = await _gitHub.Migration.Migrations.GetAll(_orgName, options);

        Assert.Single(migrations);
    }

    [IntegrationTest]
    public async Task ReturnsDistinctMigrationsBasedOnStartPage()
    {
        var startOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 1
        };

        var firstPage = await _gitHub.Migration.Migrations.GetAll(_orgName, startOptions);

        var skipStartOptions = new ApiOptions
        {
            PageCount = 1,
            PageSize = 1,
            StartPage = 2
        };
        var secondPage = await _gitHub.Migration.Migrations.GetAll(_orgName, skipStartOptions);

        Assert.Single(firstPage);
        Assert.Single(secondPage);
        Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        Assert.NotEqual(firstPage[0].Repositories, secondPage[0].Repositories);
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

        var contents = await _gitHub.Migration.Migrations.GetArchive(_orgName, _migrationContext.Id);

        Assert.NotEmpty(contents);
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
        _repos.ForEach((repo) => repo.Dispose());
    }
}
