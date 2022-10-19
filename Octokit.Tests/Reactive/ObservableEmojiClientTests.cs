using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableEmojiClientTests
    {
        public class TheGetAllEmojisMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableEmojisClient(gitHubClient);

                client.GetAllEmojis();

                gitHubClient.Emojis.Received(1).GetAllEmojis();
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableEmojisClient((IGitHubClient)null));
            }
        }
    }
}
