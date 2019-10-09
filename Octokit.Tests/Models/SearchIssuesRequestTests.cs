using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;
// hack because Travis on LInux is somehow upgraded to a version of .NET (Core?) that also has System.Range
using Range = Octokit.Range;

public class SearchIssuesRequestTests
{
    public class TheMergedQualifiersMethod
    {
        [Fact]
        public void ReturnsAReadOnlyDictionary()
        {
            var request = new SearchIssuesRequest("test");

            var result = request.MergedQualifiers();

            // If I can cast this to a writeable collection, then that defeats the purpose of a read only.
            AssertEx.IsReadOnlyCollection<string>(result);
        }

        [Fact]
        public void SortNotSpecifiedByDefault()
        {
            var request = new SearchIssuesRequest("test");
            Assert.True(string.IsNullOrWhiteSpace(request.Sort));
            Assert.False(request.Parameters.ContainsKey("sort"));
        }

        [Fact]
        public void HandlesStringAttributesCorrectly()
        {
            var stringProperties = new Dictionary<string, Action<SearchIssuesRequest, string>>
            {
                { "author:", (x,value) => x.Author = value },
                { "assignee:", (x,value) => x.Assignee = value },
                { "mentions:", (x,value) => x.Mentions = value },
                { "commenter:", (x,value) => x.Commenter = value },
                { "involves:", (x,value) => x.Involves = value },
                { "team:", (x,value) => x.Team = value },
                { "head:", (x,value) => x.Head = value },
                { "base:", (x,value) => x.Base = value },
                { "user:", (x,value) => x.User = value },
                { "milestone:", (x,value) => x.Milestone = value }
            };

            foreach (var property in stringProperties)
            {
                var request = new SearchIssuesRequest("query");

                // Ensure the specified parameter does not exist when not set
                Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains(property.Key));

                // Set the parameter
                property.Value(request, "blah");

                // Ensure the specified parameter now exists
                Assert.True(request.MergedQualifiers().Count(x => x.Contains(property.Key)) == 1);
            }
        }

        [Fact]
        public void HandlesDateRangeAttributesCorrectly()
        {
            var dateProperties = new Dictionary<string, Action<SearchIssuesRequest, DateRange>>
            {
                { "created:", (x,value) => x.Created = value },
                { "updated:", (x,value) => x.Updated = value },
                { "merged:", (x,value) => x.Merged = value },
                { "closed:", (x,value) => x.Closed = value }
            };

            foreach (var property in dateProperties)
            {
                var request = new SearchIssuesRequest("query");

                // Ensure the specified parameter does not exist when not set
                Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains(property.Key));

                // Set the parameter
                property.Value(request, DateRange.GreaterThan(new DateTimeOffset(DateTime.Today.AddDays(-7))));

                // Ensure the specified parameter now exists
                Assert.True(request.MergedQualifiers().Count(x => x.Contains(property.Key)) == 1);
            }
        }

        [Fact]
        public void HandlesInAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("in:"));

            request.In = new List<IssueInQualifier> { IssueInQualifier.Body, IssueInQualifier.Comment };
            Assert.Contains("in:body,comment", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesStateAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("state:"));

            request.State = ItemState.Closed;
            Assert.Contains("state:closed", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesLabelsAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("label:"));

            request.Labels = new[] { "label1", "label2" };
            Assert.Contains("label:label1", request.MergedQualifiers());
            Assert.Contains("label:label2", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesMilestoneAttributeWithoutQuotes()
        {
            var request = new SearchIssuesRequest("text");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("milestone:"));

            request.Milestone = "testMilestone";
            Assert.Contains("milestone:\"testMilestone\"", request.MergedQualifiers());
        }

        [Fact]
        public void DoesntWrapMilestoneWithDoubleQuotesForQuotedMilestone()
        {
            var request = new SearchIssuesRequest("text");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("milestone:"));

            request.Milestone = "\"testMilestone\"";
            Assert.Contains<string>("milestone:\"\\\"testMilestone\\\"\"", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesNoMetadataAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("no:"));

            request.No = IssueNoMetadataQualifier.Milestone;
            Assert.Contains("no:milestone", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesLanguageAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("language:"));

            request.Language = Language.CSharp;
            Assert.Contains("language:CSharp", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesIsAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("is:"));

            request.Is = new List<IssueIsQualifier> { IssueIsQualifier.Merged, IssueIsQualifier.PullRequest };
            Assert.Contains("is:merged", request.MergedQualifiers());
            Assert.Contains("is:pr", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesStatusAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("status:"));

            request.Status = CommitState.Error;
            Assert.Contains("status:error", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesCommentsAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("comments:"));

            request.Comments = Range.GreaterThan(5);
            Assert.Contains("comments:>5", request.MergedQualifiers());
        }

        [Fact]
        public void HandlesRepoAttributeCorrectly()
        {
            var request = new SearchIssuesRequest("test");
            Assert.DoesNotContain(request.MergedQualifiers(), x => x.Contains("repo:"));

            request.Repos.Add("myorg", "repo1");
            request.Repos.Add("myorg", "repo2");
            Assert.Contains("repo:myorg/repo1", request.MergedQualifiers());
            Assert.Contains("repo:myorg/repo2", request.MergedQualifiers());
        }
    }
}