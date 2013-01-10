using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nocto.Tests.Integration
{
    public class RepositoriesEndpointTests
    {
        public class TheGetAsyncMethod
        {
            [Fact]
            public async Task ReturnsSpecifiedUser()
            {
                var github = new GitHubClient();

                var repository = await github.Repository.Get("github", "ReactiveCocoa");

                repository.CloneUrl.Should().Be("https://github.com/github/ReactiveCocoa.git");
            }
        }
    }
}
