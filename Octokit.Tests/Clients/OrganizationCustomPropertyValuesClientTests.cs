using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class OrganizationCustomPropertyValuesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new OrganizationCustomPropertyValuesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertyValuesClient(connection);

                await client.GetAll("org");

                connection.Received()
                    .Get<IReadOnlyList<OrganizationCustomPropertyValues>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/values"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertyValuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PatchTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertyValuesClient(connection);
                var propertyValues = new UpsertOrganizationCustomPropertyValues
                {
                    RepositoryNames = new List<string> { "repo" },
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate { PropertyName = "name", Value = "value" }
                    }
                };

                await client.CreateOrUpdate("org", propertyValues);

                connection.Received()
                    .Patch<IReadOnlyList<CustomPropertyValue>>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/values"), propertyValues);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertyValuesClient(Substitute.For<IApiConnection>());
                var propertyValues = new UpsertOrganizationCustomPropertyValues
                {
                    RepositoryNames = new List<string> { "repo" },
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate { PropertyName = "name", Value = "value" }
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, propertyValues));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", new UpsertOrganizationCustomPropertyValues()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", propertyValues));
            }
        }
    }
}
