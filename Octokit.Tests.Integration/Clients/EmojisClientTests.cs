using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class EmojisClientTests
    {
        [IntegrationTest]
        public async Task GetsAllTheEmojis()
        {
            var github = Helper.GetAuthenticatedClient();

            var emojis = await github.Emojis.GetAllEmojis();

            Assert.True(emojis.Count > 1);
        }
    }
}