using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowJobStepTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""name"": ""Set up job"",
""status"": ""completed"",
""conclusion"": ""success"",
""number"": 1,
""started_at"": ""2020-01-20T09:42:40.000-08:00"",
""completed_at"": ""2020-01-20T09:42:41.000-08:00""
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowJobStep>(json);

            Assert.NotNull(payload);
            Assert.Equal("Set up job", payload.Name);
            Assert.Equal(WorkflowJobStatus.Completed, payload.Status);
            Assert.Equal(WorkflowJobConclusion.Success, payload.Conclusion);
            Assert.Equal(1, payload.Number);
            Assert.Equal(new DateTimeOffset(2020, 01, 20, 09, 42, 40, TimeSpan.FromHours(-8)), payload.StartedAt);
            Assert.Equal(new DateTimeOffset(2020, 01, 20, 09, 42, 41, TimeSpan.FromHours(-8)), payload.CompletedAt);
        }
    }
}
