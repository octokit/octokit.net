using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableProjectsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableProjectsClient(null));
            }
        }

        public class TheGetAllForRepositoryMethod
        {
            [Fact]
            public void RequestCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForRepository("owner", "repo");

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/projects"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestCorrectUrlWithRequestParameter()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForRepository("owner", "repo", new ProjectRequest(ItemStateFilter.All));

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")));
            }

            [Fact]
            public void RequestCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForRepository(1);

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestCorrectUrlWithRequestParameterWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForRepository(1, new ProjectRequest(ItemStateFilter.All));

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")));
            }

            [Fact]
            public async Task EnsureNonNullOrEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(null, "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", (ProjectRequest)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", (ApiOptions)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", new ProjectRequest(ItemStateFilter.All), null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository("owner", "repo", null, ApiOptions.None).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("", "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForRepository("owner", "").ToTask());
            }

            [Fact]
            public async Task EnsureNonNullArgumentsWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ApiOptions)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, (ProjectRequest)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, new ProjectRequest(ItemStateFilter.All), null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForRepository(1, null, ApiOptions.None).ToTask());
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public void RequestCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForOrganization("org");

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public void RequestCorrectUrlWithRequestParameter()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForOrganization("org", new ProjectRequest(ItemStateFilter.Closed));

                connection.Received().Get<List<Project>>(
                    Arg.Is<Uri>(u => u.ToString() == "orgs/org/projects"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")));
            }

            [Fact]
            public async Task EnsureNonNullOrEmptyArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("").ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ApiOptions)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, new ProjectRequest(ItemStateFilter.All)).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", new ProjectRequest(ItemStateFilter.All)).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", (ProjectRequest)null).ToTask());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization(null, new ProjectRequest(ItemStateFilter.All), ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrganization("", new ProjectRequest(ItemStateFilter.All), ApiOptions.None).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", new ProjectRequest(ItemStateFilter.All), null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrganization("org", null, ApiOptions.None).ToTask());
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                client.Get(1);

                gitHubClient.Repository.Project.Received().Get(1);
            }
        }

        public class TheCreateForRepositoryMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var newProject = new NewProject("someName");

                client.CreateForRepository(1, newProject);

                gitHubClient.Repository.Project.Received().CreateForRepository(1, newProject);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var newProject = new NewProject("someName");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForRepository(1, null).ToTask());
            }
        }

        public class TheCreateForOrganizationMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var newProject = new NewProject("someName");

                client.CreateForOrganization("org", newProject);

                gitHubClient.Repository.Project.Received().CreateForOrganization("org", newProject);
            }

            [Fact]
            public async Task EnsureNonNullOrEmptyArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var newProject = new NewProject("someName");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization(null, newProject).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateForOrganization("", newProject).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateForOrganization("org", null).ToTask());
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var updateProject = new ProjectUpdate { Name = "someNewName" };

                client.Update(1, updateProject);

                gitHubClient.Repository.Project.Received().Update(1, updateProject);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var updateProject = new ProjectUpdate { Name = "someNewName" };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null).ToTask());
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                client.Delete(1);

                gitHubClient.Repository.Project.Received().Delete(1);
            }
        }
    }
}
