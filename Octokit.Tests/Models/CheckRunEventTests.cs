﻿using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class CheckRunEventTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
  ""action"": ""rerequested"",
  ""check_run"": {
    ""id"": 4,
    ""head_sha"": ""d6fde92930d4715a2b49857d24b940956b26d2d3"",
    ""external_id"": """",
    ""url"": ""https://api.github.com/repos/github/hello-world/check-runs/4"",
    ""html_url"": ""http://github.com/github/hello-world/runs/4"",
    ""status"": ""completed"",
    ""conclusion"": ""neutral"",
    ""started_at"": ""2018-05-04T01:14:52Z"",
    ""completed_at"": ""2018-05-04T01:14:52Z"",
    ""output"": {
      ""title"": ""Report"",
      ""summary"": ""It's all good."",
      ""text"": ""Minus odio facilis repudiandae. Soluta odit aut amet magni nobis. Et voluptatibus ex dolorem et eum."",
      ""annotations_count"": 2,
      ""annotations_url"": ""https://api.github.com/repos/github/hello-world/check-runs/4/annotations""
    },
    ""name"": ""randscape"",
    ""check_suite"": {
      ""id"": 5,
      ""head_branch"": ""main"",
      ""head_sha"": ""d6fde92930d4715a2b49857d24b940956b26d2d3"",
      ""status"": ""completed"",
      ""conclusion"": ""neutral"",
      ""url"": ""https://api.github.com/repos/github/hello-world/check-suites/5"",
      ""before"": ""146e867f55c26428e5f9fade55a9bbf5e95a7912"",
      ""after"": ""d6fde92930d4715a2b49857d24b940956b26d2d3"",
      ""pull_requests"": [

      ],
      ""app"": {
        ""id"": 2,
        ""node_id"": ""MDExOkludGVncmF0aW9uMQ=="",
        ""owner"": {
          ""login"": ""github"",
          ""id"": 340,
          ""node_id"": ""MDEyOk9yZ2FuaXphdGlvbjE="",
          ""avatar_url"": ""http://alambic.github.com/avatars/u/340?"",
          ""gravatar_id"": """",
          ""url"": ""https://api.github.com/users/github"",
          ""html_url"": ""http://github.com/github"",
          ""followers_url"": ""https://api.github.com/users/github/followers"",
          ""following_url"": ""https://api.github.com/users/github/following{/other_user}"",
          ""gists_url"": ""https://api.github.com/users/github/gists{/gist_id}"",
          ""starred_url"": ""https://api.github.com/users/github/starred{/owner}{/repo}"",
          ""subscriptions_url"": ""https://api.github.com/users/github/subscriptions"",
          ""organizations_url"": ""https://api.github.com/users/github/orgs"",
          ""repos_url"": ""https://api.github.com/users/github/repos"",
          ""events_url"": ""https://api.github.com/users/github/events{/privacy}"",
          ""received_events_url"": ""https://api.github.com/users/github/received_events"",
          ""type"": ""Organization"",
          ""site_admin"": false
        },
        ""name"": ""Super Duper"",
        ""description"": null,
        ""external_url"": ""http://super-duper.example.com"",
        ""html_url"": ""http://github.com/apps/super-duper"",
        ""created_at"": ""2018-04-25T20:42:10Z"",
        ""updated_at"": ""2018-04-25T20:42:10Z""
      },
      ""created_at"": ""2018-05-04T01:14:52Z"",
      ""updated_at"": ""2018-05-04T01:14:52Z""
    },
    ""app"": {
      ""id"": 2,
      ""node_id"": ""MDExOkludGVncmF0aW9uMQ=="",
      ""owner"": {
        ""login"": ""github"",
        ""id"": 340,
        ""node_id"": ""MDEyOk9yZ2FuaXphdGlvbjE="",
        ""avatar_url"": ""http://alambic.github.com/avatars/u/340?"",
        ""gravatar_id"": """",
        ""url"": ""https://api.github.com/users/github"",
        ""html_url"": ""http://github.com/github"",
        ""followers_url"": ""https://api.github.com/users/github/followers"",
        ""following_url"": ""https://api.github.com/users/github/following{/other_user}"",
        ""gists_url"": ""https://api.github.com/users/github/gists{/gist_id}"",
        ""starred_url"": ""https://api.github.com/users/github/starred{/owner}{/repo}"",
        ""subscriptions_url"": ""https://api.github.com/users/github/subscriptions"",
        ""organizations_url"": ""https://api.github.com/users/github/orgs"",
        ""repos_url"": ""https://api.github.com/users/github/repos"",
        ""events_url"": ""https://api.github.com/users/github/events{/privacy}"",
        ""received_events_url"": ""https://api.github.com/users/github/received_events"",
        ""type"": ""Organization"",
        ""site_admin"": false
      },
      ""name"": ""Super Duper"",
      ""description"": null,
      ""external_url"": ""http://super-duper.example.com"",
      ""html_url"": ""http://github.com/apps/super-duper"",
      ""created_at"": ""2018-04-25T20:42:10Z"",
      ""updated_at"": ""2018-04-25T20:42:10Z""
    },
    ""pull_requests"": [

    ]
  },
  ""requested_action"": {
    ""identifier"": ""dosomeaction""
  },
  ""repository"": {
    ""id"": 526,
    ""node_id"": ""MDEwOlJlcG9zaXRvcnkxMzU0OTMyMzM="",
    ""name"": ""hello-world"",
    ""full_name"": ""github/hello-world"",
    ""owner"": {
      ""login"": ""github"",
      ""id"": 340,
      ""node_id"": ""MDQ6VXNlcjIxMDMxMDY3"",
      ""avatar_url"": ""http://alambic.github.com/avatars/u/340?"",
      ""gravatar_id"": """",
      ""url"": ""https://api.github.com/users/github"",
      ""html_url"": ""http://github.com/github"",
      ""followers_url"": ""https://api.github.com/users/github/followers"",
      ""following_url"": ""https://api.github.com/users/github/following{/other_user}"",
      ""gists_url"": ""https://api.github.com/users/github/gists{/gist_id}"",
      ""starred_url"": ""https://api.github.com/users/github/starred{/owner}{/repo}"",
      ""subscriptions_url"": ""https://api.github.com/users/github/subscriptions"",
      ""organizations_url"": ""https://api.github.com/users/github/orgs"",
      ""repos_url"": ""https://api.github.com/users/github/repos"",
      ""events_url"": ""https://api.github.com/users/github/events{/privacy}"",
      ""received_events_url"": ""https://api.github.com/users/github/received_events"",
      ""type"": ""Organization"",
      ""site_admin"": false
    },
    ""private"": false,
    ""html_url"": ""http://github.com/github/hello-world"",
    ""description"": null,
    ""fork"": false,
    ""url"": ""https://api.github.com/repos/github/hello-world"",
    ""forks_url"": ""https://api.github.com/repos/github/hello-world/forks"",
    ""keys_url"": ""https://api.github.com/repos/github/hello-world/keys{/key_id}"",
    ""collaborators_url"": ""https://api.github.com/repos/github/hello-world/collaborators{/collaborator}"",
    ""teams_url"": ""https://api.github.com/repos/github/hello-world/teams"",
    ""hooks_url"": ""https://api.github.com/repos/github/hello-world/hooks"",
    ""issue_events_url"": ""https://api.github.com/repos/github/hello-world/issues/events{/number}"",
    ""events_url"": ""https://api.github.com/repos/github/hello-world/events"",
    ""assignees_url"": ""https://api.github.com/repos/github/hello-world/assignees{/user}"",
    ""branches_url"": ""https://api.github.com/repos/github/hello-world/branches{/branch}"",
    ""tags_url"": ""https://api.github.com/repos/github/hello-world/tags"",
    ""blobs_url"": ""https://api.github.com/repos/github/hello-world/git/blobs{/sha}"",
    ""git_tags_url"": ""https://api.github.com/repos/github/hello-world/git/tags{/sha}"",
    ""git_refs_url"": ""https://api.github.com/repos/github/hello-world/git/refs{/sha}"",
    ""trees_url"": ""https://api.github.com/repos/github/hello-world/git/trees{/sha}"",
    ""statuses_url"": ""https://api.github.com/repos/github/hello-world/statuses/{sha}"",
    ""languages_url"": ""https://api.github.com/repos/github/hello-world/languages"",
    ""stargazers_url"": ""https://api.github.com/repos/github/hello-world/stargazers"",
    ""contributors_url"": ""https://api.github.com/repos/github/hello-world/contributors"",
    ""subscribers_url"": ""https://api.github.com/repos/github/hello-world/subscribers"",
    ""subscription_url"": ""https://api.github.com/repos/github/hello-world/subscription"",
    ""commits_url"": ""https://api.github.com/repos/github/hello-world/commits{/sha}"",
    ""git_commits_url"": ""https://api.github.com/repos/github/hello-world/git/commits{/sha}"",
    ""comments_url"": ""https://api.github.com/repos/github/hello-world/comments{/number}"",
    ""issue_comment_url"": ""https://api.github.com/repos/github/hello-world/issues/comments{/number}"",
    ""contents_url"": ""https://api.github.com/repos/github/hello-world/contents/{+path}"",
    ""compare_url"": ""https://api.github.com/repos/github/hello-world/compare/{base}...{head}"",
    ""merges_url"": ""https://api.github.com/repos/github/hello-world/merges"",
    ""archive_url"": ""https://api.github.com/repos/github/hello-world/{archive_format}{/ref}"",
    ""downloads_url"": ""https://api.github.com/repos/github/hello-world/downloads"",
    ""issues_url"": ""https://api.github.com/repos/github/hello-world/issues{/number}"",
    ""pulls_url"": ""https://api.github.com/repos/github/hello-world/pulls{/number}"",
    ""milestones_url"": ""https://api.github.com/repos/github/hello-world/milestones{/number}"",
    ""notifications_url"": ""https://api.github.com/repos/github/hello-world/notifications{?since,all,participating}"",
    ""labels_url"": ""https://api.github.com/repos/github/hello-world/labels{/name}"",
    ""releases_url"": ""https://api.github.com/repos/github/hello-world/releases{/id}"",
    ""deployments_url"": ""https://api.github.com/repos/github/hello-world/deployments"",
    ""created_at"": ""2018-04-25T20:42:10Z"",
    ""updated_at"": ""2018-04-25T20:43:34Z"",
    ""pushed_at"": ""2018-05-04T01:14:47Z"",
    ""git_url"": ""git://github.com/github/hello-world.git"",
    ""ssh_url"": ""ssh://git@localhost:3035/github/hello-world.git"",
    ""clone_url"": ""http://github.com/github/hello-world.git"",
    ""svn_url"": ""http://github.com/github/hello-world"",
    ""homepage"": null,
    ""size"": 0,
    ""stargazers_count"": 0,
    ""watchers_count"": 0,
    ""language"": null,
    ""has_issues"": true,
    ""has_projects"": true,
    ""has_downloads"": true,
    ""has_wiki"": true,
    ""has_pages"": false,
    ""forks_count"": 0,
    ""mirror_url"": null,
    ""archived"": false,
    ""open_issues_count"": 3,
    ""license"": null,
    ""forks"": 0,
    ""open_issues"": 3,
    ""watchers"": 0,
    ""default_branch"": ""main""
  },
  ""organization"": {
    ""login"": ""github"",
    ""id"": 340,
    ""node_id"": ""MDEyOk9yZ2FuaXphdGlvbjM4MzAyODk5"",
    ""url"": ""https://api.github.com/orgs/github"",
    ""repos_url"": ""https://api.github.com/orgs/github/repos"",
    ""events_url"": ""https://api.github.com/orgs/github/events"",
    ""hooks_url"": ""https://api.github.com/orgs/github/hooks"",
    ""issues_url"": ""https://api.github.com/orgs/github/issues"",
    ""members_url"": ""https://api.github.com/orgs/github/members{/member}"",
    ""public_members_url"": ""https://api.github.com/orgs/github/public_members{/member}"",
    ""avatar_url"": ""http://alambic.github.com/avatars/u/340?"",
    ""description"": ""How people build software.""
  },
  ""sender"": {
    ""login"": ""octocat"",
    ""id"": 5346,
    ""node_id"": ""MDQ6VXNlcjIxMDMxMDY3"",
    ""avatar_url"": ""http://alambic.github.com/avatars/u/5346?"",
    ""gravatar_id"": """",
    ""url"": ""https://api.github.com/users/octocat"",
    ""html_url"": ""http://github.com/octocat"",
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
  ""installation"": {
    ""id"": 1
  }
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<CheckRunEventPayload>(json);

            Assert.Equal("rerequested", payload.Action);
            Assert.Equal("d6fde92930d4715a2b49857d24b940956b26d2d3", payload.CheckRun.HeadSha);
            Assert.Equal(4, payload.CheckRun.Id);
            Assert.Equal(CheckStatus.Completed, payload.CheckRun.Status);
            Assert.Equal(CheckConclusion.Neutral, payload.CheckRun.Conclusion);
            Assert.Equal("dosomeaction", payload.RequestedAction.Identifier);
            Assert.Equal(5, payload.CheckRun.CheckSuite.Id);
            Assert.Equal(CheckStatus.Completed, payload.CheckRun.CheckSuite.Status.Value);
            Assert.Equal(CheckConclusion.Neutral, payload.CheckRun.CheckSuite.Conclusion);
        }
    }
}
