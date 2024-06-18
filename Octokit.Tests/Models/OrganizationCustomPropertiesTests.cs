using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class OrganizationCustomPropertiesTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"[
  {
    ""property_name"": ""test_ms"",
    ""value_type"": ""multi_select"",
    ""required"": true,
    ""default_value"": [
      ""option_d"",
      ""option_e""
    ],
    ""description"": ""multi_select property"",
    ""allowed_values"": [
      ""option_a"",
      ""option_b"",
      ""option_c"",
      ""option_d"",
      ""option_e""
    ],
    ""values_editable_by"": ""org_actors""
  },
  {
    ""property_name"": ""test_ss"",
    ""value_type"": ""single_select"",
    ""required"": true,
    ""default_value"": ""option_c"",
    ""description"": ""single_select property"",
    ""allowed_values"": [
      ""option_a"",
      ""option_b"",
      ""option_c"",
      ""option_d"",
      ""option_e""
    ],
    ""values_editable_by"": ""org_actors""
  },
  {
    ""property_name"": ""test_str"",
    ""value_type"": ""string"",
    ""required"": false,
    ""description"": ""string property"",
    ""values_editable_by"": ""org_actors""
  },
  {
    ""property_name"": ""test_tf"",
    ""value_type"": ""true_false"",
    ""required"": true,
    ""default_value"": ""unset"",
    ""description"": ""true_false property"",
    ""values_editable_by"": ""org_actors""
  }
]
";
            var serializer = new SimpleJsonSerializer();

            var properties = serializer.Deserialize<IReadOnlyList<OrganizationCustomProperty>>(json);
            Assert.NotNull(properties);
            Assert.Equal(4, properties.Count);

            var testMs = properties[0];
            Assert.Equal("test_ms", testMs.PropertyName);
            Assert.Equal(CustomPropertyValueType.MultiSelect, testMs.ValueType);
            Assert.True(testMs.Required);
            Assert.Equal(new List<string> { "option_d", "option_e" }, testMs.DefaultValues);
            Assert.Equal("multi_select property", testMs.Description);
            Assert.Equal(new List<string> { "option_a", "option_b", "option_c", "option_d", "option_e" }, testMs.AllowedValues);
            Assert.Equal(CustomPropertyValuesEditableBy.OrgActors, testMs.ValuesEditableBy);

            var testSs = properties[1];
            Assert.Equal("test_ss", testSs.PropertyName);
            Assert.Equal(CustomPropertyValueType.SingleSelect, testSs.ValueType);
            Assert.True(testSs.Required);
            Assert.Equal("option_c", testSs.DefaultValue);
            Assert.Equal(new List<string> { "option_c" }, testSs.DefaultValues);
            Assert.Equal("single_select property", testSs.Description);
            Assert.Equal(new List<string> { "option_a", "option_b", "option_c", "option_d", "option_e" }, testSs.AllowedValues);
            Assert.Equal(CustomPropertyValuesEditableBy.OrgActors, testSs.ValuesEditableBy);

            var testStr = properties[2];
            Assert.Equal("test_str", testStr.PropertyName);
            Assert.Equal(CustomPropertyValueType.String, testStr.ValueType);
            Assert.False(testStr.Required);
            Assert.Equal("string property", testStr.Description);
            Assert.Equal(CustomPropertyValuesEditableBy.OrgActors, testStr.ValuesEditableBy);

            var testTf = properties[3];
            Assert.Equal("test_tf", testTf.PropertyName);
            Assert.Equal(CustomPropertyValueType.TrueFalse, testTf.ValueType);
            Assert.True(testTf.Required);
            Assert.Equal("unset", testTf.DefaultValue);
            Assert.Equal("true_false property", testTf.Description);
            Assert.Equal(CustomPropertyValuesEditableBy.OrgActors, testTf.ValuesEditableBy);
        }
    }
}
