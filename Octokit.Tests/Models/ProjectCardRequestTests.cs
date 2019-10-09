using Octokit;
using Xunit;

public class ProjectCardRequestTests
{
    public class TheToParametersDictionaryMethod
    {
        [Fact]
        public void ContainsSetValues()
        {
            var request = new ProjectCardRequest(ProjectCardArchivedStateFilter.All);

            var parameters = request.ToParametersDictionary();

            Assert.Equal("all", parameters["archived_state"]);
        }

        [Fact]
        public void ReturnsDefaultValuesForDefaultRequest()
        {
            var request = new ProjectCardRequest();

            var parameters = request.ToParametersDictionary();

            Assert.Empty(parameters);
        }
    }
}
