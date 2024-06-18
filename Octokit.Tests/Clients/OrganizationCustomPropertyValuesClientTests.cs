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
                    .GetAll<OrganizationCustomPropertyValues>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/values"), Arg.Any<IDictionary<string, string>>());
            }

            [Fact]
            public void RequestsTheCorrectUrlWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new OrganizationCustomPropertyValuesClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                client.GetAll("org", options);

                connection.Received()
                    .GetAll<OrganizationCustomPropertyValues>(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/values"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new OrganizationCustomPropertyValuesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, new OrganizationCustomPropertyValuesRequest()));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", repositoryQuery: null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll(""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", new OrganizationCustomPropertyValuesRequest()));
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
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await client.CreateOrUpdate("org", propertyValues);

                connection.Received()
                    .Patch(Arg.Is<Uri>(u => u.ToString() == "orgs/org/properties/values"), propertyValues);
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
                        new CustomPropertyValueUpdate("name", "value")
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
