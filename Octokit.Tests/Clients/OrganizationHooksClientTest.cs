using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationHooksClientTests
    {
        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                client.Hooks.GetAll("org");

                connection.Received().GetAll<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.GetAll(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                client.Hooks.Get("org", 12345678);

                connection.Received().Get<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Get(null, 123));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var hook = new NewOrganizationHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Hooks.Create("org", hook);

                connection.Received().Post<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                var config = new Dictionary<string, string> { { "config", "" } };
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Create(null, new NewOrganizationHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Create("name", null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var newOrganizationHook = new NewOrganizationHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Hooks.Create("org", newOrganizationHook);

                connection.Received().Post<OrganizationHook>(Arg.Any<Uri>(), newOrganizationHook);
            }
        }

        public class TheEditMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var hook = new EditOrganizationHook();

                client.Hooks.Edit("org", 12345678, hook);

                connection.Received().Patch<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Edit( null, 12345678, new EditOrganizationHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Edit( "name", 12345678, null));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var editOrganizationHook = new EditOrganizationHook() { Active = false };

                client.Hooks.Edit("org", 12345678, editOrganizationHook);

                connection.Received().Patch<OrganizationHook>(Arg.Any<Uri>(), editOrganizationHook);
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Ping(null, 12345678));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                client.Hooks.Ping("org", 12345678);

                connection.Received().Post(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678/pings"));
            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                client.Hooks.Delete("org", 12345678);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hooks.Delete(null, 12345678));
            }
        }
    }
}
