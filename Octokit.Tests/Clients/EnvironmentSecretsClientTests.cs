using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnvironmentSecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EnvironmentSecretsClient(null));
            }
        }

        public class GetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentSecretsClient(connection);

                await client.GetPublicKey(1, "envName");

                connection.Received()
                    .Get<SecretsPublicKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets/public-key"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(0, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(1, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey(0, "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey(1, ""));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrlWithoutPageCount()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentSecretsClient(connection);

                await client.GetAll(1, "envName");

                connection.Received()
                    .Get<EnvironmentSecretsCollection>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets"));
            }

			[Fact]
			public async Task RequestsTheCorrectUrlWithPageCount()
			{
				var connection = Substitute.For<IApiConnection>();
				var client = new EnvironmentSecretsClient(connection);

				await client.GetAll(1, "envName", 30);

				connection.Received()
					.Get<EnvironmentSecretsCollection>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets?per_page=30"));
			}

			[Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(0, "envName", 10));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null, 10));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(0, "envName", 10));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(1, "", 10));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentSecretsClient(connection);

                await client.Get(1, "envName", "secret");

                connection.Received()
                    .Get<EnvironmentSecret>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(0, "envName", "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, "envName", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(0, "envName", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, "", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, "envName", ""));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentSecretsClient(connection);
                var upsertSecret = new UpsertEnvironmentSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };
                await client.CreateOrUpdate(1, "envName", "secret", upsertSecret);

                connection.Received()
                    .Put<EnvironmentSecret>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets/secret"), upsertSecret);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentSecretsClient(Substitute.For<IApiConnection>());

                var upsertSecret = new UpsertEnvironmentSecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(0, "envName", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(1, null, "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(1, "envName", null, upsertSecret));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(1, "envName", "secret", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(1, "envName", "secret", new UpsertEnvironmentSecret()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate(0, "envName", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate(1, "", "secret", upsertSecret));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate(1, "envName", "", upsertSecret));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnvironmentSecretsClient(connection);

                await client.Delete(1, "envName", "secret");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/environments/envName/secrets/secret"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new EnvironmentSecretsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(0, "envName", "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null, "secret"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, "envName", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(0, "envName", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, "", "secret"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, "envName", ""));
            }
        }
    }
}
