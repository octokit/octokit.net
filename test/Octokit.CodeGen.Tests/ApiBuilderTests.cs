using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
        public void Build_WillFormatInterfaceAndType_UsingPath()
        {
            var metadata = new List<PathMetadata>
            {
              new PathMetadata
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
              }
            };

            apiBuilder.Register(Builders.AddTypeNamesAndFileName);

            var results = apiBuilder.Build(metadata);
            var result = Assert.Single(results);

            Assert.Equal("MarketplaceListingAccountsClient", result.Client.ClassName);
            Assert.Equal("IMarketplaceListingAccountsClient", result.Client.InterfaceName);
            var expectedPath = Path.Join("Octokit", "Clients", "MarketplaceListingAccountsClient.cs");
            Assert.Equal(expectedPath, result.FileName);
        }

        [Fact]
        public async Task Build_WillUseAliasForNames_InTypesAndFileName()
        {
            var stream = TestFixtureLoader.LoadTopicsRoute();

            var paths = await PathProcessor.Process(stream);

            apiBuilder.Register(Builders.AddTypeNamesAndFileName);

            var results = apiBuilder.Build(paths);
            var result = Assert.Single(results);

            Assert.Equal("RepositoriesTopicsClient", result.Client.ClassName);
            Assert.Equal("IRepositoriesTopicsClient", result.Client.InterfaceName);
            var expectedPath = Path.Join("Octokit", "Clients", "RepositoriesTopicsClient.cs");
            Assert.Equal(expectedPath, result.FileName);
        }

        [Fact]
        public void Build_WillAddMethod_RepresentingGet()
        {
            var metadata = new List<PathMetadata>
            {
              new PathMetadata
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
              }
            };

            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var results = apiBuilder.Build(metadata);
            var result = Assert.Single(results);

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

            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var results = apiBuilder.Build(paths);
            var result = Assert.Single(results);

            Assert.Equal(3, result.Client.Methods.Count);

            var get = Assert.Single(result.Client.Methods.Where(m => m.Name == "Get"));
            var getParameter = Assert.Single(get.Parameters);
            Assert.Equal("username", getParameter.Name);
            Assert.Equal("string", getParameter.Type);

            var returnType = Assert.IsType<TaskOfType>(get.ReturnType);
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

        [Fact]
        public async Task Build_ForPathReturningObjectResponse_GeneratesRequiredModel()
        {
            var stream = TestFixtureLoader.LoadPathWithGet();

            var paths = await PathProcessor.Process(stream);

            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var results = apiBuilder.Build(paths);
            var result = Assert.Single(results);

            var get = Assert.Single(result.Client.Methods.Where(m => m.Name == "Get"));
            var returnType = Assert.IsType<TaskOfType>(get.ReturnType);
            Assert.Equal("MarketplaceListingAccount", returnType.Type);

            var model = Assert.Single(result.Models);
            Assert.Equal("MarketplaceListingAccount", model.Type);
            Assert.NotEmpty(model.Properties);
        }

        [Fact]
        public async Task Build_ForPathReturningArrayResponse_GeneratesRequiredModel()
        {
            var stream = TestFixtureLoader.LoadPathWithGetAndPost();

            var paths = await PathProcessor.Process(stream);

            apiBuilder.Register(Builders.AddMethodForEachVerb);

            var results = apiBuilder.Build(paths);
            var result = Assert.Single(results);

            var get = Assert.Single(result.Client.Methods.Where(m => m.Name == "Get"));
            var returnType = Assert.IsType<TaskOfListType>(get.ReturnType);
            Assert.Equal("CommitComment", returnType.ListType);

            Assert.Equal(3, result.Models.Count);

            var commitComment = Assert.Single(result.Models.Where(m => m.Type =="CommitComment"));
            Assert.NotEmpty(commitComment.Properties);

            var commitCommentUser = Assert.Single(result.Models.Where(m => m.Type =="CommitCommentUser"));
            Assert.NotEmpty(commitCommentUser.Properties);

            // TODO: how can we test where this is being inserted into the client code?

            var commitCommentRequest = Assert.Single(result.Models.Where(m => m.Type =="CommitCommentRequest"));
            Assert.NotEmpty(commitComment.Properties);
        }

        // TODO: how do we represent parameters that are required rather than optional?
        // TODO: what shall we do about pagination?
    }
}
