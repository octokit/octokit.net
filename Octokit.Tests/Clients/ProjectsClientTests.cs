using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ProjectsClientTests
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

                await client.GetAllForRepository("owner", "repo");

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/projects"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRequestParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForRepository("owner", "repo", new ProjectRequest(ItemStateFilter.All));

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForRepository(1);

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRequestParameterWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForRepository(1, new ProjectRequest(ItemStateFilter.All));

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsureNonNullOrEmptyArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", (ProjectRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", new ProjectRequest(ItemStateFilter.All), null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", null, ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", ""));
            }

            [Fact]
            public async Task EnsureNonNullArgumentsWithRepositoryId()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ProjectRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, new ProjectRequest(ItemStateFilter.All), null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None));
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

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"),
                    Args.EmptyDictionary,
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestCorrectUrlWithRequestParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);

                await client.GetAllForOrganization("org", new ProjectRequest(ItemStateFilter.Closed));

                connection.Received().GetAll<Project>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsureNonNullOrEmptyArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization(""));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, new ProjectRequest(ItemStateFilter.All)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", new ProjectRequest(ItemStateFilter.All)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ProjectRequest)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, new ProjectRequest(ItemStateFilter.All), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", new ProjectRequest(ItemStateFilter.All), ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", new ProjectRequest(ItemStateFilter.All), null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", null, ApiOptions.None));
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

                connection.Received().Get<Project>(Arg.Is<Uri>(u => u.ToString() == "projects/1"), null);
            }
        }

        public class TheCreateForRepositoryMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.CreateForRepository(1, newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"), newProject);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForRepository(1, null));
            }
        }

        public class TheCreateForOrganizationMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var newProject = new NewProject("someName");

                await client.CreateForOrganization("org", newProject);

                connection.Received().Post<Project>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"), newProject);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());
                var newProject = new NewProject("someName");

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateForOrganization("", newProject));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization(null, newProject));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task PostsToCorrectURL()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ProjectsClient(connection);
                var updateProject = new ProjectUpdate();

                await client.Update(1, updateProject);

                connection.Received().Patch<Project>(Arg.Is<Uri>(u => u.ToString() == "projects/1"), updateProject);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ProjectsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
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

                connection.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "projects/1"), Arg.Any<object>());
            }
        }
    }
}
