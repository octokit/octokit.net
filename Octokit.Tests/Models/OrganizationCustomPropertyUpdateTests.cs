using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class OrganizationCustomPropertyUpdateTests
    {
        [Fact]
        public void CanSerializeMultiSelect()
        {
            var expected = "{\"property_name\":\"test_ms\"," +
                           "\"value_type\":\"multi_select\"," +
                           "\"required\":true," +
                           "\"default_value\":[\"option_d\",\"option_e\"]}";


            var update = new OrganizationCustomPropertyUpdate("test_ms", CustomPropertyValueType.MultiSelect, new List<string> { "option_d", "option_e" });

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeSingleSelect()
        {
            var expected = "{\"property_name\":\"test_ss\"," +
                            "\"value_type\":\"single_select\"," +
                            "\"required\":true," +
                            "\"default_value\":\"option_c\"}";

            var update = new OrganizationCustomPropertyUpdate("test_ss", CustomPropertyValueType.SingleSelect, "option_c");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeString()
        {
            var expected = "{\"property_name\":\"test_str\"," +
                            "\"value_type\":\"string\"," +
                            "\"required\":true," +
                            "\"default_value\":\"hello\"}";

            var update = new OrganizationCustomPropertyUpdate("test_str", CustomPropertyValueType.String, "hello");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeTrueFalse()
        {
            var expected = "{\"property_name\":\"test_tf\"," +
                            "\"value_type\":\"true_false\"," +
                            "\"required\":true," +
                            "\"default_value\":\"true\"}";

            var update = new OrganizationCustomPropertyUpdate("test_tf", CustomPropertyValueType.TrueFalse, "true");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }
    }
}
