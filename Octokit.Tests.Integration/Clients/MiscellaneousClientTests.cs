using System.Threading.Tasks;
using Octokit;
using Octokit.Tests.Integration;
using Xunit;

public class MiscellaneousClientTests
{
    public class TheGetEmojisMethod
    {
        [IntegrationTest]
        public async Task GetsAllTheEmojis()
        {
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
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
            var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            
            var result = await github.Miscellaneous.RenderRawMarkdown("This is a **test**");
                
            Assert.Equal("<p>This is a <strong>test</strong></p>", result);
        }
    }
}
