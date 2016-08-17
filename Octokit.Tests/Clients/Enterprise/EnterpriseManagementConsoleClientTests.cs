using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseManagementConsoleClientTests
    {
        public class TheGetMaintenanceModeMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                string expectedUri = "setup/api/maintenance?api_key=Password01";
                client.GetMaintenanceMode("Password01");

                connection.Received().Get<MaintenanceModeResponse>(Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetMaintenanceMode(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetMaintenanceMode(""));
            }
        }

        public class TheEditMaintenanceModeMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                string expectedUri = "setup/api/maintenance?api_key=Password01";
                client.EditMaintenanceMode(new UpdateMaintenanceRequest(), "Password01");

                connection.Received().Post<MaintenanceModeResponse>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<string>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                client.EditMaintenanceMode(new UpdateMaintenanceRequest(new UpdateMaintenanceRequestDetails(true)), "Password01");

                connection.Received().Post<MaintenanceModeResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<string>(a =>
                        a == "maintenance={\"enabled\":true,\"when\":\"now\"}"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditMaintenanceMode(null, "Password01"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(), null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(), ""));
            }
        }

        public class TheGetAllAuthorizedKeysMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                string expectedUri = "setup/api/settings/authorized-keys?api_key=Password01";
                client.GetAllAuthorizedKeys("Password01");

                connection.Received().Get<IReadOnlyList<AuthorizedKey>>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAllAuthorizedKeys(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAllAuthorizedKeys(""));
            }
        }

        public class TheAddAuthorizedKeyMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                string expectedUri = "setup/api/settings/authorized-keys?authorized_key=123ABC&api_key=Password01";
                client.AddAuthorizedKey(new AuthorizedKeyRequest("123ABC"), "Password01");

                connection.Received().Post<IReadOnlyList<AuthorizedKey>>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAuthorizedKey(null, "Password01"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.AddAuthorizedKey(new AuthorizedKeyRequest("123ABC"), null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.AddAuthorizedKey(new AuthorizedKeyRequest("123ABC"), ""));
            }
        }

        public class TheDeleteAuthorizedKeyMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                string expectedUri = "setup/api/settings/authorized-keys?authorized_key=123ABC&api_key=Password01";
                client.DeleteAuthorizedKey(new AuthorizedKeyRequest("123ABC"), "Password01");

                connection.Received().Delete<IReadOnlyList<AuthorizedKey>>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAuthorizedKey(null, "Password01"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.DeleteAuthorizedKey(new AuthorizedKeyRequest("123ABC"), null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.DeleteAuthorizedKey(new AuthorizedKeyRequest("123ABC"), ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new EnterpriseManagementConsoleClient(null));
            }
        }
    }
}
