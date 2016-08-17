using System.Linq;
using Octokit;
using Octokit.Internal;
using Xunit;

public class OrganizationTest
{
    [Fact]
    public void CanBeDeserializedWithNullPrivateGistsDiskUsageAndCollaborators()
    {
        const string json = @"{
  ""login"": ""octocat"",
  ""id"": 1234,
  ""url"": ""https://api.github.com/orgs/octocat"",
  ""repos_url"": ""https://api.github.com/orgs/octocat/repos"",
  ""events_url"": ""https://api.github.com/orgs/octocat/events"",
  ""hooks_url"": ""https://api.github.com/orgs/octocat/hooks"",
  ""issues_url"": ""https://api.github.com/orgs/octocat/issues"",
  ""members_url"": ""https://api.github.com/orgs/octocat/members{/member}"",
  ""public_members_url"": ""https://api.github.com/orgs/octocat/public_members{/member}"",
  ""avatar_url"": ""https://avatars.githubusercontent.com/u/1234?v=3"",
  ""description"": ""Test org."",
  ""name"": ""Octocat"",
  ""company"": null,
  ""blog"": ""http://octocat.abc"",
  ""location"": """",
  ""email"": """",
  ""public_repos"": 13,
  ""public_gists"": 0,
  ""followers"": 0,
  ""following"": 0,
  ""html_url"": ""https://github.com/octocat"",
  ""created_at"": ""2012-09-11T21:54:25Z"",
  ""updated_at"": ""2016-08-02T05:44:12Z"",
  ""type"": ""Organization"",
  ""total_private_repos"": 1,
  ""owned_private_repos"": 1,
  ""private_gists"": null,
  ""disk_usage"": null,
  ""collaborators"": null,
  ""billing_email"": null,
  ""plan"": {
    ""name"": ""organization"",
    ""space"": 976562499,
    ""private_repos"": 9999,
    ""filled_seats"": 45,
    ""seats"": 45
  }
}";
        var serializer = new SimpleJsonSerializer();

        var org = serializer.Deserialize<Organization>(json);

        Assert.Equal("octocat", org.Login);
        Assert.Equal(1234, org.Id);
    }
}

