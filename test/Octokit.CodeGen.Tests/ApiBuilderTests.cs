using System;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class ApiBuilderTests
    {
        [Fact]
        public void Register_AddingFixedField_DoesPerformFunction()
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
    }
}
