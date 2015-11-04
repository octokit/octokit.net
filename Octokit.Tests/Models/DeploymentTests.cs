using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class DeploymentTests
    {
        [Fact]
        public void CanDeserialize()
        {
            const string json = @"{
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

            Assert.Equal(1, actual.Id);
            Assert.Equal("topic-branch", actual.Sha);
            Assert.Equal("https://api.github.com/repos/octocat/example/deployments/1", actual.Url);
            Assert.Equal(new ReadOnlyDictionary<string, string>(new Dictionary<string, string> { { "environment", "production" } }), actual.Payload);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.UpdatedAt);
            Assert.Equal("Deploy request from hubot", actual.Description);
            Assert.Equal("https://api.github.com/repos/octocat/example/deployments/1/statuses", actual.StatusesUrl);
        }
    }
}
