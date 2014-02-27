using System.Collections.Generic;
using Octokit;
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
            Assert.Null(result as ICollection<string>);
        }
    }
}
