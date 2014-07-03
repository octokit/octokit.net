using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryDeployKeysClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryDeployKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);

                deployKeysClient.Get("user", "repo", 42);

                githubClient.Repository.DeployKeys.Received(1).Get("user", "repo", 42);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get(null, "repo", 42));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("", "repo", 42));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get("user", null, 42));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("user", "", 42));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);

                deployKeysClient.GetAll("user", "repo");

                githubClient.Connection.Received(1).Get<List<DeployKey>>(
                    new Uri("repos/user/repo/keys", UriKind.Relative), null, null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo"));
                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("", "repo"));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", null));
                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("user", ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var githubClient = Substitute.For<IGitHubClient>();
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(githubClient);
                var data = new NewDeployKey { Key = "ABC123", Title = "user@repo" };

                deployKeysClient.Create("user", "repo", data);

                githubClient.Repository.DeployKeys.Received(1).Create("user", "repo", data);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new ObservableRepositoryDeployKeysClient(Substitute.For<IGitHubClient>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create(null, "repo", new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("", "repo", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", null, new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", "repo", null));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Title = "user@repo" }));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123" }));
            }
        }
    }
}
