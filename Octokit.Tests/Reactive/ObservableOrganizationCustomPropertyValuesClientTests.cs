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
    public class ObservableOrganizationCustomPropertyValuesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableOrganizationCustomPropertyValuesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertyValuesClient(gitHubClient);

                client.GetAll("org");

                gitHubClient.Received().Organization.CustomProperty.Values.GetAll("org");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertyValuesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("").ToTask());
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableOrganizationCustomPropertyValuesClient(gitHubClient);
                var propertyValues = new UpsertOrganizationCustomPropertyValues
                {
                    RepositoryNames = new List<string> { "repo" },
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await client.CreateOrUpdate("org", propertyValues);

                gitHubClient.Received().Organization.CustomProperty.Values.CreateOrUpdate("org", propertyValues);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableOrganizationCustomPropertyValuesClient(Substitute.For<IGitHubClient>());
                var propertyValues = new UpsertOrganizationCustomPropertyValues
                {
                    RepositoryNames = new List<string> { "repo" },
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, propertyValues).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", new UpsertOrganizationCustomPropertyValues()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", propertyValues).ToTask());
            }
        }
    }
}
