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
                () => new RepositoryProjectsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public async Task RequestCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryProjectsClient(connection);

                await client.GetAllForRepository("fake", "repo");

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryProjectsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryProjectsClient(Substitute.For<IApiConnection>());

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
                var client = new RepositoryProjectsClient(connection);

                await client.Get("fake", "repo", 1);

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task RequestCorrectURLWithId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryProjectsClient(connection);

                await client.Get(1, 2);

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2"), null, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryProjectsClient(Substitute.For<IApiConnection>());

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
                var client = new RepositoryProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.Create("fake", "repo", newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryProjectsClient(connection);
                var newProject = new NewProject("test");

                await client.Create(1, newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryProjectsClient(Substitute.For<IApiConnection>());
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
                var client = new RepositoryProjectsClient(connection);
                var updateProject = new ProjectUpdate("someNewName");

                await client.Update("fake", "repo", 1, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), updateProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task PostToCorrectUrlWithId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryProjectsClient(connection);
                var updateProject = new ProjectUpdate("someNewName");

                await client.Update(1, 2, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects/2"), updateProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new RepositoryProjectsClient(Substitute.For<IApiConnection>());
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
                var client = new RepositoryProjectsClient(connection);

                await client.Delete("fake", "repo", 1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/projects/1"), Arg.Is<object>(new object()), "application/vnd.github.inertia-preview+json");
            }
        }
    }
}
