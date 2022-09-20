using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MarkdownClientTests
    {
        public class TheRenderRawMarkdownMethod
        {
            [Fact]
            public async Task RequestsTheRawMarkdownEndpoint()
            {
                var markdown = "**Test**";
                var response = "<strong>Test</strong>";
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Post<string>(
                        Arg.Is<Uri>(u => u.ToString() == "markdown/raw"),
                        markdown,
                        "text/html",
                        "text/plain")
                    .Returns(Task.FromResult(response));
                var client = new MarkdownClient(apiConnection);

                var html = await client.RenderRawMarkdown(markdown);

                Assert.Equal("<strong>Test</strong>", html);
                apiConnection.Received()
                    .Post<string>(Arg.Is<Uri>(u => u.ToString() == "markdown/raw"),
                    markdown,
                    "text/html",
                    "text/plain");
            }
        }
        public class TheRenderArbitraryMarkdownMethod
        {
            [Fact]
            public async Task RequestsTheMarkdownEndpoint()
            {
                var response = "<strong>Test</strong>";

                var payload = new NewArbitraryMarkdown("testMarkdown", "gfm", "testContext");

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Post<string>(Args.Uri, payload, "text/html", "text/plain")
                    .Returns(Task.FromResult(response));

                var client = new MarkdownClient(apiConnection);

                var html = await client.RenderArbitraryMarkdown(payload);
                Assert.Equal("<strong>Test</strong>", html);
                apiConnection.Received()
                    .Post<string>(Arg.Is<Uri>(u => u.ToString() == "markdown"),
                    payload,
                    "text/html",
                    "text/plain");
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new MiscellaneousClient(null));
            }
        }
    }
}
