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
    public class ObservableRepositoryContentsClientTest
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

                var githubClient = Substitute.For<IGitHubClient>();
                var readmeFake = new Readme(readmeInfo, githubClient.Connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);

                githubClient.Repository.Content.GetReadme("fake", "repo").Returns(Task.FromResult(readmeFake));

                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");
                githubClient.Connection.GetHtml(Args.Uri, null)
                    .Returns(Task.FromResult(apiResponse));
                
                var readme = await contentsClient.GetReadme("fake", "repo");

                Assert.Equal("README.md", readme.Name);

                githubClient.Repository.Content.Received(1).GetReadme("fake", "repo");
                githubClient.Connection.DidNotReceive().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"),
                    Args.EmptyDictionary);

                var htmlReadme = await readme.GetHtmlContent();
                Assert.Equal("<html>README</html>", htmlReadme);
                githubClient.Connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme.md"), null);
            }
        }

        public class TheGetReadmeHtmlMethod
        {
            [Fact]
            public async Task ReturnsReadmeHtml()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<string> apiResponse = new ApiResponse<string>(new Response(), "<html>README</html>");

                connection.GetHtml(Args.Uri, null).Returns(Task.FromResult(apiResponse));

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

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), Args.EmptyDictionary, null);
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

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null).Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo", options).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/"), 
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsWithPath()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo", "readme.md").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md"), Args.EmptyDictionary, null);
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

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null).Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContents("fake", "repo", "readme.md", options).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRef()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master").ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/?ref=master"), Args.EmptyDictionary, null);
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

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null).Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master", options).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/?ref=master"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public async Task ReturnsContentsByRefWithPath()
            {
                var result = new List<RepositoryContent> { new RepositoryContent() };

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );
                connection.Get<List<RepositoryContent>>(Args.Uri, Args.EmptyDictionary, null)
                    .Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master", "readme.md").ToList();

                connection.Received()
                    .Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md?ref=master"), Args.EmptyDictionary, null);
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

                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var contentsClient = new ObservableRepositoryContentsClient(githubClient);
                IApiResponse<List<RepositoryContent>> response = new ApiResponse<List<RepositoryContent>>
                    (
                    new Response { ApiInfo = new ApiInfo(new Dictionary<string, Uri>(), new List<string>(), new List<string>(), "etag", new RateLimit()) },
                    result
                    );

                connection.Get<List<RepositoryContent>>(Args.Uri, Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null).Returns(Task.FromResult(response));

                var contents = await contentsClient.GetAllContentsByRef("fake", "repo", "master", "readme.md", options).ToList();

                connection.Received().Get<List<RepositoryContent>>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/contents/readme.md?ref=master"),
                    Arg.Is<Dictionary<string, string>>(d => d.Count == 2), null);
                Assert.Equal(1, contents.Count);
            }

            [Fact]
            public void EnsuresNonNullarguments()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, ApiOptions options)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", (string)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path, ApiOptions options)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, "readme.md", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, null, "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", "readme.md", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents(null, "name", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, "readme.md", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", null, "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", "readme.md", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference, ApiOptions options)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, (ApiOptions)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, (string)null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "master"));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (string)null));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference, ApiOptions options)
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, null, "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, null, "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", null, "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef(null, "name", "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, "readme.md", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, null, "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", null, "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "master", null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", null, "master", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", null, null));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", null, ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "master", null));

                // empty string checks

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name)
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", ""));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, ApiOptions options)
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path)
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "name", ""));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContents(string owner, string name, string path, ApiOptions options)
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", "readme.md", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "readme.md", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("", "name", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "readme.md", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "name", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContents("owner", "name", "", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContents("owner", "name", "readme.md", null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference)
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", ""));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string reference, ApiOptions options)
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", (ApiOptions)null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", (ApiOptions)null));

                // public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference)
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", ""));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "master"));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", ""));

                //public Task<IReadOnlyList<RepositoryContent>> GetAllContentsByRef(string owner, string name, string path, string reference, ApiOptions options)
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "", "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("", "name", "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", "readme.md", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "", "readme.md", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "", "readme.md", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "master", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "", "master", ApiOptions.None));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "", null));
                Assert.Throws<ArgumentException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "", ApiOptions.None));
                Assert.Throws<ArgumentNullException>(() => client.GetAllContentsByRef("owner", "name", "readme.md", "master", null));
            }
        }

        public class TheCreateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                client.CreateFile("org", "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(
                    Arg.Any<Uri>(),
                    Arg.Is<CreateFileRequest>(a =>
                        a.Message == "message"
                        && a.Content == "myfilecontents"
                        && a.Branch == "mybranch"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.CreateFile(null, "repo", "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", null, "path/to/file", new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", "repo", null, new CreateFileRequest("message", "myfilecontents", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.CreateFile("org", "repo", "path/to/file", null));
            }
        }

        public class TheDeleteFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.DeleteFile("org", "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch"));

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

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
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.DeleteFile(null, "repo", "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", null, "path/to/file", new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", "repo", null, new DeleteFileRequest("message", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.DeleteFile("org", "repo", "path/to/file", null));
            }
        }

        public class TheUpdateFileMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                string expectedUri = "repos/org/repo/contents/path/to/file";
                client.UpdateFile("org", "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch"));

                connection.Received().Put<RepositoryContentChangeSet>(Arg.Is<Uri>(u => u.ToString() == expectedUri), Arg.Any<object>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

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
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                Assert.Throws<ArgumentNullException>(() => client.UpdateFile(null, "repo", "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", null, "path/to/file", new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", "repo", null, new UpdateFileRequest("message", "myfilecontents", "1234abc", "mybranch")));
                Assert.Throws<ArgumentNullException>(() => client.UpdateFile("org", "repo", "path/to/file", null));
            }
        }

        public class TheGetArchiveMethod
        {
            [Fact]
            public void EnsurePassingCorrectParameters()
            {
                var connection = Substitute.For<IConnection>();
                var githubClient = new GitHubClient(connection);
                var client = new ObservableRepositoryContentsClient(githubClient);

                client.GetArchive("org", "repo", ArchiveFormat.Tarball, "dev");

                const string expectedUri = "repos/org/repo/tarball/dev";
                var expectedTimeSpan = TimeSpan.FromMinutes(60);

                connection.Received().Get<byte[]>(Arg.Is<Uri>(uri => uri.ToString() == expectedUri), Arg.Is<TimeSpan>(span => span == expectedTimeSpan));
            }
        }
    }
}