using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowDispatchTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
  ""workflow_run_id"": 21217723379,
  ""run_url"": ""https://api.github.com/repos/octo-org/octo-repo/actions/runs/21217723379"",
  ""html_url"": ""https://github.com/octo-org/octo-repo/actions/runs/21217723379""
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowDispatch>(json);

            Assert.NotNull(payload);
            Assert.Equal(21217723379, payload.WorkflowRunId);
            Assert.Equal("https://api.github.com/repos/octo-org/octo-repo/actions/runs/21217723379", payload.RunUrl);
            Assert.Equal("https://github.com/octo-org/octo-repo/actions/runs/21217723379", payload.HtmlUrl);
        }
    }
}
