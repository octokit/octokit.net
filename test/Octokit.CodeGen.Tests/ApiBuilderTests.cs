using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    using TypeBuilderFunc = Func<PathMetadata, ApiClientFileMetadata, ApiClientFileMetadata>;

    public class ApiBuilderTests
    {
        readonly ApiBuilder apiBuilder;

        public ApiBuilderTests()
        {
            apiBuilder = new ApiBuilder();
        }

        [Fact]
        public void Build_SettingProperty_IsInvoked()
        {
            var metadata = new List<PathMetadata> {
              new PathMetadata()
            };

            TypeBuilderFunc addInterfaceName = (path, data) =>
            {
                data.Client.InterfaceName = "Monkey";
                return data;
            };

            apiBuilder.Register(addInterfaceName);

            var results = apiBuilder.Build(metadata);
            var result = Assert.Single(results);

            Assert.Equal("Monkey", result.Client.InterfaceName);
        }

        [Fact]
        public void Build_UsingPropertyFromInput_DoesPassInMetadata()
        {
            var metadata = new List<PathMetadata>
            {
              new PathMetadata()
              {
                  Path = "some-path"
              }
            };

            TypeBuilderFunc addInterfaceName = (metadata, data) =>
            {
                data.Client.InterfaceName = metadata.Path;
                return data;
            };

            apiBuilder.Register(addInterfaceName);

            var results = apiBuilder.Build(metadata);
            var result = Assert.Single(results);

            Assert.Equal("some-path", result.Client.InterfaceName);
        }

        [Fact]
        public async Task Build_ForPathReturningArrayResponse_GeneratesRequiredModel()
        {
            var stream = TestFixtureLoader.LoadPathWithGetAndPost();

            var paths = await PathProcessor.Process(stream);

            apiBuilder.Register(Builders.AddResponseModels);
            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var results = apiBuilder.Build(paths);
            var result = Assert.Single(results);

            Assert.Equal(2, result.Models.Count);

            var commitComment = Assert.Single(result.Models.Where(m => m.Name == "RepositoriesCommitComment"));
            Assert.NotEmpty(commitComment.Properties);

            var commitCommentUser = Assert.Single(result.Models.Where(m => m.Name == "User"));
            Assert.NotEmpty(commitCommentUser.Properties);

            // TODO: how should we handle the request model being found and rendered?

            // var commitCommentRequest = Assert.Single(result.Models.Where(m => m.Name == "RepositoriesCommitCommentRequest"));
            // Assert.NotEmpty(commitComment.Properties);

            var get = Assert.Single(result.Client.Methods.Where(m => m.Name == "Get"));
            var returnType = Assert.IsType<TaskOfListType>(get.ReturnType.AsT1);
            Assert.Equal("RepositoriesCommitComment", returnType.ListType);
        }

        // TODO: how do we represent parameters that are required rather than optional?
        // TODO: what shall we do about pagination?
    }
}
