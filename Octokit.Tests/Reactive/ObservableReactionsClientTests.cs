using NSubstitute;
using Octokit.Reactive;
using System;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableReactionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableReactionsClient(null));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void PostsToCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableReactionsClient(gitHubClient);

                client.Delete(13);

                gitHubClient.Reaction.Received().Delete(13);
            }
        }
    }
}
