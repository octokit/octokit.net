using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;
using System.Linq;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationSecretsClientTests
    {
        /// <summary>
        /// Fill these in for tests to work
        /// </summary>
        internal const string ORG = "mptolly-test-org";

        public class GetPublicKeyMethod
        {
            [OrganizationTest]
            public async Task GetPublicKey()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;

                Assert.True(!string.IsNullOrWhiteSpace(key.KeyId));
            }
        }

        public class GetAllMethod
        {
            [OrganizationTest]
            public async Task GetSecrets()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);

                var secretsObservable = clients.GetAll(ORG);
                var secrets = await secretsObservable;

                Assert.NotEmpty(secrets.Secrets);
            }
        }

        /// <summary>
        /// Please create a secret in your specific repo called TEST
        /// </summary>
        public class GetMethod
        {
            [OrganizationTest]
            public async Task GetSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);

                var secretObservable = clients.Get(ORG, "TEST");
                var secret = await secretObservable;

                Assert.NotNull(secret);
                Assert.True(secret.Name == "TEST");
            }
        }

        public class CreateOrUpdateMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task UpsertSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);
                var now = DateTime.Now;

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;

                var upsertValue = GetSecretForCreate("value", key);

                var secretObservable = clients.CreateOrUpdate(ORG, "REACTIVE_UPSERT_TEST", upsertValue);
                var secret = await secretObservable;

                Assert.NotNull(secret);
                Assert.True(secret.UpdatedAt > now);
            }
#endif
        }

        public class DeleteMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task DeleteSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);

                var secretName = "REACTIVE_DELETE_TEST";

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;
                var upsertValue = GetSecretForCreate("value", key);

                var createSecretObservable = clients.CreateOrUpdate(ORG, secretName, upsertValue);
                await createSecretObservable;

                var deleteSecretObservable = clients.Delete(ORG, secretName);
                await deleteSecretObservable;
            }
#endif
        }

        public class GetSelectedRepositoriesForSecretMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task GetSelectedRepositoriesForSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);
                var repoClients = new ObservableRepositoriesClient(github);

                var secretName = "REACTIVE_LIST_SELECTED_REPO_TEST";

                var repo = await CreateRepoIfNotExists(repoClients, "reactive-list-secrets-selected-repo-test");

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo });

                var secretObservable = clients.CreateOrUpdate(ORG, secretName, upsertSecret);
                await secretObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(ORG, secretName);
                var visibilityRepos = await visibilityReposObservable;

                Assert.NotEmpty(visibilityRepos.Repositories);
            }
#endif
        }

        public class SetSelectedRepositoriesForSecretMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task SetSelectedRepositoriesForSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);
                var repoClients = new ObservableRepositoriesClient(github);

                var secretName = "REACTIVE_SET_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(repoClients, "reactive-set-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(repoClients, "reactive-set-secrets-selected-repo-test-2");

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo1 });

                var secretObservable = clients.CreateOrUpdate(ORG, secretName, upsertSecret);
                await secretObservable;

                var setRepoListObservable = clients.SetSelectedRepositoriesForSecret(ORG, secretName, new SelectedRepositoryCollection(new long[] { repo1.Id, repo2.Id }));
                await setRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(ORG, secretName);
                var visibilityRepos = await visibilityReposObservable;

                Assert.NotEmpty(visibilityRepos.Repositories);
                Assert.Equal(2, visibilityRepos.Count);
            }
#endif
        }

        public class AddRepoToOrganizationSecretMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task AddSelectedRepositoriesForSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);
                var repoClients = new ObservableRepositoriesClient(github);

                var secretName = "REACTIVE_ADD_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(repoClients, "reactive-add-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(repoClients, "reactive-add-secrets-selected-repo-test-2");

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo1 });

                var secretObservable = clients.CreateOrUpdate(ORG, secretName, upsertSecret);
                await secretObservable;

                var addRepoListObservable = clients.AddRepoToOrganizationSecret(ORG, secretName, repo2.Id);
                await addRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(ORG, secretName);
                var visibilityRepos = await visibilityReposObservable;

                Assert.NotEmpty(visibilityRepos.Repositories);
                Assert.Equal(2, visibilityRepos.Count);
            }
#endif
        }

        public class RemoveRepoFromOrganizationSecretMethod
        {
#if SODIUM_CORE_AVAILABLE
            [OrganizationTest]
            public async Task RemoveSelectedRepositoriesForSecret()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);
                var repoClients = new ObservableRepositoriesClient(github);

                var secretName = "REACTIVE_REMOVE_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(repoClients, "reactive-remove-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(repoClients, "reactive-remove-secrets-selected-repo-test-2");

                var keyObservable = clients.GetPublicKey(ORG);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo1, repo2 });

                var secretObservable = clients.CreateOrUpdate(ORG, secretName, upsertSecret);
                await secretObservable;

                var removeRepoListObservable = clients.RemoveRepoFromOrganizationSecret(ORG, secretName, repo2.Id);
                await removeRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(ORG, secretName);
                var visibilityRepos = await visibilityReposObservable;

                Assert.NotEmpty(visibilityRepos.Repositories);
                Assert.Equal(1, visibilityRepos.Count);
            }
#endif
        }

#if SODIUM_CORE_AVAILABLE
        private static UpsertOrganizationSecret GetSecretForCreate(string secretValue, SecretsPublicKey key)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretValue);
            var publicKey = Convert.FromBase64String(key.Key);
            var sealedPublicKeyBox = SealedPublicKeyBox.Create(secretBytes, publicKey);

            var upsertValue = new UpsertOrganizationSecret
            {
                EncryptedValue = Convert.ToBase64String(sealedPublicKeyBox),
                KeyId = key.KeyId,
                Visibility = "all"

            };

            return upsertValue;
        }

        private static UpsertOrganizationSecret GetSecretForCreate(string secretValue, SecretsPublicKey key, Repository[] repos)
        {
            var secretBytes = Encoding.UTF8.GetBytes(secretValue);
            var publicKey = Convert.FromBase64String(key.Key);
            var sealedPublicKeyBox = SealedPublicKeyBox.Create(secretBytes, publicKey);

            var upsertValue = new UpsertOrganizationSecret
            {
                EncryptedValue = Convert.ToBase64String(sealedPublicKeyBox),
                KeyId = key.KeyId,
                Visibility = "selected",
                SelectedRepositoriesIds = repos.Select(r => r.Id)

            };

            return upsertValue;
        }
#endif

        private static async Task<Repository> CreateRepoIfNotExists(ObservableRepositoriesClient client, string name)
        {
            try
            {
                var existingRepo = client.Get(ORG, name);
                return await existingRepo;
            }
            catch
            {
                var newRepo = client.Create(ORG, new NewRepository(name));
                return await newRepo;
            }
        }
    }
}
