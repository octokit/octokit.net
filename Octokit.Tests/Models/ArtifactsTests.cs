using System;
using Octokit.Internal;
using Xunit;

namespace Octokit.Tests.Models
{
    public class ArtifactsTests
    {
        [Fact]
        public void CanBeDeserialized()
        {
          var json = @"{
  ""total_count"": 2,
  ""artifacts"": [
    {
      ""id"": 11,
      ""node_id"": ""MDg6QXJ0aWZhY3QxMQ=="",
      ""name"": ""Rails"",
      ""size_in_bytes"": 556,
      ""url"": ""https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/11"",
      ""archive_download_url"": ""https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/11/zip"",
      ""expired"": false,
      ""created_at"": ""2020-01-10T14:59:22Z"",
      ""expires_at"": ""2020-03-21T14:59:22Z"",
      ""updated_at"": ""2020-02-21T14:59:22Z"",
      ""workflow_run"": {
        ""id"": 2332938,
        ""repository_id"": 1296269,
        ""head_repository_id"": 1296269,
        ""head_branch"": ""main"",
        ""head_sha"": ""328faa0536e6fef19753d9d91dc96a9931694ce3""
      }
    },
    {
      ""id"": 13,
      ""node_id"": ""MDg6QXJ0aWZhY3QxMw=="",
      ""name"": ""Test output"",
      ""size_in_bytes"": 453,
      ""url"": ""https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/13"",
      ""archive_download_url"": ""https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/13/zip"",
      ""expired"": false,
      ""created_at"": ""2020-01-10T14:59:22Z"",
      ""expires_at"": ""2020-03-21T14:59:22Z"",
      ""updated_at"": ""2020-02-21T14:59:22Z"",
      ""workflow_run"": {
        ""id"": 2332942,
        ""repository_id"": 1296269,
        ""head_repository_id"": 1296269,
        ""head_branch"": ""main"",
        ""head_sha"": ""178f4f6090b3fccad4a65b3e83d076a622d59652""
      }
    }
  ]
}";
          
          var serializer = new SimpleJsonSerializer();

          var payload = serializer.Deserialize<ListArtifactsResponse>(json);

          Assert.NotNull(payload);
          
          Assert.Equal(2, payload.TotalCount);
          Assert.Equal(2, payload.Artifacts.Count);
          
          Assert.Equal(11, payload.Artifacts[0].Id);
          Assert.Equal("MDg6QXJ0aWZhY3QxMQ==", payload.Artifacts[0].NodeId);
          Assert.Equal("Rails", payload.Artifacts[0].Name);
          Assert.Equal(556, payload.Artifacts[0].SizeInBytes);
          Assert.Equal("https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/11", payload.Artifacts[0].Url);
          Assert.Equal("https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/11/zip", payload.Artifacts[0].ArchiveDownloadUrl);
          Assert.False(payload.Artifacts[0].Expired);
          Assert.Equal(new DateTime(2020, 1, 10, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[0].CreatedAt);
          Assert.Equal(new DateTime(2020, 3, 21, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[0].ExpiresAt);
          Assert.Equal(new DateTime(2020, 2, 21, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[0].UpdatedAt);
          Assert.Equal(2332938, payload.Artifacts[0].WorkflowRun.Id);
          Assert.Equal(1296269, payload.Artifacts[0].WorkflowRun.RepositoryId);
          Assert.Equal(1296269, payload.Artifacts[0].WorkflowRun.HeadRepositoryId);
          Assert.Equal("main", payload.Artifacts[0].WorkflowRun.HeadBranch);
          Assert.Equal("328faa0536e6fef19753d9d91dc96a9931694ce3", payload.Artifacts[0].WorkflowRun.HeadSha);

          Assert.Equal(13, payload.Artifacts[1].Id);
          Assert.Equal("MDg6QXJ0aWZhY3QxMw==", payload.Artifacts[1].NodeId);
          Assert.Equal("Test output", payload.Artifacts[1].Name);
          Assert.Equal(453, payload.Artifacts[1].SizeInBytes);
          Assert.Equal("https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/13", payload.Artifacts[1].Url);
          Assert.Equal("https://api.github.com/repos/octo-org/octo-docs/actions/artifacts/13/zip", payload.Artifacts[1].ArchiveDownloadUrl);
          Assert.False(payload.Artifacts[1].Expired);
          Assert.Equal(new DateTime(2020, 1, 10, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[1].CreatedAt);
          Assert.Equal(new DateTime(2020, 3, 21, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[1].ExpiresAt);
          Assert.Equal(new DateTime(2020, 2, 21, 14, 59, 22, DateTimeKind.Utc), payload.Artifacts[1].UpdatedAt);
          Assert.Equal(2332942, payload.Artifacts[1].WorkflowRun.Id);
          Assert.Equal(1296269, payload.Artifacts[1].WorkflowRun.RepositoryId);
          Assert.Equal(1296269, payload.Artifacts[1].WorkflowRun.HeadRepositoryId);
          Assert.Equal("main", payload.Artifacts[1].WorkflowRun.HeadBranch);
          Assert.Equal("178f4f6090b3fccad4a65b3e83d076a622d59652", payload.Artifacts[1].WorkflowRun.HeadSha);
        }
    }
}
