using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseAuditLogClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new EnterpriseAuditLogClient(null));
            }
        }
    }
}