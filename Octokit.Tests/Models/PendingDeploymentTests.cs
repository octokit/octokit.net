using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class PendingDeploymentTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""environment"": {
    ""id"": 161088068,
    ""node_id"": ""MDExOkVudmlyb25tZW50MTYxMDg4MDY4"",
    ""name"": ""staging"",
    ""url"": ""https://api.github.com/repos/github/hello-world/environments/staging"",
    ""html_url"": ""https://github.com/github/hello-world/deployments/activity_log?environments_filter=staging""
},
""wait_timer"": 30,
""wait_timer_started_at"": ""2020-11-23T22:00:40Z"",
""current_user_can_approve"": true,
""reviewers"": [
    {
        ""type"": ""User"",
        ""reviewer"": {
            ""login"": ""octocat"",
            ""id"": 1,
            ""node_id"": ""MDQ6VXNlcjE="",
            ""avatar_url"": ""https://github.com/images/error/octocat_happy.gif"",
            ""gravatar_id"": """",
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
        }
    },
    {
        ""type"": ""Team"",
            ""reviewer"": {
            ""id"": 1,
            ""node_id"": ""MDQ6VGVhbTE="",
            ""url"": ""https://api.github.com/teams/1"",
            ""html_url"": ""https://github.com/orgs/github/teams/justice-league"",
            ""name"": ""Justice League"",
            ""slug"": ""justice-league"",
            ""description"": ""A great team."",
            ""privacy"": ""closed"",
            ""permission"": ""admin"",
            ""members_url"": ""https://api.github.com/teams/1/members{/member}"",
            ""repositories_url"": ""https://api.github.com/teams/1/repos"",
            ""parent"": null
        }
    }
]
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<PendingDeployment>(json);

            Assert.NotNull(payload);
            Assert.Equal(30, payload.WaitTimer);
            Assert.Equal(new DateTimeOffset(2020, 11, 23, 22, 00, 40, TimeSpan.Zero), payload.WaitTimerStartedAt);
            Assert.True(payload.CurrentUserCanApprove);

            Assert.NotNull(payload.Environment);
            Assert.Equal(161088068, payload.Environment.Id);
            Assert.Equal("MDExOkVudmlyb25tZW50MTYxMDg4MDY4", payload.Environment.NodeId);
            Assert.Equal("staging", payload.Environment.Name);
            Assert.Equal("https://api.github.com/repos/github/hello-world/environments/staging", payload.Environment.Url);
            Assert.Equal("https://github.com/github/hello-world/deployments/activity_log?environments_filter=staging", payload.Environment.HtmlUrl);

            Assert.NotNull(payload.Reviewers);
            Assert.Equal(2, payload.Reviewers.Count);

            Assert.Equal(DeploymentReviewerType.User, payload.Reviewers[0].Type);
            Assert.NotNull(payload.Reviewers[0].Reviewer);

            Assert.Equal(DeploymentReviewerType.Team, payload.Reviewers[1].Type);
            Assert.NotNull(payload.Reviewers[1].Reviewer);
        }
    }
}
