using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class PublicKeysClientTests
    {
        public class TheCtor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new PublicKeysClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public async Task RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new PublicKeysClient(connection);

                await client.Get(PublicKeyType.CopilotApi);

                connection.Received()
                    .Get<MetaPublicKeys>(Arg.Is<Uri>(u => u.ToString() == "meta/public_keys/copilot_api"));
            }

            [Fact]
            public async Task RequestsCopilotApiPublicKeysEndpoint()
            {
                var publicKeys = new MetaPublicKeys(publicKeys: new[] {
                    new MetaPublicKey("4fe6b016179b74078ade7581abf4e84fb398c6fae4fb973972235b84fcd70ca3", "-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAELPuPiLVQbHY/clvpNnY+0BzYIXgo\nS0+XhEkTWUZEEznIVpS3rQseDTG6//gEWr4j9fY35+dGOxwOx3Z9mK3i7w==\n-----END PUBLIC KEY-----\n", true),
                    new MetaPublicKey("df3454252d91570ae1bc597182d1183c7a8d42ff0ae96e0f2be4ba278d776546", "-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEl5xbyr5bmETCJzqAvDnYl1ZKJrkf\n89Nyq5j06TTKrnHXXDw4FYNY1uF2S/w6EOaxbf9BxOidCLvjJ8ZgKzNpww==\n-----END PUBLIC KEY-----\n", false)
                });

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Get<MetaPublicKeys>(Arg.Is<Uri>(u => u.ToString() == "meta/public_keys/copilot_api")).Returns(Task.FromResult(publicKeys));

                var client = new PublicKeysClient(apiConnection);

                var result = await client.Get(PublicKeyType.CopilotApi);

                Assert.Equal(2, result.PublicKeys.Count);
                Assert.Equal("4fe6b016179b74078ade7581abf4e84fb398c6fae4fb973972235b84fcd70ca3", result.PublicKeys[0].KeyIdentifier);
                Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAELPuPiLVQbHY/clvpNnY+0BzYIXgo\nS0+XhEkTWUZEEznIVpS3rQseDTG6//gEWr4j9fY35+dGOxwOx3Z9mK3i7w==\n-----END PUBLIC KEY-----\n", result.PublicKeys[0].Key);
                Assert.True(result.PublicKeys[0].IsCurrent);

                Assert.Equal("df3454252d91570ae1bc597182d1183c7a8d42ff0ae96e0f2be4ba278d776546", result.PublicKeys[1].KeyIdentifier);
                Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEl5xbyr5bmETCJzqAvDnYl1ZKJrkf\n89Nyq5j06TTKrnHXXDw4FYNY1uF2S/w6EOaxbf9BxOidCLvjJ8ZgKzNpww==\n-----END PUBLIC KEY-----\n", result.PublicKeys[1].Key);
                Assert.False(result.PublicKeys[1].IsCurrent);

                apiConnection.Received()
                    .Get<MetaPublicKeys>(Arg.Is<Uri>(u => u.ToString() == "meta/public_keys/copilot_api"));
            }

            [Fact]
            public async Task RequestSecretScanningPublicKeysEndpoint()
            {
                var publicKeys = new MetaPublicKeys(publicKeys: new[] {
                    new MetaPublicKey("90a421169f0a406205f1563a953312f0be898d3c7b6c06b681aa86a874555f4a", "-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE9MJJHnMfn2+H4xL4YaPDA4RpJqUq\nkCmRCBnYERxZanmcpzQSXs1X/AljlKkbJ8qpVIW4clayyef9gWhFbNHWAA==\n-----END PUBLIC KEY-----\n", false),
                    new MetaPublicKey("bcb53661c06b4728e59d897fb6165d5c9cda0fd9cdf9d09ead458168deb7518c", "-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEYAGMWO8XgCamYKMJS6jc/qgvSlAd\nAjPuDPRcXU22YxgBrz+zoN19MzuRyW87qEt9/AmtoNP5GrobzUvQSyJFVw==\n-----END PUBLIC KEY-----\n", true)
                });

                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Get<MetaPublicKeys>(Arg.Is<Uri>(u => u.ToString() == "meta/public_keys/secret_scanning")).Returns(Task.FromResult(publicKeys));

                var client = new PublicKeysClient(apiConnection);

                var result = await client.Get(PublicKeyType.SecretScanning);

                Assert.Equal(2, result.PublicKeys.Count);
                Assert.Equal("90a421169f0a406205f1563a953312f0be898d3c7b6c06b681aa86a874555f4a", result.PublicKeys[0].KeyIdentifier);
                Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE9MJJHnMfn2+H4xL4YaPDA4RpJqUq\nkCmRCBnYERxZanmcpzQSXs1X/AljlKkbJ8qpVIW4clayyef9gWhFbNHWAA==\n-----END PUBLIC KEY-----\n", result.PublicKeys[0].Key);
                Assert.False(result.PublicKeys[0].IsCurrent);

                Assert.Equal("bcb53661c06b4728e59d897fb6165d5c9cda0fd9cdf9d09ead458168deb7518c", result.PublicKeys[1].KeyIdentifier);
                Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEYAGMWO8XgCamYKMJS6jc/qgvSlAd\nAjPuDPRcXU22YxgBrz+zoN19MzuRyW87qEt9/AmtoNP5GrobzUvQSyJFVw==\n-----END PUBLIC KEY-----\n", result.PublicKeys[1].Key);
                Assert.True(result.PublicKeys[1].IsCurrent);

                apiConnection.Received()
                    .Get<MetaPublicKeys>(Arg.Is<Uri>(u => u.ToString() == "meta/public_keys/secret_scanning"));
            }
        }
    }
}
