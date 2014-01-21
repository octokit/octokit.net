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
            public void EnsuresNonNullArguments()
            {
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll(null, "repo"));
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.GetAll("owner", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("", "repo"));
                AssertEx.Throws<ArgumentException>(
                    async () => await _client.GetAll("owner", ""));
            }

            [Fact]
            public void EnsuresNonWhitespaceArguments()
            {
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(whitespace, "repo"));
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
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
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create(null, "repo", new NewDeployment()));
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", null, new NewDeployment()));
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", "repo", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("", "repo", new NewDeployment()));
                AssertEx.Throws<ArgumentNullException>(
                    async () => await _client.Create("owner", "", new NewDeployment()));
            }

            [Fact]
            public void EnsuresNonWhitespaceArguments()
            {
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create(whitespace, "repo", new NewDeployment()));
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.Create("owner", whitespace, new NewDeployment()));
            }

            [Fact]
            public void CallsCreateOnRegularDeploymentsClient()
            {
                var newDeployment = new NewDeployment();
                _client.Create("owner", "repo", newDeployment);
                _githubClient.Deployment.Received(1).Create(Arg.Is("owner"),
                                                            Arg.Is("repo"),
                                                            Arg.Is(newDeployment));
            }
        }

        public class TheCtor
        {
            public void EnsuresArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableDeploymentsClient(null));
            }
        }
    } 
}