﻿using System.Linq;
using Octokit;
using Octokit.Internal;
using Xunit;

public class IssueTest
{
    [Fact]
    public void CanBeDeserialized()
    {
        const string json = @"{
""id"": 1,
""url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1347"",
""html_url"": ""https://github.com/octocat/Hello-World/issues/1347"",
""number"": 1347,
""state"": ""open"",
""title"": ""Found a bug"",
""body"": ""I'm having a problem with this."",
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
""labels"": [
{
    ""url"": ""https://api.github.com/repos/octocat/Hello-World/labels/bug"",
    ""name"": ""bug"",
    ""color"": ""f29513""
}
],
""assignee"": {
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
""milestone"": {
""url"": ""https://api.github.com/repos/octocat/Hello-World/milestones/1"",
""number"": 1,
""state"": ""open"",
""title"": ""v1.0"",
""description"": """",
""creator"": {
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
""open_issues"": 4,
""closed_issues"": 8,
""created_at"": ""2011-04-10T20:09:31Z"",
""updated_at"": ""2014-03-03T18:58:10Z"",
""closed_at"": ""2013-02-12T13:22:01Z"",
""due_on"": null
},
""comments"": 0,
""pull_request"": {
""url"": ""https://api.github.com/repos/octocat/Hello-World/pulls/1347"",
""html_url"": ""https://github.com/octocat/Hello-World/pull/1347"",
""diff_url"": ""https://github.com/octocat/Hello-World/pull/1347.diff"",
""patch_url"": ""https://github.com/octocat/Hello-World/pull/1347.patch""
},
""closed_at"": null,
""created_at"": ""2011-04-22T13:33:48Z"",
""updated_at"": ""2011-04-22T13:33:48Z"",
""closed_by"": {
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
""site_admin"": false,
},
""active_lock_reason"": null
}";
        var serializer = new SimpleJsonSerializer();

        var issue = serializer.Deserialize<Issue>(json);

        Assert.Equal(1347, issue.Number);
        Assert.Equal("octocat", issue.User.Login);
        Assert.Equal("bug", issue.Labels.First().Name);
        Assert.Null(issue.ActiveLockReason);
    }

    public class TheToUpdateMethod
    {
        [Fact]
        public void CreatesAnIssueUpdateRequestObject()
        {
            const string json = @"{
""id"": 1,
""url"": ""https://api.github.com/repos/octocat/Hello-World/issues/1347"",
""html_url"": ""https://github.com/octocat/Hello-World/issues/1347"",
""number"": 1347,
""state"": ""open"",
""title"": ""Found a bug"",
""body"": ""I'm having a problem with this."",
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
""labels"": [
{
    ""url"": ""https://api.github.com/repos/octocat/Hello-World/labels/bug"",
    ""name"": ""bug"",
    ""color"": ""f29513""
}
],
""assignee"": {
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
""assignees"": [
{
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
}
],
""milestone"": {
""url"": ""https://api.github.com/repos/octocat/Hello-World/milestones/1"",
""number"": 1,
""state"": ""open"",
""title"": ""v1.0"",
""description"": """",
""creator"": {
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
""open_issues"": 4,
""closed_issues"": 8,
""created_at"": ""2011-04-10T20:09:31Z"",
""updated_at"": ""2014-03-03T18:58:10Z"",
""closed_at"": ""2013-02-12T13:22:01Z"",
""due_on"": null
},
""comments"": 0,
""pull_request"": {
""url"": ""https://api.github.com/repos/octocat/Hello-World/pulls/1347"",
""html_url"": ""https://github.com/octocat/Hello-World/pull/1347"",
""diff_url"": ""https://github.com/octocat/Hello-World/pull/1347.diff"",
""patch_url"": ""https://github.com/octocat/Hello-World/pull/1347.patch""
},
""closed_at"": null,
""created_at"": ""2011-04-22T13:33:48Z"",
""updated_at"": ""2011-04-22T13:33:48Z"",
""closed_by"": {
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
}
}";
            var serializer = new SimpleJsonSerializer();
            var issue = serializer.Deserialize<Issue>(json);

            var update = issue.ToUpdate();

            Assert.NotNull(update.Labels);
            Assert.Equal(1, update.Milestone.GetValueOrDefault());
            Assert.Equal("octocat", update.Assignees.FirstOrDefault());
        }
    }
}
