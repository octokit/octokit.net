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
        public class TheGetAllMethod
        {
            private readonly IGitHubClient _githubClient;
            private readonly IObservableRepoCollaboratorsClient _client;
            private const string owner = "owner";
            private const string name = "name";

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
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.Add("", "repo", "user"));
                Assert.Throws<ArgumentException>(() => _client.Add("owner", "", "user"));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Add(whitespace, "repo", "user"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Add("owner", whitespace, "user"));
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
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepoCollaboratorsClient(null));
            }
        }
    }
}
