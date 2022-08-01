using System;
using System.Collections.Generic;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableProjectColumnsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                () => new ObservableProjectColumnsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableProjectColumnsClient(gitHubClient);

                client.GetAll(1);

                connection.Received().Get<List<ProjectColumn>>(
                    Arg.Is<Uri>(u => u.ToString() == "projects/1/columns"),
                    Args.EmptyDictionary);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null).ToTask());
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);

                client.Get(1);

                gitHubClient.Repository.Project.Column.Received().Get(1);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);
                var newProjectColumn = new NewProjectColumn("someName");

                client.Create(1, newProjectColumn);

                gitHubClient.Repository.Project.Column.Received().Create(1, newProjectColumn);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableProjectColumnsClient(Substitute.For<IGitHubClient>());
                var newProjectColumn = new NewProjectColumn("someName");

                Assert.Throws<ArgumentNullException>(() => client.Create(1, null));
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);
                var updatePorjectColumn = new ProjectColumnUpdate("someNewName");

                client.Update(1, updatePorjectColumn);

                gitHubClient.Repository.Project.Column.Received().Update(1, updatePorjectColumn);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableProjectColumnsClient(Substitute.For<IGitHubClient>());
                var updateProjectColumn = new ProjectColumnUpdate("someNewName");

                Assert.Throws<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);

                client.Delete(1);

                gitHubClient.Repository.Project.Column.Received().Delete(1);
            }
        }

        public class TheMoveMethod
        {
            [Fact]
            public void PostsToCorrectURL()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableProjectColumnsClient(gitHubClient);
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                client.Move(1, position);

                gitHubClient.Repository.Project.Column.Received().Move(1, position);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = new ObservableProjectColumnsClient(Substitute.For<IGitHubClient>());
                var position = new ProjectColumnMove(ProjectColumnPosition.First, null);

                Assert.Throws<ArgumentNullException>(() => client.Move(1, null));
            }
        }
    }
}
