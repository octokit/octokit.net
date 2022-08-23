using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PackagesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PackagesClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAll("fake", PackageType.RubyGems);

                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAll("fake", PackageType.RubyGems, PackageVisibility.Public);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, PackageType.Nuget));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", PackageType.Nuget));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.Get("fake", PackageType.Npm, "name");

                connection.Received().Get<Package>(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", PackageType.Npm, ""));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.Delete("fake", PackageType.Npm, "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", PackageType.Npm, ""));
            }
        }

        public class TheRestoreMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.Restore("fake", PackageType.Npm, "name");

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "/orgs/fake/packages/npm/name/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Restore(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Restore("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Restore("owner", PackageType.Npm, ""));
            }
        }
    }
}
