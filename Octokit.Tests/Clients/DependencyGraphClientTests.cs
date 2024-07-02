using System;
using Xunit;

namespace Octokit
{
    public class DependencyGraphClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new DependencyGraphClient(null));
            }
        }
    }
}
