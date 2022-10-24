using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRateLimitClientTests
    {
        public class TheGetRateLimitsMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRateLimitClient(gitHubClient);

                client.GetRateLimits();

                gitHubClient.RateLimit.Received(1).GetRateLimits();
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRateLimitClient((IGitHubClient)null));
            }
        }
    }
}
