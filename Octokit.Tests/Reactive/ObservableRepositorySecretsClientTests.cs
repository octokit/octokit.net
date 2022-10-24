using NSubstitute;
using Octokit.Reactive;
using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Reactive
{
    public class ObservableRepositorySecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new ObservableRepositorySecretsClient(null));
            }
        }

        public class GetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositorySecretsClient(gitHubClient);

                await client.GetPublicKey("owner", "repo");

                gitHubClient.Received().Repository.Actions.Secrets.GetPublicKey("owner", "repo");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositorySecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(null, "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey("owner", null).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey("", "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetPublicKey("owner", "").ToTask());
            }
        }

        public class GetAllMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositorySecretsClient(gitHubClient);

                await client.GetAll("owner", "repo");

                gitHubClient.Received().Repository.Actions.Secrets.GetAll("owner", "repo");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositorySecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "repo").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "").ToTask());
            }
        }

        public class GetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositorySecretsClient(gitHubClient);

                await client.Get("owner", "repo","secret");

                gitHubClient.Received().Repository.Actions.Secrets.Get("owner", "repo", "secret");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositorySecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", null, "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("owner", "repo", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("", "repo", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Get("owner", "repo", "").ToTask());
            }
        }

        public class CreateOrUpdateMethod
        {
            [Fact]
            public async Task PostsTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositorySecretsClient(gitHubClient);
                var upsert = new UpsertRepositorySecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };

                await client.CreateOrUpdate("owner", "repo", "secret", upsert);

                gitHubClient.Received().Repository.Actions.Secrets.CreateOrUpdate("owner", "repo", "secret", upsert);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositorySecretsClient(Substitute.For<IGitHubClient>());

                var upsertSecret = new UpsertRepositorySecret
                {
                    EncryptedValue = "encryptedValue",
                    KeyId = "keyId"
                };

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate(null, "repo", "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", null, "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", null, upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", "secret", null).ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CreateOrUpdate("owner", "repo", "secret", new UpsertRepositorySecret()).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("", "repo", "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "", "secret", upsertSecret).ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.CreateOrUpdate("owner", "repo", "", upsertSecret).ToTask());
            }
        }

        public class DeleteMethod
        {
            [Fact]
            public async Task DeletesTheCorrectUrl()
            {
                var gitHubClient = Substitute.For<IGitHubClient>();
                var client = new ObservableRepositorySecretsClient(gitHubClient);

                await client.Delete("owner", "repo", "secret");

                gitHubClient.Received().Repository.Actions.Secrets.Delete("owner", "repo", "secret");
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var client = new ObservableRepositorySecretsClient(Substitute.For<IGitHubClient>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", null, "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("owner", "repo", null).ToTask());

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "", "secret").ToTask());
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("owner", "repo", "").ToTask());
            }
        }
    }
}
