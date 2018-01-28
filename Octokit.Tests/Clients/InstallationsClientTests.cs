using System;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class InstallationsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new InstallationsClient(null));
            }
        }
    }
}
