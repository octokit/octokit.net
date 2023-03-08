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
            private const long repositoryId = 1;

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
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(owner, name, options: null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(repositoryId, options: null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(owner, name, request: null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(repositoryId, request: null));
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
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var expectedUrl = string.Format("repositories/{0}/collaborators", repositoryId);

                _client.GetAll(repositoryId);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));
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
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 3));

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilter()
            {
                var expectedUrl = string.Format("repos/{0}/{1}/collaborators", owner, name);

                var request = new RepositoryCollaboratorListRequest();

                _client.GetAll(owner, name, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "all"));

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct
                };

                _client.GetAll(owner, name, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "direct"));

                // PageCount is setted => none of options in dictionary
                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside
                };

                _client.GetAll(owner, name, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "outside"));
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
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 3));

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(repositoryId, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(repositoryId, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));
            }

            [Fact]
            public void RequestsCorrectUrlWithCollaboratorFilterAndRepositoryId()
            {
                var expectedUrl = string.Format("repositories/{0}/collaborators", repositoryId);

                var request = new RepositoryCollaboratorListRequest();

                _client.GetAll(repositoryId, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "all"));

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Direct
                };

                _client.GetAll(repositoryId, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "direct"));

                request = new RepositoryCollaboratorListRequest
                {
                    Affiliation = CollaboratorAffiliation.Outside
                };

                _client.GetAll(repositoryId, request);
                _githubClient.Connection.Received(1)
                    .Get<List<Collaborator>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(d => d["affiliation"] == "outside"));
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

                var id = 1L;
                _client.IsCollaborator(id, "user");

                _githubClient.Repository.Collaborator.Received(1).IsCollaborator(Arg.Is(id),
                    Arg.Is("user"));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                var id = 1L;
                _client.IsCollaborator(id, "user");

                _githubClient.Repository.Collaborator.Received(1).IsCollaborator(Arg.Is(id),
                    Arg.Is("user"));
            }
        }

        public class TheReviewPermissionMethod
        {
            private readonly IGitHubClient _gitHubClient;
            private IObservableRepoCollaboratorsClient _client;

            public TheReviewPermissionMethod()
            {
                _gitHubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableRepoCollaboratorsClient(_gitHubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var collaboratorsClient = new RepoCollaboratorsClient(Substitute.For<IApiConnection>());
                _gitHubClient.Repository.Collaborator.Returns(collaboratorsClient);
                _client = new ObservableRepoCollaboratorsClient(_gitHubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => _client.ReviewPermission(null, "test", "user1"));
                Assert.Throws<ArgumentNullException>(() => _client.ReviewPermission("owner", null, "user1"));
                Assert.Throws<ArgumentNullException>(() => _client.ReviewPermission("owner", "test", null));
                Assert.Throws<ArgumentNullException>(() => _client.ReviewPermission(1L, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.ReviewPermission("", "test", "user1"));
                Assert.Throws<ArgumentException>(() => _client.ReviewPermission("owner", "", "user1"));
                Assert.Throws<ArgumentException>(() => _client.ReviewPermission("owner", "test", ""));
                Assert.Throws<ArgumentException>(() => _client.ReviewPermission(1L, ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.ReviewPermission(whitespace, "repo", "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.ReviewPermission("owner", whitespace, "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.ReviewPermission("owner", "repo", whitespace));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.ReviewPermission(1L, whitespace));
            }

            [Fact]
            public void CallsReviewPermissionOnRegularRepoCollaboratorsClient()
            {
                SetupWithoutNonReactiveClient();

                _client.ReviewPermission("owner", "repo", "user");

                _gitHubClient.Repository.Collaborator.Received(1).ReviewPermission(
                    Arg.Is("owner"),
                    Arg.Is("repo"),
                    Arg.Is("user"));
            }

            [Fact]
            public void CallsReviewPermissionOnRegularRepoCollaboratorsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                var id = 1L;
                _client.ReviewPermission(id, "user");

                _gitHubClient.Repository.Collaborator.Received(1).ReviewPermission(
                    Arg.Is(id),
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

                var id = 1L;
                _client.Add(id, "user");

                _githubClient.Repository.Collaborator.Received(1).Add(Arg.Is(id),
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
                var permission = new CollaboratorRequest("push");

                Assert.Throws<ArgumentNullException>(() => _client.Invite(null, "repo", "user", permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", null, "user", permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", "repo", null, permission));
                Assert.Throws<ArgumentNullException>(() => _client.Invite("owner", "repo", "user", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();
                var permission = new CollaboratorRequest("push");

                Assert.Throws<ArgumentException>(() => _client.Invite("", "repo", "user", permission));
                Assert.Throws<ArgumentException>(() => _client.Invite("owner", "", "user", permission));
                Assert.Throws<ArgumentException>(() => _client.Invite("owner", "repo", "", permission));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();
                var permission = new CollaboratorRequest("push");

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
                var permission = new CollaboratorRequest("push");

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

                var id = 1L;
                _client.Delete(id, "user");

                _githubClient.Repository.Collaborator.Received(1).Delete(Arg.Is(id),
                    Arg.Is("user"));
            }
        }
    }
}
