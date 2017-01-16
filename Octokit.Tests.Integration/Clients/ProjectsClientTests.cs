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
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetAllForRepositoryMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsAllProjects()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId);

            Assert.Equal(2, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsWithoutStartForRepositories()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 3,
                PageCount = 1
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, options);

            Assert.Equal(1, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project.Name).Id == project.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfProjectsWithStartForRepositories()
        {
            var project1 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var project2 = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 1
            };

            var projects = await _github.Repository.Project.GetAllForRepository(_context.RepositoryId, options);

            Assert.Equal(1, projects.Count);
            Assert.Equal(project1.Id, projects[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctProjectsWithBasedOnStartPageForRepositories()
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
        IGitHubClient _github;

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
    }

    public class TheGetMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

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
        IGitHubClient _github;
        RepositoryContext _context;

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

            var projectUpdate = new ProjectUpdate("newName");

            var result = await _github.Repository.Project.Update(project.Id, projectUpdate);

            Assert.Equal("newName", result.Name);
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
        IGitHubClient _github;
        RepositoryContext _context;

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

    public class TheGetAllColumnsMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetAllColumnsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsAllColumns()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);

            var result = await _github.Repository.Project.Column.GetAll(project.Id);

            Assert.Equal(2, result.Count);
            Assert.True(result.FirstOrDefault(x => x.Id == column1.Id).Id == column1.Id);
            Assert.True(result.FirstOrDefault(x => x.Id == column2.Id).Id == column2.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheGetColumnMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetColumnMethod()
        {
            _github = Helper.GetAuthenticatedClient();
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
        IGitHubClient _github;
        RepositoryContext _context;

        public TheCreateColumnMethod()
        {
            _github = Helper.GetAuthenticatedClient();
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
        IGitHubClient _github;
        RepositoryContext _context;

        public TheUpdateColumnMethod()
        {
            _github = Helper.GetAuthenticatedClient();
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
        IGitHubClient _github;
        RepositoryContext _context;

        public TheDeleteColumnMethod()
        {
            _github = Helper.GetAuthenticatedClient();
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
        IGitHubClient _github;
        RepositoryContext _context;

        public TheMoveColumnMethod()
        {
            _github = Helper.GetAuthenticatedClient();
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

    public class TheGetAllCardsMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetAllCardsMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsAllCards()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateCardHelper(_github, column.Id);

            var result = await _github.Repository.Project.Card.GetAll(column.Id);

            Assert.Equal(2, result.Count);
            Assert.True(result.FirstOrDefault(x => x.Id == card1.Id).Id == card1.Id);
            Assert.True(result.FirstOrDefault(x => x.Id == card2.Id).Id == card2.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheGetCardMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetCardMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task GetsCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);

            var result = await _github.Repository.Project.Card.Get(card.Id);

            Assert.Equal(card.Id, result.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheCreateCardMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheCreateCardMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task CreatesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);

            Assert.NotNull(card);
        }

        [IntegrationTest]
        public async Task CreatesIssueCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var issue = await _github.Issue.Create(_context.RepositoryId, new NewIssue("a test issue"));
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateIssueCardHelper(_github, issue.Id, column.Id);

            Assert.NotNull(card);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheUpdateCardMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheUpdateCardMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task UpdatesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);
            var cardUpdate = new ProjectCardUpdate("newNameOfNote");

            var result = await _github.Repository.Project.Card.Update(card.Id, cardUpdate);

            Assert.Equal("newNameOfNote", result.Note);
            Assert.Equal(card.Id, result.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheDeleteCardMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheDeleteCardMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task DeletesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);

            var result = await _github.Repository.Project.Card.Delete(card.Id);

            Assert.True(result);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheMoveCardMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheMoveCardMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task MovesCardWithinColumn()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateCardHelper(_github, column.Id);
            var card3 = await CreateCardHelper(_github, column.Id);

            var positionTop = new ProjectCardMove(ProjectCardPosition.Top, column.Id, null);
            var positionBottom = new ProjectCardMove(ProjectCardPosition.Top, column.Id, null);
            var positionAfter = new ProjectCardMove(ProjectCardPosition.Top, column.Id, card1.Id);

            var resultTop = await _github.Repository.Project.Card.Move(card2.Id, positionTop);
            var resultBottom = await _github.Repository.Project.Card.Move(card2.Id, positionBottom);
            var resultAfter = await _github.Repository.Project.Card.Move(card2.Id, positionTop);

            Assert.True(resultTop);
            Assert.True(resultBottom);
            Assert.True(resultAfter);
        }

        [IntegrationTest]
        public async Task MovesCardBetweenColumns()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column1 = await CreateColumnHelper(_github, project.Id);
            var column2 = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column1.Id);
            var card2 = await CreateCardHelper(_github, column1.Id);
            var card3 = await CreateCardHelper(_github, column1.Id);

            var positionTop = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, null);
            var positionBottom = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, null);
            var positionAfter = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, card1.Id);

            var resultTop = await _github.Repository.Project.Card.Move(card1.Id, positionTop);
            var resultBottom = await _github.Repository.Project.Card.Move(card2.Id, positionBottom);
            var resultAfter = await _github.Repository.Project.Card.Move(card3.Id, positionTop);

            Assert.True(resultTop);
            Assert.True(resultBottom);
            Assert.True(resultAfter);
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

    private static async Task<ProjectColumn> CreateColumnHelper(IGitHubClient githubClient, int projectId)
    {
        var newColumn = new NewProjectColumn(Helper.MakeNameWithTimestamp("new-project-column"));
        var result = await githubClient.Repository.Project.Column.Create(projectId, newColumn);

        return result;
    }

    private static async Task<ProjectCard> CreateCardHelper(IGitHubClient githubClient, int columnId)
    {
        var newCard = new NewProjectCard(Helper.MakeNameWithTimestamp("new-card"));
        var result = await githubClient.Repository.Project.Card.Create(columnId, newCard);

        return result;
    }

    private static async Task<ProjectCard> CreateIssueCardHelper(IGitHubClient githubClient, int issueId, int columnId)
    {
        var newCard = new NewProjectCard(issueId, ProjectCardContentType.Issue);
        var result = await githubClient.Repository.Project.Card.Create(columnId, newCard);

        return result;
    }
}

