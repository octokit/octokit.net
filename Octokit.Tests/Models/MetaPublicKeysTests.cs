using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class MetaPublicKeysTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
  ""public_keys"": [
    {
      ""key_identifier"": ""90a421169f0a406205f1563a953312f0be898d3c7b6c06b681aa86a874555f4a"",
      ""key"": ""-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE9MJJHnMfn2+H4xL4YaPDA4RpJqUq\nkCmRCBnYERxZanmcpzQSXs1X/AljlKkbJ8qpVIW4clayyef9gWhFbNHWAA==\n-----END PUBLIC KEY-----\n"",
      ""is_current"": false
    },
    {
      ""key_identifier"": ""bcb53661c06b4728e59d897fb6165d5c9cda0fd9cdf9d09ead458168deb7518c"",
      ""key"": ""-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEYAGMWO8XgCamYKMJS6jc/qgvSlAd\nAjPuDPRcXU22YxgBrz+zoN19MzuRyW87qEt9/AmtoNP5GrobzUvQSyJFVw==\n-----END PUBLIC KEY-----\n"",
      ""is_current"": true
    }
  ]
}
";
            var serializer = new SimpleJsonSerializer();

            var keys = serializer.Deserialize<MetaPublicKeys>(json);

            Assert.NotNull(keys);
            Assert.Equal(2, keys.PublicKeys.Count);

            var key1 = keys.PublicKeys[0];
            Assert.Equal("90a421169f0a406205f1563a953312f0be898d3c7b6c06b681aa86a874555f4a", key1.KeyIdentifier);
            Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE9MJJHnMfn2+H4xL4YaPDA4RpJqUq\nkCmRCBnYERxZanmcpzQSXs1X/AljlKkbJ8qpVIW4clayyef9gWhFbNHWAA==\n-----END PUBLIC KEY-----\n", key1.Key);
            Assert.False(key1.IsCurrent);

            var key2 = keys.PublicKeys[1];
            Assert.Equal("bcb53661c06b4728e59d897fb6165d5c9cda0fd9cdf9d09ead458168deb7518c", key2.KeyIdentifier);
            Assert.Equal("-----BEGIN PUBLIC KEY-----\nMFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAEYAGMWO8XgCamYKMJS6jc/qgvSlAd\nAjPuDPRcXU22YxgBrz+zoN19MzuRyW87qEt9/AmtoNP5GrobzUvQSyJFVw==\n-----END PUBLIC KEY-----\n", key2.Key);
            Assert.True(key2.IsCurrent);
        }
    }
}
