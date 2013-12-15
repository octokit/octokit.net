using System;
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
        public async Task CanGetAllForUser()
        {
            var retrieved = await _gistsClient.GetAllForUser(Helper.Credentials.Login);
            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanGetAllForCurrent()
        {
            var retrieved = await _gistsClient.GetAllForCurrent();
            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanGetPublic()
        {
            var retrieved = await _gistsClient.GetPublic();
            Assert.NotNull(retrieved);
        }

    }
}