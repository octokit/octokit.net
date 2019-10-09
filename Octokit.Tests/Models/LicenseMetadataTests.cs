using System;
using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class LicenseMetadataTests
    {
        [Fact]
        public void CanBeDeserializedFromLicenseJson()
        {
            const string json = @"{
    ""key"": ""mit"",
    ""name"": ""MIT License"",
    ""spdx_id"": ""MIT"",
    ""url"": ""https://api.github.com/licenses/mit"",
    ""featured"": true
}";
            var serializer = new SimpleJsonSerializer();

            var license = serializer.Deserialize<LicenseMetadata>(json);

            Assert.Equal("mit", license.Key);
            Assert.Equal("MIT License", license.Name);
            Assert.Equal("MIT", license.SpdxId);
            Assert.Equal("https://api.github.com/licenses/mit", license.Url);
            Assert.True(license.Featured);
        }
    }
}

