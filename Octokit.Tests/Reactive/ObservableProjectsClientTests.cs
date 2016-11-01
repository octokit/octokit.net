using NSubstitute;
using Octokit.Reactive;
using System;
using System.Threading.Tasks;
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
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForRepository(1);

                gitHubClient.Received().Repository.Projects.GetAllForRepository(1);
            }
        }

        public class TheGetAllForOrganizationMethod
        {
            [Fact]
            public void RequestCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);

                client.GetAllForOrganization("org");

                gitHubClient.Received().Repository.Projects.GetAllForOrganization("org");
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

                gitHubClient.Repository.Projects.Received().Get(1);
            }                     
        }

        public class TheCreateForRepositoryMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var newProject = new NewProject("someName");

                client.CreateForRepository(1, newProject);

                gitHubClient.Repository.Projects.Received().CreateForRepository(1, newProject);
            }          

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var newProject = new NewProject("someName");

                Assert.Throws<ArgumentNullException>(() => client.CreateForRepository(1, null));               
            }
        }

        public class TheCreateForOrganizationMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var newProject = new NewProject("someName");

                client.CreateForOrganization("org", newProject);

                gitHubClient.Repository.Projects.Received().CreateForOrganization("org", newProject);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var newProject = new NewProject("someName");

                Assert.Throws<ArgumentNullException>(() => client.CreateForOrganization("org", null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectsClient(gitHubClient);
                var updateProject = new ProjectUpdate("someNewName");

                client.Update(1, updateProject);

                gitHubClient.Repository.Projects.Received().Update(1, updateProject);
            }            

            [Fact]
            public void EnsureNonNullArguments()
            {
                var client = new ObservableProjectsClient(Substitute.For<IGitHubClient>());
                var updateProject = new ProjectUpdate("someNewName");

                Assert.Throws<ArgumentNullException>(() => client.Update(1, null));
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

                gitHubClient.Repository.Projects.Received().Delete(1);
            }            
        }
    }
}
