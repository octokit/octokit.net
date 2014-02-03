using NSubstitute;
using Octokit.Reactive.Clients;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

namespace Octokit.Tests.Reactive
{
    public class ObservableDeploymentsClientTests
    {
        const string ExpectedAcceptHeader = "application/vnd.github.cannonball-preview+json";

        public class TheGetAllMethod
        {
            readonly IGitHubClient _githubClient;
            readonly ObservableDeploymentsClient _client;

            public TheGetAllMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll(null, "repo"));
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll("owner", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("", "repo"));
                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("owner", ""));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(whitespace, "repo"));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll("owner", whitespace));
            }

            [Fact]
            public void CallsDeploymentsUrl()
            {
                var expectedUri = ApiUrls.Deployments("owner", "repo");

                _client.GetAll("owner", "repo");
                _githubClient.Connection
                             .Received(1)
                             .GetAsync<List<Deployment>>(Arg.Is(expectedUri),
                                                         Arg.Any<IDictionary<string, string>>(),
                                                         Arg.Any<string>());
            }

            [Fact]
            public void UsesPreviewAcceptHeader()
            {
                _client.GetAll("owner", "repo");
                _githubClient.Connection
                             .Received(1)
                             .GetAsync<List<Deployment>>(Arg.Any<Uri>(),
                                                         Arg.Any<IDictionary<string, string>>(),
                                                         ExpectedAcceptHeader);
            }
        }

        public class TheCreateMethod
        {
            IGitHubClient _githubClient;
            ObservableDeploymentsClient _client;

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
                _githubClient.Deployment.Returns(deploymentsClient);
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create(null, "repo", new NewDeployment()));
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", null, new NewDeployment()));
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", "repo", null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.Create("", "repo", new NewDeployment()));
                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.Create("owner", "", new NewDeployment()));
            }

            [Fact]
            public async Task EnsuresNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();

                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create(whitespace, "repo", new NewDeployment()));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create("owner", whitespace, new NewDeployment()));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                SetupWithoutNonReactiveClient();

                var newDeployment = new NewDeployment();
                _client.Create("owner", "repo", newDeployment);
                _githubClient.Deployment.Received(1).Create(Arg.Is("owner"),
                                                            Arg.Is("repo"),
                                                            Arg.Is(newDeployment));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableDeploymentsClient(null));
            }
        }
    } 
}