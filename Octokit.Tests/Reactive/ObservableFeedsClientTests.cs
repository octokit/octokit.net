using NSubstitute;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableFeedsClientTests
    {
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