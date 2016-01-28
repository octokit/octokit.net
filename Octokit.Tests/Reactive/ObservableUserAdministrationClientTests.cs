using System;
using System.Collections.Generic;
using System.IO;
using NSubstitute;
using Octokit.Reactive;
using Xunit;
using System.Reactive.Linq;

namespace Octokit.Tests.Reactive
{
    public class ObservableUserAdministrationClientTests
    {
        public class ThePromoteMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNull()
            {
                var client = new ObservableUserAdministrationClient();                
                Assert.Throws<ArgumentNullException>(() =>  client.Promote(null));
            }
        }
    }
}
