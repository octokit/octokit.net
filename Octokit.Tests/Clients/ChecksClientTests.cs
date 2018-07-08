using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ChecksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ChecksClient(null));
            }
        }
    }
}
