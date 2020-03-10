using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen
{
    public class AddRequestModelsTests
    {
        [Fact]
        public async Task ForTopicsRoute_RequestModelWithNamesProperty_IsFound()
        {
            var stream = TestFixtureLoader.LoadTopicsRoute();

            var paths = await PathProcessor.Process(stream);
            var path = paths[0];

            var data = new ApiClientFileMetadata();

            var result = Builders.AddRequestModels(path, data);

            var request = Assert.Single(result.RequestModels);

            Assert.Equal("RepositoriesTopicRequest", request.Name);
            var property = Assert.Single(request.Properties);

            Assert.Equal("Names", property.Name);
        }

        [Fact]
        public async Task ForTopicsRoute_RequestModelWithMultiplePrperties_AreListed()
        {
            var stream = TestFixtureLoader.LoadPathWithGetAndPost();

            var paths = await PathProcessor.Process(stream);
            var path = paths[0];

            var data = new ApiClientFileMetadata();

            var result = Builders.AddRequestModels(path, data);

            var request = Assert.Single(result.RequestModels);

            Assert.Equal("RepositoriesCommitCommentRequest", request.Name);
            Assert.Equal(4, request.Properties.Count);

            Assert.Single(request.Properties.Where(p => p.Name == "Body"));
            Assert.Single(request.Properties.Where(p => p.Name == "Path"));
            Assert.Single(request.Properties.Where(p => p.Name == "Line"));
            Assert.Single(request.Properties.Where(p => p.Name == "Position"));
        }
    }
}
