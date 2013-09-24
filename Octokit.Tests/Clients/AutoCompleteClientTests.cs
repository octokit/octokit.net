using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Clients;
using Octokit.Http;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class AutoCompleteClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgumentsNotNull()
            {
                Assert.Throws<ArgumentNullException>(() => new AutoCompleteClient(null));
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
                var autoComplete = new AutoCompleteClient(connection);

                var emojis = await autoComplete.GetEmojis();

                Assert.Equal(2, emojis.Count);
                connection.Received()
                    .GetAsync<Dictionary<string, string>>(Arg.Is<Uri>(u => u.ToString() == "/emojis"), null);
            }
        }
    }
}
