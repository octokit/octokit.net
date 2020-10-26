using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Octokit.Tests.Integration.Helpers;
using Xunit;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Clients
{
    /// <summary>
    /// Access to view and update secrets is required for the following tests
    /// </summary>
    public class RespositorySecretsClientTests
    {
        /// <summary>
        /// Fill these in for tests to work
        /// </summary>
        internal const string OWNER = "octokit";
        internal const string REPO = "octokit.net";

        public class GetPublicKeyMethod
        {
            [IntegrationTest]
            public async Task GetPublicKey()
            {
                var github = Helper.GetAuthenticatedClient();

                var key = await github.Repository.Actions.Secrets.GetPublicKey(OWNER, REPO);

                Assert.True(!string.IsNullOrWhiteSpace(key.KeyId));
            }
        }

        public class GetAllMethod
        {
            [IntegrationTest]
            public async Task GetSecrets()
            {
                var github = Helper.GetAuthenticatedClient();

                var secrets = await github.Repository.Actions.Secrets.GetAll(OWNER, REPO);

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

                var secret = await github.Repository.Actions.Secrets.Get(OWNER, REPO, "TEST");

                Assert.NotNull(secret);
                Assert.True(secret.Name == "TEST");
            }
        }

        public class CreateOrUpdateMethod
        {
#if SODIUM_CORE_AVAILABLE
            [IntegrationTest]
            public async Task UpsertSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var now = DateTime.Now;

                var publicKey = await github.Repository.Actions.Secrets.GetPublicKey(OWNER, REPO);
                var upsertValue = GetSecretForCreate("value", publicKey);

                var secret = await github.Repository.Actions.Secrets.CreateOrUpdate(OWNER, REPO, "UPSERT_TEST", upsertValue);

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

                var secretName = "DELETE_TEST";

                var publicKey = await github.Repository.Actions.Secrets.GetPublicKey(OWNER, REPO);
                var upsertValue = GetSecretForCreate("value", publicKey);

                await github.Repository.Actions.Secrets.CreateOrUpdate(OWNER, REPO, secretName, upsertValue);
                await github.Repository.Actions.Secrets.Delete(OWNER, REPO, secretName);

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
