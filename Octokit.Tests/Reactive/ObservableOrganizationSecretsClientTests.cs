using NSubstitute;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationSecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableOrganizationSecretsClient(null));
            }
        }

        public class GetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);

                await client.GetPublicKey("org");

                gitHubClient.Received().Organization.Actions.Secrets.GetPublicKey("org");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey("").ToTask());
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);

                await client.GetAll("org");

                gitHubClient.Received().Organization.Actions.Secrets.GetAll("org");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("").ToTask());
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);

                await client.Get("org", "secret");

                gitHubClient.Received().Organization.Actions.Secrets.Get("org", "secret");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", "").ToTask());
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);
                var upsertSecret = new UpsertOrganizationSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId",
                    Visibility = "private"
                };

                await client.CreateOrUpdate("org", "secret", upsertSecret);

                gitHubClient.Received().Organization.Actions.Secrets.CreateOrUpdate("org", "secret", upsertSecret);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                var upsertSecret = new UpsertOrganizationSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "secret", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "secret", new UpsertOrganizationSecret()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", upsertSecret).ToTask());
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);

                await client.Delete("org", "secret");

                gitHubClient.Received().Organization.Actions.Secrets.Delete("org", "secret");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "").ToTask());
            }
        }

        public class GetSelectedRepositoriesForSecretMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationSecretsClient(gitHubClient);

                await client.GetSelectedRepositoriesForSecret("org", "secret");

                gitHubClient.Received().Organization.Actions.Secrets.GetSelectedRepositoriesForSecret("org", "secret");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationSecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForSecret(null, "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForSecret("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForSecret("", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForSecret("org", "").ToTask());
            }
        }

        public class SetSelectedRepositoriesForSecretMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                var repoIds = new List<long>
                    {
                        1,
                        2,
                        3
                    };
                var repos = new SelectedRepositoryCollection(repoIds);

                await client.SetSelectedRepositoriesForSecret("org", "secret", repos);

                connection.Received()
                    .Put<SelectedRepositoryCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret/repositories"), repos);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                var repoIds = new List<long>
                    {
                        1,
                        2,
                        3
                    };
                var repos = new SelectedRepositoryCollection(repoIds);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForSecret(null, "secret", repos));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForSecret("org", null, repos));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForSecret("org", "secret", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.SetSelectedRepositoriesForSecret("org", "secret", new SelectedRepositoryCollection()));
                await Assert.ThrowsAsync<ArgumentException>(() => client.SetSelectedRepositoriesForSecret("", "secret", repos));
                await Assert.ThrowsAsync<ArgumentException>(() => client.SetSelectedRepositoriesForSecret("org", "", repos));
            }
        }

        public class AddRepoToOrganizationSecretMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.AddRepoToOrganizationSecret("org", "secret", 1);

                connection.Received()
                    .Put(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret/repositories/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepoToOrganizationSecret(null, "secret", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddRepoToOrganizationSecret("org", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRepoToOrganizationSecret("", "secret", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.AddRepoToOrganizationSecret("org", "", 1));
            }
        }

        public class RemoveRepoFromOrganizationSecretMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.RemoveRepoFromOrganizationSecret("org", "secret", 1);

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret/repositories/1"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepoFromOrganizationSecret(null, "secret", 1));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepoFromOrganizationSecret("org", null, 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepoFromOrganizationSecret("", "secret", 1));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepoFromOrganizationSecret("org", "", 1));
            }
        }
    }
}
