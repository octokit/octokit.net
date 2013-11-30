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

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll(null, "name"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAll("owner", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("", "name"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAll("owner", ""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAll("owner", "repo");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs"));
            }
        }

        public class TheGetAllForSubNamespaceMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForSubNamespace(null, "name", "heads"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.GetAllForSubNamespace("owner", null, "heads"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForSubNamespace("", "name", "heads"));
                await AssertEx.Throws<ArgumentException>(async () => await client.GetAllForSubNamespace("owner", "", "heads"));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAllForSubNamespace("owner", "repo", "heads");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create(null, "name", new NewReference("heads/develop", "sha")));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", null, new NewReference("heads/develop", "sha")));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Create("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("", "name", new NewReference("heads/develop", "sha")));
                await AssertEx.Throws<ArgumentException>(async () => await client.Create("owner", "", new NewReference("heads/develop", "sha")));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var newReference = new NewReference("heads/develop", "sha");
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Create("owner", "repo", newReference);

                connection.Received().Post<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs"), newReference);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update(null, "name", "heads/develop", new ReferenceUpdate("sha")));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", null, "heads/develop", new ReferenceUpdate("sha")));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", "name", null, new ReferenceUpdate("sha")));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Update("owner", "name", "heads/develop", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("", "name", "heads/develop", new ReferenceUpdate("sha")));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("owner", "", "heads/develop", new ReferenceUpdate("sha")));
                await AssertEx.Throws<ArgumentException>(async () => await client.Update("owner", "name", "", new ReferenceUpdate("sha")));
            }

            [Fact]
            public async Task PostsToCorrectUrl()
            {
                var referenceUpdate = new ReferenceUpdate("sha");
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Update("owner", "repo", "heads/develop", referenceUpdate);

                connection.Received().Patch<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"), referenceUpdate);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete(null, "name", "heads/develop"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", null, "heads/develop"));
                await AssertEx.Throws<ArgumentNullException>(async () => await client.Delete("owner", "name", null));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("", "name", "heads/develop"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("owner", "", "heads/develop"));
                await AssertEx.Throws<ArgumentException>(async () => await client.Delete("owner", "name", ""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Delete("owner", "repo", "heads/develop");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"));
            }
        }
    }
}