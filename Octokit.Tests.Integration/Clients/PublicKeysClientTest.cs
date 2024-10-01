using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class PublicKeysClientTests
    {
        public class TheGetMethod
        {
            [IntegrationTest]
            public async Task CanRetrievePublicKeys()
            {
                var github = Helper.GetAnonymousClient();

                var result = await github.Meta.PublicKeys.Get(PublicKeyType.SecretScanning);

                Assert.NotNull(result);
                Assert.Equal(2, result.PublicKeys.Count);

                Assert.NotNull(result.PublicKeys[0].KeyIdentifier);
                Assert.NotNull(result.PublicKeys[0].Key);

                Assert.NotNull(result.PublicKeys[1].KeyIdentifier);
                Assert.NotNull(result.PublicKeys[1].Key);
            }
        }
    }
}
