using System.Threading.Tasks;
using Octokit.Tests.Integration;
using Xunit;

public class MiscellaneousClientTests
{
    public class TheGetEmojisMethod
    {
        [IntegrationTest]
        public async Task GetsAllTheEmojis()
        {
            var github = Helper.GetAuthenticatedClient();

            var emojis = await github.Miscellaneous.GetAllEmojis();

            Assert.True(emojis.Count > 1);
        }
    }

    public class TheRenderRawMarkdownMethod
    {
        [IntegrationTest]
        public async Task CanRenderGitHubFlavoredMarkdown()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.RenderRawMarkdown("This is\r\n a **test**");

            Assert.Equal("<p>This is\n a <strong>test</strong></p>\n", result);
        }
    }

    public class TheGetGitIgnoreTemplatesMethod
    {
        [IntegrationTest]
        public async Task ReturnsListOfGitIgnoreTemplates()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetAllGitIgnoreTemplates();

            Assert.True(result.Count > 2);
        }
    }

    public class TheGetLicensesMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveListOfLicenses()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetAllLicenses();

            Assert.True(result.Count > 2);
            Assert.Contains(result, license => license.Key == "mit");
        }
    }

    public class TheGetLicenseMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveListOfLicenses()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetLicense("mit");

            Assert.Equal("mit", result.Key);
            Assert.Equal("MIT License", result.Name);
        }
    }

    public class TheGetResourceRateLimitsMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveResourceRateLimits()
        {
            var github = Helper.GetAuthenticatedClient();

            var result = await github.Miscellaneous.GetRateLimits();

            // Test the core limits
            Assert.True(result.Resources.Core.Limit > 0);
            Assert.True(result.Resources.Core.Remaining > -1);
            Assert.True(result.Resources.Core.Remaining <= result.Resources.Core.Limit);
            Assert.True(result.Resources.Core.ResetAsUtcEpochSeconds > 0);
            Assert.NotNull(result.Resources.Core.Reset);

            // Test the search limits
            Assert.True(result.Resources.Search.Limit > 0);
            Assert.True(result.Resources.Search.Remaining > -1);
            Assert.True(result.Resources.Search.Remaining <= result.Resources.Search.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotNull(result.Resources.Search.Reset);

            // Test the depreciated rate limits
            Assert.True(result.Rate.Limit > 0);
            Assert.True(result.Rate.Remaining > -1);
            Assert.True(result.Rate.Remaining <= result.Rate.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotNull(result.Resources.Search.Reset);
        }
    }

    public class TheGetMetadataMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveMetadata()
        {
            var github = Helper.GetAnonymousClient();

            var result = await github.Miscellaneous.GetMetadata();

            Assert.True(result.VerifiablePasswordAuthentication);
        }
    }
}