using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ProjectColumnsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ProjectColumnsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<ProjectColumn>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/1/columns"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectColumnsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);

                await client.Get(1);

                connection.Received().Get<ProjectColumn>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1"),
                    null);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);
                var newProjectColumn = new NewProjectColumn("someName");

                await client.Create(1, newProjectColumn);

                connection.Received().Post<ProjectColumn>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/1/columns"),
                    newProjectColumn);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectColumnsClient(Substitute.For<IApiConnection>());
                var newProjectColumn = new NewProjectColumn("someName");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);
                var updateProjectColumn = new ProjectColumnUpdate("someNewName");

                await client.Update(1, updateProjectColumn);

                connection.Received().Patch<ProjectColumn>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1"),
                    updateProjectColumn);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectColumnsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);

                await client.Delete(1);

                connection.Connection.Received().Delete(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1"),
                    Arg.Any<object>());
            }
        }

        public class TheMoveMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectColumnsClient(connection);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                await client.Move(1, position);

                connection.Connection.Received().Post(
                    Arg.Is<Uri>(u => u.ToString() == "projects/columns/1/moves"),
                    position,
                    null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectColumnsClient(Substitute.For<IApiConnection>());
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Move(1, null));
            }
        }
    }
}
