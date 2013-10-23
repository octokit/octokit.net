using Octokit;
using Xunit;

public class MilestoneRequestTests
{
    public class TheToParametersDictionaryMethod
    {
        [Fact]
        public void OnlyContainsChangedValues()
        {
            var request = new MilestoneRequest { SortDirection = SortDirection.Descending };

            var parameters = request.ToParametersDictionary();

            Assert.Equal(1, parameters.Count);
            Assert.Equal("desc", parameters["direction"]);
        }

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
        public void DoesNotAddDefaultAscendingSort()
        {
            var request = new MilestoneRequest
            {
                SortDirection = SortDirection.Ascending,
            };

            var parameters = request.ToParametersDictionary();

            Assert.Empty(parameters);
        }

        [Fact]
        public void ReturnsEmptyDictionaryForDefaultRequest()
        {
            var request = new MilestoneRequest();

            var parameters = request.ToParametersDictionary();

            Assert.Empty(parameters);
        }
    }
}
