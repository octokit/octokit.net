using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepoCollaboratorsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepoCollaboratorsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            private readonly IGitHubClient _githubClient;
            private readonly IObservableRepoCollaboratorsClient _client;
            private const string owner = "owner";
            private const string name = "name";
            private const int repositoryId = 1;

            public TheGetAllMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(null, name));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(owner, null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(owner, name, null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(repositoryId, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                Assert.Throws<ArgumentException>(() => _client.GetAll("", name));
                Assert.Throws<ArgumentException>(() => _client.GetAll(owner, ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(whitespace, name));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(owner, whitespace));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedUrl = string.Format("repos/{0}/{1}/collaborators", owner, name);

                _client.GetAll(owner, name);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                        Arg.Any<string>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var expectedUrl = string.Format("repositories/{0}/collaborators", repositoryId);

                _client.GetAll(repositoryId);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                        Arg.Any<string>());
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var expectedUrl = string.Format("repos/{0}/{1}/collaborators", owner, name);

                // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2),
                        null);

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1),
                        null);

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                        null);
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptionsAndRepositoryId()
            {
                var expectedUrl = string.Format("repositories/{0}/collaborators", repositoryId);

                // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                _client.GetAll(repositoryId, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2),
                        null);

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(repositoryId, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1),
                        null);

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(repositoryId, options);
                _githubClient.Connection.Received(1)
                    .Get<List<User>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0),
                        null);
            }
        }

        public class TheIsCollaboratorMethod
        {
            private readonly IGitHubClient _githubClient;
            private IObservableRepoCollaboratorsClient _client;

            public TheIsCollaboratorMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var deploymentsClient = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                _githubClient.Repository.Collaborator.Returns(deploymentsClient);
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => _client.IsCollaborator(null, "repo", "user"));
                Assert.Throws<ArgumentNullException>(() => _client.IsCollaborator("owner", null, "user"));
                Assert.Throws<ArgumentNullException>(() => _client.IsCollaborator("owner", "repo", null));
                Assert.Throws<ArgumentNullException>(() => _client.IsCollaborator(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.IsCollaborator("", "repo", "user"));
                Assert.Throws<ArgumentException>(() => _client.IsCollaborator("owner", "", "user"));
                Assert.Throws<ArgumentException>(() => _client.IsCollaborator(1, ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.IsCollaborator(whitespace, "repo", "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.IsCollaborator("owner", whitespace, "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.IsCollaborator(1, whitespace));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();

                _client.IsCollaborator(1, "user");

                _githubClient.Repository.Collaborator.Received(1).IsCollaborator(Arg.Is(1),
                    Arg.Is("user"));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                _client.IsCollaborator(1, "user");

                _githubClient.Repository.Collaborator.Received(1).IsCollaborator(Arg.Is(1),
                    Arg.Is("user"));
            }
        }

        public class TheAddMethod
        {
            private readonly IGitHubClient _githubClient;
            private IObservableRepoCollaboratorsClient _client;

            public TheAddMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var collaboratorsClient = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                _githubClient.Repository.Collaborator.Returns(collaboratorsClient);
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => _client.Add(null, "repo", "user"));
                Assert.Throws<ArgumentNullException>(() => _client.Add("owner", null, "user"));
                Assert.Throws<ArgumentNullException>(() => _client.Add("owner", "repo", null));
                Assert.Throws<ArgumentNullException>(() => _client.Add(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.Add("", "repo", "user"));
                Assert.Throws<ArgumentException>(() => _client.Add("owner", "", "user"));
                Assert.Throws<ArgumentException>(() => _client.Add(1, ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Add(whitespace, "repo", "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Add("owner", whitespace, "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Add(1, whitespace));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();

                _client.Add("owner", "repo", "user");

                _githubClient.Repository.Collaborator.Received(1).Add(Arg.Is("owner"),
                    Arg.Is("repo"),
                    Arg.Is("user"));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                _client.Add(1, "user");

                _githubClient.Repository.Collaborator.Received(1).Add(Arg.Is(1),
                    Arg.Is("user"));
            }
        }

        public class TheInviteMethod
        {
            private readonly IGitHubClient _githubClient;
            private IObservableRepoCollaboratorsClient _client;

            public TheInviteMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var collaboratorsClient = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                _githubClient.Repository.Collaborator.Returns(collaboratorsClient);
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();
                var permission = new CollaboratorRequest(Permission.Push);

                Assert.Throws<ArgumentNullException>(() => _client.Invite(null, "repo", "user", permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", null, "user", permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", "repo", null, permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", "repo", "user", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();
                var permission = new CollaboratorRequest(Permission.Push);

                Assert.Throws<ArgumentException>(() => _client.Invite("", "repo", "user", permission));
                Assert.Throws<ArgumentException>(() => _client.Invite("owner", "", "user", permission));
                Assert.Throws<ArgumentException>(() => _client.Invite("owner", "repo", "", permission));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();
                var permission = new CollaboratorRequest(Permission.Push);

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Invite(whitespace, "repo", "user", permission));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Invite("owner", whitespace, "user", permission));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Invite("owner", "repo", whitespace, permission));
            }

            [Fact]
            public void CallsInviteOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();
                var permission = new CollaboratorRequest(Permission.Push);

                _client.Invite("owner", "repo", "user", permission);

                _githubClient.Repository.Collaborator.Received(1).Invite(Arg.Is("owner"),
                    Arg.Is("repo"),
                    Arg.Is("user"),
                    Arg.Is(permission));
            }
        }

        public class TheDeleteMethod
        {
            private readonly IGitHubClient _githubClient;
            private IObservableRepoCollaboratorsClient _client;

            public TheDeleteMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var deploymentsClient = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                _githubClient.Repository.Collaborator.Returns(deploymentsClient);
                _client = new ObservableRepoCollaboratorsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => _client.Delete(null, "repo", "user"));
                Assert.Throws<ArgumentNullException>(() => _client.Delete("owner", null, "user"));
                Assert.Throws<ArgumentNullException>(() => _client.Delete("owner", "repo", null));
                Assert.Throws<ArgumentNullException>(() => _client.Delete(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.Delete("", "repo", "user"));
                Assert.Throws<ArgumentException>(() => _client.Delete("owner", "", "user"));
                Assert.Throws<ArgumentException>(() => _client.Delete(1, ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Delete(whitespace, "repo", "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Delete("owner", whitespace, "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Delete(1, whitespace));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();

                _client.Delete("owner", "repo", "user");

                _githubClient.Repository.Collaborator.Received(1).Delete(Arg.Is("owner"),
                    Arg.Is("repo"),
                    Arg.Is("user"));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                _client.Delete(1, "user");

                _githubClient.Repository.Collaborator.Received(1).Delete(Arg.Is(1),
                    Arg.Is("user"));
            }
        }
    }
}