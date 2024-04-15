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
            Assert.Single(actual.Payload);
            Assert.Equal("production", actual.Payload["environment"]);
            Assert.Equal("https://gist.github.com/628b2736d379f", actual.TargetUrl);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.UpdatedAt);
            Assert.Equal("Deploy request from hubot", actual.Description);
        }

        [Fact]
        public void CanDeserialize64BitId()
        {
            const string json = @"{
                ""url"": ""https://api.github.com/repos/tgstation/TerraGov-Marine-Corps/deployments/965900823/statuses/2151661540"",
                ""id"": 2151661540,
                ""node_id"": ""DES_kwDOCXi6sc6AP7_k"",
                ""state"": ""in_progress"",
                ""creator"": {
                    ""login"": ""comfyorange"",
                    ""id"": 61334995,
                    ""node_id"": ""MDQ6VXNlcjYxMzM0OTk1"",
                    ""avatar_url"": ""https://avatars.githubusercontent.com/u/61334995?v=4"",
                    ""gravatar_id"": """",
                    ""url"": ""https://api.github.com/users/comfyorange"",
                    ""html_url"": ""https://github.com/comfyorange"",
                    ""followers_url"": ""https://api.github.com/users/comfyorange/followers"",
                    ""following_url"": ""https://api.github.com/users/comfyorange/following{/other_user}"",
                    ""gists_url"": ""https://api.github.com/users/comfyorange/gists{/gist_id}"",
                    ""starred_url"": ""https://api.github.com/users/comfyorange/starred{/owner}{/repo}"",
                    ""subscriptions_url"": ""https://api.github.com/users/comfyorange/subscriptions"",
                    ""organizations_url"": ""https://api.github.com/users/comfyorange/orgs"",
                    ""repos_url"": ""https://api.github.com/users/comfyorange/repos"",
                    ""events_url"": ""https://api.github.com/users/comfyorange/events{/privacy}"",
                    ""received_events_url"": ""https://api.github.com/users/comfyorange/received_events"",
                    ""type"": ""User"",
                    ""site_admin"": false
                },
                ""description"": ""The project is being deployed"",
                ""environment"": ""TGS: TGMC"",
                ""target_url"": """",
                ""created_at"": ""2023-06-29T19:10:55Z"",
                ""updated_at"": ""2023-06-29T19:10:55Z"",
                ""deployment_url"": ""https://api.github.com/repos/tgstation/TerraGov-Marine-Corps/deployments/965900823"",
                ""repository_url"": ""https://api.github.com/repos/tgstation/TerraGov-Marine-Corps"",
                ""environment_url"": """",
                ""log_url"": """",
                ""performed_via_github_app"": null
            }";

            var actual = new SimpleJsonSerializer().Deserialize<DeploymentStatus>(json);

            Assert.Equal(2151661540, actual.Id);
            Assert.Equal("https://api.github.com/repos/tgstation/TerraGov-Marine-Corps/deployments/965900823/statuses/2151661540", actual.Url);
            Assert.Equal(DeploymentState.InProgress, actual.State);
            Assert.Null(actual.Payload);
            Assert.Equal(String.Empty, actual.TargetUrl);
            Assert.Equal(DateTimeOffset.Parse("2023-06-29T19:10:55Z"), actual.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2023-06-29T19:10:55Z"), actual.UpdatedAt);
            Assert.Equal("The project is being deployed", actual.Description);
        }
    }
}
