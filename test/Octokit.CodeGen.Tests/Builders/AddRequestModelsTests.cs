using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen
{
    public class AddRequestModelsTests
    {
        [Fact]
        public async Task ForTopicsRoute_RequestModelWithNamesProperty_IsFound()
        {
            var stream = TestFixtureLoader.LoadPathWithGetAndPost();

            var paths = await PathProcessor.Process(stream);
            var path = paths[0];

            var data = new ApiClientFileMetadata();

            var result = Builders.AddRequestModels(path, data);

            var request = Assert.Single(result.Models);

            var property = Assert.Single(request.Properties);

            Assert.Equal("Names", property.Name);
        }
    }
}
