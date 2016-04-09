using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class EnterpriseManagementConsoleClientTests
    {
        public class TheGetMaintenanceModeMethod
        {
            readonly string _distinguishedNameUser = "uid=test-user,ou=users,dc=company,dc=com";

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
                client.EditMaintenanceMode(new UpdateMaintenanceRequest(false, MaintenanceDate.Now()), "Password01");

                connection.Received().Post<MaintenanceModeResponse>(
                    Arg.Is<Uri>(u => u.ToString() == expectedUri),
                    Arg.Any<string>());
            }

            [Fact]
            public void PassesRequestObject()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                client.EditMaintenanceMode(new UpdateMaintenanceRequest(true, MaintenanceDate.Now()), "Password01");

                connection.Received().Post<MaintenanceModeResponse>(
                    Arg.Any<Uri>(),
                    Arg.Is<string>(a =>
                        a == "maintenance={\"enabled\":true, \"when\":\"now\"}"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditMaintenanceMode(null, "Password01"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(false, MaintenanceDate.Now()), null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new EnterpriseManagementConsoleClient(connection);

                await Assert.ThrowsAsync<ArgumentException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(false, MaintenanceDate.Now()), ""));
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
