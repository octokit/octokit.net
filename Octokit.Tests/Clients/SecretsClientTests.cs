using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class SecretsClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(
                    () => new SecretsClient(null));
            }
        }

        public class TheGetPublicKeyMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.GetPublicKey("fake", "repo");

                connection.Received().Get<PublicKey>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets/public-key"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.GetPublicKey(1);

                connection.Received().Get<PublicKey>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets/public-key"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey(null, "repo"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetPublicKey("fake", null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.Get("fake", "repo", "name");

                connection.Received().Get<Secret>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets/name"));
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.Get(1, "name");

                connection.Received().Get<Secret>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets/name"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(null, "repo", "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Get(1, null));
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.GetAll("fake", "repo");

                connection.Received().GetAll<SecretsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await client.GetAll(1);

                connection.Received().GetAll<SecretsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets"), Args.ApiOptions);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryNameWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll("fake", "repo", options);

                connection.Received().GetAll<SecretsResponse>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets"), options);
            }

            [Fact]
            public async Task RequestsCorrectUrlWithRepositoryIdWithApiOptions()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                var options = new ApiOptions
                {
                    PageCount = 1,
                    StartPage = 1,
                    PageSize = 1
                };

                await client.GetAll(1, options);

                connection.Received().GetAll<SecretsResponse>(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets"), options);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(null, "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", null, ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll("owner", "name", (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.GetAll(1, (ApiOptions)null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", ""));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("", "name", ApiOptions.None));
                await Assert.ThrowsAsync<ArgumentException>(() => client.GetAll("owner", "", ApiOptions.None));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var secretRequest = new SecretRequest()
                {
                    EncryptedValue = "encryptedBlob",
                    KeyId = "keyId"
                };

                client.Create("fake", "repo", "name", secretRequest);

                connection.Received().Put<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets/name"),
                    Arg.Is<SecretRequest>(r => r.EncryptedValue == "encryptedBlob"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var secretRequest = new SecretRequest()
                {
                    EncryptedValue = "encryptedBlob",
                    KeyId = "keyId"
                };

                client.Create(1, "name", secretRequest);

                connection.Received().Put<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets/name"),
                    Arg.Is<SecretRequest>(r => r.EncryptedValue == "encryptedBlob"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var req = new SecretRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, "repo", "name", req));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", null, "name", req));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("fake", "repo", null, req));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(1, null, req));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", "repo", "name", req));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fake", "", "name", req));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("fake", "repo", "", req));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Create(1, "", req));

            }
        }

        public class TheUpdateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var secretRequest = new SecretRequest()
                {
                    EncryptedValue = "encryptedBlob",
                    KeyId = "keyId"
                };

                client.Update("fake", "repo", "name", secretRequest);

                connection.Received().Put<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets/name"),
                    Arg.Is<SecretRequest>(r => r.EncryptedValue == "encryptedBlob"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var secretRequest = new SecretRequest()
                {
                    EncryptedValue = "encryptedBlob",
                    KeyId = "keyId"
                };

                client.Update(1, "name", secretRequest);

                connection.Received().Put<object>(
                    Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets/name"),
                    Arg.Is<SecretRequest>(r => r.EncryptedValue == "encryptedBlob"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);
                var req = new SecretRequest();

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(null, "repo", "name", req));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("fake", null, "name", req));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update("fake", "repo", null, req));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null, req));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("", "repo", "name", req));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("fake", "", "name", req));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Update("fake", "repo", "", req));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Update(1, "", req));

            }
        }

        public class TheDeleteMethod
        {
            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryName()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                client.Delete("fake", "repo", "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/actions/secrets/name"));
            }

            [Fact]
            public void RequestsTheCorrectUrlWithRepositoryId()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                client.Delete(1, "name");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "repositories/1/actions/secrets/name"));
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new SecretsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(null, "repo", "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("fake", null, "name"));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete("fake", "repo", null));

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Delete(1, null));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("", "repo", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("fake", "", "name"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete("fake", "repo", ""));

                await Assert.ThrowsAsync<ArgumentException>(() => client.Delete(1, ""));

            }
        }
    }
}
