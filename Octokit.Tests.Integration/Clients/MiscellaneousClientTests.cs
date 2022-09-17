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

            Assert.Equal("<p>This is\na <strong>test</strong></p>\n", result);
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

        [IntegrationTest]
        public async Task CanRetrieveListOfLicensesWithPagination()
        {
            var github = Helper.GetAuthenticatedClient();

            var options = new ApiOptions
            {
                PageCount = 1,
                PageSize = 5,
            };

            var result = await github.Miscellaneous.GetAllLicenses(options);

            Assert.Equal(5, result.Count);
        }

        [IntegrationTest]
        public async Task CanRetrieveDistinctListOfLicensesBasedOnPageStart()
        {
            var github = Helper.GetAuthenticatedClient();

            var startOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1
            };

            var firstPage = await github.Miscellaneous.GetAllLicenses(startOptions);

            var skipStartOptions = new ApiOptions
            {
                PageSize = 1,
                PageCount = 1,
                StartPage = 2
            };

            var secondPage = await github.Miscellaneous.GetAllLicenses(skipStartOptions);

            Assert.NotEqual(firstPage[0].Key, secondPage[0].Key);
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

        [IntegrationTest]
        public async Task ReportsErrorWhenInvalidLicenseProvided()
        {
            var github = Helper.GetAuthenticatedClient();

            await Assert.ThrowsAsync<NotFoundException>(() => github.Miscellaneous.GetLicense("purple-monkey-dishwasher"));
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
            Assert.NotEqual(default, result.Resources.Core.Reset);

            // Test the search limits
            Assert.True(result.Resources.Search.Limit > 0);
            Assert.True(result.Resources.Search.Remaining > -1);
            Assert.True(result.Resources.Search.Remaining <= result.Resources.Search.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotEqual(default, result.Resources.Search.Reset);

            // Test the depreciated rate limits
            Assert.True(result.Rate.Limit > 0);
            Assert.True(result.Rate.Remaining > -1);
            Assert.True(result.Rate.Remaining <= result.Rate.Limit);
            Assert.True(result.Resources.Search.ResetAsUtcEpochSeconds > 0);
            Assert.NotEqual(default, result.Resources.Search.Reset);
        }
    }

    public class TheGetMetadataMethod
    {
        [IntegrationTest]
        public async Task CanRetrieveMetadata()
        {
            var github = Helper.GetAnonymousClient();

            var result = await github.Miscellaneous.GetMetadata();

            Assert.False(result.VerifiablePasswordAuthentication); // is username password allowed, probably not any more
#pragma warning disable CS0618 // Type or member is obsolete
            Assert.True(string.IsNullOrEmpty(result.GitHubServicesSha));
#pragma warning restore CS0618 // Type or member is obsolete
            Assert.True(result.Hooks.Count > 0);
            Assert.True(result.Web.Count > 0);
            Assert.True(result.Api.Count > 0);
            Assert.True(result.Git.Count > 0);
            Assert.True(result.Packages.Count > 0);
            Assert.True(result.Pages.Count > 0);
            Assert.True(result.Importer.Count > 0);
            Assert.True(result.Actions.Count > 0);
            Assert.True(result.Dependabot.Count > 0);
        }
    }
}
