using System;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class ApiBuilderTests
    {
        [Fact]
        public void Register_SettingProperty_IsInvoked()
        {
            var metadata = new PathMetadata();

            var apiBuilder = new ApiBuilder();

            Func<PathMetadata, ApiBuilderResult, ApiBuilderResult> addInterfaceName = (path, data) => {
                data.InterfaceName = "Monkey";
                return data;
            };


            apiBuilder.Register(addInterfaceName);

            var result = apiBuilder.Build(metadata);

            Assert.Equal("Monkey", result.InterfaceName);
        }

        [Fact]
        public void Register_UsingPropertyFromInput_DoesPassInMetadata()
        {
            var metadata = new PathMetadata()
            {
                Path = "some-path"
            };

            var apiBuilder = new ApiBuilder();

            Func<PathMetadata, ApiBuilderResult, ApiBuilderResult> addInterfaceName = (metadata, data) => {
                data.InterfaceName = metadata.Path;
                return data;
            };


            apiBuilder.Register(addInterfaceName);

            var result = apiBuilder.Build(metadata);

            Assert.Equal("some-path", result.InterfaceName);
        }
    }
}
