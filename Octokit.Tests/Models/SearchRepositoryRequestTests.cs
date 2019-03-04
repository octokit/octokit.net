using System;
using Octokit;
using Octokit.Tests.Helpers;
using Xunit;

public class SearchRepositoryRequestTests
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
            Assert.True(string.IsNullOrWhiteSpace(request.Sort));
            Assert.False(request.Parameters.ContainsKey("sort"));
        }

        [Fact]
        public void LanguageUsesParameterTranslation()
        {
            var request = new SearchRepositoriesRequest() { Language = Language.CPlusPlus };
            var result = request.MergedQualifiers();
            Assert.Contains(result, x => string.Equals(x, "language:C++"));
        }
    }
}
