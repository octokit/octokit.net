using Octokit;
using Xunit;

public class PullRequestRequestTests
{
    public class TheToParametersDictionaryMethod
    {
        [Fact]
        public void ContainsSetValues()
        {
            var request = new PullRequestRequest
            {
                State = ItemState.Closed,
                Head = "user:ref-name",
                Base = "fake_base_branch"
            };

            var parameters = request.ToParametersDictionary();

            Assert.Equal("closed", parameters["state"]);
            Assert.Equal("user:ref-name", parameters["head"]);
            Assert.Equal("fake_base_branch", parameters["base"]);
        }

        [Fact]
        public void ReturnsDefaultValuesForDefaultRequest()
        {
            var request = new PullRequestRequest();

            var parameters = request.ToParametersDictionary();

            Assert.Equal("open", parameters["state"]);
        }
    }
}
