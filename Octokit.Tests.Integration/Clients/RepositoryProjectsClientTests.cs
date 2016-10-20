using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

public class RepositoryProjectsClientTests
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
            var project1 = await CreateProjectHelper(_github, _context);
            var project2 = await CreateProjectHelper(_github, _context);

            var projects = await _github.Repository.Projects.GetAllForRepository(_context.RepositoryOwner, _context.RepositoryName);

            Assert.Equal(2, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllProjectsWithRepositoryId()
        {
            var project1 = await CreateProjectHelper(_github, _context);
            var project2 = await CreateProjectHelper(_github, _context);

            var projects = await _github.Repository.Projects.GetAllForRepository(_context.RepositoryId);

            Assert.Equal(2, projects.Count);
            Assert.True(projects.FirstOrDefault(x => x.Name == project1.Name).Id == project1.Id);
            Assert.True(projects.FirstOrDefault(x => x.Name == project2.Name).Id == project2.Id);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
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
        public async Task GetProject()
        {
            var project = await CreateProjectHelper(_github, _context);

            var result = await _github.Repository.Projects.Get(_context.RepositoryOwner, _context.RepositoryName, project.Number);

            Assert.NotNull(result);
            Assert.Equal(project.Name, result.Name);
        }

        [IntegrationTest]
        public async Task GetProjectWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);

            var result = await _github.Repository.Projects.Get(_context.RepositoryId, project.Number);

            Assert.NotNull(result);
            Assert.Equal(project.Name, result.Name);
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }

    public class TheUpdateProjectMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheUpdateProjectMethod()
        {
            _github = Helper.GetAuthenticatedClient();
            var repoName = Helper.MakeNameWithTimestamp("public-repo");

            _context = _github.CreateRepositoryContext(new NewRepository(repoName)).Result;
        }

        [IntegrationTest]
        public async Task UpdateProject()
        {
            var project = await CreateProjectHelper(_github, _context);

            var projectUpdate = new ProjectUpdate("newName");

            var result = await _github.Repository.Projects.Update(_context.RepositoryOwner, _context.RepositoryName, project.Number, projectUpdate);

            Assert.Equal("newName", result.Name);
            Assert.Equal(project.Id, result.Id);
        }

        [IntegrationTest]
        public async Task UpdateProjectWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);

            var projectUpdate = new ProjectUpdate("newName");

            var result = await _github.Repository.Projects.Update(_context.RepositoryId, project.Number, projectUpdate);

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
        public async Task DeleteProject()
        {
            var project = await CreateProjectHelper(_github, _context);

            var result = await _github.Repository.Projects.Delete(_context.RepositoryOwner, _context.RepositoryName, project.Number);

            Assert.True(result);
        }

        [IntegrationTest]
        public async Task DeleteProjectWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);

            var result = await _github.Repository.Projects.Delete(_context.RepositoryId, project.Number);

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
            var project = await CreateProjectHelper(_github, _context);
            var column1 = await CreateColumnHelper(_github, _context, project.Number);
            var column2 = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.GetAllColumns(_context.RepositoryOwner, _context.RepositoryName, project.Number);

            Assert.Equal(2, result.Count);
            Assert.True(result.FirstOrDefault(x => x.Id == column1.Id).Id == column1.Id);
            Assert.True(result.FirstOrDefault(x => x.Id == column2.Id).Id == column2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllColumnsWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column1 = await CreateColumnHelper(_github, _context, project.Number);
            var column2 = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.GetAllColumns(_context.RepositoryId, project.Number);

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
        public async Task GetColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.GetColumn(_context.RepositoryOwner, _context.RepositoryName, column.Id);

            Assert.Equal(column.Id, result.Id);
        }

        [IntegrationTest]
        public async Task GetColumnWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.GetColumn(_context.RepositoryId, column.Id);

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
        public async Task CreateColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

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
        public async Task UpdateColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var columnUpdate = new ProjectColumnUpdate("newName");

            var result = await _github.Repository.Projects.UpdateColumn(_context.RepositoryOwner, _context.RepositoryName, column.Id, columnUpdate);

            Assert.Equal("newName", result.Name);
            Assert.Equal(column.Id, result.Id);
        }

        [IntegrationTest]
        public async Task UpdateColumnWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var columnUpdate = new ProjectColumnUpdate("newName");

            var result = await _github.Repository.Projects.UpdateColumn(_context.RepositoryId, column.Id, columnUpdate);

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
        public async Task DeleteColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.DeleteColumn(_context.RepositoryOwner, _context.RepositoryName, column.Id);

            Assert.True(result);
        }

        [IntegrationTest]
        public async Task DeleteColumnWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);

            var result = await _github.Repository.Projects.DeleteColumn(_context.RepositoryId, column.Id);

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
        public async Task MoveColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column1 = await CreateColumnHelper(_github, _context, project.Number);
            var column2 = await CreateColumnHelper(_github, _context, project.Number);

            var positionFirst = new ProjectColumnMove(ProjectColumnPosition.First, null);
            var positionLast = new ProjectColumnMove(ProjectColumnPosition.Last, null);
            var positionAfter = new ProjectColumnMove(ProjectColumnPosition.After, column1.Id);

            var resultFirst = await _github.Repository.Projects.MoveColumn(_context.RepositoryOwner, _context.RepositoryName, column2.Id, positionFirst);
            var resultLast = await _github.Repository.Projects.MoveColumn(_context.RepositoryOwner, _context.RepositoryName, column2.Id, positionLast);
            var resultAfter = await _github.Repository.Projects.MoveColumn(_context.RepositoryOwner, _context.RepositoryName, column2.Id, positionAfter);

            Assert.True(resultFirst);
            Assert.True(resultLast);
            Assert.True(resultAfter);
        }

        [IntegrationTest]
        public async Task MoveColumnWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column1 = await CreateColumnHelper(_github, _context, project.Number);
            var column2 = await CreateColumnHelper(_github, _context, project.Number);

            var positionFirst = new ProjectColumnMove(ProjectColumnPosition.First, null);
            var positionLast = new ProjectColumnMove(ProjectColumnPosition.Last, null);
            var positionAfter = new ProjectColumnMove(ProjectColumnPosition.After, column1.Id);

            var resultFirst = await _github.Repository.Projects.MoveColumn(_context.RepositoryId, column2.Id, positionFirst);
            var resultLast = await _github.Repository.Projects.MoveColumn(_context.RepositoryId, column2.Id, positionLast);
            var resultAfter = await _github.Repository.Projects.MoveColumn(_context.RepositoryId, column2.Id, positionAfter);

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
        public async Task GetAllCards()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card1 = await CreateCardHelper(_github, _context, column.Id);
            var card2 = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.GetAllCards(_context.RepositoryOwner, _context.RepositoryName, column.Id);

            Assert.Equal(2, result.Count);
            Assert.True(result.FirstOrDefault(x => x.Id == card1.Id).Id == card1.Id);
            Assert.True(result.FirstOrDefault(x => x.Id == card2.Id).Id == card2.Id);
        }

        [IntegrationTest]
        public async Task GetAllCardsWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card1 = await CreateCardHelper(_github, _context, column.Id);
            var card2 = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.GetAllCards(_context.RepositoryId, column.Id);

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
        public async Task GetCard()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.GetCard(_context.RepositoryOwner, _context.RepositoryName, card.Id);

            Assert.Equal(card.Id, result.Id);
        }

        [IntegrationTest]
        public async Task GetCardWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.GetCard(_context.RepositoryId, card.Id);

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
        public async Task CreateCard()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);

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
        public async Task UpdateCard()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);
            var cardUpdate = new ProjectCardUpdate("newNameOfNote");

            var result = await _github.Repository.Projects.UpdateCard(_context.RepositoryOwner, _context.RepositoryName, card.Id, cardUpdate);

            Assert.Equal("newNameOfNote", result.Note);
            Assert.Equal(card.Id, result.Id);
        }

        [IntegrationTest]
        public async Task UpdateCardWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);
            var cardUpdate = new ProjectCardUpdate("newNameOfNote");

            var result = await _github.Repository.Projects.UpdateCard(_context.RepositoryId, card.Id, cardUpdate);

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
        public async Task DeleteCard()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.DeleteCard(_context.RepositoryOwner, _context.RepositoryName, card.Id);

            Assert.True(result);
        }

        [IntegrationTest]
        public async Task DeleteCardWithRepositoryId()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card = await CreateCardHelper(_github, _context, column.Id);

            var result = await _github.Repository.Projects.DeleteCard(_context.RepositoryId, card.Id);

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
        public async Task MoveCardInsideSameColumn()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column = await CreateColumnHelper(_github, _context, project.Number);
            var card1 = await CreateCardHelper(_github, _context, column.Id);
            var card2 = await CreateCardHelper(_github, _context, column.Id);
            var card3 = await CreateCardHelper(_github, _context, column.Id);

            var positionTop = new ProjectCardMove(ProjectCardPosition.Top, column.Id, null);
            var positionBottom = new ProjectCardMove(ProjectCardPosition.Top, column.Id, null);
            var positionAfter = new ProjectCardMove(ProjectCardPosition.Top, column.Id, card1.Id);

            var resultTop = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card2.Id, positionTop);
            var resultBottom = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card2.Id, positionBottom);
            var resultAfter = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card2.Id, positionTop);

            Assert.True(resultTop);
            Assert.True(resultBottom);
            Assert.True(resultAfter);
        }

        [IntegrationTest]
        public async Task MoveCardBetweenDifferentColumns()
        {
            var project = await CreateProjectHelper(_github, _context);
            var column1 = await CreateColumnHelper(_github, _context, project.Number);
            var column2 = await CreateColumnHelper(_github, _context, project.Number);
            var card1 = await CreateCardHelper(_github, _context, column1.Id);
            var card2 = await CreateCardHelper(_github, _context, column1.Id);
            var card3 = await CreateCardHelper(_github, _context, column1.Id);

            var positionTop = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, null);
            var positionBottom = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, null);
            var positionAfter = new ProjectCardMove(ProjectCardPosition.Top, column2.Id, card1.Id);

            var resultTop = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card1.Id, positionTop);
            var resultBottom = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card2.Id, positionBottom);
            var resultAfter = await _github.Repository.Projects.MoveCard(_context.RepositoryOwner, _context.RepositoryName, card3.Id, positionTop);

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

    private static async Task<Project> CreateProjectHelper(IGitHubClient githubClient, RepositoryContext context)
    {
        var newProject = new NewProject(Helper.MakeNameWithTimestamp("new-project"));
        var result = await githubClient.Repository.Projects.Create(context.RepositoryOwner, context.RepositoryName, newProject);

        return result;
    }

    private static async Task<ProjectColumn> CreateColumnHelper(IGitHubClient githubClient, RepositoryContext context, int number)
    {
        var newColumn = new NewProjectColumn(Helper.MakeNameWithTimestamp("new-project-column"));
        var result = await githubClient.Repository.Projects.CreateColumn(context.RepositoryOwner, context.RepositoryName, number, newColumn);

        return result;
    }

    private static async Task<ProjectCard> CreateCardHelper(IGitHubClient githubClient, RepositoryContext context, int id)
    {
        var newCard = new NewProjectCard(Helper.MakeNameWithTimestamp("new-card"));
        var result = await githubClient.Repository.Projects.CreateCard(context.RepositoryOwner, context.RepositoryName, id, newCard);

        return result;
    }
}

