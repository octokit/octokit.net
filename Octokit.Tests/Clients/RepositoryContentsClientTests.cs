using System;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryContentsClientTests
    {
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

        public class TheGetArchiveLinkMethod
        {
            [Fact]
            public async Task ReturnsArchiveLinkWithDefaults()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.GetRedirect(Args.Uri).Returns(Task.FromResult("https://codeload.github.com/fake/repo/legacy.tar.gz/master"));
                var contentsClient = new RepositoryContentsClient(connection);

                var archiveLink = await contentsClient.GetArchiveLink("fake", "repo");

                connection.Received().GetRedirect(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/tarball/master"));
                Assert.Equal("https://codeload.github.com/fake/repo/legacy.tar.gz/master", archiveLink);
            }

            [Fact]
            public async Task ReturnsArchiveLinkWithSpecifiedValues()
            {
                var connection = Substitute.For<IApiConnection>();
                connection.GetRedirect(Args.Uri).Returns(Task.FromResult("https://codeload.github.com/fake/repo/legazy.zip/release"));
                var contentsClient = new RepositoryContentsClient(connection);

                var archiveLink = await contentsClient.GetArchiveLink("fake", "repo", ArchiveFormat.Zipball, "release");

                connection.Received().GetRedirect(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/zipball/release"));
                Assert.Equal("https://codeload.github.com/fake/repo/legazy.zip/release", archiveLink);
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var contentsClient = new RepositoryContentsClient(connection);

                AssertEx.Throws<ArgumentNullException>(async () => await contentsClient.GetArchiveLink(null, "name"));
                AssertEx.Throws<ArgumentNullException>(async () => await contentsClient.GetArchiveLink("owner", null));
                AssertEx.Throws<ArgumentNullException>(async () => await contentsClient.GetArchiveLink("owner", "name", ArchiveFormat.Tarball, null));
                AssertEx.Throws<ArgumentException>(async () => await contentsClient.GetArchiveLink("", "name"));
                AssertEx.Throws<ArgumentException>(async () => await contentsClient.GetArchiveLink("owner", ""));
                AssertEx.Throws<ArgumentException>(async () => await contentsClient.GetArchiveLink("owner", "name", ArchiveFormat.Zipball, ""));
            }
        }
    }
}