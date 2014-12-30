using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryContentsClientTests
    {
        public class TheGetContentsMethod
        {
            [IntegrationTest]
            public async Task GetsFileContent()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                var contents = await github
                    .Repository
                    .Content
                    .GetContents("octokit", "octokit.net", "Octokit.Reactive/ObservableGitHubClient.cs");

                Assert.Equal(1, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octokit/octokit.net/blob/master/Octokit.Reactive/ObservableGitHubClient.cs"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContent()
            {
                var github = new GitHubClient(new ProductHeaderValue("OctokitTests"))
                {
                    Credentials = Helper.Credentials
                };

                var contents = await github
                    .Repository
                    .Content
                    .GetContents("octokit", "octokit.net", "Octokit");

                Assert.True(contents.Count > 2);
                Assert.Equal(ContentType.Dir, contents.First().Type);
            }
        }

        [IntegrationTest]
        public async Task CrudTest()
        {
            var client = new GitHubClient(new ProductHeaderValue("OctokitTests"))
            {
                Credentials = Helper.Credentials
            };
            Repository repository = null;
            try
            {
                var fixture = client.Repository.Content;
                var repoName = Helper.MakeNameWithTimestamp("source-repo");
                repository = await client.Repository.Create(new NewRepository { Name = repoName, AutoInit = true });

                var file = await fixture.CreateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "Some Content"));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetContents(repository.Owner.Login, repository.Name, "somefile.txt");
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "New Content", fileSha));
                string updatedFileSha = update.Commit.Sha;
                Assert.Equal("somefile.txt", update.Content.Name);

                await fixture.DeleteFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", updatedFileSha));

                await Assert.ThrowsAsync<FileNotFoundException>(
                    async () => await fixture.GetContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
            finally
            {
                Assert.NotNull(repository);
                client.Repository.Delete(repository.Owner.Login, repository.Name);
            }
        }
    }
}