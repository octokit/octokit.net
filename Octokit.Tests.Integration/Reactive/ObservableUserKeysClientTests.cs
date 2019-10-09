using System.Linq;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;
using System.Reactive.Linq;

namespace Octokit.Tests.Integration.Clients
{
    public class ObservableUserKeysClientTests
    {
        readonly IObservableGitHubClient _github;

        public ObservableUserKeysClientTests()
        {
            _github = new ObservableGitHubClient(Helper.GetAuthenticatedClient());
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrentUser()
        {
            using (var context = await _github.CreatePublicKeyContext())
            {
                var observable = _github.User.GitSshKey.GetAllForCurrent();
                var keys = await observable.ToList();

                Assert.NotEmpty(keys);

                var first = keys[0];
                Assert.True(first.Id > 0);
                Assert.NotNull(first.Key);
                Assert.NotNull(first.Title);
                Assert.NotNull(first.Url);
            }
        }

        [IntegrationTest]
        public async Task CanGetAllForGivenUser()
        {
            var observable = _github.User.GitSshKey.GetAll("shiftkey");
            var keys = await observable.ToList();

            Assert.NotEmpty(keys);

            var first = keys[0];
            Assert.True(first.Id > 0);
            Assert.NotNull(first.Key);
            Assert.Null(first.Title);
            Assert.Null(first.Url);
        }

        [IntegrationTest]
        public async Task CanGetKeyById()
        {
            using (var context = await _github.CreatePublicKeyContext())
            {
                var observable = _github.User.GitSshKey.Get(context.KeyId);
                var key = await observable;

                Assert.Equal(key.Title, context.KeyTitle);
                Assert.Equal(key.Key, context.KeyData);
            }
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteKey()
        {
            // Use context helper to create/destroy a key safely (to avoid test failures when a key exists due to not having been deleted)
            string keyTitle = null;
            string keyData = null;
            using (var context = await _github.CreatePublicKeyContext())
            {
                var observable = _github.User.GitSshKey.Get(context.KeyId);
                var key = await observable;

                keyTitle = key.Title;
                keyData = key.Key;

                Assert.NotNull(key);
            }

            // Verify key no longer exists
            var keys = await _github.User.GitSshKey.GetAllForCurrent().ToList();
            Assert.DoesNotContain(keys, k => k.Title == keyTitle && k.Key == keyData);
        }
    }
}
