using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class AutoCompleteClientTests
    {
        public class TheGetEmojisMethod
        {
            [IntegrationTest]
            public async Task GetsAllTheEmojis()
            {
                var github = new GitHubClient("Octokit Test Runner")
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };

                var emojis = await github.AutoComplete.GetEmojis();

                Assert.True(emojis.Count > 1);
            }
        }
    }
}
