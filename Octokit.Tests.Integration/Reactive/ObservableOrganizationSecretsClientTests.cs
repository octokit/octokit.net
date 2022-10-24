using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Xunit;
using System.Linq;
using Octokit.Tests.Integration.Helpers;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableOrganizationSecretsClientTests
    {
        public class GetPublicKeyMethod
        {
            [OrganizationTest]
            public async Task GetPublicKey()
            {
                var github = Helper.GetAuthenticatedClient();
                var clients = new ObservableOrganizationSecretsClient(github);

                var keyObservable = clients.GetPublicKey(Helper.Organization);
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

                var secretsObservable = clients.GetAll(Helper.Organization);
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

                var secretObservable = clients.Get(Helper.Organization, "TEST");
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

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;

                var upsertValue = GetSecretForCreate("value", key);

                var secretObservable = clients.CreateOrUpdate(Helper.Organization, "REACTIVE_UPSERT_TEST", upsertValue);
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

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;
                var upsertValue = GetSecretForCreate("value", key);

                var createSecretObservable = clients.CreateOrUpdate(Helper.Organization, secretName, upsertValue);
                await createSecretObservable;

                var deleteSecretObservable = clients.Delete(Helper.Organization, secretName);
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

                var secretName = "REACTIVE_LIST_SELECTED_REPO_TEST";

                var repoName = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test");
                var repo = await github.CreateRepositoryContext(new NewRepository(repoName));

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo.Repository });

                var secretObservable = clients.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);
                await secretObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);
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

                var secretName = "REACTIVE_SET_SELECTED_REPO_TEST";

                var repo1Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-1");
                var repo2Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-2");

                var repo1 = await github.CreateRepositoryContext(new NewRepository(repo1Name));
                var repo2 = await github.CreateRepositoryContext(new NewRepository(repo2Name));

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo1.Repository });

                var secretObservable = clients.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);
                await secretObservable;

                var setRepoListObservable = clients.SetSelectedRepositoriesForSecret(Helper.Organization, secretName, new SelectedRepositoryCollection(new long[] { repo1.RepositoryId, repo2.RepositoryId }));
                await setRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);
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

                var secretName = "REACTIVE_ADD_SELECTED_REPO_TEST";

                var repo1Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-1");
                var repo2Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-2");

                var repo1 = await github.CreateRepositoryContext(new NewRepository(repo1Name));
                var repo2 = await github.CreateRepositoryContext(new NewRepository(repo2Name));

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("value", key, new Repository[] { repo1.Repository });

                var secretObservable = clients.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);
                await secretObservable;

                var addRepoListObservable = clients.AddRepoToOrganizationSecret(Helper.Organization, secretName, repo2.RepositoryId);
                await addRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);
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

                var secretName = "REACTIVE_REMOVE_SELECTED_REPO_TEST";

                var repo1Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-1");
                var repo2Name = Helper.MakeNameWithTimestamp("reactive-add-secrets-selected-repo-test-2");

                var repo1 = await github.CreateRepositoryContext(new NewRepository(repo1Name));
                var repo2 = await github.CreateRepositoryContext(new NewRepository(repo2Name));

                var keyObservable = clients.GetPublicKey(Helper.Organization);
                var key = await keyObservable;
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo1.Repository, repo2.Repository });

                var secretObservable = clients.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);
                await secretObservable;

                var removeRepoListObservable = clients.RemoveRepoFromOrganizationSecret(Helper.Organization, secretName, repo2.RepositoryId);
                await removeRepoListObservable;

                var visibilityReposObservable = clients.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);
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
    }
}
