using System;
using System.Collections.Generic;
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
    }
}
