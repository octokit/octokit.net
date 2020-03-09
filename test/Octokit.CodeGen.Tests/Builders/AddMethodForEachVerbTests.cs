using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class AddMethodForEachVerbTests
    {
        [Fact]
        public void UsingExamplePath_SetsExpectedMethodAndParameter()
        {
            var metadata = new PathMetadata
            {
                Path = "/marketplace_listing/accounts/{account_id}",
                Verbs = new List<VerbResult>
                  {
                      new VerbResult {
                          Method = HttpMethod.Get,
                          Parameters = new List<Parameter>
                          {
                              new Parameter
                              {
                                  Name = "account_id",
                                  In = "path",
                                  Required = true,
                                  Type = "number",
                              }
                          }
                      }
              }
            };

            var data = new ApiClientFileMetadata();

            var result = Builders.AddMethodForEachVerb(metadata, data);

            var method = Assert.Single(result.Client.Methods);

            Assert.Equal("Get", method.Name);

            var parameter = Assert.Single(method.Parameters);

            Assert.Equal("accountId", parameter.Name);
            Assert.Equal("number", parameter.Type);
        }

        [Fact]
        public async Task Build_ForPathWithMultipleMethods_GeneratesResultingClient()
        {
            var stream = TestFixtureLoader.LoadPathWithGetPutAndDelete();

            var paths = await PathProcessor.Process(stream);

            var data = new ApiClientFileMetadata();

            var result = Builders.AddMethodForEachVerb(paths[0], data);

            Assert.Equal(3, result.Client.Methods.Count);

            var get = Assert.Single(result.Client.Methods.Where(m => m.Name == "Get"));
            var getParameter = Assert.Single(get.Parameters);
            Assert.Equal("username", getParameter.Name);
            Assert.Equal("string", getParameter.Type);

            var returnType = Assert.IsType<TaskOfType>(get.ReturnType.AsT0);
            Assert.Equal("boolean", returnType.Type);

            var delete = Assert.Single(result.Client.Methods.Where(m => m.Name == "Delete"));
            var deleteParameter = Assert.Single(delete.Parameters);
            Assert.Equal("username", deleteParameter.Name);
            Assert.Equal("string", deleteParameter.Type);

            var getOrCreate = Assert.Single(result.Client.Methods.Where(m => m.Name == "GetOrCreate"));
            var getOrCreateParameter = Assert.Single(getOrCreate.Parameters);
            Assert.Equal("username", getOrCreateParameter.Name);
            Assert.Equal("string", getOrCreateParameter.Type);
        }
    }
}
