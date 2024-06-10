using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowRunUsageTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""billable"": {
  ""UBUNTU"": {
    ""total_ms"": 180000,
    ""jobs"": 1,
    ""job_runs"": [
      {
        ""job_id"": 1,
        ""duration_ms"": 180000
      }
    ]
  },
  ""MACOS"": {
    ""total_ms"": 240000,
    ""jobs"": 4,
    ""job_runs"": [
      {
        ""job_id"": 2,
        ""duration_ms"": 60000
      },
      {
        ""job_id"": 3,
        ""duration_ms"": 60000
      },
      {
        ""job_id"": 4,
        ""duration_ms"": 60000
      },
      {
        ""job_id"": 5,
        ""duration_ms"": 60000
      }
    ]
  },
  ""WINDOWS"": {
    ""total_ms"": 300000,
    ""jobs"": 2,
    ""job_runs"": [
      {
        ""job_id"": 6,
        ""duration_ms"": 150000
      },
      {
        ""job_id"": 7,
        ""duration_ms"": 150000
      }
    ]
  }
},
""run_duration_ms"": 500000
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowRunUsage>(json);

            Assert.NotNull(payload);
            Assert.Equal(500000, payload.RunDurationMs);
            Assert.NotNull(payload.Billable);
            Assert.NotNull(payload.Billable.Ubuntu);
            Assert.NotNull(payload.Billable.MacOS);
            Assert.NotNull(payload.Billable.Windows);
            Assert.Equal(180000, payload.Billable.Ubuntu.TotalMs);
            Assert.Equal(1, payload.Billable.Ubuntu.Jobs);
            Assert.NotNull(payload.Billable.Ubuntu.JobRuns);
            Assert.Single(payload.Billable.Ubuntu.JobRuns);
            Assert.Equal(240000, payload.Billable.MacOS.TotalMs);
            Assert.Equal(4, payload.Billable.MacOS.Jobs);
            Assert.NotNull(payload.Billable.MacOS.JobRuns);
            Assert.Equal(4, payload.Billable.MacOS.JobRuns.Count);
            Assert.Equal(300000, payload.Billable.Windows.TotalMs);
            Assert.Equal(2, payload.Billable.Windows.Jobs);
            Assert.NotNull(payload.Billable.Windows.JobRuns);
            Assert.Equal(2, payload.Billable.Windows.JobRuns.Count);
        }
    }
}
