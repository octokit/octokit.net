using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryProjectsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ProjectsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task RequestCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));

                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "name"));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.Create("fake", "repo", newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("test");

                await client.CreateForRepository(1, newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newProject = new NewProject("someName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", newProject));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, newProject));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", newProject));
                Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", newProject));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updateProject = new ProjectUpdate("someNewName");

                await client.Update("fake", "repo", 1, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), updateProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updateProject = new ProjectUpdate("someNewName");

                await client.Update(1, 2, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2"), updateProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var updateProject = new ProjectUpdate("someNewName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", 1, updateProject));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, 1, updateProject));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", 1, updateProject));
                Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", 1, updateProject));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Delete("fake", "repo", 1);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task DeletesCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Delete(1, 2);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }

        public class TheGetAllColumnsMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllColumns("fake", "repo", 1);

                connection.Received().GetAll<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1/columns"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllColumns(1, 2);

                connection.Received().GetAll<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2/columns"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllColumns("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllColumns(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllColumns("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllColumns("", "name", 1));
            }
        }

        public class TheGetColumnMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetColumn("fake", "repo", 1);

                connection.Received().Get<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetColumn(1, 2);

                connection.Received().Get<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetColumn("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetColumn(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.GetColumn("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetColumn("", "name", 1));
            }
        }

        public class TheCreateColumnMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProjectColumn = new NewProjectColumn("someName");

                await client.CreateColumn("fake", "repo", 1, newProjectColumn);

                connection.Received().Post<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1/columns"), newProjectColumn, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProjectColumn = new NewProjectColumn("someName");

                await client.CreateColumn(1, 2, newProjectColumn);

                connection.Received().Post<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2/columns"), newProjectColumn, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newProjectColumn = new NewProjectColumn("someName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateColumn(null, "owner", 1, newProjectColumn));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateColumn("name", null, 1, newProjectColumn));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateColumn("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.CreateColumn("owner", "", 1, newProjectColumn));
                Assert.ThrowsAsync<ArgumentException>(() => client.CreateColumn("", "name", 1, newProjectColumn));
            }
        }

        public class TheUpdateColumnMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updatePorjectColumn = new ProjectColumnUpdate("someNewName");

                await client.UpdateColumn("fake", "repo", 1, updatePorjectColumn);

                connection.Received().Patch<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1"), updatePorjectColumn, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updatePorjectColumn = new ProjectColumnUpdate("someNewName");

                await client.UpdateColumn(1, 2, updatePorjectColumn);

                connection.Received().Patch<ProjectColumn>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2"), updatePorjectColumn, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var updateProjectColumn = new ProjectColumnUpdate("someNewName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateColumn(null, "owner", 1, updateProjectColumn));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateColumn("name", null, 1, updateProjectColumn));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateColumn("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.UpdateColumn("owner", "", 1, updateProjectColumn));
                Assert.ThrowsAsync<ArgumentException>(() => client.UpdateColumn("", "name", 1, updateProjectColumn));
            }
        }

        public class TheDeleteColumnMethod
        {
            [Fact]
            public async Task DeletesCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.DeleteColumn("fake", "repo", 1);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task DeletesCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.DeleteColumn(1, 2);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteColumn(null, "owner", 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteColumn("name", null, 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.DeleteColumn("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.DeleteColumn("", "name", 1));
            }
        }

        public class TheMoveColumnMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                await client.MoveColumn("fake", "repo", 1, position);

                connection.Connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1/moves"), position, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                await client.MoveColumn(1, 2, position);

                connection.Connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2/moves"), position, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveColumn(null, "name", 1, position));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveColumn("owner", null, 1, position));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveColumn("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.MoveColumn("owner", "", 1, position));
                Assert.ThrowsAsync<ArgumentException>(() => client.MoveColumn("", "name", 1, position));
            }
        }

        public class TheGetAllCardsMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllCards("fake", "repo", 1);

                connection.Received().GetAll<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1/cards"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllCards(1, 2);

                connection.Received().GetAll<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2/cards"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCards("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllCards(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCards("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetAllCards("", "name", 1));
            }
        }

        public class TheGetCardMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetCard("fake", "repo", 1);

                connection.Received().Get<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/cards/1"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetCard(1, 2);

                connection.Received().Get<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/cards/2"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCard("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetCard(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.GetCard("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.GetCard("", "name", 1));
            }
        }

        public class TheCreateCardMethod
        {
            [Fact]
            public async Task PostToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newCard = new NewProjectCard("someNote");

                await client.CreateCard("fake", "repo", 1, newCard);

                connection.Received().Post<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/1/cards"), newCard, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newCard = new NewProjectCard("someNote");

                await client.CreateCard(1, 2, newCard);

                connection.Received().Post<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/2/cards"), newCard, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newCard = new NewProjectCard("someNote");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateCard(null, "name", 1, newCard));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateCard("owner", null, 1, newCard));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateCard("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.CreateCard("owner", "", 1, newCard));
                Assert.ThrowsAsync<ArgumentException>(() => client.CreateCard("", "name", 1, newCard));
            }
        }

        public class TheUpdateCardMethod
        {
            [Fact]
            public async Task PostToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updateCard = new ProjectCardUpdate("someNewNote");

                await client.UpdateCard("fake", "repo", 1, updateCard);

                connection.Received().Patch<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/cards/1"), updateCard, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectURLWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updateCard = new ProjectCardUpdate("someNewNote");

                await client.UpdateCard(1, 2, updateCard);

                connection.Received().Patch<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/cards/2"), updateCard, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var updateCard = new ProjectCardUpdate("someNewNote");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateCard(null, "name", 1, updateCard));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateCard("owner", null, 1, updateCard));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateCard("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.UpdateCard("owner", "", 1, updateCard));
                Assert.ThrowsAsync<ArgumentException>(() => client.UpdateCard("", "name", 1, updateCard));
            }
        }

        public class TheDeleteCardMethod
        {
            [Fact]
            public async Task DeletesCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.DeleteCard("fake", "repo", 1);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/cards/1"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task DeletesCorrectURLWithRepositoryID()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.DeleteCard(1, 2);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/cards/2"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteCard("owner", null, 1));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteCard(null, "name", 1));

                Assert.ThrowsAsync<ArgumentException>(() => client.DeleteCard("owner", "", 1));
                Assert.ThrowsAsync<ArgumentException>(() => client.DeleteCard("", "name", 1));
            }
        }

        public class TheMoveCardMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                await client.MoveCard("fake", "repo", 1, position);

                connection.Connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/columns/cards/1/moves"), position, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                await client.MoveCard(1, 2, position);

                connection.Connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/columns/cards/2/moves"), position, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveCard(null, "name", 1, position));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveCard("owner", null, 1, position));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.MoveCard("owner", "name", 1, null));

                Assert.ThrowsAsync<ArgumentException>(() => client.MoveCard("owner", "", 1, position));
                Assert.ThrowsAsync<ArgumentException>(() => client.MoveCard("", "name", 1, position));
            }
        }
    }
}
