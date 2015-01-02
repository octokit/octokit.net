using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class MiscellaneousClientTests
    {
        public class TheRenderRawMarkdownMethod
        {
            [Fact]
            public async Task RequestsTheEmojiEndpoint()
            {
                IApiResponse<string> response = new ApiResponse<string>(new Response(), "<strong>Test</strong>");
                var connection = Substitute.For<IConnection>();
                connection.Post<string>(Args.Uri, "**Test**", "text/html", "text/plain")
                    .Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var html = await client.RenderRawMarkdown("**Test**");

                Assert.Equal("<strong>Test</strong>", html);
                connection.Received()
                    .Post<string>(Arg.Is<Uri>(u => u.ToString() == "markdown/raw"),
                    "**Test**",
                    "text/html",
                    "text/plain");
            }
        }

        public class TheGetEmojisMethod
        {
            [Fact]
            public async Task RequestsTheEmojiEndpoint()
            {
                IApiResponse<Dictionary<string, string>> response = new ApiResponse<Dictionary<string, string>>
                (
                    new Response(),
                    new Dictionary<string, string>
                    {
                        { "foo", "http://example.com/foo.gif" },
                        { "bar", "http://example.com/bar.gif" }
                    }
                );
                var connection = Substitute.For<IConnection>();
                connection.Get<Dictionary<string, string>>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var emojis = await client.GetEmojis();

                Assert.Equal(2, emojis.Count);
                Assert.Equal("foo", emojis[0].Name);
                connection.Received()
                    .Get<Dictionary<string, string>>(Arg.Is<Uri>(u => u.ToString() == "emojis"), null, null);
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new MiscellaneousClient(null));
            }
        }
    }
}
