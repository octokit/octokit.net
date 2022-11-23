using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class EnvironmentApprovalTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""state"": ""approved"",
""comment"": ""Ship it!"",
""environments"": [
    {
        ""id"": 161088068,
        ""node_id"": ""MDExOkVudmlyb25tZW50MTYxMDg4MDY4"",
        ""name"": ""staging"",
        ""url"": ""https://api.github.com/repos/github/hello-world/environments/staging"",
        ""html_url"": ""https://github.com/github/hello-world/deployments/activity_log?environments_filter=staging"",
        ""created_at"": ""2020-11-23T22:00:40Z"",
        ""updated_at"": ""2020-11-23T22:00:41Z""
    }
],
""user"": {
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
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<EnvironmentApprovals>(json);

            Assert.NotNull(payload);
            Assert.Equal("approved", payload.State);
            Assert.Equal("Ship it!", payload.Comment);
            Assert.NotNull(payload.User);
            Assert.NotNull(payload.Environments);

            var approval = Assert.Single(payload.Environments);

            Assert.NotNull(approval);
            Assert.Equal(161088068, approval.Id);
            Assert.Equal("MDExOkVudmlyb25tZW50MTYxMDg4MDY4", approval.NodeId);
            Assert.Equal("staging", approval.Name);
            Assert.Equal("https://api.github.com/repos/github/hello-world/environments/staging", approval.Url);
            Assert.Equal("https://github.com/github/hello-world/deployments/activity_log?environments_filter=staging", approval.HtmlUrl);
            Assert.Equal(new DateTimeOffset(2020, 11, 23, 22, 00, 40, TimeSpan.Zero), approval.CreatedAt);
            Assert.Equal(new DateTimeOffset(2020, 11, 23, 22, 00, 41, TimeSpan.Zero), approval.UpdatedAt);
        }
    }
}
