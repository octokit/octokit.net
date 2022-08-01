using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ProjectCardsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ProjectCardsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<ProjectCard>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/cards"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task SendsAppropriateParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);

                await client.GetAll(1, new ProjectCardRequest(ProjectCardArchivedStateFilter.NotArchived));

                connection.Received().GetAll<ProjectCard>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/cards"),
                    Arg.Is<Dictionary<string, string>>(x =>
                         x.Count == 1
                         && x["archived_state"] == "not_archived"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var request = new ProjectCardRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ProjectCardRequest)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, request, null));
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

                connection.Received().Get<ProjectCard>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"),
                    null);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var newCard = new NewProjectCard("someNote");

                await client.Create(1, newCard);

                connection.Received().Post<ProjectCard>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/cards"),
                    newCard);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var newCard = new NewProjectCard("someNote");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var updateCard = new ProjectCardUpdate
                {
                    Note = "someNewNote"
                };

                await client.Update(1, updateCard);

                connection.Received().Patch<ProjectCard>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"),
                    updateCard);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());
                var updateCard = new ProjectCardUpdate
                {
                    Note = "someNewNote"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
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

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1"),
                    Arg.Any<object>());
            }
        }

        public class TheMoveMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectCardsClient(connection);
                var position = new ProjectCardMove(ProjectCardPosition.Top, 1, null);

                await client.Move(1, position);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/cards/1/moves"),
                    position,
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectCardsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Move(1, null));
            }
        }
    }
}
