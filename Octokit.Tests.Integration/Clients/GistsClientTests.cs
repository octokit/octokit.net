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
        readonly string testGistId = "6305249";

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
            var retrieved = await this._gistsClient.Get(testGistId);
            Assert.NotNull(retrieved);
        }

        [IntegrationTest]
        public async Task CanCreateEditAndDeleteAGist()
        {
            var newGist = new NewGist();
            newGist.Description = "my new gist";
            newGist.Public = true;

            newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");

            var createdGist = await this._gistsClient.Create(newGist);

            Assert.NotNull(createdGist);
            Assert.Equal(newGist.Description, createdGist.Description);
            Assert.Equal(newGist.Public, createdGist.Public);

            var gistUpdate = new GistUpdate();
            gistUpdate.Description = "my newly updated gist";
            var gistFileUpdate = new GistFileUpdate
            {
                NewFileName = "myNewGistTestFile.cs",
                Content = "new GistsClient(connection).Edit();"
            };

            gistUpdate.Files.Add("myGistTestFile.cs", gistFileUpdate);

            var updatedGist = await this._gistsClient.Edit(createdGist.Id, gistUpdate);

            Assert.NotNull(updatedGist);
            Assert.Equal<string>(updatedGist.Description, gistUpdate.Description);

            Assert.DoesNotThrow(async () => { await this._gistsClient.Delete(createdGist.Id); });
        }

        [IntegrationTest]
        public async Task CanStarAndUnstarAGist()
        {
            Assert.DoesNotThrow(async () => { await this._gistsClient.Star(testGistId); });

            bool isStarredTrue = await this._gistsClient.IsStarred(testGistId);
            Assert.True(isStarredTrue);

            Assert.DoesNotThrow(async () => { await this._gistsClient.Unstar(testGistId); });

            bool isStarredFalse = await this._gistsClient.IsStarred(testGistId);
            Assert.False(isStarredFalse);
        }

        [IntegrationTest]
        public async Task CanForkAGist()
        {
            var forkedGist = await this._gistsClient.Fork(testGistId);
            
            Assert.NotNull(forkedGist);

            await this._gistsClient.Delete(forkedGist.Id);
        }

        [IntegrationTest]
        public async Task CanListGists()
        {
            // Time is tricky between local and remote, be lenient
            var startTime = DateTimeOffset.Now.Subtract(TimeSpan.FromHours(1));
            var newGist = new NewGist();
            newGist.Description = "my new gist";
            newGist.Public = true;

            newGist.Files.Add("myGistTestFile.cs", "new GistsClient(connection).Create();");

            var createdGist = await this._gistsClient.Create(newGist);

            // Test get all Gists
            var gists = await this._gistsClient.GetAll();
            Assert.NotNull(gists);

            // Test get all Gists since startTime
            gists = await this._gistsClient.GetAll(startTime);

            Assert.NotNull(gists);
            Assert.True(gists.Count > 0);

            // Make sure we can successfully request gists for another user
            Assert.DoesNotThrow(async () => { await this._gistsClient.GetAllForUser("FakeHaacked"); });
            Assert.DoesNotThrow(async () => { await this._gistsClient.GetAllForUser("FakeHaacked", startTime); });

            // Test public gists
            var publicGists = await this._gistsClient.GetAllPublic();
            Assert.True(publicGists.Count > 1);

            var publicGistsSinceStartTime = await this._gistsClient.GetAllPublic(startTime);
            Assert.True(publicGistsSinceStartTime.Count > 0);

            // Test starred gists
            await this._gistsClient.Star(createdGist.Id);
            var starredGists = await this._gistsClient.GetAllStarred();

            Assert.NotNull(starredGists);
            Assert.True(starredGists.Any(x => x.Id == createdGist.Id));

            var starredGistsSinceStartTime = await this._gistsClient.GetAllStarred(startTime);
            Assert.NotNull(starredGistsSinceStartTime);
            Assert.True(starredGistsSinceStartTime.Any(x => x.Id == createdGist.Id));

            await this._gistsClient.Delete(createdGist.Id);
        }
    }
}