using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class ReferencesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
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

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "name", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "heads/develop"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "name", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get(1, ""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Get("owner", "repo", "heads/develop");

                connection.Received().Get<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Get(1, "heads/develop");

                connection.Received().Get<Reference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs/heads/develop"));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, null));


                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAll("owner", "repo");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs"), Args.ApiOptions);
            }
        }

        public class TheGetAllForSubNamespaceMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace(null, "name", "heads"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", null, "heads"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace(null, "name", "heads", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", null, "heads", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", "name", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace("owner", "name", "heads", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace(1, null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace(1, null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForSubNamespace(1, "subnamespace", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("", "name", "heads"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("owner", "", "heads"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("", "name", "heads", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("owner", "", "heads", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace("owner", "name", "", ApiOptions.None));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace(1, ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForSubNamespace(1, "", ApiOptions.None));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAllForSubNamespace("owner", "repo", "heads");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.GetAllForSubNamespace(1, "heads");

                connection.Received().GetAll<Reference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs/heads"), Args.ApiOptions);
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "name", new NewReference("heads/develop", "sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", null, new NewReference("heads/develop", "sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "name", new NewReference("heads/develop", "sha")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("owner", "", new NewReference("heads/develop", "sha")));
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

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var newReference = new NewReference("heads/develop", "sha");
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Create(1, newReference);

                connection.Received().Post<Reference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs"), newReference);
            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "name", "heads/develop", new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", null, "heads/develop", new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", null, new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("owner", "name", "heads/develop", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null, new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, "heads/develop", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "name", "heads/develop", new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "", "heads/develop", new ReferenceUpdate("sha")));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("owner", "name", "", new ReferenceUpdate("sha")));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "", new ReferenceUpdate("sha")));
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

            [Fact]
            public async Task PostsToCorrectUrlWithRepositoryId()
            {
                var referenceUpdate = new ReferenceUpdate("sha");
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Update(1, "heads/develop", referenceUpdate);

                connection.Received().Patch<Reference>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs/heads/develop"), referenceUpdate);
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ReferencesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "name", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "heads/develop"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "name", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "name", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "heads/develop"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "name", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, ""));
            }

            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Delete("owner", "repo", "heads/develop");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/owner/repo/git/refs/heads/develop"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReferencesClient(connection);

                await client.Delete(1, "heads/develop");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/git/refs/heads/develop"));
            }
        }
    }
}