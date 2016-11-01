using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ProjectCardsClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/cards"), "application/vnd.github.inertia-preview+json");
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);

                await client.Get(1);

                connection.Received().Get<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"), null, "application/vnd.github.inertia-preview+json");
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var newCard = new NewProjectCard("someNote");

                await client.Create(1, newCard);

                connection.Received().Post<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/cards"), newCard, "application/vnd.github.inertia-preview+json");
            }
                        
            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var newCard = new NewProjectCard("someNote");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var updateCard = new ProjectCardUpdate("someNewNote");

                await client.Update(1, updateCard);

                connection.Received().Patch<ProjectCard>(Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"), updateCard, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var updateCard = new ProjectCardUpdate("someNewNote");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);

                await client.Delete(1);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }
        }

        public class TheMoveMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                await client.Move(1, position);

                connection.Connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1/moves"), position, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Move(1, position));
            }
        }
    }
}
