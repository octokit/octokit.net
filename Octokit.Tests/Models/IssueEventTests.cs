using System.Linq;
using Octokit;
using Octokit.Internal;
using Xunit;


public class IssueEventTests
{
    [Fact]
    public void CanDeserializeAnUnsubscribedIssueEvent()
    {
        const string json = @"{
  ""id"": 42,
  ""url"": ""https://api.github.com/repos/octocat/Hello-World/issues/events/42"",
  ""actor"": {
    ""login"": ""octocat"",
    ""id"": 1060,
    ""avatar_url"": ""https://avatars.githubusercontent.com/u/1?v=3"",
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
  },
  ""event"": ""unsubscribed"",
  ""commit_id"": null,
  ""created_at"": ""2014-07-16T15:41:42Z"",
  ""issue"": {
    ""url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1205"",
    ""labels_url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1205/labels{/name}"",
    ""comments_url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1205/comments"",
    ""events_url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1205/events"",
    ""html_url"": ""https://github.com/octocat/Hello-World/issues/1205"",
    ""id"": 37995243,
    ""number"": 1205,
    ""title"": ""settings icon should not be visible on sidebar if you are not a collaborator for public repos (and should not be accessible at /settings)"",
    ""user"": {
      ""login"": ""ashumz"",
      ""id"": 100216,
      ""avatar_url"": ""https://avatars.githubusercontent.com/u/6?v=3"",
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
    },
    ""labels"": [
      {
        ""url"": ""https://api.github.com/repos/octocat/Hello-World/labels/bug"",
        ""name"": ""bug"",
        ""color"": ""fc2929""
      },
      {
        ""url"": ""https://api.github.com/repos/octocat/Hello-World/labels/waffle:ready"",
        ""name"": ""octocat:ready"",
        ""color"": ""00c5fe""
      }
    ],
    ""state"": ""closed"",
    ""locked"": false,
    ""assignee"": null,
    ""milestone"": null,
    ""comments"": 0,
    ""created_at"": ""2014-07-16T15:39:21Z"",
    ""updated_at"": ""2014-07-16T22:16:37Z"",
    ""closed_at"": ""2014-07-16T22:16:37Z"",
    ""body"": ""body content""
  }
}";
        var serializer = new SimpleJsonSerializer();
        var issueEvent = serializer.Deserialize<IssueEvent>(json);

        Assert.NotNull(issueEvent);
        Assert.Equal(EventInfoState.Unsubscribed, issueEvent.Event);
    }
}
