using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class MarkdownClientTests
    {
        [IntegrationTest]
        public async Task CanRenderGitHubFlavoredMarkdown()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Markdown.RenderRawMarkdown("This is\r\n a **test**");

            Assert.Equal("<p>This is\na <strong>test</strong></p>\n", result);
        }
    }
}