using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ProjectClientTests
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

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), Args.EmptyDictionary, "application/vnd.github.inertia-preview+json", Args.ApiOptions);
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public async Task RequestCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForOrganization("org");

                connection.Received().GetAll<Project>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"), Args.EmptyDictionary, "application/vnd.github.inertia-preview+json", Args.ApiOptions);
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Get(1);

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "projects/1"), null, "application/vnd.github.inertia-preview+json");
            }
        }

        public class TheCreateForRepositoryMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.CreateForRepository(1, newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newProject = new NewProject("someName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForRepository(1, null));
            }
        }

        public class TheCreateForOrganizationMethod
        {
            [Fact]
            public async Task PostToCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.CreateForOrganization("org", newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"), newProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newProject = new NewProject("someName");

                Assert.ThrowsAsync<ArgumentException>(() => client.CreateForOrganization("", newProject));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization("org", null));
                Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization(null, newProject));
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

                await client.Update(1, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "projects/1"), updateProject, "application/vnd.github.inertia-preview+json");
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var updateProject = new ProjectUpdate("someNewName");

                Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.Delete(1);

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "projects/1"), Arg.Any<object>(), "application/vnd.github.inertia-preview+json");
            }
        }
    }
}
