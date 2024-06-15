using NSubstitute;
using Octokit.Reactive;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableOrganizationCustomPropertiesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableOrganizationCustomPropertiesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);

                await client.GetAll("org");

                gitHubClient.Received().Organization.CustomProperty.GetAll("org");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("").ToTask());
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);

                await client.Get("org", "custom_property_name");

                gitHubClient.Received().Organization.CustomProperty.Get("org", "custom_property_name");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "custom_property_name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "custom_property_name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", "").ToTask());
            }
        }

        public class BatchCreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);
                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new() { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String }
                    }
                };
                await client.CreateOrUpdate("org", properties);

                gitHubClient.Received().Organization.CustomProperty.CreateOrUpdate("org", properties);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new() { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String }
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, properties).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", new UpsertOrganizationCustomProperties()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", properties).ToTask());
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);
                var update = new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String };

                await client.CreateOrUpdate("org", "custom_property_name", update);

                gitHubClient.Received().Organization.CustomProperty.CreateOrUpdate("org", "custom_property_name", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                var update = new OrganizationCustomPropertyUpdate { PropertyName = "name", DefaultValue = "value", ValueType = CustomPropertyValueType.String };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "custom_property_name", update).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, update).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "custom_property_name", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "custom_property_name", new OrganizationCustomPropertyUpdate()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "custom_property_name", update).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", update).ToTask());
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);

                await client.Delete("org", "custom_property_name");

                gitHubClient.Received().Organization.CustomProperty.Delete("org", "custom_property_name");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "custom_property_name").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "custom_property_name").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "").ToTask());
            }
        }
    }
}
