using System.Linq;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class DeploymentStatusTests
    {
        [Fact]
        public void CanDeserialize()
        {
            var expected = new DeploymentStatus
            {
                Id = 1,
                Url = "https://api.github.com/repos/octocat/example/deployments/1/statuses/42",
                State = DeploymentState.Success,
                Payload = new Dictionary<string, string> { { "environment", "production" } },
                TargetUrl = "https://gist.github.com/628b2736d379f",
                CreatedAt = DateTimeOffset.Parse("2012-07-20T01:19:13Z"),
                UpdatedAt = DateTimeOffset.Parse("2012-07-20T01:19:13Z"),
                Description = "Deploy request from hubot"
            };

            var json =
            @"{
              ""id"": 1,
              ""url"": ""https://api.github.com/repos/octocat/example/deployments/1/statuses/42"",
              ""state"": ""success"",
              ""creator"": {
                ""login"": ""octocat"",
                ""id"": 1,
                ""avatar_url"": ""https://github.com/images/error/octocat_happy.gif"",
                ""gravatar_id"": ""somehexcode"",
                ""url"": ""https://api.github.com/users/octocat"",
                ""html_url"": ""https://github.com/octocat"",
                ""followers_url"": ""https://api.github.com/users/octocat/followers"",
                ""following_url"": ""https://api.github.com/users/octocat/following{/other_user}"",
                ""gists_url"": ""https://api.github.com/users/octocat/gists{/gist_id}"",
                ""starred_url"": ""https://api.github.com/users/octocat/starred{/owner}{/repo}"",
                ""subscriptions_url"": ""https://api.github.com/users/octocat/subscriptions"",
                ""organizations_url"": ""https://api.github.com/users/octocat/orgs"",
                ""repos_url"": ""https://api.github.com/users/octocat/repos"",
                ""events_url"": ""https://api.github.com/users/octocat/events{/privacy}"",
                ""received_events_url"": ""https://api.github.com/users/octocat/received_events"",
                ""type"": ""User"",
                ""site_admin"": false
              },
              ""payload"": { ""environment"":""production""},
              ""target_url"": ""https://gist.github.com/628b2736d379f"",
              ""created_at"": ""2012-07-20T01:19:13Z"",
              ""updated_at"": ""2012-07-20T01:19:13Z"",
              ""description"": ""Deploy request from hubot""
            }";

            var actual = new SimpleJsonSerializer().Deserialize<DeploymentStatus>(json);

            Assert.Equal(expected, actual, new DeploymentStatusEqualityComparer());
        }
    }

    public class DeploymentStatusEqualityComparer : IEqualityComparer<DeploymentStatus>
    {
        public bool Equals(DeploymentStatus x, DeploymentStatus y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;

            if (x.Payload.Keys.Any(key => x.Payload[key] != y.Payload[key]))
            {
                return false;
            }

            if (y.Payload.Keys.Any(key => x.Payload[key] != y.Payload[key]))
            {
                return false;
            }

            return x.Id == y.Id &&
                   x.Url == y.Url &&
                   x.State == y.State &&
                   x.TargetUrl == y.TargetUrl &&
                   x.CreatedAt == y.CreatedAt &&
                   x.UpdatedAt == y.UpdatedAt &&
                   x.Description == y.Description;
        }

        public int GetHashCode(DeploymentStatus obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
