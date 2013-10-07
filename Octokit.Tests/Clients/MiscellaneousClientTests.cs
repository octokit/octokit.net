using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Clients;
using Octokit.Http;
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
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<string> response = new ApiResponse<string>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    Body = "<strong>Test</strong>"
                };
                var connection = Substitute.For<IConnection>();
                connection.PostAsync<string>(Args.Uri, "**Test**", "text/plain", "text/html")
                    .Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var html = await client.RenderRawMarkdown("**Test**");

                Assert.Equal("<strong>Test</strong>", html);
                connection.Received()
                    .PostAsync<string>(Arg.Is<Uri>(u => u.ToString() == "/markdown/raw"),
                    "**Test**",
                    "text/plain",
                    "text/html");
            }
        }

        public class TheGetEmojisMethod
        {
            [Fact]
            public async Task RequestsTheEmojiEndpoint()
            {
                var links = new Dictionary<string, Uri>();
                var scopes = new List<string>();
                IResponse<Dictionary<string, string>> response = new ApiResponse<Dictionary<string, string>>
                {
                    ApiInfo = new ApiInfo(links, scopes, scopes, "", 1, 1),
                    BodyAsObject = new Dictionary<string, string>
                    {
                        { "foo", "http://example.com/foo.gif" },
                        { "bar", "http://example.com/bar.gif" }
                    }
                };
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<Dictionary<string, string>>(Args.Uri, null).Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var emojis = await client.GetEmojis();

                Assert.Equal(2, emojis.Count);
                connection.Received()
                    .GetAsync<Dictionary<string, string>>(Arg.Is<Uri>(u => u.ToString() == "/emojis"), null);
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
