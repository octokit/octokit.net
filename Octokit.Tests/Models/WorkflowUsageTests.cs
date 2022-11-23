using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowUsageTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
""billable"": {
    ""UBUNTU"": {
        ""total_ms"": 180000
    },
    ""MACOS"": {
        ""total_ms"": 240000
    },
    ""WINDOWS"": {
        ""total_ms"": 300000
    }
}
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowUsage>(json);

            Assert.NotNull(payload);
            Assert.NotNull(payload.Billable);
            Assert.NotNull(payload.Billable.Ubuntu);
            Assert.NotNull(payload.Billable.MacOS);
            Assert.NotNull(payload.Billable.Windows);
            Assert.Equal(180000, payload.Billable.Ubuntu.TotalMs);
            Assert.Equal(240000, payload.Billable.MacOS.TotalMs);
            Assert.Equal(300000, payload.Billable.Windows.TotalMs);
        }
    }
}
