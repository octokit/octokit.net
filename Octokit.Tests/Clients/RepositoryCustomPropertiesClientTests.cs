using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class RepositoryCustomPropertiesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoryCustomPropertiesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCustomPropertiesClient(connection);

                await client.GetAll("org", "repo");

                connection.Received()
                    .Get<IReadOnlyList<CustomPropertyValue>>(Arg.Is<Uri>(u => u.ToString() == "repos/org/repo/properties/values"), null);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryCustomPropertiesClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("org", ""));
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PatchTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new RepositoryCustomPropertiesClient(connection);
                var propertyValues = new UpsertRepositoryCustomPropertyValues
                {
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await client.CreateOrUpdate("org", "repo", propertyValues);

                connection.Received()
                    .Patch(Arg.Is<Uri>(u => u.ToString() == "repos/org/repo/properties/values"), propertyValues);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new RepositoryCustomPropertiesClient(Substitute.For<IApiConnection>());
                var propertyValues = new UpsertRepositoryCustomPropertyValues
                {
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "repo", propertyValues));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", null, propertyValues));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", "repo", null));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", "repo", new UpsertRepositoryCustomPropertyValues()));

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "repo", propertyValues));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("org", "", propertyValues));
            }
        }
    }
}
