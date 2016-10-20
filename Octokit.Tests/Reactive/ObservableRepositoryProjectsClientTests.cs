using NSubstitute;
using Octokit.Reactive;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryProjectsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableRepositoryProjectsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllForRepository("fake", "repo");

                gitHubClient.Received().Repository.Projects.GetAllForRepository("fake", "repo");
            }

            [Fact]
            public void RequestCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Received().Repository.Projects.GetAllForRepository(1);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, "name"));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository("", "name"));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.Get("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().Get("fake", "repo", 1);
            }

            [Fact]
            public void RequestCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.Get(1, 2);

                gitHubClient.Repository.Projects.Received().Get(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Get("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.Get(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.Get("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.Get("", "name", 1));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newProject = new NewProject("someName");

                client.Create("fake", "repo", newProject);

                gitHubClient.Repository.Projects.Received().Create("fake", "repo", newProject);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newProject = new NewProject("test");

                client.Create(1, newProject);

                gitHubClient.Repository.Projects.Received().Create(1, newProject);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var newProject = new NewProject("someName");

                Assert.Throws<ArgumentNullException>(() => client.Create(null, "name", newProject));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", null, newProject));
                Assert.Throws<ArgumentNullException>(() => client.Create("owner", "name", null));

                Assert.Throws<ArgumentException>(() => client.Create("", "name", newProject));
                Assert.Throws<ArgumentException>(() => client.Create("owner", "", newProject));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updateProject = new ProjectUpdate("someNewName");

                client.Update("fake", "repo", 1, updateProject);

                gitHubClient.Repository.Projects.Received().Update("fake", "repo", 1, updateProject);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updateProject = new ProjectUpdate("someNewName");

                client.Update(1, 2, updateProject);

                gitHubClient.Repository.Projects.Received().Update(1, 2, updateProject);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var updateProject = new ProjectUpdate("someNewName");

                Assert.Throws<ArgumentNullException>(() => client.Update(null, "name", 1, updateProject));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", null, 1, updateProject));
                Assert.Throws<ArgumentNullException>(() => client.Update("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.Update("", "name", 1, updateProject));
                Assert.Throws<ArgumentException>(() => client.Update("owner", "", 1, updateProject));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.Delete("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().Delete("fake", "repo", 1);
            }

            [Fact]
            public void DeletesCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.Delete(1, 2);

                gitHubClient.Repository.Projects.Received().Delete(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.Delete(null, "name", 1));
                Assert.Throws<ArgumentNullException>(() => client.Delete("owner", null, 1));

                Assert.Throws<ArgumentException>(() => client.Delete("", "name", 1));
                Assert.Throws<ArgumentException>(() => client.Delete("owner", "", 1));
            }
        }

        public class TheGetAllColumnsMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllColumns("fake", "repo", 1);

                gitHubClient.Received().Repository.Projects.GetAllColumns("fake", "repo", 1);
            }

            [Fact]
            public void RequestCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllColumns(1, 2);

                gitHubClient.Received().Repository.Projects.GetAllColumns(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllColumns("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllColumns(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.GetAllColumns("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllColumns("", "name", 1));
            }
        }

        public class TheGetColumnMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetColumn("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().GetColumn("fake", "repo", 1);
            }

            [Fact]
            public void RequestCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetColumn(1, 2);

                gitHubClient.Repository.Projects.Received().GetColumn(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetColumn("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetColumn(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.GetColumn("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetColumn("", "name", 1));
            }
        }

        public class TheCreateColumnMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newProjectColumn = new NewProjectColumn("someName");

                client.CreateColumn("fake", "repo", 1, newProjectColumn);

                gitHubClient.Repository.Projects.Received().CreateColumn("fake", "repo", 1, newProjectColumn);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newProjectColumn = new NewProjectColumn("someName");

                client.CreateColumn(1, 2, newProjectColumn);

                gitHubClient.Repository.Projects.Received().CreateColumn(1, 2, newProjectColumn);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var newProjectColumn = new NewProjectColumn("someName");

                Assert.Throws<ArgumentNullException>(() => client.CreateColumn(null, "owner", 1, newProjectColumn));
                Assert.Throws<ArgumentNullException>(() => client.CreateColumn("name", null, 1, newProjectColumn));
                Assert.Throws<ArgumentNullException>(() => client.CreateColumn("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.CreateColumn("owner", "", 1, newProjectColumn));
                Assert.Throws<ArgumentException>(() => client.CreateColumn("", "name", 1, newProjectColumn));
            }
        }

        public class TheUpdateColumnMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updatePorjectColumn = new ProjectColumnUpdate("someNewName");

                client.UpdateColumn("fake", "repo", 1, updatePorjectColumn);

                gitHubClient.Repository.Projects.Received().UpdateColumn("fake", "repo", 1, updatePorjectColumn);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updatePorjectColumn = new ProjectColumnUpdate("someNewName");

                client.UpdateColumn(1, 2, updatePorjectColumn);

                gitHubClient.Repository.Projects.Received().UpdateColumn(1, 2, updatePorjectColumn);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var updateProjectColumn = new ProjectColumnUpdate("someNewName");

                Assert.Throws<ArgumentNullException>(() => client.UpdateColumn(null, "owner", 1, updateProjectColumn));
                Assert.Throws<ArgumentNullException>(() => client.UpdateColumn("name", null, 1, updateProjectColumn));
                Assert.Throws<ArgumentNullException>(() => client.UpdateColumn("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.UpdateColumn("owner", "", 1, updateProjectColumn));
                Assert.Throws<ArgumentException>(() => client.UpdateColumn("", "name", 1, updateProjectColumn));
            }
        }

        public class TheDeleteColumnMethod
        {
            [Fact]
            public void DeletesCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.DeleteColumn("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().DeleteColumn("fake", "repo", 1);
            }

            [Fact]
            public void DeletesCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.DeleteColumn(1, 2);

                gitHubClient.Repository.Projects.Received().DeleteColumn(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteColumn(null, "owner", 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteColumn("name", null, 1));

                Assert.Throws<ArgumentException>(() => client.DeleteColumn("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteColumn("", "name", 1));
            }
        }

        public class TheMoveColumnMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                client.MoveColumn("fake", "repo", 1, position);

                gitHubClient.Repository.Projects.Received().MoveColumn("fake", "repo", 1, position);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                client.MoveColumn(1, 2, position);

                gitHubClient.Repository.Projects.Received().MoveColumn(1, 2, position);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                Assert.Throws<ArgumentNullException>(() => client.MoveColumn(null, "name", 1, position));
                Assert.Throws<ArgumentNullException>(() => client.MoveColumn("owner", null, 1, position));
                Assert.Throws<ArgumentNullException>(() => client.MoveColumn("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.MoveColumn("owner", "", 1, position));
                Assert.Throws<ArgumentException>(() => client.MoveColumn("", "name", 1, position));
            }
        }

        public class TheGetAllCardsMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllCards("fake", "repo", 1);

                gitHubClient.Received().Repository.Projects.GetAllCards("fake", "repo", 1);
            }

            [Fact]
            public void RequestCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetAllCards(1, 2);

                gitHubClient.Received().Repository.Projects.GetAllCards(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetAllCards("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetAllCards(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.GetAllCards("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetAllCards("", "name", 1));
            }
        }

        public class TheGetCardMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetCard("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().GetCard("fake", "repo", 1);
            }

            [Fact]
            public void RequestCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.GetCard(1, 2);

                gitHubClient.Repository.Projects.Received().GetCard(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.GetCard("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.GetCard(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.GetCard("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.GetCard("", "name", 1));
            }
        }

        public class TheCreateCardMethod
        {
            [Fact]
            public void PostToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newCard = new NewProjectCard("someNote");

                client.CreateCard("fake", "repo", 1, newCard);

                gitHubClient.Repository.Projects.Received().CreateCard("fake", "repo", 1, newCard);
            }

            [Fact]
            public void PostToCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var newCard = new NewProjectCard("someNote");

                client.CreateCard(1, 2, newCard);

                gitHubClient.Repository.Projects.Received().CreateCard(1, 2, newCard);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var newCard = new NewProjectCard("someNote");

                Assert.Throws<ArgumentNullException>(() => client.CreateCard(null, "name", 1, newCard));
                Assert.Throws<ArgumentNullException>(() => client.CreateCard("owner", null, 1, newCard));
                Assert.Throws<ArgumentNullException>(() => client.CreateCard("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.CreateCard("owner", "", 1, newCard));
                Assert.Throws<ArgumentException>(() => client.CreateCard("", "name", 1, newCard));
            }
        }

        public class TheUpdateCardMethod
        {
            [Fact]
            public void PostToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updateCard = new ProjectCardUpdate("someNewNote");

                client.UpdateCard("fake", "repo", 1, updateCard);

                gitHubClient.Repository.Projects.Received().UpdateCard("fake", "repo", 1, updateCard);
            }

            [Fact]
            public void PostToCorrectURLWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var updateCard = new ProjectCardUpdate("someNewNote");

                client.UpdateCard(1, 2, updateCard);

                gitHubClient.Repository.Projects.Received().UpdateCard(1, 2, updateCard);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var updateCard = new ProjectCardUpdate("someNewNote");

                Assert.Throws<ArgumentNullException>(() => client.UpdateCard(null, "name", 1, updateCard));
                Assert.Throws<ArgumentNullException>(() => client.UpdateCard("owner", null, 1, updateCard));
                Assert.Throws<ArgumentNullException>(() => client.UpdateCard("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.UpdateCard("owner", "", 1, updateCard));
                Assert.Throws<ArgumentException>(() => client.UpdateCard("", "name", 1, updateCard));
            }
        }

        public class TheDeleteCardMethod
        {
            [Fact]
            public void DeletesCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.DeleteCard("fake", "repo", 1);

                gitHubClient.Repository.Projects.Received().DeleteCard("fake", "repo", 1);
            }

            [Fact]
            public void DeletesCorrectURLWithRepositoryID()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);

                client.DeleteCard(1, 2);

                gitHubClient.Repository.Projects.Received().DeleteCard(1, 2);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => client.DeleteCard("owner", null, 1));
                Assert.Throws<ArgumentNullException>(() => client.DeleteCard(null, "name", 1));

                Assert.Throws<ArgumentException>(() => client.DeleteCard("owner", "", 1));
                Assert.Throws<ArgumentException>(() => client.DeleteCard("", "name", 1));
            }
        }

        public class TheMoveCardMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                client.MoveCard("fake", "repo", 1, position);

                gitHubClient.Repository.Projects.Received().MoveCard("fake", "repo", 1, position);
            }

            [Fact]
            public void PostToCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryProjectsClient(gitHubClient);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                client.MoveCard(1, 2, position);

                gitHubClient.Repository.Projects.Received().MoveCard(1, 2, position);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableRepositoryProjectsClient(Substitute.For<IGitHubClient>());
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                Assert.Throws<ArgumentNullException>(() => client.MoveCard(null, "name", 1, position));
                Assert.Throws<ArgumentNullException>(() => client.MoveCard("owner", null, 1, position));
                Assert.Throws<ArgumentNullException>(() => client.MoveCard("owner", "name", 1, null));

                Assert.Throws<ArgumentException>(() => client.MoveCard("owner", "", 1, position));
                Assert.Throws<ArgumentException>(() => client.MoveCard("", "name", 1, position));
            }
        }
    }
}
