using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xunit;

namespace Octokit.CodeGen.Tests
{
    public class AddTypeNamesAndFileNameTests
    {
        [Fact]
        public void Build_WillFormatInterfaceAndType_UsingPath()
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

            var result = Builders.AddTypeNamesAndFileName(metadata, data);

            Assert.Equal("MarketplaceListingAccountsClient", result.Client.ClassName);
            Assert.Equal("IMarketplaceListingAccountsClient", result.Client.InterfaceName);
            var expectedPath = Path.Join("Octokit", "Clients", "MarketplaceListingAccountsClient.cs");
            Assert.Equal(expectedPath, result.FileName);
        }
    }
}
