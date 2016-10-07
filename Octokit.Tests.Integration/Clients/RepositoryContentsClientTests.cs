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
            public async Task ReturnsReadmeForSeeGitWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var readme = await github.Repository.Content.GetReadme(7528679);
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

            [IntegrationTest]
            public async Task ReturnsReadmeHtmlForSeeGitWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var readmeHtml = await github.Repository.Content.GetReadmeHtml(7528679);
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
            public async Task GetsFileContentWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents(7528679, "Octokit.Reactive/ObservableGitHubClient.cs");

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

            [IntegrationTest]
            public async Task GetsDirectoryContentWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents(7528679, "Octokit");

                Assert.True(contents.Count > 2);
                Assert.Equal(ContentType.Dir, contents.First().Type);
            }

            [IntegrationTest]
            public async Task GetsFileContentWholeRepo()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents("octocat", "Spoon-Knife");

                Assert.Equal(3, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octocat/Spoon-Knife/blob/master/README.md"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsFileContentWholeRepoWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents(1300192);

                Assert.Equal(3, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octocat/Spoon-Knife/blob/master/README.md"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContentWholeRepo()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents("octocat", "octocat.github.io");

                Assert.NotEmpty(contents);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContentWholeRepoWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContents(17881631);

                Assert.NotEmpty(contents);
            }
        }

        public class TheGetContentsByRefMethod
        {
            [IntegrationTest]
            public async Task GetsFileContent()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef("octokit", "octokit.net", "Octokit.Reactive/ObservableGitHubClient.cs", "master");

                Assert.Equal(1, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octokit/octokit.net/blob/master/Octokit.Reactive/ObservableGitHubClient.cs"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsFileContentWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef(7528679, "Octokit.Reactive/ObservableGitHubClient.cs", "master");

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
                    .GetAllContentsByRef("octokit", "octokit.net", "Octokit", "master");

                Assert.True(contents.Count > 2);
                Assert.Equal(ContentType.Dir, contents.First().Type);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContentWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef(7528679, "Octokit", "master");

                Assert.True(contents.Count > 2);
                Assert.Equal(ContentType.Dir, contents.First().Type);
            }

            [IntegrationTest]
            public async Task GetsFileContentWholeRepo()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef("octocat", "Spoon-Knife", "master");

                Assert.Equal(3, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octocat/Spoon-Knife/blob/master/README.md"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsFileContentWholeRepoWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef(1300192, "master");

                Assert.Equal(3, contents.Count);
                Assert.Equal(ContentType.File, contents.First().Type);
                Assert.Equal(new Uri("https://github.com/octocat/Spoon-Knife/blob/master/README.md"), contents.First().HtmlUrl);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContentWholeRepo()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef("octocat", "octocat.github.io", "master");

                Assert.NotEmpty(contents);
            }

            [IntegrationTest]
            public async Task GetsDirectoryContentWholeRepoWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var contents = await github
                    .Repository
                    .Content
                    .GetAllContentsByRef(17881631, "master");

                Assert.NotEmpty(contents);
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
        public async Task CrudTestWithRepositoryId()
        {
            var client = Helper.GetAuthenticatedClient();
            var fixture = client.Repository.Content;
            var repoName = Helper.MakeNameWithTimestamp("source-repo");

            using (var context = await client.CreateRepositoryContext(new NewRepository(repoName) { AutoInit = true }))
            {
                var repository = context.Repository;

                var file = await fixture.CreateFile(
                    repository.Id,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "Some Content"));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Id,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "New Content", fileSha));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Id,
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

        [IntegrationTest]
        public async Task CrudTestWithNamedBranchWithRepositoryId()
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
                    repository.Id,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "Some Content", branchName));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Id,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "New Content", fileSha, branchName));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Id,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", fileSha, branchName));

                await Assert.ThrowsAsync<NotFoundException>(
                    () => fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
        }

        [IntegrationTest]
        public async Task CrudTestWithExplicitBase64()
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
                    new CreateFileRequest("Test commit", "U29tZSBDb250ZW50", false));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "TmV3IENvbnRlbnQ=", fileSha, false));
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
        public async Task CrudTestWithRepositoryIdWithExplicitBase64()
        {
            var client = Helper.GetAuthenticatedClient();
            var fixture = client.Repository.Content;
            var repoName = Helper.MakeNameWithTimestamp("source-repo");

            using (var context = await client.CreateRepositoryContext(new NewRepository(repoName) { AutoInit = true }))
            {
                var repository = context.Repository;

                var file = await fixture.CreateFile(
                    repository.Id,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "U29tZSBDb250ZW50", false));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Id,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "TmV3IENvbnRlbnQ=", fileSha, false));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt");
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Id,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", fileSha));

                await Assert.ThrowsAsync<NotFoundException>(
                     () => fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
        }

        [IntegrationTest]
        public async Task CrudTestWithNamedBranchWithExplicitBase64()
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
                    new CreateFileRequest("Test commit", "U29tZSBDb250ZW50", branchName, false));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Owner.Login,
                    repository.Name,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "TmV3IENvbnRlbnQ=", fileSha, branchName, false));
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

        [IntegrationTest]
        public async Task CrudTestWithNamedBranchWithRepositoryIdWithExplicitBase64()
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
                    repository.Id,
                    "somefile.txt",
                    new CreateFileRequest("Test commit", "U29tZSBDb250ZW50", branchName, false));
                Assert.Equal("somefile.txt", file.Content.Name);

                var contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                string fileSha = contents.First().Sha;
                Assert.Equal("Some Content", contents.First().Content);

                var update = await fixture.UpdateFile(
                    repository.Id,
                    "somefile.txt",
                    new UpdateFileRequest("Updating file", "TmV3IENvbnRlbnQ=", fileSha, branchName, false));
                Assert.Equal("somefile.txt", update.Content.Name);

                contents = await fixture.GetAllContentsByRef(repository.Owner.Login, repository.Name, "somefile.txt", branchName);
                Assert.Equal("New Content", contents.First().Content);
                fileSha = contents.First().Sha;

                await fixture.DeleteFile(
                    repository.Id,
                    "somefile.txt",
                    new DeleteFileRequest("Deleted file", fileSha, branchName));

                await Assert.ThrowsAsync<NotFoundException>(
                    () => fixture.GetAllContents(repository.Owner.Login, repository.Name, "somefile.txt"));
            }
        }

        public class TheGetArchiveMethod
        {
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
            public async Task GetsArchiveAsTarballWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive(1296269); // octocat/Hello-World repo

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveAsZipball()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive("octocat", "Hello-World", ArchiveFormat.Zipball);

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveAsZipballWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive(1296269, ArchiveFormat.Zipball); // octocat/Hello-World repo

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveForReleaseBranchAsTarball()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive("octocat", "Hello-World", ArchiveFormat.Tarball, "master");

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveForReleaseBranchAsTarballWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive(1296269, ArchiveFormat.Tarball, "master"); // octocat/Hello-World repo

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveForReleaseBranchAsTarballWithTimeout()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive("octocat", "Hello-World", ArchiveFormat.Tarball, "master", TimeSpan.FromMinutes(60));

                Assert.NotEmpty(archive);
            }

            [IntegrationTest]
            public async Task GetsArchiveForReleaseBranchAsTarballWithTimeoutWithRepositoryId()
            {
                var github = Helper.GetAuthenticatedClient();

                var archive = await github
                    .Repository
                    .Content
                    .GetArchive(1296269, ArchiveFormat.Tarball, "master", TimeSpan.FromMinutes(60)); // octocat/Hello-World repo

                Assert.NotEmpty(archive);
            }
        }
    }
}