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

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForOrg("fake", PackageType.RubyGems);

                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForOrg("fake", PackageType.RubyGems, PackageVisibility.Public);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrg(null, PackageType.Nuget));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrg("", PackageType.Nuget));
            }
        }

        public class TheGetForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetForOrg("fake", PackageType.Npm, "name");

                connection.Received().Get<Package>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForOrg(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForOrg("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForOrg("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheDeleteForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.DeleteForOrg("fake", PackageType.Npm, "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForOrg(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForOrg("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForOrg("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheRestoreForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.RestoreForOrg("fake", PackageType.Npm, "name");

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForOrg(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForOrg("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForOrg("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheGetAllForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForActiveUser(PackageType.RubyGems);

                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "user/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForActiveUser(PackageType.RubyGems, PackageVisibility.Public);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "user/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }
        }

        public class TheGetForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetForActiveUser(PackageType.Npm, "name");

                connection.Received().Get<Package>(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForActiveUser(PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForActiveUser(PackageType.Npm, ""));
            }
        }

        public class TheDeleteForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.DeleteForActiveUser(PackageType.Npm, "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForActiveUser(PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForActiveUser(PackageType.Npm, ""));
            }
        }

        public class TheRestoreForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.RestoreForActiveUser(PackageType.Npm, "name");

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForActiveUser(PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForActiveUser(PackageType.Npm, ""));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForUser("fake", PackageType.RubyGems);

                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type")));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetAllForUser("fake", PackageType.RubyGems, PackageVisibility.Public);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<Package>(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages"), Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("package_type") && d.ContainsKey("visibility")));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, PackageType.Nuget));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", PackageType.Nuget));
            }
        }

        public class TheGetForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.GetForUser("fake", PackageType.Npm, "name");

                connection.Received().Get<Package>(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForUser(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForUser("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForUser("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForUser("owner", PackageType.Npm, ""));
            }
        }

        public class TheDeleteForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.DeleteForUser("fake", PackageType.Npm, "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForUser(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForUser("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForUser("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForUser("owner", PackageType.Npm, ""));
            }
        }

        public class TheRestoreForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackagesClient(connection);

                await client.RestoreForUser("fake", PackageType.Npm, "name");

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackagesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForUser(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForUser("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForUser("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForUser("owner", PackageType.Npm, ""));
            }
        }

    }
}
