using System;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using System.Collections.Generic;
using System.Net;
using Octokit.Internal;

namespace Octokit.Tests.Clients
{
    public class RepositoryContentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new RepositoryContentsClient(null));
            }
        }

        public class TheGetReadmeMethod
        {
            [Fact]
            public async Task ReturnsReadme()
            {
                string encodedContent = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello world"));
                var readmeInfo = new ReadmeResponse(
                    encodedContent,
                    "README.md",
                    "https://github.example.com/readme",
                    "https://github.example.com/readme.md",
                    "base64");
                var connection = Substitute.For<IApiConnection>();
                connection.Get<ReadmeResponse>(Args.Uri, null).Returns(Task.FromResult(readmeInfo));
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var contentsClient = new RepositoryContentsClient(connection);

                var readme = await contentsClient.GetReadme("fake", "repo");

                Assert.Equal("README.md", readme.Name);
                connection.Received().Get<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"),
                    null);
                connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    null);
                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
            }

            [Fact]
            public async Task ReturnsReadmeWithRepositoryId()
            {
                string encodedContent = Convert.ToBase64String(Encoding.UTF8.GetBytes("Hello world"));
                var readmeInfo = new ReadmeResponse(
                    encodedContent,
                    "README.md",
                    "https://github.example.com/readme",
                    "https://github.example.com/readme.md",
                    "base64");
                var connection = Substitute.For<IApiConnection>();
                connection.Get<ReadmeResponse>(Args.Uri, null).Returns(Task.FromResult(readmeInfo));
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var contentsClient = new RepositoryContentsClient(connection);

                var readme = await contentsClient.GetReadme(1);

                Assert.Equal("README.md", readme.Name);
                connection.Received().Get<ReadmeResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/readme"),
                    null);
                connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    null);
                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReadme(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReadme("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReadme("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReadme("owner", ""));
            }
        }

        public class TheGetReadmeHtmlMethod
        {
            [Fact]
            public async Task ReturnsReadmeHtml()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var contentsClient = new RepositoryContentsClient(connection);

                var readme = await contentsClient.GetReadmeHtml("fake", "repo");

                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }

            [Fact]
            public async Task ReturnsReadmeHtmlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult("<html>README</html>"));
                var contentsClient = new RepositoryContentsClient(connection);

                var readme = await contentsClient.GetReadmeHtml(1);

                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repositories/1/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReadmeHtml(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetReadmeHtml("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReadmeHtml("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetReadmeHtml("owner", ""));
            }
        }

        public class TheGetContentsMethod
        {
            [Fact]
            public async Task ReturnsContents()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo", "readme.md");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsContentsWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents(1, "readme.md");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/readme.md"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsAllContents()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsAllContentsWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents(1);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name", "path"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null, "path"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name", "path"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "", "path"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents(1, ""));
            }
        }

        public class TheGetRawContentMethod
        {
            [Fact]
            public async Task ReturnsRawContent()
            {
                var result = new byte[] { 1, 2, 3 };

                var connection = Substitute.For<IApiConnection>();
                connection.GetRaw(Args.Uri, default).Returns(result);
                var contentsClient = new RepositoryContentsClient(connection);

                var rawContent = await contentsClient.GetRawContent("fake", "repo", "path/to/file.txt");

                connection.Received().GetRaw(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/path/to/file.txt"), null);
                Assert.Same(result, rawContent);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContent(null, "name", "path"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContent("owner", null, "path"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContent("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContent("", "name", "path"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContent("owner", "", "path"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContent("owner", "name", ""));
            }
        }

        public class TheGetContentsByRefMethod
        {
            [Fact]
            public async Task ReturnsContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "readme.md", GitHubConstants.DefaultBranchName);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == $"repos/fake/repo/contents/readme.md?ref={GitHubConstants.DefaultBranchName}"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef(1, "readme.md", GitHubConstants.DefaultBranchName);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == $"repositories/1/contents/readme.md?ref={GitHubConstants.DefaultBranchName}"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsAllContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", GitHubConstants.DefaultBranchName);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == $"repos/fake/repo/contents/?ref={GitHubConstants.DefaultBranchName}"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task ReturnsAllContentsByRefWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef(1, GitHubConstants.DefaultBranchName);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == $"repositories/1/contents/?ref={GitHubConstants.DefaultBranchName}"));
                Assert.Single(contents);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "path", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "path", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(1, null, "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(1, "path", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "path", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef(1, "", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef(1, "path", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef(1, ""));
            }
        }

        public class TheGetRawContentByRefMethod
        {
            [Fact]
            public async Task ReturnsRawContent()
            {
                var result = new byte[] { 1, 2, 3 };

                var connection = Substitute.For<IApiConnection>();
                connection.GetRaw(Args.Uri, default).Returns(result);
                var contentsClient = new RepositoryContentsClient(connection);

                var rawContent = await contentsClient.GetRawContentByRef("fake", "repo", "path/to/file.txt", "reference");

                connection.Received().GetRaw(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/path/to/file.txt?ref=reference"), null);
                Assert.Same(result, rawContent);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContentByRef(null, "name", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContentByRef("owner", null, "path", "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContentByRef("owner", "name", null, "reference"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetRawContentByRef("owner", "name", "path", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContentByRef("", "name", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContentByRef("owner", "", "path", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContentByRef("owner", "name", "", "reference"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetRawContentByRef("owner", "name", "path", ""));
            }
        }

        public class TheCreateFileMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                await client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repositories/1/contents/path/to/file";
                await client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task PassesRequestObjectWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                await client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repositories/1/contents/path/to/file";
                await client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task PassesRequestObjectWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task PassesRequestObjectWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile(null, "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile("org", null, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile("org", "repo", null, new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile("org", "repo", "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile(1, null, new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateFile(1, "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateFile("", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateFile("org", "", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateFile("org", "repo", "", new CreateFileRequest("message", "myfilecontents", "mybranch")));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateFile(1, "", new CreateFileRequest("message", "myfilecontents", "mybranch")));
            }
        }

        public class TheDeleteFileMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                await client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repositories/1/contents/path/to/file";
                await client.DeleteFile(1, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(
                    Arg.Any<Uri>(),
                    Arg.Is<DeleteFileRequest>(a =>
                        a.Message == "message"
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task PassesRequestObjectWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.DeleteFile(1, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(
                    Arg.Any<Uri>(),
                    Arg.Is<DeleteFileRequest>(a =>
                        a.Message == "message"
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile(null, "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile("org", null, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile("org", "repo", null, new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile("org", "repo", "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile(1, null, new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteFile(1, "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteFile("", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteFile("org", "", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteFile("org", "repo", "", new DeleteFileRequest("message", "1234abc", "mybranch")));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteFile(1, "", new DeleteFileRequest("message", "1234abc", "mybranch")));
            }
        }

        public class TheUpdateFileMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                await client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repositories/1/contents/path/to/file";
                await client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task PassesRequestObjectWithRepositoriesId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                await client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repositories/1/contents/path/to/file";
                await client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public async Task PassesRequestObjectWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task PassesRequestObjectWithRepositoriesIdWithExplicitBase64()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile(null, "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile("org", null, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile("org", "repo", null, new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile("org", "repo", "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile(1, null, new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.UpdateFile(1, "path/to/file", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateFile("", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateFile("org", "", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateFile("org", "repo", "", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));

                await Assert.ThrowsAsync<ArgumentException>(() => client.UpdateFile(1, "", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl1()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive("org", "repo");

                const string expectedUri = "repos/org/repo/tarball/";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl1WithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive(1);

                const string expectedUri = "repositories/1/tarball/";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl2()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive("org", "repo", ArchiveFormat.Zipball);

                const string expectedUri = "repos/org/repo/zipball/";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl2WithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive(1, ArchiveFormat.Zipball);

                const string expectedUri = "repositories/1/zipball/";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl3()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref");

                const string expectedUri = "repos/org/repo/zipball/ref";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl3WithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive(1, ArchiveFormat.Zipball, "ref");

                const string expectedUri = "repositories/1/zipball/ref";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl4()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));

                const string expectedUri = "repos/org/repo/zipball/ref";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task RequestsCorrectUrl4WithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await client.GetArchive(1, ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));

                const string expectedUri = "repositories/1/zipball/ref";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().GetRaw(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), null, Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball, "ref"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, null, TimeSpan.MaxValue));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(1, ArchiveFormat.Tarball, null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetArchive(1, ArchiveFormat.Tarball, null, TimeSpan.MaxValue));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("org", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball, "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball, "ref"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, "ref", TimeSpan.Zero));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetArchive(1, ArchiveFormat.Tarball, "ref", TimeSpan.Zero));
            }

            [Fact]
            public async Task ReturnsExpectedContent()
            {
                var headers = new Dictionary<string, string>();
                var response = TestSetup.CreateResponse(HttpStatusCode.OK, new byte[] { 1, 2, 3, 4 }, headers);
                var responseTask = Task.FromResult<IApiResponse<byte[]>>(new ApiResponse<byte[]>(response));

                var connection = Substitute.For<IConnection>();
                connection.GetRaw(Arg.Is<Uri>(u => u.ToString() == "repos/org/repo/tarball/"), null, TimeSpan.FromMinutes(60))
                    .Returns(responseTask);

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);

                var client = new RepositoryContentsClient(apiConnection);

                var actual = await client.GetArchive("org", "repo");

                Assert.Equal(new byte[] { 1, 2, 3, 4 }, actual);
            }
        }
    }
}