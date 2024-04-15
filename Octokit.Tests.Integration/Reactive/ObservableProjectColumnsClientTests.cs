using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Octokit.Reactive;
using System.Reactive.Linq;

public class ObservableProjectColumnsClientTests
{
    public class TheGetAllColumnsMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheGetAllColumnsMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsAllColumns()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var result = await _github.Repository.Project.Column.GetAll(project.Id).ToList();

            Assert.Equal(2, result.Count);
            Assert.True(result.FirstOrDefault(x => x.Id == column1.Id).Id == column1.Id);
            Assert.True(result.FirstOrDefault(x => x.Id == column2.Id).Id == column2.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfColumnsWithoutStart()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var columns = await _github.Repository.Project.Column.GetAll(project.Id, options).ToList();

            Assert.Single(columns);
            Assert.Equal(column1.Id, columns[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfColumnsWithStart()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var columns = await _github.Repository.Project.Column.GetAll(project.Id, options).ToList();

            Assert.Single(columns);
            Assert.Equal(column2.Id, columns[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctColumnsBasedOnStartPage()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _github.Repository.Project.Column.GetAll(project.Id, startOptions).ToList();

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.Repository.Project.Column.GetAll(project.Id, skipStartOptions).ToList();

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheGetColumnMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheGetColumnMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);

            var result = await _github.Repository.Project.Column.Get(column.Id);

            Assert.Equal(column.Id, result.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheCreateColumnMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheCreateColumnMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task CreatesColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);

            Assert.Equal(project.Url, column.ProjectUrl);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheUpdateColumnMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheUpdateColumnMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task UpdatesColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);

            var columnUpdate = new ProjectColumnUpdate("newName");

            var result = await _github.Repository.Project.Column.Update(column.Id, columnUpdate);

            Assert.Equal("newName", result.Name);
            Assert.Equal(column.Id, result.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheDeleteColumnMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheDeleteColumnMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task DeletesColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);

            var result = await _github.Repository.Project.Column.Delete(column.Id);

            Assert.True(result);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheMoveColumnMethod : IDisposable
    {
        readonly IObservableGitHubClient _github;
        readonly RepositoryContext _context;

        public TheMoveColumnMethod()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task MovesColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var positionFirst = new ProjectColumnMove(ProjectColumnPosition.First, null);
            var positionLast = new ProjectColumnMove(ProjectColumnPosition.Last, null);
            var positionAfter = new ProjectColumnMove(ProjectColumnPosition.After, column1.Id);

            var resultFirst = await _github.Repository.Project.Column.Move(column2.Id, positionFirst);
            var resultLast = await _github.Repository.Project.Column.Move(column2.Id, positionLast);
            var resultAfter = await _github.Repository.Project.Column.Move(column2.Id, positionAfter);

            Assert.True(resultFirst);
            Assert.True(resultLast);
            Assert.True(resultAfter);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    private static async Task<Project> CreateRepositoryProjectHelper(IObservableGitHubClient githubClient, long repositoryId)
    {
        var newProject = new NewProject(Helper.MakeNameWithTimestamp("new-project"));
        var result = await githubClient.Repository.Project.CreateForRepository(repositoryId, newProject);

        return result;
    }

    private static async Task<ProjectColumn> CreateColumnHelper(IObservableGitHubClient githubClient, int projectId)
    {
        var newColumn = new NewProjectColumn(Helper.MakeNameWithTimestamp("new-project-column"));
        var result = await githubClient.Repository.Project.Column.Create(projectId, newColumn);

        return result;
    }
}

