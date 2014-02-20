using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class FeedsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new FeedsClient(null));
            }
        }

        public class TheGetFeedsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var client = Substitute.For<IApiConnection>();
                var orgsClient = new FeedsClient(client);

                orgsClient.GetFeeds();

                client.Received().Get<Feed>(Arg.Is<Uri>(u => u.ToString() == "feeds"), null);
            }
        }
    }
}