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

        [Fact]
        public async Task DocumentedApiMatchesImplementation()
        {
            var dictionary = WebsiteScraper.GetListOfDocumentedApis();

            var documentedApis = dictionary
                .Where(x => x.Endpoints.Any());

            var allErrors = documentedApis.SelectMany(api => api.Validate())
                .ToList();
        }
    }
}
