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

            public TheGetAllMethod()
            {
                _githubClient = Substitute.For<IGitHubClient>();
                _client = new ObservableDeploymentsClient(_githubClient);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => _client.GetAll(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll("owner", null));
                Assert.Throws<ArgumentNullException>(() => _client.GetAll("owner", "repo", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                Assert.Throws<ArgumentException>(() => _client.GetAll("", "repo"));
                Assert.Throws<ArgumentException>(() => _client.GetAll("owner", ""));
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
            public void RequestsCorrectUrl()
            {
                var expectedUri = ApiUrls.Deployments("owner", "repo");

                _client.GetAll("owner", "repo");
                _githubClient.Connection
                             .Received(1)
                             .Get<List<Deployment>>(Arg.Is(expectedUri),
                                                         Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 0), Arg.Any<string>());
            }

            [Fact]
            public void RequestsCorrectUrlWithApiOptions()
            {
                var expectedUri = ApiUrls.Deployments("owner", "repo");
                
                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                _client.GetAll("owner", "repo", options);
                _githubClient.Connection
                             .Received(1)
                             .Get<List<Deployment>>(Arg.Is(expectedUri),
                                                         Arg.Is<IDictionary<string, string>>(dictionary => dictionary.Count == 3), Arg.Any<string>());
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
                _githubClient.Repository.Deployment.Received(1).Create(Arg.Is("owner"),
                                                            Arg.Is("repo"),
                                                            Arg.Is(newDeployment));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableDeploymentsClient(null));
            }
        }
    }
}