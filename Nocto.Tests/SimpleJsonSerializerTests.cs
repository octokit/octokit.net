using FluentAssertions;
using Nocto.Http;
using Xunit;

namespace Nocto.Tests
{
    public class SimpleJsonSerializerTests
    {
        public class TheSerializeMethod
        {
            [Fact]
            public void UsesRubyCasing()
            {
                var item = new Sample { Id = 42, FirstName = "Phil" };

                var json = new SimpleJsonSerializer().Serialize(item);

                json.Should().Be("{\"id\":42,\"first_name\":\"Phil\"}");
            }
        }

        public class TheDeserializeMethod
        {
            [Fact]
            public void UnderstandsRubyCasing()
            {
                const string json = "{\"id\":42,\"first_name\":\"Phil\"}";

                var sample = new SimpleJsonSerializer().Deserialize<Sample>(json);

                sample.Id.Should().Be(42);
                sample.FirstName.Should().Be("Phil");
            }
        }

        public class Sample
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
        }
    }
}
