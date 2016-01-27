using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Internal;
using Xunit;
using System.Globalization;

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
        public class TheRenderArbitrryMarkdownMethod
        {
            [Fact]
            public async Task RequestsTheEmojiEndpoint()
            {
                IApiResponse<string> response = new ApiResponse<string>(new Response(), "<strong>Test</strong>");
                var connection = Substitute.For<IConnection>();
                var forTest = new NewArbitraryMarkdown("testMarkdown", "gfm", "testContext");
                connection.Post<string>(Args.Uri, forTest, "text/html", "text/plain")
                    .Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var html = await client.RenderArbitraryMarkdown(forTest);
                Assert.Equal("<strong>Test</strong>", html);
                connection.Received()
                    .Post<string>(Arg.Is<Uri>(u => u.ToString() == "markdown"),
                    forTest,
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

                var emojis = await client.GetAllEmojis();

                Assert.Equal(2, emojis.Count);
                Assert.Equal("foo", emojis[0].Name);
                connection.Received()
                    .Get<Dictionary<string, string>>(Arg.Is<Uri>(u => u.ToString() == "emojis"), null, null);
            }
        }

        public class TheGetResourceRateLimitsMethod
        {
            [Fact]
            public async Task RequestsTheRecourceRateLimitEndpoint()
            {
                IApiResponse<MiscellaneousRateLimit> response = new ApiResponse<MiscellaneousRateLimit>
                (
                    new Response(),
                    new MiscellaneousRateLimit(
                        new ResourceRateLimit(
                            new RateLimit(5000, 4999, 1372700873),
                            new RateLimit(30, 18, 1372700873)
                        ),
                        new RateLimit(100, 75, 1372700873)
                    )
                );
                var connection = Substitute.For<IConnection>();
                connection.Get<MiscellaneousRateLimit>(Args.Uri, null, null).Returns(Task.FromResult(response));
                var client = new MiscellaneousClient(connection);

                var result = await client.GetRateLimits();

                // Test the core limits
                Assert.Equal(5000, result.Resources.Core.Limit);
                Assert.Equal(4999, result.Resources.Core.Remaining);
                Assert.Equal(1372700873, result.Resources.Core.ResetAsUtcEpochSeconds);
                var expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Resources.Core.Reset);

                // Test the search limits
                Assert.Equal(30, result.Resources.Search.Limit);
                Assert.Equal(18, result.Resources.Search.Remaining);
                Assert.Equal(1372700873, result.Resources.Search.ResetAsUtcEpochSeconds);
                expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Resources.Search.Reset);

                // Test the depreciated rate limits
                Assert.Equal(100, result.Rate.Limit);
                Assert.Equal(75, result.Rate.Remaining);
                Assert.Equal(1372700873, result.Rate.ResetAsUtcEpochSeconds);
                expectedReset = DateTimeOffset.ParseExact(
                    "Mon 01 Jul 2013 5:47:53 PM -00:00",
                    "ddd dd MMM yyyy h:mm:ss tt zzz",
                    CultureInfo.InvariantCulture);
                Assert.Equal(expectedReset, result.Rate.Reset);

                connection.Received()
                    .Get<MiscellaneousRateLimit>(Arg.Is<Uri>(u => u.ToString() == "rate_limit"), null, null);
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
