using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class ReleasesClientTests
    {
        public class TheGetReleasesMethod
        {
            [IntegrationTest]
            public async Task ReturnsReleases()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                var releases = await github.Release.GetAll("git-tfs", "git-tfs");

                Assert.True(releases.Count > 5);
                Assert.True(releases.Any(release => release.TagName == "v0.18.0"));
            }

            [IntegrationTest]
            public async Task ReturnsReleasesWithNullPublishDate()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                var releases = await github.Release.GetAll("Particular", "ServiceInsight");

                Assert.True(releases.Count == 1);
                Assert.False(releases.First().PublishedAt.HasValue);
            }
        }
    }
}
