using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PackageVersionsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PackageVersionsClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAll("fake", PackageType.RubyGems, "name");

                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u => 
                    u.ToString() == "/orgs/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAll("fake", PackageType.RubyGems, "name", PackageVersionState.Deleted);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "/orgs/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state") && d["state"] == "deleted"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.Get("fake", PackageType.Npm, "name", 5);

                connection.Received().Get<PackageVersion>(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.Delete("fake", PackageType.Npm, "name", 5);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheRestoreMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.Restore("fake", PackageType.Npm, "name", 5);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name/versions/5/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Restore(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Restore("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, "", 0));
            }
        }
    }

}
