using Xunit;
using System.Threading.Tasks;
using System.Linq;

namespace Octokit.Tests.Integration
{
    /// <summary>
    /// Tests to make sure our tests are ok.
    /// </summary>
    public class SelfTests
    {
        [Fact]
        public void NoTestsUseAsyncVoid()
        {
            var errors = typeof(SelfTests).Assembly.GetAsyncVoidMethodsList();
            Assert.Equal("", errors);
        }

        [Fact(Skip = "test doesn't fail, so let's not worry about it for now")]
        public async Task DocumentedApiMatchesImplementation()
        {
            var documentedApis = WebsiteScraper.GetListOfDocumentedApis();

            var allErrors = documentedApis.SelectMany(api => api.Validate())
                .ToList();
        }
    }
}
