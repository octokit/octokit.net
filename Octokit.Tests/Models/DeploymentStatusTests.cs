using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class DeploymentStatusTests
    {
        [Fact]
        public void CanDeserialize()
        {
            const string json = @"{
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

            Assert.Equal(1, actual.Id);
            Assert.Equal("https://api.github.com/repos/octocat/example/deployments/1/statuses/42", actual.Url);
            Assert.Equal(DeploymentState.Success, actual.State);
            Assert.Equal(1, actual.Payload.Count);
            Assert.Equal("production", actual.Payload["environment"]);
            Assert.Equal("https://gist.github.com/628b2736d379f", actual.TargetUrl);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.UpdatedAt);
            Assert.Equal("Deploy request from hubot", actual.Description);
        }
    }
}
