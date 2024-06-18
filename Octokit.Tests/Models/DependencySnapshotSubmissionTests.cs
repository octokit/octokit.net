using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Octokit
{
    public class DependencySnapshotSubmissionTests
    {
        [Fact]
        public void CanDeserialize()
        {
            const string json = @"
            {
              ""id"": 12345,
              ""created_at"": ""2018-05-04T01:14:52Z"",
              ""message"": ""Dependency results for the repo have been successfully updated."",
              ""result"": ""SUCCESS""
            }";

            var serializer = new SimpleJsonSerializer();

            var actual = serializer.Deserialize<DependencySnapshotSubmission>(json);

            Assert.Equal(12345, actual.Id);
            Assert.Equal(DateTimeOffset.Parse("2018-05-04T01:14:52Z"), actual.CreatedAt);
            Assert.Equal("Dependency results for the repo have been successfully updated.", actual.Message);
            Assert.Equal("SUCCESS", actual.Result);
        }
    }
}
