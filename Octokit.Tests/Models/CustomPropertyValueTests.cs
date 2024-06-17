using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class CustomPropertyValuesTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"[
  {
    ""property_name"": ""test_ms"",
    ""value"": [
      ""option_d"",
      ""option_e""
    ]
  },
  {
    ""property_name"": ""test_ss"",
    ""value"": ""option_c""
  },
  {
    ""property_name"": ""test_str"",
    ""value"": ""hello""
  },
  {
    ""property_name"": ""test_tf"",
    ""value"": ""unset""
  }
]
";
            var serializer = new SimpleJsonSerializer();

            var properties = serializer.Deserialize<IReadOnlyList<CustomPropertyValue>>(json);

            Assert.NotNull(properties);
            Assert.Equal(4, properties.Count);

            var testMs = properties[0];
            Assert.Equal("test_ms", testMs.PropertyName);
            Assert.Equal(new List<string> { "option_d", "option_e" }, testMs.Values);

            var testSs = properties[1];
            Assert.Equal("test_ss", testSs.PropertyName);
            Assert.Equal("option_c", testSs.Value);
            Assert.Equal(new List<string> { "option_c" }, testSs.Values);

            var testStr = properties[2];
            Assert.Equal("test_str", testStr.PropertyName);
            Assert.Equal("hello", testStr.Value);

            var testTf = properties[3];
            Assert.Equal("test_tf", testTf.PropertyName);
            Assert.Equal("unset", testTf.Value);
        }
    }
}
