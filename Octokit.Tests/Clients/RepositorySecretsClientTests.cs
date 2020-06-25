using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositorySecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new AssigneesClient(null));
            }
        }

        public class GetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositorySecretsClient(connection);

                await client.GetPublicKey("owner", "repo");

                connection.Received()
                    .Get<RepositorySecretsPublicKey>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/secrets/public-key"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositorySecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey("owner", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey("owner", ""));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositorySecretsClient(connection);

                await client.GetAll("owner", "repo");

                connection.Received()
                    .GetAll<RepositorySecret>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/secrets"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositorySecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "repo", ApiOptions.None));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositorySecretsClient(connection);

                await client.Get("owner", "repo", "secret");

                connection.Received()
                    .Get<RepositorySecret>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositorySecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", ""));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositorySecretsClient(connection);
                var upsertSecret = new UpsertRepositorySecret
                {
                    EncryptedValue = "encryptedValue",
                    EncryptionKeyId = "keyId"
                };
                await client.CreateOrUpdate("owner", "repo", "secret", upsertSecret);

                connection.Received()
                    .Put<RepositorySecret>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/secrets/secret"), upsertSecret);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositorySecretsClient(Substitute.For<IApiConnection>());

                var upsertSecret = new UpsertRepositorySecret
                {
                    EncryptedValue = "encryptedValue",
                    EncryptionKeyId = "keyId"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "repo", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", null, upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", "secret", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", "secret", new UpsertRepositorySecret()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "repo", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "repo", "", upsertSecret));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositorySecretsClient(connection);

                await client.Delete("owner", "repo", "secret");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/actions/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositorySecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "repo", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "repo", ""));
            }
        }
    }
}
