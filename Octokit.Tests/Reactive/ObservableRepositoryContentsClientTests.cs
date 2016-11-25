using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryContentsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableRepositoryContentsClient(null));
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

                var gitHubClient = Substitute.For<IGitHubClient>();
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.GetHtml(new Uri(readmeInfo.Url)).Returns(Task.FromResult("<html>README</html>"));
                var readmeFake = new Readme(readmeInfo, apiConnection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);

                gitHubClient.Repository.Content.GetReadme("fake", "repo").Returns(Task.FromResult(readmeFake));

                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");
                gitHubClient.Connection.GetHtml(Args.Uri, null)
                    .Returns(Task.FromResult(apiResponse));

                var readme = await contentsClient.GetReadme("fake", "repo");

                Assert.Equal("README.md", readme.Name);

                gitHubClient.Repository.Content.Received(1).GetReadme("fake", "repo");
                gitHubClient.Connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    Args.EmptyDictionary);

                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                apiConnection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
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

                var gitHubClient = Substitute.For<IGitHubClient>();
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.GetHtml(new Uri(readmeInfo.Url)).Returns(Task.FromResult("<html>README</html>"));
                var readmeFake = new Readme(readmeInfo, apiConnection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);

                gitHubClient.Repository.Content.GetReadme(1).Returns(Task.FromResult(readmeFake));

                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");
                gitHubClient.Connection.GetHtml(Args.Uri, null)
                    .Returns(Task.FromResult(apiResponse));

                var readme = await contentsClient.GetReadme(1);

                Assert.Equal("README.md", readme.Name);

                gitHubClient.Repository.Content.Received(1).GetReadme(1);
                gitHubClient.Connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    Args.EmptyDictionary);

                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                apiConnection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetReadme(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetReadme("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetReadme("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetReadme("owner", ""));
            }
        }

        public class TheGetReadmeHtmlMethod
        {
            [Fact]
            public async Task ReturnsReadmeHtml()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");

                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(apiResponse));

                var readme = await contentsClient.GetReadmeHtml("fake", "repo");

                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }

            [Fact]
            public async Task ReturnsReadmeHtmlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");

                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(apiResponse));

                var readme = await contentsClient.GetReadmeHtml(1);

                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "repositories/1/readme"), null);
                Assert.Equal("<html>README</html>", readme);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetReadmeHtml(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetReadmeHtml("owner", null));

                Assert.Throws<ArgumentException>(() => client.GetReadmeHtml("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetReadmeHtml("owner", ""));
            }
        }

        public class TheGetContentsMethod
        {
            [Fact]
            public async Task ReturnsContents()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents(1).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsAllContents()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsAllContentsWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents(1).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", "path"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, "path"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "path"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "path"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "name", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllContents(1, ""));
            }
        }

        public class TheGetContentsByRefMethod
        {
            [Fact]
            public async Task ReturnsContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master", "readme.md").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md?ref=master"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef(1, "master", "readme.md").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/readme.md?ref=master"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsAllContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/?ref=master"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsAllContentsByRefWithRepositoryId()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(gitHubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, null, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef(1, "master").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/contents/?ref=master"), null, null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "path", "reference"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "path", "reference"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "reference"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "path", null));

                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(1, null, "reference"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(1, "path", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(1, null));

                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "ref"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "path", "reference"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "path", "reference"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "reference"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "path", ""));

                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef(1, "", "reference"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef(1, "path", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef(1, ""));
            }
        }

        public class TheCreateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repositories/1/contents/path/to/file";
                client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void PassesRequestObjectWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }
            [Fact]
            public void RequestsCorrectUrlWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repositories/1/contents/path/to/file";
                client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObjectWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void PassesRequestObjectWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.CreateFile(1, "path/to/file", new CreateFileRequest("message", "bXlmaWxlY29udGVudHM=", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.CreateFile(null, "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", null, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", "repo", null, new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", "repo", "path/to/file", null));

                Assert.Throws<ArgumentNullException>(() => client.CreateFile(1, null, new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile(1, "path/to/file", null));

                Assert.Throws<ArgumentException>(() => client.CreateFile("", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.CreateFile("org", "", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.CreateFile("org", "repo", "", new CreateFileRequest("message", "myfilecontents", "mybranch")));

                Assert.Throws<ArgumentException>(() => client.CreateFile(1, "", new CreateFileRequest("message", "myfilecontents", "mybranch")));
            }
        }

        public class TheDeleteFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repositories/1/contents/path/to/file";
                client.DeleteFile(1, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Delete(
                    Arg.Any<Uri>(),
                    Arg.Is<DeleteFileRequest>(a =>
                        a.Message == "message"
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void PassesRequestObjectWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.DeleteFile(1, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Delete(
                    Arg.Any<Uri>(),
                    Arg.Is<DeleteFileRequest>(a =>
                        a.Message == "message"
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.DeleteFile(null, "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", null, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", "repo", null, new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", "repo", "path/to/file", null));

                Assert.Throws<ArgumentNullException>(() => client.DeleteFile(1, null, new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile(1, "path/to/file", null));

                Assert.Throws<ArgumentException>(() => client.DeleteFile("", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.DeleteFile("org", "", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.DeleteFile("org", "repo", "", new DeleteFileRequest("message", "1234abc", "mybranch")));

                Assert.Throws<ArgumentException>(() => client.DeleteFile(1, "", new DeleteFileRequest("message", "1234abc", "mybranch")));
            }
        }

        public class TheUpdateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repositories/1/contents/path/to/file";
                client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void PassesRequestObjectWithRepositoriesId()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void RequestsCorrectUrlWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void RequestsCorrectUrlWithRepositoryIdWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                string expectedUri = "repositories/1/contents/path/to/file";
                client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObjectWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void PassesRequestObjectWithRepositoriesIdWithExplicitBase64()
            {
                var connection = Substitute.For<IConnection>();
                var gitHubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.UpdateFile(1, "path/to/file", new UpdateFileRequest("message", "bXlmaWxlY29udGVudHM=", "1234abc", "mybranch", false));

                gitHubClient.Connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<UpdateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "bXlmaWxlY29udGVudHM="
                        && a.Sha == "1234abc"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.UpdateFile(null, "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", null, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", "repo", null, new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", "repo", "path/to/file", null));

                Assert.Throws<ArgumentNullException>(() => client.UpdateFile(1, null, new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile(1, "path/to/file", null));

                Assert.Throws<ArgumentException>(() => client.UpdateFile("", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.UpdateFile("org", "", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentException>(() => client.UpdateFile("org", "repo", "", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));

                Assert.Throws<ArgumentException>(() => client.UpdateFile(1, "", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public void RequestsCorrectUrl1()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive("org", "repo");
                
                gitHubClient.Received().Repository.Content.GetArchive("org", "repo");
            }

            [Fact]
            public void RequestsCorrectUrl1WithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive(1);

                gitHubClient.Received().Repository.Content.GetArchive(1);
            }

            [Fact]
            public void RequestsCorrectUrl2()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive("org", "repo", ArchiveFormat.Zipball);

                gitHubClient.Received().Repository.Content.GetArchive("org", "repo", ArchiveFormat.Zipball);
            }

            [Fact]
            public void RequestsCorrectUrl2WithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive(1, ArchiveFormat.Zipball);

                gitHubClient.Received().Repository.Content.GetArchive(1, ArchiveFormat.Zipball);
            }

            [Fact]
            public void RequestsCorrectUrl3()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref");

                gitHubClient.Received().Repository.Content.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref");
            }

            [Fact]
            public void RequestsCorrectUrl3WithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive(1, ArchiveFormat.Zipball, "ref");
                
                gitHubClient.Received().Repository.Content.GetArchive(1, ArchiveFormat.Zipball, "ref");
            }

            [Fact]
            public void RequestsCorrectUrl4()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));

                gitHubClient.Received().Repository.Content.GetArchive("org", "repo", ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));
            }

            [Fact]
            public void RequestsCorrectUrl4WithRepositoryId()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                client.GetArchive(1, ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));

                gitHubClient.Received().Repository.Content.GetArchive(1, ArchiveFormat.Zipball, "ref", TimeSpan.FromMinutes(60));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryContentsClient(gitHubClient);

                Assert.Throws<ArgumentNullException>(() => client.GetArchive(null, "repo"));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", null));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball, "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball, "ref"));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, null));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive(null, "repo", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", null, ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, null, TimeSpan.MaxValue));

                Assert.Throws<ArgumentNullException>(() => client.GetArchive(1, ArchiveFormat.Tarball, null));
                Assert.Throws<ArgumentNullException>(() => client.GetArchive(1, ArchiveFormat.Tarball, null, TimeSpan.MaxValue));

                Assert.Throws<ArgumentException>(() => client.GetArchive("", "repo"));
                Assert.Throws<ArgumentException>(() => client.GetArchive("org", ""));
                Assert.Throws<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball));
                Assert.Throws<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball));
                Assert.Throws<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball, "ref"));
                Assert.Throws<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball, "ref"));
                Assert.Throws<ArgumentException>(() => client.GetArchive("", "repo", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));
                Assert.Throws<ArgumentException>(() => client.GetArchive("org", "", ArchiveFormat.Tarball, "ref", TimeSpan.MaxValue));

                Assert.Throws<ArgumentException>(() => client.GetArchive("org", "repo", ArchiveFormat.Tarball, "ref", TimeSpan.Zero));

                Assert.Throws<ArgumentException>(() => client.GetArchive(1, ArchiveFormat.Tarball, "ref", TimeSpan.Zero));
            }
        }
    }
}