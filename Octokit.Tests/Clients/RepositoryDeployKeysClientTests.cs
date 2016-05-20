﻿using NSubstitute;
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
            public void GetsADeployKey()
            {
                var apiConnection = Substitute.For<IApiConnection>();
                var deployKeysClient = new RepositoryDeployKeysClient(apiConnection);

                deployKeysClient.Get("user", "repo", 42);

                apiConnection.Received().Get<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys/42"));
            }

            [Fact]
            public async Task EnsureNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Get(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Get("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Get("user", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Get("user", "", 1));
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

                apiConnection.Received().GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"), Args.ApiOptions);
            }

            [Fact]
            public void GetsAListOfDeployKeysWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryDeployKeysClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                client.GetAll("user", "repo", options);

                connection.Received(1)
                    .GetAll<DeployKey>(Arg.Is<Uri>(u => u.ToString() == "repos/user/repo/keys"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, null, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", null, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll(null, "repo", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.GetAll("user", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("user", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.GetAll("", "repo"));
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
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create(null, "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("", "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create("user", null, new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Create("user", "repo", null));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey()));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Key = "ABC123" }));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Create("user", "repo", new NewDeployKey { Title = "user@repo" }));
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
            public async Task EnsuresNonNullArguments()
            {
                var deployKeysClient = new RepositoryDeployKeysClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Delete(null, "repo", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Delete("", "repo", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => deployKeysClient.Delete("user", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => deployKeysClient.Delete("user", "", 1));
            }
        }
    }
}
