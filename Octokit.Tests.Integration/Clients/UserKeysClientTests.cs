using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Octokit.Tests.Integration.Helpers;

namespace Octokit.Tests.Integration.Clients
{
    public class UserKeysClientTests
    {
        [IntegrationTest]
        public async Task CanGetAllForCurrentUser()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreatePublicKeyContext())
            {
                var keys = await github.User.GitSshKey.GetAllForCurrent();
                Assert.NotEmpty(keys);

                var first = keys[0];
                Assert.NotEqual(default, first.Id);
                Assert.NotNull(first.Key);
                Assert.NotNull(first.Title);
                Assert.NotNull(first.Url);
            }
        }

        [IntegrationTest]
        public async Task CanGetAllForGivenUser()
        {
            var github = Helper.GetAuthenticatedClient();

            var keys = await github.User.GitSshKey.GetAll("shiftkey");
            Assert.NotEmpty(keys);

            var first = keys[0];
            Assert.NotEqual(default, first.Id);
            Assert.NotNull(first.Key);
            Assert.Null(first.Title);
            Assert.Null(first.Url);
        }

        [IntegrationTest]
        public async Task CanGetKeyById()
        {
            var github = Helper.GetAuthenticatedClient();

            using (var context = await github.CreatePublicKeyContext())
            {
                var key = await github.User.GitSshKey.Get(context.KeyId);

                Assert.Equal(key.Title, context.KeyTitle);
                Assert.Equal(key.Key, context.KeyData);
            }
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteKey()
        {
            var github = Helper.GetAuthenticatedClient();

            // Use context helper to create/destroy a key safely (to avoid test failures when a key exists due to not having been deleted)
            string keyTitle = null;
            string keyData = null;
            using (var context = await github.CreatePublicKeyContext())
            {
                var observable = github.User.GitSshKey.Get(context.KeyId);
                var key = await observable;

                Assert.NotNull(key);

                keyTitle = key.Title;
                keyData = key.Key;
            }

            // Verify key no longer exists
            var keys = await github.User.GitSshKey.GetAllForCurrent();
            Assert.DoesNotContain(keys, k => k.Title == keyTitle && k.Key == keyData);
        }
    }
}
