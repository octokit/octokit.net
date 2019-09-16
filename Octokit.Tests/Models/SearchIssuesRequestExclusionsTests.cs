using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using Xunit;

public class SearchIssuesRequestExclusionsTests
{
    public class TheExclusionsMergedQualifiersMethod
    {
        [Fact]
        public void HandlesStringAttributesCorrectly()
        {
            var stringProperties = new Dictionary<string, Action<SearchIssuesRequestExclusions, string>>()
            {
                { "author:", (x,value) => x.Author = value },
                { "assignee:", (x,value) => x.Assignee = value },
                { "mentions:", (x,value) => x.Mentions = value },
                { "commenter:", (x,value) => x.Commenter = value },
                { "involves:", (x,value) => x.Involves = value },
                { "head:", (x,value) => x.Head = value },
                { "base:", (x,value) => x.Base = value },
                { "milestone:", (x,value) => x.Milestone = value }
            };

            foreach (var property in stringProperties)
            {
                var request = new SearchIssuesRequestExclusions();

                // Ensure the specified parameter does not exist when not set
                Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains(property.Key));

                // Set the parameter
                property.Value(request, "blah");

                // Ensure the specified parameter now exists
                Assert.True(request.MergedQualifiers().Count(x => x.Contains(property.Key)) == 1);
            }
        }

        [Fact]
        public void HandlesStateAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-state:"));

            request.State = ItemState.Closed;
            Assert.Contains("-state:closed", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesExcludeLabelsAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-label:"));

            request.Labels = new[] { "label1", "label2" };

            Assert.Contains("-label:label1", request.MergedQualifiers());
            Assert.Contains("-label:label2", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesLanguageAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-language:"));

            request.Language = Language.CSharp;

            Assert.Contains("-language:CSharp", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesStatusAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-status:"));

            request.Status = CommitState.Error;

            Assert.Contains("-status:error", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesMilestoneAttributeWithoutQuotes()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-milestone:"));

            request.Milestone = "testMilestone";
            Assert.Contains("-milestone:\"testMilestone\"", request.MergedQualifiers());
        }

        [Fact]
        public void DoesntWrapMilestoneWithDoubleQuotesForQuotedMilestone()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("-milestone:"));

            request.Milestone = "\"testMilestone\"";
            Assert.Contains<string>("-milestone:\"\\\"testMilestone\\\"\"", request.MergedQualifiers());
        }
    }
}