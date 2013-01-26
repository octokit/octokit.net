using System.Threading.Tasks;
using FluentAssertions;
using Nocto.Http;
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
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repository = await github.Repository.Get("github", "ReactiveCocoa");

                repository.CloneUrl.Should().Be("https://github.com/github/ReactiveCocoa.git");
            }
        }

        public class TheGetPageForCurrentMethod
        {
            [Fact]
            public async Task ReturnsRepositoriesForTheCurrentUser()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repos = await github.Repository.GetPageForCurrent();

                repos.Count.Should().Be(0);
            }
        }

        public class TheGetPageForUserMethod
        {
            [Fact]
            public async Task ReturnsAPageOfRepositoriesForUser()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repositories = await github.Repository.GetPageForUser("github");

                repositories.Count.Should().Be(30);
            }
        }

        public class TheGetAllForOrgMethod
        {
            [Fact]
            public async Task ReturnsAllRepositoriesForOrganization()
            {
                var github = new GitHubClient
                {
                    Credentials = new Credentials("xapitestaccountx", "octocat11")
                };

                var repositories = await github.Repository.GetAllForOrg("github");

                repositories.Count.Should().BeGreaterThan(80);
            }
        }
    }
}
