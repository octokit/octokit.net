using System.Linq;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class HookTests
    {
        [Fact]
        public void CanDeserialize()
        {
            var expected = new Hook()
            {
                Id = 1,
                Url = "https://api.github.com/repos/octocat/example/deployments/1/statuses/42",
                Name = "web",
                Events = new List<string> { "push", "pull_request"},
                Active = true,
                Config = new Dictionary<string, object>() { {"url", "http://example.com"}, {"content_type", "json"}},
                CreatedAt = DateTimeOffset.Parse("2011-09-06T17:26:27Z"),
                UpdatedAt = DateTimeOffset.Parse("2011-09-06T20:39:23Z"),
            };

            var json =
            @"{
              ""url"": ""https://api.github.com/repos/octocat/Hello-World/hooks/1"",
              ""updated_at"": ""2011-09-06T20:39:23Z"",
              ""created_at"": ""2011-09-06T17:26:27Z"",
              ""name"": ""web"",
              ""events"": [
                ""push"",
                ""pull_request""
              ],
              ""active"": true,
              ""config"": {
                ""url"": ""http://example.com"",
                ""content_type"": ""json""
              },
              ""id"": 1
            }";
            var actual = new SimpleJsonSerializer().Deserialize<Hook>(json);

            Assert.Equal(expected, actual, new HookEqualityComparer());
            Assert.Equal(expected.Events, actual.Events);
            Assert.Equal(expected.Config, actual.Config);
        }
    }

    public class HookEqualityComparer : IEqualityComparer<Hook>
    {
        public bool Equals(Hook x, Hook y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            return x.Id == y.Id &&
                   x.Url == y.Url &&
                   x.Name == y.Name &&
                   x.Active == y.Active &&
                   x.CreatedAt == y.CreatedAt &&
                   x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(Hook obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
