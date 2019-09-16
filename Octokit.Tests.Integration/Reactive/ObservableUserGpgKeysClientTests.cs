using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Octokit.Reactive;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Reactive
{
    public class ObservableUserGpgKeysClientTests
    {
        readonly string knownKeyId = "E88402D2E127A23A";
        readonly string knownPublicKey = "xsBNBFdTvCUBCADOaVtPoJTQOgMIVYEpI8uT60LA/kDqw/1OKn7ftKjAtxNVSgjQi/ZqZp8XKjTg2u6l4c/aPjER2BGTg90xCcbmpwq/kkQuHR4DK7dOlEOoTzDDESEFv6XXlXGCnxN8AD8YNvSO+Sy4+35ihuKUBAHDxmOl7ZAMH0STo10KuW82/DhfT3cCJNqmID7H+CW1H6IhwutPKt8XsVq2FQg2RMx+uX1KqkuBAd7b30KJ93SJqzgq5CC3DticaC0/WdZnxmYD01UvMAS6o/REs+SICdsyTxgBx/X8SIXuX2TD9PG/O2785JI5/xvBd4jU6bBH/4oWoHr3e/lyNqb1+GSeTFX3ABEBAAE=";

        readonly IObservableGitHubClient _gitHubClient;

        public ObservableUserGpgKeysClientTests()
        {
            _gitHubClient = new ObservableGitHubClient(Helper.GetBasicAuthClient());
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrentUser()
        {
            using (var context = await _gitHubClient.CreateGpgKeyContext())
            {
                var observable = _gitHubClient.User.GpgKey.GetAllForCurrent();
                var keys = await observable.ToList();

                Assert.NotEmpty(keys);

                var first = keys[0];
                Assert.NotEqual(default, first.Id);
                Assert.Null(first.PrimaryKeyId);
                Assert.NotNull(first.KeyId);
                Assert.NotNull(first.PublicKey);
                Assert.Empty(first.Subkeys);
            }
        }

        [IntegrationTest]
        public async Task CanGetKeyById()
        {
            using (var context = await _gitHubClient.CreateGpgKeyContext())
            {
                var observable = _gitHubClient.User.GpgKey.Get(context.GpgKeyId);
                var key = await observable;

                Assert.Equal(knownKeyId, key.KeyId);
                Assert.Equal(knownPublicKey, key.PublicKey);
            }
        }

        [IntegrationTest]
        public async Task CanCreateAndDeleteKey()
        {
            string publicKey = @"
-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: BCPG C# v1.6.1.0

mQENBFdTvCUBCADOaVtPoJTQOgMIVYEpI8uT60LA/kDqw/1OKn7ftKjAtxNVSgjQ
i/ZqZp8XKjTg2u6l4c/aPjER2BGTg90xCcbmpwq/kkQuHR4DK7dOlEOoTzDDESEF
v6XXlXGCnxN8AD8YNvSO+Sy4+35ihuKUBAHDxmOl7ZAMH0STo10KuW82/DhfT3cC
JNqmID7H+CW1H6IhwutPKt8XsVq2FQg2RMx+uX1KqkuBAd7b30KJ93SJqzgq5CC3
DticaC0/WdZnxmYD01UvMAS6o/REs+SICdsyTxgBx/X8SIXuX2TD9PG/O2785JI5
/xvBd4jU6bBH/4oWoHr3e/lyNqb1+GSeTFX3ABEBAAG0AIkBHAQQAQIABgUCV1O8
JQAKCRDohALS4SeiOs/QB/9PMeFNdPkB1xfBm3qvTErqvhTcQspPibucYefG6JHL
vhm6iCOVBeCuPS4P/T8RTzb0qJaTdZZWcwZ9UjRVqF/RKwdMJTBKBHRegc5hRjLH
Koxk0zXosk+CapIR6eVhHe4IzpVVxZOvunsFOclIh+qHx9UzJhz9wSO/XBn/6Rzr
DGzE9fpK1JRKC0I3PuiDCNuZvojXeUsT/zuHYsz00wnA2Em7CmcWWng3nPUSHvBB
GUJ7YE7NvYXqT09PdhoZnf9p1wOugiuG6CLzWf8stlNV3mZptpP+sCGcz/UVffRO
VO/+BCBsaoT4g1FFOmJhbBAD3G72yslBnUJmqKP/39pi
=O7Yi
-----END PGP PUBLIC KEY BLOCK-----
";
            // Create a key
            var observable = _gitHubClient.User.GpgKey.Create(new NewGpgKey(publicKey));
            var key = await observable;

            Assert.NotNull(key);
            Assert.Equal(knownKeyId, key.KeyId);
            Assert.Equal(knownPublicKey, key.PublicKey);

            // Delete the key
            await _gitHubClient.User.GpgKey.Delete(key.Id);

            // Verify key no longer exists
            var keys = await _gitHubClient.User.GpgKey.GetAllForCurrent().ToList();
            Assert.DoesNotContain(keys, k => k.KeyId == knownKeyId && k.PublicKey == knownPublicKey);
        }
    }
}
