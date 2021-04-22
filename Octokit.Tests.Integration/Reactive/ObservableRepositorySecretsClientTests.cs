using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Octokit.Reactive;
using System.Reactive.Linq;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Reactive.Clients
{
    public class ObservableRepositorySecretsClientTests
    {
        /// <summary>
        /// Fill these in for tests to work
        /// </summary>
        internal const string OWNER = "";
        internal const string REPO = "";

        public class GetPublicKeyMethod
        {
            [IntegrationTest]
            public async Task GetPublicKey()
            {
                var github = Helper.GetAuthenticatedClient();
                var client = new ObservableRepositorySecretsClient(github);

                var keyObservable = client.GetPublicKey(OWNER, REPO);
                var key = await keyObservable;

                Assert.True(!string.IsNullOrWhiteSpace(key.KeyId));
            }
        }

        public class GetAllMethod
        {
            [IntegrationTest]
            public async Task GetSecrets()
            {
                var github = Helper.GetAuthenticatedClient();
                var client = new ObservableRepositorySecretsClient(github);

                var secretsObservable = client.GetAll(OWNER, REPO);
                var secrets = await secretsObservable;

                Assert.NotEmpty(secrets.Secrets);
            }
        }

        /// <summary>
        /// Please create a secret in your specific repo called TEST
        /// </summary>
        public class GetMethod
        {
            [IntegrationTest]
            public async Task GetSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var client = new ObservableRepositorySecretsClient(github);
                var secretName = "TEST";

                var secretsObservable = client.Get(OWNER, REPO, secretName);
                var secret = await secretsObservable;

                Assert.NotNull(secret);
                Assert.True(secret.Name == secretName);
            }
        }

        public class CreateOrUpdateMethod
        {
#if SODIUM_CORE_AVAILABLE
            [IntegrationTest]
            public async Task UpsertSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var client = new ObservableRepositorySecretsClient(github);
                var now = DateTime.Now;

                var keyObservable = client.GetPublicKey(OWNER, REPO);
                var key = await keyObservable;
                var upsertValue = GetSecretForCreate("value", key);

                var secretObservable = client.CreateOrUpdate(OWNER, REPO, "REACTIVE_UPSERT_TEST", upsertValue);
                var secret = await secretObservable;

                Assert.NotNull(secret);
                Assert.True(secret.UpdatedAt > now);
            }
#endif
        }

        public class DeleteMethod
        {
#if SODIUM_CORE_AVAILABLE
            [IntegrationTest]
            public async Task DeleteSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var client = new ObservableRepositorySecretsClient(github);

                var secretName = "DELETE_TEST";

                var keyObservable = client.GetPublicKey(OWNER, REPO);
                var key = await keyObservable;
                var upsertValue = GetSecretForCreate("value", key);

                var createObservable = client.CreateOrUpdate(OWNER, REPO, secretName, upsertValue);
                await createObservable;

                var deleteObservable = client.Delete(OWNER, REPO, secretName);
                await deleteObservable;

            }
#endif
        }
#if SODIUM_CORE_AVAILABLE
        private static UpsertRepositorySecret GetSecretForCreate(string secretValue, SecretsPublicKey key)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretValue);
            var publicKey = Convert.FromBase64String(key.Key);
            var sealedPublicKeyBox = SealedPublicKeyBox.Create(secretBytes, publicKey);

            var upsertValue = new UpsertRepositorySecret
            {
                EncryptedValue = Convert.ToBase64String(sealedPublicKeyBox),
                KeyId = key.KeyId

            };

            return upsertValue;
        }
#endif
    }
}
