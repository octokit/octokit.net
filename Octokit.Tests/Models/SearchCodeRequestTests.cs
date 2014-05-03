using System;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class SearchCodeRequestTests
{
    public class TheMergedQualifiersMethod
    {
        [Fact]
        public void ReturnsAReadOnlyDictionary()
        {
            var request = new SearchCodeRequest("test");
            
            var result = request.MergedQualifiers();

            // If I can cast this to a writeable collection, then that defeats the purpose of a read only.
            AssertEx.IsReadOnlyCollection<string>(result);
        }

        [Fact]
        public void SortNotSpecifiedByDefault()
        {
            var request = new SearchCodeRequest("test");
            Assert.True(String.IsNullOrWhiteSpace(request.Sort));
            Assert.False(request.Parameters.ContainsKey("sort"));
        }
    }
}
