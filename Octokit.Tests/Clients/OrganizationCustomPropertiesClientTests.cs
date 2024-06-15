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
                    .Get<IReadOnlyList<OrganizationCustomProperty>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schemas"));
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

                await client.Get("org", "custom_property_name");

                connection.Received()
                    .Get<OrganizationSecret>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schemas/custom_property_name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "custom_property_name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "custom_property_name"));
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
                        new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String }
                    }
                };

                await client.CreateOrUpdate("org", properties);

                connection.Received()
                    .Patch<IReadOnlyList<OrganizationCustomProperty>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schemas"), properties);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String }
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
                var update = new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String };

                await client.CreateOrUpdate("org", "custom_property_name", update);

                connection.Received()
                    .Put<OrganizationCustomProperty>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schemas/custom_property_name"), update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                var update = new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "custom_property_name", update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, update));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "custom_property_name", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "custom_property_name", new OrganizationCustomPropertyUpdate()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "custom_property_name", update));
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

                await client.Delete("org", "custom_property_name");

                connection.Received()
                    .Delete(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/schemas/custom_property_name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "custom_property_name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "custom_property_name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", ""));
            }
        }
    }
}
