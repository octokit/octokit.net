using Octokit;
using Octokit.Internal;
using Xunit;

public class GistTests
{
    [Fact]
    public void CanBeDeserialized()
    {
        const string json = @"{
  ""url"": ""https://api.github.com/gists/0eb90eaf2402c59aee09"",
  ""forks_url"": ""https://api.github.com/gists/4ce20519be87ca0acb15/forks"",
  ""commits_url"": ""https://api.github.com/gists/0b764844068508e7b5a6/commits"",
  ""id"": ""1"",
  ""description"": ""description of gist"",
  ""public"": true,
  ""owner"": {
    ""login"": ""octocat"",
    ""id"": 1,
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
  },
  ""user"": null,
  ""files"": {
    ""ring.erl"": {
      ""size"": 932,
      ""raw_url"": ""https://gist.githubusercontent.com/raw/365370/8c4d2d43d178df44f4c03a7f2ac0ff512853564e/ring.erl"",
      ""type"": ""text/plain"",
      ""language"": ""Erlang"",
      ""truncated"": false,
      ""content"": ""contents of gist""
    }
  },
  ""comments"": 0,
  ""comments_url"": ""https://api.github.com/gists/d2046eed1277da6549db/comments/"",
  ""html_url"": ""https://gist.github.com/1"",
  ""git_pull_url"": ""https://gist.github.com/1.git"",
  ""git_push_url"": ""https://gist.github.com/1.git"",
  ""created_at"": ""2010-04-14T02:15:15Z"",
  ""updated_at"": ""2011-06-20T11:34:15Z"",
  ""forks"": [
    {
      ""user"": {
        ""login"": ""octocat"",
        ""id"": 1,
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
      },
      ""url"": ""https://api.github.com/gists/bb67e0c133b671e06f9f"",
      ""id"": 1,
      ""created_at"": ""2011-04-14T16:00:49Z"",
      ""updated_at"": ""2011-04-14T16:00:49Z""
    }
  ],
  ""history"": [
    {
      ""url"": ""https://api.github.com/gists/3124ac0e05b9d93f7321"",
      ""version"": ""57a7f021a713b1c5a6a199b54cc514735d2d462f"",
      ""user"": {
        ""login"": ""octocat"",
        ""id"": 1,
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
      },
      ""change_status"": {
        ""deletions"": 0,
        ""additions"": 180,
        ""total"": 180
      },
      ""committed_at"": ""2010-04-14T02:15:15Z""
    }
  ]
}";
        var serializer = new SimpleJsonSerializer();

        var gist = serializer.Deserialize<Gist>(json);

        Assert.Equal(0, gist.Comments);
        Assert.Equal("Erlang", gist.Files["ring.erl"].Language);
    }
}
