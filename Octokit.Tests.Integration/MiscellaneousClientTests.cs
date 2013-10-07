using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class MiscellaneousClientTests
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

                var emojis = await github.Miscellaneous.GetEmojis();

                Assert.True(emojis.Count > 1);
            }
        }

        public class TheRenderRawMarkdownMethod
        {
            [IntegrationTest]
            public async Task CanRenderGitHubFlavoredMarkdown()
            {
                var github = new GitHubClient("Test Runner User Agent")
                {
                    Credentials = AutomationSettings.Current.GitHubCredentials
                };
            
                var result = await github.Miscellaneous.RenderRawMarkdown("This is a **test**");
                
                Assert.Equal("<p>This is a <strong>test</strong></p>", result);
            }
        }
    }
}
