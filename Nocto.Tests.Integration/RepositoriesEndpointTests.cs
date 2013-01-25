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

        public class TheGetPageMethod
        {
            [Fact]
            public async Task ReturnsAPageOfRepositoriesForAnOwner()
            {
                var github = new GitHubClient();

                var repositories = await github.Repository.GetPage("github");

                repositories.Count.Should().Be(30);
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public async Task ReturnsAllRepositoriesForAnOwner()
            {
                var github = new GitHubClient();

                var repositories = await github.Repository.GetAll("github");

                repositories.Count.Should().BeGreaterThan(80);
            }
        }
    }
}
