using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive.Clients;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableDeploymentsClientTests
    {
        public class TheGetAllMethod
        {
            private readonly IGitHubClient _githubClient;
            private readonly ObservableDeploymentsClient _client;
            private const string owner = "owner";
            private const string name = "name";
            private const long repositoryId = 1;

            public TheGetAllMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableDeploymentsClient(_githubClient);
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
                var expectedUrl = string.Format("repos/{0}/{1}/deployments", owner, name);

                _client.GetAll(owner, name);

                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var expectedUrl = string.Format("repositories/{0}/deployments", repositoryId);

                _client.GetAll(repositoryId);

                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var expectedUrl = string.Format("repos/{0}/{1}/deployments", owner, name);

                // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(owner, name, options);
                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0));
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var expectedUrl = string.Format("repositories/{0}/deployments", repositoryId);

                // all properties are setted => only 2 options (StartPage, PageSize) in dictionary
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                _client.GetAll(repositoryId, options);

                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 2));

                // StartPage is setted => only 1 option (StartPage) in dictionary
                options = new ApiOptions
                {
                    StartPage = 1
                };

                _client.GetAll(repositoryId, options);

                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 1));

                // PageCount is setted => none of options in dictionary
                options = new ApiOptions
                {
                    PageCount = 1
                };

                _client.GetAll(repositoryId, options);

                _githubClient.Connection.Received(1)
                    .Get<List<Deployment>>(Arg.Is<Uri>(u => u.ToString() == expectedUrl),
                        Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0));
            }
        }

        public class TheCreateMethod
        {
            private readonly IGitHubClient _githubClient;
            private ObservableDeploymentsClient _client;

            public TheCreateMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
            }

            private void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            private void SetupWithNonReactiveClient()
            {
                var deploymentsClient = new DeploymentsClient(Substitute.For<IApiConnection>());
                _githubClient.Repository.Deployment.Returns(deploymentsClient);
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentNullException>(() => _client.Create(null, "repo", new NewDeployment("ref")));
                Assert.Throws<ArgumentNullException>(() => _client.Create("owner", null, new NewDeployment("ref")));
                Assert.Throws<ArgumentNullException>(() => _client.Create("owner", "repo", null));

                Assert.Throws<ArgumentNullException>(() => _client.Create(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                Assert.Throws<ArgumentException>(() => _client.Create("", "repo", new NewDeployment("ref")));
                Assert.Throws<ArgumentException>(() => _client.Create("owner", "", new NewDeployment("ref")));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create(whitespace, "repo", new NewDeployment("ref")));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create("owner", whitespace, new NewDeployment("ref")));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();

                var newDeployment = new NewDeployment("ref");

                _client.Create("owner", "repo", newDeployment);

                _githubClient.Repository.Deployment.Received(1).Create("owner", "repo", newDeployment);
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClientWithRepositoryId()
            {
                SetupWithoutNonReactiveClient();

                var newDeployment = new NewDeployment("ref");

                _client.Create(1, newDeployment);

                _githubClient.Repository.Deployment.Received(1).Create(1, newDeployment);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableDeploymentsClient(null));
            }

            [Fact]
            public void SetsStatusesClient()
            {
                var client = new ObservableDeploymentsClient(Substitute.For<IGitHubClient>());
                Assert.NotNull(client.Status);
            }
        }
    }
}
