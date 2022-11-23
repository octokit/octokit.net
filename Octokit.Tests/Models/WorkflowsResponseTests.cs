using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowsResponseTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""total_count"": 2,
""workflows"": [
    {
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
    },
    {
        ""id"": 269289,
        ""node_id"": ""MDE4OldvcmtmbG93IFNlY29uZGFyeTI2OTI4OQ=="",
        ""name"": ""Linter"",
        ""path"": "".github/workflows/linter.yaml"",
        ""state"": ""active"",
        ""created_at"": ""2020-01-08T23:48:37.000-08:00"",
        ""updated_at"": ""2020-01-08T23:50:21.000-08:00"",
        ""url"": ""https://api.github.com/repos/octo-org/octo-repo/actions/workflows/269289"",
        ""html_url"": ""https://github.com/octo-org/octo-repo/blob/master/.github/workflows/269289"",
        ""badge_url"": ""https://github.com/octo-org/octo-repo/workflows/Linter/badge.svg""
    }
]
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowsResponse>(json);

            Assert.NotNull(payload);
            Assert.Equal(2, payload.TotalCount);
            Assert.NotNull(payload.Workflows);
            Assert.NotEmpty(payload.Workflows);
            Assert.Equal(2, payload.Workflows.Count);
        }
    }
}
