using System;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using System.Collections.Generic;

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
        }

        public class TheGetContentsMethod
        {
            [Fact]
            public async Task ReturnsContents()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, Args.ApiOptions).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), Args.ApiOptions);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsWithApiOptions()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };
                
                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, options).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo", options);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), options);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsWithPath()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, Args.ApiOptions).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo", "readme.md");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md"), Args.ApiOptions);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsWithPathAndApiOptions()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, options).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContents("fake", "repo", "readme.md", options);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md"), options);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, Args.ApiOptions).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/?ref=master"), Args.ApiOptions);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithApiOptions()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, options).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master", options);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/?ref=master"), options);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithPath()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, Args.ApiOptions).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "readme.md", "master");

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md?ref=master"), Args.ApiOptions);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithPathAndApiOptions()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var options = new ApiOptions
                {
                    StartPage = 1,
                    PageCount = 1,
                    PageSize = 1
                };

                var connection = Substitute.For<IApiConnection>();
                connection.GetAll<RepositoryContent>(Args.Uri, options).Returns(Task.FromResult(result.AsReadOnly() as IReadOnlyList<RepositoryContent>));
                var contentsClient = new RepositoryContentsClient(connection);

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "readme.md", "master", options);

                connection.Received().GetAll<RepositoryContent>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md?ref=master"), options);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task EnsuresNonNullarguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name", "readme.md"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null, "readme.md"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", (string)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents(null, "name", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", null, "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", "readme.md", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", "master"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", "master"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "master"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (string)null));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "master", null));
                
                // empty string checks

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", ""));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name", "readme.md"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "", "readme.md"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "name", ""));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("", "name", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContents("owner", "name", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContents("owner", "name", "readme.md", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", ""));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "master"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "master"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "master"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", ""));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference, ApiOptions options)
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "master", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "master", null));
            }
        }

        public class TheCreateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "myfilecontents"
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
            }
        }

        public class TheDeleteFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

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
            }
        }

        public class TheUpdateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "myfilecontents"
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
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public void EnsurePassingCorrectParameters()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryContentsClient(connection);

                client.GetArchive("org", "repo", ArchiveFormat.Tarball, "dev");

                const string expectedUri = "repos/org/repo/tarball/dev";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Connection.Received().Get<byte[]>(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }
        }
    }
}