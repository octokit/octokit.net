using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration
{
    public class GistsClientTests 
    {
        readonly IGitHubClient _gitHubClient;
        readonly IGistsClient _gistsClient;

        public GistsClientTests()
        {
            this._gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };

            this._gistsClient = this._gitHubClient.Gist;
        }

        [IntegrationTest]
        public async Task CanGetGist()
        {
            var retrieved = await this._gistsClient.Get("6305249");
            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanCreateGist()
        {
            var newGist = new NewGist();
            newGist.Description = "my new gist";
            newGist.Public = true;

            newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");;

            var createdGist = await this._gistsClient.Create(newGist);

            Assert.NotNull(createdGist);
            Assert.Equal(newGist.Description, createdGist.Description);
            Assert.Equal(newGist.Public, createdGist.Public);
        }
    }
}