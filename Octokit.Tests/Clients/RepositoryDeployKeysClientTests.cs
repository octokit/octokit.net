using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class RepositoryDeployKeysClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryDeployKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.Get("user", "repo", 42);

                apiConnection.Received().Get<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys/42"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.Get(1, 42);

                apiConnection.Received().Get<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/keys/42"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Get(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Get("user", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Get("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Get("user", "", 1));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.GetAll("user", "repo");

                apiConnection.Received().GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.GetAll(1);

                apiConnection.Received().GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/keys"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryDeployKeysClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAll("user", "repo", options);

                connection.Received(1)
                    .GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"),
                        options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryDeployKeysClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.GetAll(1, options);

                connection.Received(1)
                    .GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/keys"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("user", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("", "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("user", "", ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void CreatesCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                var newDeployKey = new NewDeployKey { Key = "ABC123", Title = "user@repo" };

                deployKeysClient.Create("user", "repo", newDeployKey);

                apiConnection.Received().Post<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"),
                    newDeployKey);
            }

            [Fact]
            public void CreatesCorrectUrlWithRepositoryId()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                var newDeployKey = new NewDeployKey { Key = "ABC123", Title = "user@repo" };

                deployKeysClient.Create(1, newDeployKey);

                apiConnection.Received().Post<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/keys"),
                    newDeployKey);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create(null, "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create("user", null, new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create("user", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("", "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "", new NewDeployKey()));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123" }));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Title = "user@repo" }));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task DeletesCorrectUrl()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.Delete("user", "repo", 42);

                apiConnection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys/42"));
            }

            [Fact]
            public async Task DeletesCorrectUrlWithRepositoryId()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                await deployKeysClient.Delete(1, 42);

                apiConnection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/keys/42"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Delete(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Delete("user", null, 1));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Delete("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Delete("user", "", 1));
            }
        }
    }
}
