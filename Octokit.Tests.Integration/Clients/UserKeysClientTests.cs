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
                var keys = await github.User.Keys.GetAll();
                Assert.NotEmpty(keys);

                var first = keys[0];
                Assert.NotNull(first.Id);
                Assert.NotNull(first.Key);
                Assert.NotNull(first.Title);
                Assert.NotNull(first.Url);
            }
        }

        [IntegrationTest]
        public async Task CanGetAllForGivenUser()
        {
            var github = Helper.GetAuthenticatedClient();

            var keys = await github.User.Keys.GetAll("shiftkey");
            Assert.NotEmpty(keys);

            var first = keys[0];
            Assert.NotNull(first.Id);
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
                var key = await github.User.Keys.Get(context.KeyId);

                Assert.Equal(key.Title, context.KeyTitle);
                Assert.Equal(key.Key, context.KeyData);
            }
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteKey()
        {
            // Create a key
            string keyTitle = "title";
            string keyData = "ssh-rsa AAAAB3NzaC1yc2EAAAABJQAAAQEAjo4DqFKg8dOxiz/yjypmN1A4itU5QOStyYrfOFuTinesU/2zm9hqxJ5BctIhgtSHJ5foxkhsiBji0qrUg73Q25BThgNg8YFE8njr4EwjmqSqW13akx/zLV0GFFU0SdJ2F6rBldhi93lMnl0ex9swBqa3eLTY8C+HQGBI6MQUMw+BKp0oFkz87Kv+Pfp6lt/Uo32ejSxML1PT5hTH5n+fyl0ied+sRmPGZWmWoHB5Bc9mox7lB6I6A/ZgjtBqbEEn4HQ2/6vp4ojKfSgA4Mm7XMu0bZzX0itKjH1QWD9Lr5apV1cmZsj49Xf8SHucTtH+bq98hb8OOXEGFzplwsX2MQ==";
            var github = Helper.GetAuthenticatedClient();

            var key = await github.User.Keys.Create(new NewPublicKey(keyTitle, keyData));

            Assert.NotNull(key);
            Assert.Equal(key.Title, "title");
            Assert.Equal(key.Key, keyData);

            // Delete key
            await github.User.Keys.Delete(key.Id);

            // Verify key no longer exists
            var keys = await github.User.Keys.GetAll();
            Assert.False(keys.Any(k => k.Title == keyTitle && k.Key == keyData));
        }
    }
}
