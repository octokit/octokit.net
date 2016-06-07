using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableAssigneesClientTests
    {
        private const string owner = "owner";
        private const string name = "name";
        private const string assignee = "assignee";

        public class TheGetAllMethod
        {
            private readonly Uri _expectedUri;

            public TheGetAllMethod()
            {
                var uri = string.Format("repos/{0}/{1}/assignees", owner, name);
                _expectedUri = new Uri(uri, UriKind.Relative);
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(github);

                client.GetAllForRepository(owner, name);

                github.Connection.Received(1).Get<List<User>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0), null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOption()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(github);

                client.GetAllForRepository(owner, name, new ApiOptions { PageSize = 1, StartPage = 1});

                github.Connection.Received(1).Get<List<User>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(github);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, name));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(owner, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(owner, name, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = CreateFixtureWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository(string.Empty, name));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository(owner, string.Empty));
            }
        }

        private static ObservableAssigneesClient CreateFixtureWithNonReactiveClient()
        {
            var nonreactiveClient = new AssigneesClient(Substitute.For<IApiConnection>());
            var github = Substitute.For<IGitHubClient>();
            github.Issue.Assignee.Returns(nonreactiveClient);
            return new ObservableAssigneesClient(github);
        }

        public class TheCheckAssigneeMethod
        {
            [Fact]
            public void CallsCheckAssigneeOnClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(github);

                client.CheckAssignee(owner, name, assignee);

                github.Issue.Assignee.Received(1).CheckAssignee(Arg.Is(owner), Arg.Is(name), Arg.Is(assignee));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var client = CreateFixtureWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(null, name, assignee));
                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(owner, null, assignee));
                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(owner, name, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = CreateFixtureWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => client.CheckAssignee(string.Empty, name, assignee));
                Assert.Throws<ArgumentException>(() => client.CheckAssignee(owner, string.Empty, assignee));
                Assert.Throws<ArgumentException>(() => client.CheckAssignee(owner, name, string.Empty));
            }
        }

        public class TheAddAssigneesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.AddAssignees("fake", "repo", 2, newAssignees);

                gitHubClient.Issue.Assignee.Received().AddAssignees("fake", "repo", 2, newAssignees);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(githubClient);
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                Assert.Throws<ArgumentNullException>(() => client.AddAssignees(null, null, 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees(null, "name", 2, null));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees("owner", null, 2, null));

                Assert.Throws<ArgumentException>(() => client.AddAssignees("owner", "", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.AddAssignees("", "name", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.AddAssignees("", "", 2, newAssignees));
            }
        }

        public class TheRemoveAssigneesMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.RemoveAssignees("fake", "repo", 2, newAssignees);

                gitHubClient.Issue.Assignee.Received().RemoveAssignees("fake", "repo", 2, newAssignees);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(githubClient);
                var newAssignees = new AssigneesUpdate(new List<string>() { "assignee1", "assignee2" });

                Assert.Throws<ArgumentNullException>(() => client.AddAssignees(null, null, 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees(null, "name", 2, null));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees("owner", null, 2, null));

                Assert.Throws<ArgumentException>(() => client.AddAssignees("owner", "", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.AddAssignees("", "name", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.AddAssignees("", "", 2, newAssignees));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableAssigneesClient(null));
            }
        }
    }
}