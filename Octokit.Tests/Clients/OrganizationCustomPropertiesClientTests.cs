using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationCustomPropertiesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationCustomPropertiesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertiesClient(connection);

                await client.GetAll("org");

                connection.Received()
                    .Get<IReadOnlyList<OrganizationCustomProperty>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schema"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertiesClient(connection);

                await client.Get("org", "property");

                connection.Received()
                    .Get<OrganizationCustomProperty>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schema/property"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "property"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "property"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", ""));
            }
        }

        public class BatchCreateOrUpdateMethod
        {
            [Fact]
            public async Task PatchTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertiesClient(connection);
                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new OrganizationCustomPropertyUpdate("name", CustomPropertyValueType.String, "default")
                    }
                };

                await client.CreateOrUpdate("org", properties);

                connection.Received()
                    .Patch<IReadOnlyList<OrganizationCustomProperty>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schema"), properties);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new OrganizationCustomPropertyUpdate("name", CustomPropertyValueType.String, "default")
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, properties));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", new UpsertOrganizationCustomProperties()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", properties));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertiesClient(connection);
                var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, "value");

                await client.CreateOrUpdate("org", "property", update);

                connection.Received()
                    .Put<OrganizationCustomProperty>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schema/property"), update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, "value");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "property", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "property", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "property", new UpsertOrganizationCustomProperty()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "property", update));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", update));
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertiesClient(connection);

                await client.Delete("org", "property");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schema/property"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "property"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "property"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", ""));
            }
        }
    }
}
