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

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForOrg("fake", PackageType.RubyGems, "name");

                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u => 
                    u.ToString() == "orgs/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForOrg("fake", PackageType.RubyGems, "name", PackageVersionState.Deleted);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "orgs/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state") && d["state"] == "deleted"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrg(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrg("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForOrg("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForOrg("owner", PackageType.Npm, ""));
            }
        }

        public class TheGetForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetForOrg("fake", PackageType.Npm, "name", 5);

                connection.Received().Get<PackageVersion>(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForOrg(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForOrg("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForOrg("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForOrg("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForOrg("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheDeleteForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.DeleteForOrg("fake", PackageType.Npm, "name", 5);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForOrg(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForOrg("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForOrg("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForOrg("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForOrg("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheRestoreForOrgMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.RestoreForOrg("fake", PackageType.Npm, "name", 5);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "orgs/fake/packages/npm/name/versions/5/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForOrg(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForOrg("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForOrg("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForOrg("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForOrg("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheGetAllForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForActiveUser(PackageType.RubyGems, "name");

                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "user/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForActiveUser(PackageType.RubyGems, "name", PackageVersionState.Deleted);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "user/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state") && d["state"] == "deleted"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForActiveUser(PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForActiveUser(PackageType.Npm, ""));
            }
        }

        public class TheGetForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetForActiveUser(PackageType.Npm, "name", 5);

                connection.Received().Get<PackageVersion>(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForActiveUser(PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForActiveUser(PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForActiveUser(PackageType.Npm, "", 0));
            }
        }

        public class TheDeleteForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.DeleteForActiveUser(PackageType.Npm, "name", 5);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForActiveUser(PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForActiveUser(PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForActiveUser(PackageType.Npm, "", 0));
            }
        }

        public class TheRestoreForActiveUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.RestoreForActiveUser(PackageType.Npm, "name", 5);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "user/packages/npm/name/versions/5/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForActiveUser(PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForActiveUser(PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForActiveUser(PackageType.Npm, "", 0));
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForUser("fake", PackageType.RubyGems, "name");

                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "users/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state")),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithOptionalParameter()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetAllForUser("fake", PackageType.RubyGems, "name", PackageVersionState.Deleted);

                var calls = connection.ReceivedCalls();
                connection.Received().GetAll<PackageVersion>(Arg.Is<Uri>(u =>
                    u.ToString() == "users/fake/packages/rubygems/name/versions"),
                    Arg.Is<Dictionary<string, string>>(d => d.ContainsKey("state") && d["state"] == "deleted"),
                    Args.ApiOptions);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser(null, PackageType.Npm, "asd"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("", PackageType.Npm, "asd"));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllForUser("owner", PackageType.Npm, null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllForUser("owner", PackageType.Npm, ""));
            }
        }

        public class TheGetForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.GetForUser("fake", PackageType.Npm, "name", 5);

                connection.Received().Get<PackageVersion>(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForUser(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForUser("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetForUser("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForUser("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetForUser("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheDeleteForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.DeleteForUser("fake", PackageType.Npm, "name", 5);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name/versions/5"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForUser(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForUser("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteForUser("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForUser("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteForUser("owner", PackageType.Npm, "", 0));
            }
        }

        public class TheRestoreForUserMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PackageVersionsClient(connection);

                await client.RestoreForUser("fake", PackageType.Npm, "name", 5);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "users/fake/packages/npm/name/versions/5/restore"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new PackageVersionsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForUser(null, PackageType.Npm, "asd", 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForUser("", PackageType.Npm, "asd", 5));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RestoreForUser("owner", PackageType.Npm, null, 5));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForUser("owner", PackageType.Npm, "", 5));

                await Assert.ThrowsAsync<ArgumentException>(() => client.RestoreForUser("owner", PackageType.Npm, "", 0));
            }
        }
    }
}
