using System;

using NSubstitute;

using Octokit.Tests.Helpers;

using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReferencesClientTests
    {
        public class TheGetMethod
        {
            public class TheCtor
            {
                [Fact]
                public void EnsuresArgument()
                {
                    Assert.Throws<ArgumentNullException>(() => new ReferencesClient(null));
                }
            }

            [Fact]
            public async void EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get(null, "name", "heads/develop"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", null, "heads/develop"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Get("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("", "name", "heads/develop"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "", "heads/develop"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Get("owner", "name", ""));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                client.Get("owner", "repo", "heads/develop");

                connection.Received().Get<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"), null);
            }
        }
    }
}