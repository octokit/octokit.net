using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationSecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationSecretsClient(null));
            }
        }

        public class GetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.GetPublicKey("org");

                connection.Received()
                    .Get<SecretsPublicKey>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/public-key"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey(""));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.GetAll("org");

                connection.Received()
                    .Get<OrganizationSecretsCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.Get("org", "secret");

                connection.Received()
                    .Get<OrganizationSecret>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", ""));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);
                var upsertSecret = new UpsertOrganizationSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId",
                    Visibility = "private"
                };
                await client.CreateOrUpdate("org", "secret", upsertSecret);

                connection.Received()
                    .Put<OrganizationSecret>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret"), upsertSecret);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                var upsertSecret = new UpsertOrganizationSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "secret", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "secret", new UpsertOrganizationSecret()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", upsertSecret));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.Delete("org", "secret");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", ""));
            }
        }

        public class GetSelectedRepositoriesForSecretMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationSecretsClient(connection);

                await client.GetSelectedRepositoriesForSecret("org", "secret");

                connection.Received()
                    .Get<OrganizationSecretRepositoryCollection>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/actions/secrets/secret/repositories"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForSecret(null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetSelectedRepositoriesForSecret("org", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForSecret("", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetSelectedRepositoriesForSecret("org", ""));
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
