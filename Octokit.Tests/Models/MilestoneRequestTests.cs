using Octokit;
using Xunit;

public class MilestoneRequestTests
{
    public class TheToParametersDictionaryMethod
    {
        [Fact]
        public void ContainsSetValues()
        {
            var request = new MilestoneRequest
            {
                State = ItemState.Closed,
                SortProperty = MilestoneSort.Completeness,
                SortDirection = SortDirection.Descending,
            };

            var parameters = request.ToParametersDictionary();

            Assert.Equal("closed", parameters["state"]);
            Assert.Equal("completeness", parameters["sort"]);
            Assert.Equal("desc", parameters["direction"]);
        }

        [Fact]
        public void ReturnsDefaultValuesForDefaultRequest()
        {
            var request = new MilestoneRequest();

            var parameters = request.ToParametersDictionary();

            Assert.Equal("open", parameters["state"]);
            Assert.Equal("due_date", parameters["sort"]);
            Assert.Equal("asc", parameters["direction"]);
        }
    }
}
