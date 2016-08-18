using System;
using System.Collections.Generic;
using NSubstitute;
using Octokit.Reactive;
using Xunit;

namespace Octokit.Tests
{
    public class ObservableEnterpriseManagementConsoleClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableEnterpriseManagementConsoleClient(null));
            }
        }

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

        public class TheGetAllAuthorizedKeysMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                var expectedUri = "setup/api/settings/authorized-keys?api_key=Password01";
                client.GetAllAuthorizedKeys("Password01");

                github.Connection.Received(1).
                    Get<List<AuthorizedKey>>(
                        Arg.Is<Uri>(x => x.ToString() == expectedUri),
                        null,
                        null);
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentNullException>(() => client.GetAllAuthorizedKeys(null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentException>(() => client.GetAllAuthorizedKeys(""));
            }
        }

        public class TheAddAuthorizedKeysMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                client.AddAuthorizedKey(
                    new AuthorizedKeyRequest("123ABC"),
                    "Password01");

                github.Enterprise.ManagementConsole.Received(1).
                    AddAuthorizedKey(
                        Arg.Is<AuthorizedKeyRequest>(x =>
                            x.AuthorizedKey == "123ABC"),
                        Arg.Is("Password01"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentNullException>(() => client.AddAuthorizedKey(null, "Password01"));
                Assert.Throws<ArgumentNullException>(() => client.AddAuthorizedKey(new AuthorizedKeyRequest("123ABC"), null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentException>(() => client.AddAuthorizedKey(new AuthorizedKeyRequest("123ABC"), ""));
            }
        }

        public class TheDeleteAuthorizedKeysMethod
        {
            [Fact]
            public void CallsIntoClient()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                client.DeleteAuthorizedKey(
                    new AuthorizedKeyRequest("123ABC"),
                    "Password01");

                github.Enterprise.ManagementConsole.Received(1).
                    DeleteAuthorizedKey(
                        Arg.Is<AuthorizedKeyRequest>(x =>
                            x.AuthorizedKey == "123ABC"),
                        Arg.Is("Password01"));
            }

            [Fact]
            public void EnsuresNonNullArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentNullException>(() => client.DeleteAuthorizedKey(null, "Password01"));
                Assert.Throws<ArgumentNullException>(() => client.DeleteAuthorizedKey(new AuthorizedKeyRequest("123ABC"), null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var github = Substitute.For<IGitHubClient>();
                var client = new ObservableEnterpriseManagementConsoleClient(github);

                Assert.Throws<ArgumentException>(() => client.DeleteAuthorizedKey(new AuthorizedKeyRequest("123ABC"), ""));
            }
        }
    }
}
