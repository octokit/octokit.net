using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class CustomPropertyValueUpdateTests
    {
        [Fact]
        public void CanSerializeMultiSelect()
        {
            var expected = "{\"property_name\":\"test_ms\"," +
                            "\"value\":[\"option_d\",\"option_e\"]}";


            var update = new CustomPropertyValueUpdate("test_ms", new List<string> { "option_d", "option_e" });

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeSingleSelect()
        {
            var expected = "{\"property_name\":\"test_ss\"," +
                            "\"value\":\"option_c\"}";

            var update = new CustomPropertyValueUpdate("test_ss", "option_c");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeString()
        {
            var expected = "{\"property_name\":\"test_str\"," +
                            "\"value\":\"hello\"}";

            var update = new CustomPropertyValueUpdate("test_str", "hello");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }

        [Fact]
        public void CanSerializeTrueFalse()
        {
            var expected = "{\"property_name\":\"test_tf\"," +
                            "\"value\":\"true\"}";

            var update = new CustomPropertyValueUpdate("test_tf", "true");

            var json = new SimpleJsonSerializer().Serialize(update);

            Assert.Equal(expected, json);
        }
    }
}
