using System;
using System.Linq;
using System.Threading.Tasks;
using Octokit.Tests.Integration.Helpers;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class RepositoryContentsClientTests
    {
        public class TheGetReadmeMethod
        {
            [IntegrationTest]
            public async Task ReturnsReadmeForSeeGit()
            {
                var github = Helper.GetAuthenticatedClient();

                var readme = await github.Repository.Content.GetReadme("octokit", "octokit.net");
                Assert.Equal("README.md", readme.Name);
                string readMeHtml = await readme.GetHtmlContent();
                Assert.True(readMeHtml.StartsWith("<div class="));
                Assert.Contains(@"data-path=""README.md"" id=""file""", readMeHtml);
                Assert.Contains("Octokit - GitHub API Client Library for .NET", readMeHtml);
            }

            [IntegrationTest]
            public async Task ReturnsReadmeHtmlForSeeGit()
            {
                var github = Helper.GetAuthenticatedClient();

                var readmeHtml = await github.Repository.Content.GetReadmeHtml("octokit", "octokit.net");
                Assert.True(readmeHtml.StartsWith("<div class="));
                Assert.Contains(@"data-path=""README.md"" id=""readme""", readmeHtml);
                Assert.Contains("Octokit - GitHub API Client Library for .NET", readmeHtml);
            }
        }

        public class TheGetContentsMethod
        {
            [IntegrationTest]
            public async Task GetsFileContent()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents("octokit", "octokit.net", "Octokit.Reactive/ObservableGitHubClient.cs");

                Assert.Equal(1, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octokit/octokit.net/blob/master/Octokit.Reactive/ObservableGitHubClient.cs"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContent()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents("octokit", "octokit.net", "Octokit");

                Assert.True(contents.Count > 2);
                Assert.Equal(ContentType.Dir, contents.First().Type);
            }
        }

        [IntegrationTest]
        public async Task CrudTest()
        {
            var client = Helper.GetAuthenticatedClient();
            var fixture = client.Repository.Content;
            var repoName = Helper.MakeNameWithTimestamp("source-repo");

            using (var context = await client.CreateRepositoryContext(new NewRepository(repoName) { AutoInit = true }))
            {
                var repository = context.Repository;

                var file = await fixture.CreateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "Some Content"));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "New Content", fileSha));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", fileSha));

                await Assert.ThrowsAsync<NotFoundException>(
                     () => fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
        }

        [IntegrationTest]
        public async Task CrudTestWithNamedBranch()
        {
            var client = Helper.GetAuthenticatedClient();
            var fixture = client.Repository.Content;
            var repoName = Helper.MakeNameWithTimestamp("source-repo");
            var branchName = "other-branch";

            using (var context = await client.CreateRepositoryContext(new NewRepository(repoName) { AutoInit = true }))
            {
                var repository = context.Repository;

                var master = await client.Git.Reference.Get(Helper.UserName, repository.Name, "heads/master");
                await client.Git.Reference.Create(Helper.UserName, repository.Name, new NewReference("refs/heads/" + branchName, master.Object.Sha));
                var file = await fixture.CreateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "Some Content", branchName));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "New Content", fileSha, branchName));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", fileSha, branchName));

                await Assert.ThrowsAsync<NotFoundException>(
                    () => fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
        }

        [IntegrationTest(Skip = "this will probably take too long")]
        public async Task GetsArchiveAsTarball()
        {
            var github = Helper.GetAuthenticatedClient();

            var archive = await github
                .Repository
                .Content
                .GetArchive("octokit", "octokit.net");

            Assert.NotEmpty(archive);
        }

        [IntegrationTest]
        public async Task GetsArchiveAsZipball()
        {
            var github = Helper.GetAuthenticatedClient();

            var archive = await github
                .Repository
                .Content
                .GetArchive("shiftkey", "reactivegit", ArchiveFormat.Zipball);

            Assert.NotEmpty(archive);
        }

        [IntegrationTest]
        public async Task GetsArchiveForReleaseBranchAsTarball()
        {
            var github = Helper.GetAuthenticatedClient();

            var archive = await github
                .Repository
                .Content
                .GetArchive("alfhenrik", "ScriptCs.OctoKit", ArchiveFormat.Tarball, "dev");

            Assert.NotEmpty(archive);
        }
    }
}