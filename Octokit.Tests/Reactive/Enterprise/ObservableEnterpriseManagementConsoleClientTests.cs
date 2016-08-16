using System;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseManagementConsoleClientTests
    {
        public class TheGetMaintenanceModeMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                client.GetMaintenanceMode("Password01");

                github.Enterprise.ManagementConsole.Received(1).
                    GetMaintenanceMode(Arg.Is("Password01"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentNullException>(() => client.GetMaintenanceMode(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentException>(() => client.GetMaintenanceMode(""));
            }
        }

        public class TheEditMaintenanceModeMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                client.EditMaintenanceMode(
                    new UpdateMaintenanceRequest(new UpdateMaintenanceRequestDetails(true)),
                    "Password01");

                github.Enterprise.ManagementConsole.Received(1).
                    EditMaintenanceMode(
                        Arg.Is<UpdateMaintenanceRequest>(a =>
                            a.Maintenance.Enabled == true &&
                            a.Maintenance.When == "now"),
                        Arg.Is("Password01"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentNullException>(() => client.EditMaintenanceMode(null, "Password01"));
                Assert.Throws<ArgumentNullException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(), null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentException>(() => client.EditMaintenanceMode(new UpdateMaintenanceRequest(), ""));
            }
        }

        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableEnterpriseManagementConsoleClient(null));
            }
        }
    }
}
