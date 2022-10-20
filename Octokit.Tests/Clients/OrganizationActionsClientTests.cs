using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationActionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationActionsClient(null));
            }
        }
    }
}