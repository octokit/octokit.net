﻿using System;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
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
                var readmeInfo = new ReadmeResponse
                {
                    Content = encodedContent,
                    Encoding = "base64",
                    Name = "README.md",
                    Url = "https://github.example.com/readme.md",
                    HtmlUrl = "https://github.example.com/readme"
                };
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
                connection.Received().GetHtml(Arg.Is<Uri>(u => u.ToString() == "https://github.example.com/readme"), null);
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
    }
}