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
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);

                client.GetAll("org");

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

                await client.Get("org", "property");

                gitHubClient.Received().Organization.CustomProperty.Get("org", "property");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "property").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("org", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "property").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("org", "").ToTask());
            }
        }

        public class BatchCreateOrUpdateMethod
        {
            [Fact]
            public void PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertiesClient(gitHubClient);
                var properties = new UpsertOrganizationCustomProperties
                {
                    Properties = new List<OrganizationCustomPropertyUpdate>
                    {
                        new OrganizationCustomPropertyUpdate("name", CustomPropertyValueType.String, "default")
                    }
                };

                client.CreateOrUpdate("org", properties);

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
                        new OrganizationCustomPropertyUpdate("name", CustomPropertyValueType.String, "default")
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
                var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, "value");

                await client.CreateOrUpdate("org", "property", update);

                gitHubClient.Received().Organization.CustomProperty.CreateOrUpdate("org", "property", update);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, "value");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "property", update).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, update).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "property", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "property", new UpsertOrganizationCustomProperty()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "property", update).ToTask());
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

                await client.Delete("org", "property");

                gitHubClient.Received().Organization.CustomProperty.Delete("org", "property");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "property").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "property").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "").ToTask());
            }
        }
    }
}
