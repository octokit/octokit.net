using NSubstitute;
using Octokit.Reactive.Clients;
using Octokit.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Octokit.Tests.Reactive
{
    public class ObservableDeploymentStatusClientTests
    {
        const string ExpectedAcceptHeader = "application/vnd.github.cannonball-preview+json";

        public class TheGetAllMethod
        {
            readonly IGitHubClient _githubClient;
            readonly ObservableDeploymentStatusClient _client;

            public TheGetAllMethod()
            {
                _githubClient = new GitHubClient(Substitute.For<IConnection>());
                _client = new ObservableDeploymentStatusClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll(null, "repo", 1));
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll("owner", null, 1));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("", "repo", 1));
                AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("owner", "", 1));
            }

            [Fact]
            public async Task EnsureNonWhitespaceArguments()
            {
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(whitespace, "repo", 1));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll("owner", whitespace, 1));
            }

            [Fact]
            public void GetsFromCorrectUrl()
            {
                var expectedUri = ApiUrls.DeploymentStatuses("owner", "repo", 1);

                _client.GetAll("owner", "repo", 1);

                _githubClient.Connection
                             .Received(1)
                             .GetAsync<List<DeploymentStatus>>(Arg.Is(expectedUri),
                                                               Arg.Any<IDictionary<string, string>>(),
                                                               Arg.Any<string>());
            }

            [Fact]
            public void UsesPreviewAcceptHeader()
            {
                _client.GetAll("owner", "repo", 1);
                
                _githubClient.Connection
                             .Received(1)
                             .GetAsync<List<DeploymentStatus>>(Arg.Any<Uri>(),
                                                               Arg.Any<IDictionary<string, string>>(),
                                                               Arg.Is(ExpectedAcceptHeader));
            }
        }

        public class TheCreateMethod
        {
            IGitHubClient _githubClient = Substitute.For<IGitHubClient>();
            ObservableDeploymentStatusClient _client;

            public void SetupWithoutNonReactiveClient()
            {
                _client = new ObservableDeploymentStatusClient(_githubClient);
            }

            public void SetupWithNonReactiveClient()
            {
                var deploymentStatusClient = new DeploymentStatusClient(Substitute.For<IApiConnection>());
                _githubClient.Deployment.Status.Returns(deploymentStatusClient);
                _client = new ObservableDeploymentStatusClient(_githubClient);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                SetupWithNonReactiveClient();
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create(null, "repo", 1, new NewDeploymentStatus()));
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", null, 1, new NewDeploymentStatus()));
                await AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", "repo", 1, null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                SetupWithNonReactiveClient();
                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.Create("", "repo", 1, new NewDeploymentStatus()));
                await AssertEx.Throws<ArgumentException>(
                    async () => await _client.Create("owner", "", 1, new NewDeploymentStatus()));
            }

            [Fact]
            public async Task EnsureNonWhitespaceArguments()
            {
                SetupWithNonReactiveClient();
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create(whitespace, "repo", 1, new NewDeploymentStatus()));
                await AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create("owner", whitespace, 1, new NewDeploymentStatus()));
            }

            [Fact]
            public void CallsIntoDeploymentStatusClient()
            {
                SetupWithoutNonReactiveClient();

                var newStatus = new NewDeploymentStatus();
                _client.Create("owner", "repo", 1, newStatus);
                _githubClient.Deployment
                             .Status
                             .Received(1)
                             .Create(Arg.Is("owner"),
                                     Arg.Is("repo"),
                                     Arg.Is(1),
                                     Arg.Is(newStatus));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableDeploymentStatusClient(null));
            }
        }
    }
}
