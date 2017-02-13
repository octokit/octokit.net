using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableFeedsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new ObservableFeedsClient(null));
            }
        }

        public class TheGetFeedsMethod
        {
            [Fact]
            public void GetsFees()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableFeedsClient(gitHubClient);

                client.GetFeeds();

                gitHubClient.Activity.Feeds.Received().GetFeeds();
            }
        }
    }
}