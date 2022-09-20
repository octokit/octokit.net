using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EmojisClientTests
    {
        public class TheGetEmojisMethod
        {
            [Fact]
            public async Task RequestsTheEmojiEndpoint()
            {
                IReadOnlyList<Emoji> response = new List<Emoji>
                {
                    { new Emoji("foo", "http://example.com/foo.gif") },
                    { new Emoji("bar", "http://example.com/bar.gif") }
                };

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.GetAll<Emoji>(Args.Uri)
                          .Returns(Task.FromResult(response));

                var client = new EmojisClient(apiConnection);

                var emojis = await client.GetAllEmojis();

                Assert.Equal(2, emojis.Count);
                Assert.Equal("foo", emojis[0].Name);
                apiConnection.Received()
                    .GetAll<Emoji>(Arg.Is<Uri>(u => u.ToString() == "emojis"));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EmojisClient(null));
            }
        }
    }
}
