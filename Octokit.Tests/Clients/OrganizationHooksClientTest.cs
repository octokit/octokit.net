using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationHooksClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new OrganizationHooksClient(null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.Hook.GetAll("org");

                connection.Received().GetAll<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    PageSize = 1,
                    StartPage = 1
                };

                await client.Hook.GetAll("org", options);

                connection.Received(1)
                    .GetAll<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks"),
                        options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.GetAll(null));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Hook.GetAll(""));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                await client.Hook.Get("org", 12345678);

                connection.Received().Get<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Get(null, 123));
            }

            [Fact]
            public async Task EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Hook.Get("", 123));
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

                client.Hook.Create("org", hook);

                connection.Received().Post<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                var config = new Dictionary<string, string> { { "config", "" } };
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Create(null, new NewOrganizationHook("name", config)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Create("name", null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());
                var config = new Dictionary<string, string> { { "url", "" } };
                Assert.ThrowsAsync<ArgumentException>(() => client.Hook.Create("", new NewOrganizationHook("name", config)));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var newOrganizationHook = new NewOrganizationHook("name", new Dictionary<string, string> { { "config", "" } });

                client.Hook.Create("org", newOrganizationHook);

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

                client.Hook.Edit("org", 12345678, hook);

                connection.Received().Patch<OrganizationHook>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"), hook);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Edit(null, 12345678, new EditOrganizationHook()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Edit("name", 12345678, null));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());
                Assert.ThrowsAsync<ArgumentException>(() => client.Hook.Edit("", 123, new EditOrganizationHook()));
            }

            [Fact]
            public void UsesTheSuppliedHook()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);
                var editOrganizationHook = new EditOrganizationHook() { Active = false };

                client.Hook.Edit("org", 12345678, editOrganizationHook);

                connection.Received().Patch<OrganizationHook>(Arg.Any<Uri>(), editOrganizationHook);
            }
        }

        public class ThePingMethod
        {
            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Ping(null, 12345678));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());
                Assert.ThrowsAsync<ArgumentException>(() => client.Hook.Ping("", 123));
            }

            [Fact]
            public void RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationsClient(connection);

                client.Hook.Ping("org", 12345678);

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

                client.Hook.Delete("org", 12345678);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/hooks/12345678"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Hook.Delete(null, 12345678));
            }

            [Fact]
            public void EnsuresNonEmptyArguments()
            {
                var client = new OrganizationsClient(Substitute.For<IApiConnection>());
                Assert.ThrowsAsync<ArgumentException>(() => client.Hook.Delete("", 123));
            }

        }
    }
}
