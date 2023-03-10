using Octokit;
using Octokit.Internal;
using Xunit;

public class CollaboratorPermissionResponseTests
{
  [Fact]
  public void CanDeserialize()
  {
    const string json = @"{
      ""permission"": ""read"",
      ""user"": {
        ""login"": ""octocat"",
        ""id"": 583231,
        ""node_id"": ""MDQ6VXNlcjU4MzIzMQ=="",
        ""avatar_url"": ""https://avatars.githubusercontent.com/u/583231?v=4"",
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
        ""site_admin"": false,
        ""permissions"": {
          ""admin"": false,
          ""maintain"": false,
          ""push"": false,
          ""triage"": false,
          ""pull"": true
        },
        ""role_name"": ""read""
      },
      ""role_name"": ""read""
    }";

    var actual = new SimpleJsonSerializer().Deserialize<CollaboratorPermissionResponse>(json);

    Assert.Equal("read", actual.Permission);
    Assert.Equal("octocat", actual.Collaborator.Login);
    Assert.Equal(583231, actual.Collaborator.Id);
    Assert.Equal("read", actual.RoleName);

    Assert.Equal("Collaborator: 583231 Permission: read RoleName: read", actual.DebuggerDisplay);
  }
}
