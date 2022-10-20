using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;

#if SODIUM_CORE_AVAILABLE
using Sodium;
#endif

namespace Octokit.Tests.Integration.Clients
{
    public class OrganizationSecretsClientTests
    {

        public class GetPublicKeyMethod
        {
            [OrganizationTest]
            public async Task GetPublicKey()
            {
                var github = Helper.GetAuthenticatedClient();

                var key = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);

                Assert.True(!string.IsNullOrWhiteSpace(key.KeyId));
            }
        }

        public class GetAllMethod
        {
            [OrganizationTest]
            public async Task GetSecrets()
            {
                var github = Helper.GetAuthenticatedClient();

                var secrets = await github.Organization.Actions.Secrets.GetAll(Helper.Organization);

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

                var secret = await github.Organization.Actions.Secrets.Get(Helper.Organization, "TEST");

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
                var now = DateTime.Now;

                var publicKey = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertValue = GetSecretForCreate("value", publicKey);

                var secret = await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, "UPSERT_TEST", upsertValue);

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

                var secretName = "DELETE_TEST";

                var publicKey = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertValue = GetSecretForCreate("value", publicKey);

                await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, secretName, upsertValue);
                await github.Organization.Actions.Secrets.Delete(Helper.Organization, secretName);

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

                var secretName = "LIST_SELECTED_REPO_TEST";

                var repo = await CreateRepoIfNotExists(github, "list-secrets-selected-repo-test");

                var key = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo });
                var secret = await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);

                var visibilityRepos = await github.Organization.Actions.Secrets.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);

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

                var secretName = "SET_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(github, "set-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(github, "set-secrets-selected-repo-test-2");

                var key = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo1 });
                await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);

                await github.Organization.Actions.Secrets.SetSelectedRepositoriesForSecret(Helper.Organization, secretName, new SelectedRepositoryCollection(new long[] { repo1.Id, repo2.Id }));

                var visibilityRepos = await github.Organization.Actions.Secrets.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);

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

                var secretName = "ADD_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(github, "add-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(github, "add-secrets-selected-repo-test-2");

                var key = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo1 });
                await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);

                await github.Organization.Actions.Secrets.AddRepoToOrganizationSecret(Helper.Organization, secretName, repo2.Id);

                var visibilityRepos = await github.Organization.Actions.Secrets.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);

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

                var secretName = "REMOVE_SELECTED_REPO_TEST";

                var repo1 = await CreateRepoIfNotExists(github, "remove-secrets-selected-repo-test-1");
                var repo2 = await CreateRepoIfNotExists(github, "remove-secrets-selected-repo-test-2");

                var key = await github.Organization.Actions.Secrets.GetPublicKey(Helper.Organization);
                var upsertSecret = GetSecretForCreate("secret", key, new Repository[] { repo1, repo2 });
                await github.Organization.Actions.Secrets.CreateOrUpdate(Helper.Organization, secretName, upsertSecret);

                await github.Organization.Actions.Secrets.RemoveRepoFromOrganizationSecret(Helper.Organization, secretName, repo2.Id);

                var visibilityRepos = await github.Organization.Actions.Secrets.GetSelectedRepositoriesForSecret(Helper.Organization, secretName);

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

        private static async Task<Repository> CreateRepoIfNotExists(IGitHubClient github, string name)
        {
            try
            {
                var existingRepo = await github.Repository.Get(Helper.Organization, name);
                return existingRepo;
            }
            catch
            {
                var newRepo = await github.Repository.Create(Helper.Organization, new NewRepository(name));
                return newRepo;
            }
        }
    }
}
