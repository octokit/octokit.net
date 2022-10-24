using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Policy;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class DeploymentTests
    {
        [Fact]
        public void CanSerialize()
        {
            var deployment = new NewDeployment("ref")
            {
                Payload = new Dictionary<string, string> { { "environment", "production" } }
            };
            var deserialized = new SimpleJsonSerializer().Serialize(deployment);

            Assert.Equal(@"{""ref"":""ref"",""payload"":{""environment"":""production""}}", deserialized);
        }

        [Fact]
        public void CanDeserialize()
        {
            const string json = @"{
                    ""url"": ""https://api.github.com/repos/octocat/example/deployments/1"",
                    ""id"": 1,
                    ""node_id"": ""MDEwOkRlcGxveW1lbnQx"",
                    ""sha"": ""a84d88e7554fc1fa21bcbc4efae3c782a70d2b9d"",
                    ""ref"": ""topic-branch"",
                    ""task"": ""deploy"",
                    ""payload"": {},
                    ""original_environment"": ""staging"",
                    ""environment"": ""production"",
                    ""description"": ""Deploy request from hubot"",
                    ""creator"": {
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
                    },
                    ""created_at"": ""2012-07-20T01:19:13Z"",
                    ""updated_at"": ""2012-07-20T01:19:13Z"",
                    ""statuses_url"": ""https://api.github.com/repos/octocat/example/deployments/1/statuses"",
                    ""repository_url"": ""https://api.github.com/repos/octocat/example"",
                    ""transient_environment"": false,
                    ""production_environment"": true
                }";

            var actual = new SimpleJsonSerializer().Deserialize<Deployment>(json);

            Assert.Equal(1, actual.Id);
            Assert.Equal("a84d88e7554fc1fa21bcbc4efae3c782a70d2b9d", actual.Sha);
            Assert.Equal("topic-branch", actual.Ref);
            Assert.Equal("https://api.github.com/repos/octocat/example/deployments/1", actual.Url);
            Assert.Equal("production", actual.Environment);
            Assert.Equal("staging", actual.OriginalEnvironment);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2012-07-20T01:19:13Z"), actual.UpdatedAt);
            Assert.Equal("Deploy request from hubot", actual.Description);
            Assert.Equal("https://api.github.com/repos/octocat/example/deployments/1/statuses", actual.StatusesUrl);
            Assert.Equal("https://api.github.com/repos/octocat/example", actual.RepositoryUrl);
            Assert.Equal("deploy", actual.Task);
        }
    }
}
