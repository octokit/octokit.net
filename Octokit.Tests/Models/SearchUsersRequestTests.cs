using System.Collections.Generic;
using Octokit;
using Xunit;

internal class SearchUsersRequestTests
{
    public class TheMergedQualifiersMethod
    {
        [Fact]
        public void ReturnsAReadOnlyDictionary()
        {
            var request = new SearchUsersRequest("test");

            var result = request.MergedQualifiers();

            // If I can cast this to a writeable collection, then that defeats the purpose of a read only.
            Assert.Null(result as ICollection<string>);
        }
    }
}
