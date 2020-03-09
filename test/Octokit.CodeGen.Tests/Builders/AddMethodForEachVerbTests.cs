using System.Collections.Generic;
using System.IO;
using System.Net.Http;
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

    }
}
