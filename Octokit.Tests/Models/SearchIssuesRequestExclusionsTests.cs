using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class SearchIssuesRequestExclusionsTests
{
    public class TheExclusionsMergedQualifiersMethod
    {
        [Fact]
        public void HandlesStringAttributesCorrectly()
        {
            var stringProperties = new List<Tuple<string, Action<SearchIssuesRequestExclusions, string>>>()
            {
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("author:", (x,value) => x.Author = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("assignee:", (x,value) => x.Assignee = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("mentions:", (x,value) => x.Mentions = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("commenter:", (x,value) => x.Commenter = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("involves:", (x,value) => x.Involves = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("head:", (x,value) => x.Head = value),
                new Tuple<string, Action<SearchIssuesRequestExclusions, string>>("base:", (x,value) => x.Base = value)
            };

            foreach (var property in stringProperties)
            {
                var request = new SearchIssuesRequestExclusions();

                // Ensure the specified parameter does not exist when not set
                Assert.False(request.MergedQualifiers().Any(x => x.Contains(property.Item1)));

                // Set the parameter
                property.Item2(request, "blah");

                // Ensure the specified parameter now exists
                Assert.True(request.MergedQualifiers().Count(x => x.Contains(property.Item1)) == 1);
            }
        }

        [Fact]
        public void HandlesStateAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.False(request.MergedQualifiers().Any(x => x.Contains("-state:")));

            request.State = ItemState.Closed;
            Assert.True(request.MergedQualifiers().Contains("-state:closed"));
        }

        [Fact]
        public void HandlesExcludeLabelsAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.False(request.MergedQualifiers().Any(x => x.Contains("-label:")));

            request.Labels = new[] { "label1", "label2" };

            Assert.True(request.MergedQualifiers().Contains("-label:label1"));
            Assert.True(request.MergedQualifiers().Contains("-label:label2"));
        }

        [Fact]
        public void HandlesLanguageAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.False(request.MergedQualifiers().Any(x => x.Contains("-language:")));

            request.Language = Language.CSharp;

            Assert.True(request.MergedQualifiers().Contains("-language:C#"));
        }

        [Fact]
        public void HandlesStatusAttributeCorrectly()
        {
            var request = new SearchIssuesRequestExclusions();
            Assert.False(request.MergedQualifiers().Any(x => x.Contains("-status:")));

            request.Status = CommitState.Error;

            Assert.True(request.MergedQualifiers().Contains("-status:error"));
        }
    }
}