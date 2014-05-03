using System.Linq;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using Xunit;

namespace Octokit.Tests.Models
{
    public class DeploymentTests
    {
        [Fact]
        public void CanDeserialize()
        {
            var expected = new Deployment {
                Id = 1,
                Sha = "topic-branch",
                Url = "https://api.github.com/repos/octocat/example/deployments/1",
                Payload = new Dictionary<string, string>{{ "environment", "production"}},
                CreatedAt = DateTimeOffset.Parse("2012-07-20T01:19:13Z"),
                UpdatedAt = DateTimeOffset.Parse("2012-07-20T01:19:13Z"),
                Description = "Deploy request from hubot",
                StatusesUrl = "https://api.github.com/repos/octocat/example/deployments/1/statuses"
            };

            var json = 
                @"{
                    ""id"": 1,
                    ""sha"": ""topic-branch"",
                    ""url"": ""https://api.github.com/repos/octocat/example/deployments/1"",
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
                    ""created_at"": ""2012-07-20T01:19:13Z"",
                    ""updated_at"": ""2012-07-20T01:19:13Z"",
                    ""description"": ""Deploy request from hubot"",
                    ""statuses_url"": ""https://api.github.com/repos/octocat/example/deployments/1/statuses""
                }";

            var actual = new SimpleJsonSerializer().Deserialize<Deployment>(json);

            Assert.Equal(expected, actual, new DeploymentEqualityComparer());
        }
    }

    // Equaliy for the sake of testing serialization/deserialization.
    // Actual production equality should most likely just be a check
    // of `Url` equality.
    public class DeploymentEqualityComparer : IEqualityComparer<Deployment>
    {
        public bool Equals(Deployment x, Deployment y)
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
                   x.Sha == y .Sha &&
                   x.Url == y.Url &&
                   x.CreatedAt == y.CreatedAt &&
                   x.UpdatedAt == y.UpdatedAt &&
                   x.Description == y.Description &&
                   x.StatusesUrl == y.StatusesUrl;
        }

        public int GetHashCode(Deployment obj)
        {
            throw new NotImplementedException();
        }
    }
}
