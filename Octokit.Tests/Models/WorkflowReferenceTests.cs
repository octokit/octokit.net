using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class WorkflowReferenceTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
            const string json = @"{
  ""path"": ""octocat/Hello-World/.github/workflows/deploy.yml@main"",
  ""sha"": ""86e8bc9ecf7d38b1ed2d2cfb8eb87ba9b35b01db"",
  ""ref"": ""refs/heads/main""
}";

            var serializer = new SimpleJsonSerializer();

            var payload = serializer.Deserialize<WorkflowReference>(json);

            Assert.NotNull(payload);
            Assert.Equal("octocat/Hello-World/.github/workflows/deploy.yml@main", payload.Path);
            Assert.Equal("86e8bc9ecf7d38b1ed2d2cfb8eb87ba9b35b01db", payload.Sha);
            Assert.Equal("refs/heads/main", payload.Ref);
        }
    }
}
