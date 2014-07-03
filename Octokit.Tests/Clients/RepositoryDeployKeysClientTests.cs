using NSubstitute;
using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepositoryDeployKeysClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryDeployKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void GetsADeployKey()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                deployKeysClient.Get("user", "repo", 42);

                apiConnection.Received().Get<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys/42"),
                    null);
            }

            [Fact]
            public void EnsureNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get(null, "repo", 1));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("", "repo", 1));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Get("user", null, 1));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Get("user", "", 1));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void GetsAListOfDeployKeys()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                deployKeysClient.GetAll("user", "repo");

                apiConnection.Received().GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo"));
                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("", "repo"));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.GetAll("user", null));
                Assert.Throws<ArgumentException>(() => deployKeysClient.GetAll("user", ""));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void SendsCreateToCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123", Title = "user@repo" });

                apiConnection.Received().Post<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"),
                    Args.NewDeployKey);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create(null, "repo", new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("", "repo", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", null, new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "", new NewDeployKey()));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Create("user", "repo", null));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey()));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123" }));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Title = "user@repo" }));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void DeletesCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                deployKeysClient.Delete("user", "repo", 42);

                apiConnection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys/42"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Delete(null, "repo", 1));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Delete("", "repo", 1));
                Assert.Throws<ArgumentNullException>(() => deployKeysClient.Delete("user", null, 1));
                Assert.Throws<ArgumentException>(() => deployKeysClient.Delete("user", "", 1));
            }
        }
    }
}
