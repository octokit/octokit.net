using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class UpsertOrganizationCustomPropertyTests
    {
        [Fact]
        public void CanSerializeMultiSelect()
        {
            var expected = "{\"value_type\":\"multi_select\"," +
                            "\"required\":true," +
                            "\"default_value\":[\"option_d\",\"option_e\"]}";


            var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.MultiSelect, new List<string> { "option_d", "option_e" });

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeSingleSelect()
        {
            var expected = "{\"value_type\":\"single_select\"," +
                            "\"required\":true," +
                            "\"default_value\":\"option_c\"}";

            var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.SingleSelect, "option_c");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeString()
        {
            var expected = "{\"value_type\":\"string\"," +
                            "\"required\":true," +
                            "\"default_value\":\"hello\"}";

            var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.String, "hello");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeTrueFalse()
        {
            var expected = "{\"value_type\":\"true_false\"," +
                            "\"required\":true," +
                            "\"default_value\":\"true\"}";

            var update = new UpsertOrganizationCustomProperty(CustomPropertyValueType.TrueFalse, "true");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }
    }
}
