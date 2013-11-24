using System;
using System.Threading.Tasks;

using NSubstitute;

using Octokit.Tests.Helpers;

using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReferencesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresArgument()
            {
                Assert.Throws<ArgumentNullException>(() => new ReferencesClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
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
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Get("owner", "repo", "heads/develop");

                connection.Received().Get<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"), null);
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null, "name", "heads"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", null, "heads"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("", "name", "heads"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("owner", "", "heads"));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAll("owner", "repo", "heads");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewReference()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewReference()));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewReference()));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewReference()));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var newReference = new NewReference();
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Create("owner", "repo", newReference);

                connection.Received().Post<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs"), newReference);
            }
        }
    }
}