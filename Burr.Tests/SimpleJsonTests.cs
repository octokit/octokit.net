using FluentAssertions;
using Xunit;

namespace Burr.Tests
{
    public class SimpleJsonTests
    {
        public class TheSerializeMethod
        {
            [Fact]
            public void UsesRubyCasing()
            {
                var item = new Sample { Id = 42, FirstName = "Phil" };

                var json = SimpleJson.SerializeObject(item);

                json.Should().Be("{{\"id\":42,\"first_name\":\"Phil\"}}");
            }
        }

        public class TheDeserializeMethod
        {
            [Fact]
            public void UnderstandsRubyCasing()
            {
                const string json = "{\"id\":42,\"first_name\":\"Phil\"}";

                var sample = SimpleJson.DeserializeObject<Sample>(json);

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
