using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositoryActionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryActionsClient(null));
            }
        }
    }
}
