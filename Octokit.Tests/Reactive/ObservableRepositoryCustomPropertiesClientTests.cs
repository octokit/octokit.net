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
    public class ObservableRepositoryCustomPropertiesClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositoryCustomPropertiesClient(null));
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCustomPropertiesClient(gitHubClient);

                client.GetAll("org", "repo");

                gitHubClient.Received().Repository.CustomProperty.GetAll("org", "repo");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCustomPropertiesClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("org", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("org", "").ToTask());
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositoryCustomPropertiesClient(gitHubClient);
                var propertyValues = new UpsertRepositoryCustomPropertyValues
                {
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await client.CreateOrUpdate("org", "repo", propertyValues);

                gitHubClient.Received().Repository.CustomProperty.CreateOrUpdate("org", "repo", propertyValues);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositoryCustomPropertiesClient(Substitute.For<IGitHubClient>());
                var propertyValues = new UpsertRepositoryCustomPropertyValues
                {
                    Properties = new List<CustomPropertyValueUpdate>
                    {
                        new CustomPropertyValueUpdate("name", "value")
                    }
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "repo", propertyValues).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", null, propertyValues).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", "repo", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("org", "repo", new UpsertRepositoryCustomPropertyValues()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "repo", propertyValues).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("org", "", propertyValues).ToTask());
            }
        }
    }
}
