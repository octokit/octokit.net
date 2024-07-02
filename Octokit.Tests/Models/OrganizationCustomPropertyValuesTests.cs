using System.Collections.Generic;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class OrganizationCustomPropertyValuesTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"[
  {
    ""repository_id"": 816170000,
    ""repository_name"": ""somerepo"",
    ""repository_full_name"": ""contoso/somerepo"",
    ""properties"": [
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
        ""value"": null
      },
      {
        ""property_name"": ""test_tf"",
        ""value"": ""true""
      }
    ]
  },
  {
    ""repository_id"": 813230000,
    ""repository_name"": ""entities"",
    ""repository_full_name"": ""contoso/entities"",
    ""properties"": [
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
  }
]
";
            var serializer = new SimpleJsonSerializer();

            var properties = serializer.Deserialize<IReadOnlyList<OrganizationCustomPropertyValues>>(json);
            Assert.NotNull(properties);
            Assert.Equal(2, properties.Count);

            var somerepo = properties[0];
            Assert.Equal(816170000, somerepo.RepositoryId);
            Assert.Equal("somerepo", somerepo.RepositoryName);
            Assert.Equal("contoso/somerepo", somerepo.RepositoryFullName);
            Assert.Equal(4, somerepo.Properties.Count);

            var somerepoTestMs = somerepo.Properties[0];
            Assert.Equal("test_ms", somerepoTestMs.PropertyName);
            Assert.Equal(new List<string> { "option_d", "option_e" }, somerepoTestMs.Values);

            var somerepoTestSs = somerepo.Properties[1];
            Assert.Equal("test_ss", somerepoTestSs.PropertyName);
            Assert.Equal("option_c", somerepoTestSs.Value);
            Assert.Equal(new List<string> { "option_c" }, somerepoTestSs.Values);

            var somerepoTestStr = somerepo.Properties[2];
            Assert.Equal("test_str", somerepoTestStr.PropertyName);
            Assert.Null(somerepoTestStr.Value);

            var somerepoTestTf = somerepo.Properties[3];
            Assert.Equal("test_tf", somerepoTestTf.PropertyName);
            Assert.Equal("true", somerepoTestTf.Value);


            var entities = properties[1];
            Assert.Equal(813230000, entities.RepositoryId);
            Assert.Equal("entities", entities.RepositoryName);
            Assert.Equal("contoso/entities", entities.RepositoryFullName);
            Assert.Equal(4, entities.Properties.Count);

            var entitiesTestMs = entities.Properties[0];
            Assert.Equal("test_ms", entitiesTestMs.PropertyName);
            Assert.Equal(new List<string> { "option_d", "option_e" }, entitiesTestMs.Values);

            var entitiesTestSs = entities.Properties[1];
            Assert.Equal("test_ss", entitiesTestSs.PropertyName);
            Assert.Equal("option_c", entitiesTestSs.Value);
            Assert.Equal(new List<string> { "option_c" }, entitiesTestSs.Values);

            var entitiesTestStr = entities.Properties[2];
            Assert.Equal("test_str", entitiesTestStr.PropertyName);
            Assert.Equal("hello", entitiesTestStr.Value);

            var entitiesTestTf = entities.Properties[3];
            Assert.Equal("test_tf", entitiesTestTf.PropertyName);
            Assert.Equal("unset", entitiesTestTf.Value);
        }
    }
}
