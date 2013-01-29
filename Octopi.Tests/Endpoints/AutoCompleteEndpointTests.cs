using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Octopi.Endpoints;
using Octopi.Http;
using Xunit;

namespace Octopi.Tests.Integration
{
    public class AutoCompleteEndpointTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new AutoCompleteEndpoint(null));
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
                connection.GetAsync<Dictionary<string, string>>(Args.Uri).Returns(Task.FromResult(response));
                var autoComplete = new AutoCompleteEndpoint(connection);

                var emojis = await autoComplete.GetEmojis();

                emojis.Count.Should().Be(2);
                connection.Received()
                    .GetAsync<Dictionary<string, string>>(Arg.Is<Uri>(u => u.ToString() == "/emojis"));
            }
        }
    }
}
