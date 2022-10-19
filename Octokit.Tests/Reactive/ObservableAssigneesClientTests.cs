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
        private const long repositoryId = 1;

        public class TheGetAllMethod
        {
            private readonly Uri _expectedUri;
            private readonly Uri _expectedUriWithRepositoryId;

            public TheGetAllMethod()
            {
                var uri = string.Format("repos/{0}/{1}/assignees", owner, name);
                var uriWithRepositoryId = string.Format("repositories/{0}/assignees", repositoryId);

                _expectedUri = new Uri(uri, UriKind.Relative);
                _expectedUriWithRepositoryId = new Uri(uriWithRepositoryId, UriKind.Relative);
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.GetAllForRepository(owner, name);

                gitHubClient.Connection.Received(1).Get<List<User>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.GetAllForRepository(repositoryId);

                gitHubClient.Connection.Received(1).Get<List<User>>(_expectedUriWithRepositoryId,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOption()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    StartPage = 1,
                    PageCount = 1
                };
                client.GetAllForRepository(owner, name, new ApiOptions { PageSize = 1, StartPage = 1 });
                client.GetAllForRepository(owner, name, options);

                gitHubClient.Connection.Received(2).Get<List<User>>(_expectedUri,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOption()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                var options = new ApiOptions
                {
                    PageSize = 1,
                    StartPage = 1,
                    PageCount = 1
                };

                client.GetAllForRepository(repositoryId, options);

                gitHubClient.Connection.Received(1).Get<List<User>>(_expectedUriWithRepositoryId,
                    Arg.Is<Dictionary<string, string>>(dictionary => dictionary.Count == 2));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(null, name));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(owner, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(owner, name, null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllForRepository(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllForRepository(string.Empty, name));
                Assert.Throws<ArgumentException>(() => client.GetAllForRepository(owner, string.Empty));
            }
        }

        public class TheCheckAssigneeMethod
        {
            [Fact]
            public void CallsCheckAssigneeOnClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.CheckAssignee(owner, name, assignee);

                gitHubClient.Issue.Assignee.Received(1).CheckAssignee(owner, name, assignee);
            }

            [Fact]
            public void CallsCheckAssigneeOnClientWithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                client.CheckAssignee(repositoryId, assignee);

                gitHubClient.Issue.Assignee.Received(1).CheckAssignee(repositoryId, assignee);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableAssigneesClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(null, name, assignee));
                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(owner, null, assignee));
                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(owner, name, null));

                Assert.Throws<ArgumentNullException>(() => client.CheckAssignee(1, null));

                Assert.Throws<ArgumentException>(() => client.CheckAssignee(string.Empty, name, assignee));
                Assert.Throws<ArgumentException>(() => client.CheckAssignee(owner, string.Empty, assignee));
                Assert.Throws<ArgumentException>(() => client.CheckAssignee(owner, name, string.Empty));

                Assert.Throws<ArgumentException>(() => client.CheckAssignee(1, string.Empty));
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

                Assert.Throws<ArgumentNullException>(() => client.AddAssignees(null, "name", 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees("name", null, 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.AddAssignees("owner", "name", 2, null));

                Assert.Throws<ArgumentException>(() => client.AddAssignees("owner", "", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.AddAssignees("", "name", 2, newAssignees));
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

                Assert.Throws<ArgumentNullException>(() => client.RemoveAssignees(null, "name", 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.RemoveAssignees("owner", null, 2, newAssignees));
                Assert.Throws<ArgumentNullException>(() => client.RemoveAssignees("owner", "name", 2, null));

                Assert.Throws<ArgumentException>(() => client.RemoveAssignees("owner", "", 2, newAssignees));
                Assert.Throws<ArgumentException>(() => client.RemoveAssignees("", "name", 2, newAssignees));
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
