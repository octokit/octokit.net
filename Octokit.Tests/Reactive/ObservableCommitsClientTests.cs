using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Reactive;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableCommitsClientTests
    {
        public class TheCtorMethod
        {
            [Fact]
            public void EnsuresArgumentIsNotNulll()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableCommitsClient(null));
            }
        }

        public class TheGetMethod
        {
            public async Task EnsureNonNullArguments()
            {
                var client = new ObservableCommitsClient(Substitute.For<IGitHubClient>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, ""));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "reference"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "reference"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));                
            } 
        }
    }
}