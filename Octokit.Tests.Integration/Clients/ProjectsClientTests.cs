using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

public class ProjectsClientTests
{
    public class TheGetAllForRepositoryMethod : IDisposable
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;

        public TheGetAllForRepositoryMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsAllProjectsForRepository()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

            Assert.Equal(2, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllProjectsForRepositoryWithRepositoryId()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId);

            Assert.Equal(2, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllFilteredProjectsForRepository()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            // Make 2nd project closed
            var result = await _github.Repository.Project.Update(project2.Id, new ProjectUpdate { State = ItemState.Closed });

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, new ProjectRequest(ItemStateFilter.Closed));

            Assert.Single(projects);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllFilteredProjectsForRepositoryWithRepositoryId()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            // Make 2nd project closed
            var result = await _github.Repository.Project.Update(project2.Id, new ProjectUpdate { State = ItemState.Closed });

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, new ProjectRequest(ItemStateFilter.Closed));

            Assert.Single(projects);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsForRepositoryWithoutStart()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

            Assert.Single(projects);
            Assert.Equal(project1.Id, projects[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsForRepositoryWithRepositoryIdWithoutStart()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, options);

            Assert.Single(projects);
            Assert.Equal(project1.Id, projects[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsForRepositoryWithStart()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, options);

            Assert.Single(projects);
            Assert.Equal(project2.Id, projects[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsForRepositoryWithRepositoryIdWithStart()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, options);

            Assert.Single(projects);
            Assert.Equal(project2.Id, projects[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctProjectsForRepositoryBasedOnStartPage()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.Repository.Project.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctProjectsForRepositoryBasedOnStartPageWithRepositoryId()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheGetAllForOrganizationMethod
    {
        readonly IGitHubClient _github;

        public TheGetAllForOrganizationMethod()
        {
            _github = Helper.GetAuthenticatedClient();
        }

        [IntegrationTest]
        public async Task GetsAllProjects()
        {
            var project1 = await CreateOrganizationProjectHelper(_github, Helper.Organization);
            var project2 = await CreateOrganizationProjectHelper(_github, Helper.Organization);

            var projects = await _github.Repository.Project.GetAllForOrganization(Helper.Organization);

            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllFilteredProjectsForRepository()
        {
            var project = await CreateOrganizationProjectHelper(_github, Helper.Organization);

            // Make project closed
            var result = await _github.Repository.Project.Update(project.Id, new ProjectUpdate { State = ItemState.Closed });

            var projects = await _github.Repository.Project.GetAllForOrganization(Helper.Organization, new ProjectRequest(ItemStateFilter.Closed));

            Assert.True(projects.FirstOrDefault(x => x.Name == project.Name).Id == project.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsForOrganization()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 1
            };

            var projects = await _github.Repository.Project.GetAllForOrganization(Helper.Organization, options);

            Assert.Equal(5, projects.Count);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctProjectsForOrganizationBasedOnStartPage()
        {
            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _github.Repository.Project.GetAllForOrganization(Helper.Organization, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.Repository.Project.GetAllForOrganization(Helper.Organization, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
        }
    }

    public class TheGetMethod : IDisposable
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;

        public TheGetMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsProject()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var result = await _github.Repository.Project.Get(project.Id);

            Assert.NotNull(result);
            Assert.Equal(project.Name, result.Name);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheUpdateMethod : IDisposable
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;

        public TheUpdateMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task UpdatesProject()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var projectUpdate = new ProjectUpdate
            {
                Name = "newName",
                State = ItemState.Closed
            };

            var result = await _github.Repository.Project.Update(project.Id, projectUpdate);

            Assert.Equal("newName", result.Name);
            Assert.Equal(ItemState.Closed, result.State);
            Assert.Equal(project.Id, result.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheDeleteMethod : IDisposable
    {
        readonly IGitHubClient _github;
        readonly RepositoryContext _context;

        public TheDeleteMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task DeletesProject()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var result = await _github.Repository.Project.Delete(project.Id);

            Assert.True(result);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    private static async Task<Project> CreateRepositoryProjectHelper(IGitHubClient githubClient, long repositoryId)
    {
        var newProject = new NewProject(Helper.MakeNameWithTimestamp("new-project"));
        var result = await githubClient.Repository.Project.CreateForRepository(repositoryId, newProject);

        return result;
    }

    private static async Task<Project> CreateOrganizationProjectHelper(IGitHubClient githubClient, string organization)
    {
        var newProject = new NewProject(Helper.MakeNameWithTimestamp("new-project"));
        var result = await githubClient.Repository.Project.CreateForOrganization(organization, newProject);

        return result;
    }
}

