using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""id"": 161335,
""node_id"": ""MDg6V29ya2Zsb3cxNjEzMzU="",
""name"": ""CI"",
""path"": "".github/workflows/blank.yaml"",
""state"": ""active"",
""created_at"": ""2020-01-08T23:48:37.000-08:00"",
""updated_at"": ""2020-01-08T23:50:21.000-08:00"",
""url"": ""https://api.github.com/repos/octo-org/octo-repo/actions/workflows/161335"",
""html_url"": ""https://github.com/octo-org/octo-repo/blob/master/.github/workflows/161335"",
""badge_url"": ""https://github.com/octo-org/octo-repo/workflows/CI/badge.svg""
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<Workflow>(json);

            Assert.NotNull(payload);
            Assert.Equal(161335, payload.Id);
            Assert.Equal("MDg6V29ya2Zsb3cxNjEzMzU=", payload.NodeId);
            Assert.Equal("CI", payload.Name);
            Assert.Equal(".github/workflows/blank.yaml", payload.Path);
            Assert.Equal("active", payload.State);
            Assert.Equal(new DateTimeOffset(2020, 01, 08, 23, 48, 37, TimeSpan.FromHours(-8)), payload.CreatedAt);
            Assert.Equal(new DateTimeOffset(2020, 01, 08, 23, 50, 21, TimeSpan.FromHours(-8)), payload.UpdatedAt);
            Assert.Equal("https://api.github.com/repos/octo-org/octo-repo/actions/workflows/161335", payload.Url);
            Assert.Equal("https://github.com/octo-org/octo-repo/blob/master/.github/workflows/161335", payload.HtmlUrl);
            Assert.Equal("https://github.com/octo-org/octo-repo/workflows/CI/badge.svg", payload.BadgeUrl);
        }
    }
}
