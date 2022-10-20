using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryActionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryActionsClient(null));
            }
        }
    }
}
