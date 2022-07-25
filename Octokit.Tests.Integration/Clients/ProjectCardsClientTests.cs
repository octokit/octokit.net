using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using System;
using System.Threading.Tasks;
using Xunit;

public class ProjectCardsClientTests
{
    public class TheGetAllMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheGetAllMethod()
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
            Assert.Contains(result, x => x.Id == card1.Id);
            Assert.Contains(result, x => x.Id == card2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllArchivedCards()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateArchivedCardHelper(_github, column.Id);

            var request = new ProjectCardRequest(ProjectCardArchivedStateFilter.Archived);

            var result = await _github.Repository.Project.Card.GetAll(column.Id, request);

            Assert.Equal(1, result.Count);
            Assert.Contains(result, x => x.Id == card2.Id);
        }

        [IntegrationTest]
        public async Task GetsAllNotArchivedCards()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateArchivedCardHelper(_github, column.Id);

            var request = new ProjectCardRequest(ProjectCardArchivedStateFilter.NotArchived);

            var result = await _github.Repository.Project.Card.GetAll(column.Id, request);

            Assert.Equal(1, result.Count);
            Assert.Contains(result, x => x.Id == card1.Id);
        }

        [IntegrationTest]
        public async Task GetsAllArchivedAndNotArchivedCards()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateArchivedCardHelper(_github, column.Id);

            var request = new ProjectCardRequest(ProjectCardArchivedStateFilter.All);

            var result = await _github.Repository.Project.Card.GetAll(column.Id, request);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, x => x.Id == card1.Id);
            Assert.Contains(result, x => x.Id == card2.Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCardWithoutStart()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateCardHelper(_github, column.Id);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var cards = await _github.Repository.Project.Card.GetAll(column.Id, options);

            // NOTE: cards are returned in reverse order
            Assert.Equal(1, cards.Count);
            Assert.Equal(card2.Id, cards[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfCardsWithStart()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateCardHelper(_github, column.Id);

            var options = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var cards = await _github.Repository.Project.Card.GetAll(column.Id, options);

            // NOTE: cards are returned in reverse order
            Assert.Equal(1, cards.Count);
            Assert.Equal(card1.Id, cards[0].Id);
        }

        [IntegrationTest]
        public async Task ReturnsDistinctCardsBasedOnStartPage()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card1 = await CreateCardHelper(_github, column.Id);
            var card2 = await CreateCardHelper(_github, column.Id);

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await _github.Repository.Project.Card.GetAll(column.Id, startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await _github.Repository.Project.Card.GetAll(column.Id, skipStartOptions);

            Assert.NotEqual(firstPage[0].Id, secondPage[0].Id);
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

    public class TheCreateMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheCreateMethod()
        {
            _github = Helper.GetAuthenticatedClient();

            _context = _github.CreateRepositoryContextWithAutoInit("public-repo").Result;
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

        [IntegrationTest]
        public async Task CreatesPullRequestCard()
        {
            await _github.CreateTheWorld(_context.Repository);

            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);

            var pullRequest = await _github.CreatePullRequest(_context.Repository);

            var column = await CreateColumnHelper(_github, project.Id);

            var card = await CreatePullRequestCardHelper(_github, pullRequest.Id, column.Id);

            Assert.NotNull(card);
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
        public async Task UpdatesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);
            var cardUpdate = new ProjectCardUpdate
            {
                Note = "newNameOfNote"
            };

            var result = await _github.Repository.Project.Card.Update(card.Id, cardUpdate);

            Assert.Equal("newNameOfNote", result.Note);
            Assert.Equal(card.Id, result.Id);
        }

        [IntegrationTest]
        public async Task ArchivesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateCardHelper(_github, column.Id);
            var cardUpdate = new ProjectCardUpdate
            {
                Archived = true
            };

            var result = await _github.Repository.Project.Card.Update(card.Id, cardUpdate);

            Assert.Equal(card.Id, result.Id);
            Assert.False(card.Archived);
            Assert.True(result.Archived);
        }

        [IntegrationTest]
        public async Task UnarchivesCard()
        {
            var project = await CreateRepositoryProjectHelper(_github, _context.RepositoryId);
            var column = await CreateColumnHelper(_github, project.Id);
            var card = await CreateArchivedCardHelper(_github, column.Id);
            var cardUpdate = new ProjectCardUpdate
            {
                Archived = false
            };

            var result = await _github.Repository.Project.Card.Update(card.Id, cardUpdate);

            Assert.Equal(card.Id, result.Id);
            Assert.True(card.Archived);
            Assert.False(result.Archived);
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

    public class TheMoveMethod : IDisposable
    {
        IGitHubClient _github;
        RepositoryContext _context;

        public TheMoveMethod()
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

    private static async Task<ProjectCard> CreateArchivedCardHelper(IGitHubClient githubClient, int columnId)
    {
        var newCard = new NewProjectCard(Helper.MakeNameWithTimestamp("new-card"));
        var card = await githubClient.Repository.Project.Card.Create(columnId, newCard);
        var result = await githubClient.Repository.Project.Card.Update(card.Id, new ProjectCardUpdate { Archived = true });

        return result;
    }

    private static async Task<ProjectCard> CreateIssueCardHelper(IGitHubClient githubClient, int issueId, int columnId)
    {
        var newCard = new NewProjectCard(issueId, ProjectCardContentType.Issue);
        var result = await githubClient.Repository.Project.Card.Create(columnId, newCard);

        return result;
    }

    private static async Task<ProjectCard> CreatePullRequestCardHelper(IGitHubClient githubClient, long pullRequestId, int columnId)
    {
        var newCard = new NewProjectCard(pullRequestId, ProjectCardContentType.PullRequest);
        var result = await githubClient.Repository.Project.Card.Create(columnId, newCard);

        return result;
    }
}

