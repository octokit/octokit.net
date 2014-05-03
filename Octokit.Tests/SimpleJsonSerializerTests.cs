using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests
{
    public class SimpleJsonSerializerTests
    {
        public class TheSerializeMethod
        {
            [Fact]
            public void UsesRubyCasing()
            {
                var item = new Sample { Id = 42, FirstName = "Phil", IsSomething = true, Private = true };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"id\":42,\"first_name\":\"Phil\",\"is_something\":true,\"private\":true}", json);
            }

            [Fact]
            public void OmitsPropertiesWithNullValue()
            {
                var item = new
                {
                    Object = (object)null,
                    NullableInt = (int?)null,
                    NullableBool = (bool?)null
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{}", json);
            }

            [Fact]
            public void DoesNotOmitsNullablePropertiesWithAValue()
            {
                var item = new
                {
                    Object = new { Id = 42 },
                    NullableInt = (int?)1066,
                    NullableBool = (bool?)true
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"object\":{\"id\":42},\"nullable_int\":1066,\"nullable_bool\":true}", json);
            }

            [Fact]
            public void HandlesMixingNullAndNotNullData()
            {
                var item = new
                {
                    Int = 42,
                    Bool = true,
                    NullableInt = (int?)null,
                    NullableBool = (bool?)null
                };

                var json = new SimpleJsonSerializer().Serialize(item);

                Assert.Equal("{\"int\":42,\"bool\":true}", json);
            }
        }

        public class TheDeserializeMethod
        {
            [Fact]
            public void UnderstandsRubyCasing()
            {
                const string json = "{\"id\":42,\"first_name\":\"Phil\",\"is_something\":true,\"private\":true}";

                var sample = new SimpleJsonSerializer().Deserialize<Sample>(json);

                Assert.Equal(42, sample.Id);
                Assert.Equal("Phil", sample.FirstName);
                Assert.True(sample.IsSomething);
                Assert.True(sample.Private);
            }

            [Fact]
            public void DeserializesInheritedProperties()
            {
                const string json = "{\"sha\":\"commit-sha\",\"url\":\"commit-url\",\"message\":\"commit-message\"}";

                var result = new SimpleJsonSerializer().Deserialize<Commit>(json);

                Assert.Equal("commit-sha", result.Sha);
                Assert.Equal("commit-url", result.Url);
                Assert.Equal("commit-message", result.Message);
            }

            [Fact]
            public void IgnoresUnderscore()
            {
                const string json = "{\"_links\":\"blah\"}";

                var result = new SimpleJsonSerializer().Deserialize<Sample>(json);

                Assert.Equal("blah", result.Links);
            }
        }

        public class Sample
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public bool IsSomething { get; set; }
            public bool Private { get; set; }
            public string Links { get; set; }
        }
    }
}
