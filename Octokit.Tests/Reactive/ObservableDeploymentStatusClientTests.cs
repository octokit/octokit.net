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
            public void EnsureNonWhitespaceArguments()
            {
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async whitespace => await _client.GetAll(whitespace, "repo", 1));
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
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
            readonly IGitHubClient _githubClient = Substitute.For<IGitHubClient>();
            readonly ObservableDeploymentStatusClient _client;

            public TheCreateMethod()
            {
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
            public void EnsureNonWhitespaceArguments()
            {
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async ws => await _client.GetAll(ws, "repo", 1));
                AssertEx.ThrowsWhenGivenWhitespaceArgument(
                    async ws => await _client.GetAll("owner", ws, 1));
            }

            [Fact]
            public void CallsIntoDeploymentStatusClient()
            {
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
